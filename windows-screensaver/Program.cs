using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.WinForms;

namespace ApplebeeAcres
{
    // Append-only log so we can see, AFTER THE FACT, exactly what the displays and the render
    // surface did during a sleep/wake. The dual-monitor "tear" has survived two blind fixes
    // (exit-on-DisplaySettingsChanged, then exit-on-monitor-power-on) so this build is instrumented:
    // it records every display event + geometry so the next tear tells us the real mechanism.
    // File: %LOCALAPPDATA%\ApplebeeAcres\saver.log
    internal static class SaverLog
    {
        private static string _path;
        private static readonly object _lock = new object();

        public static void Init(string dataDir)
        {
            try
            {
                _path = Path.Combine(dataDir, "saver.log");
                // don't let it grow forever across relaunches
                if (File.Exists(_path) && new FileInfo(_path).Length > 256 * 1024) File.Delete(_path);
            }
            catch { _path = null; }
        }

        public static void Write(string msg)
        {
            if (_path == null) return;
            try
            {
                lock (_lock)
                    File.AppendAllText(_path,
                        DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture) + "  " + msg + Environment.NewLine);
            }
            catch { }
        }
    }

    internal static class Program
    {
        public const string Version = "1.2-diag";

        [STAThread]
        private static void Main(string[] args)
        {
            // Screensaver protocol: /s run, /c configure, /p <hwnd> mini-preview.
            string mode = args.Length > 0 ? args[0].Trim().ToLowerInvariant() : "/s";

            if (mode.StartsWith("/p")) return; // nothing in the tiny preview box

            if (mode.StartsWith("/c"))
            {
                MessageBox.Show(
                    "Applebee Acres follows the real clock, calendar and Liberal, Kansas weather.\n" +
                    "There is nothing to configure. Move the mouse or press a key to exit the saver.",
                    "Applebee Acres", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Application.SetHighDpiMode(HighDpiMode.PerMonitorV2);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // The .scr may live in C:\Windows (not writable as user), so unpack the
            // page and keep WebView2's data under %LOCALAPPDATA%.
            string dataDir = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ApplebeeAcres");
            string siteDir = Path.Combine(dataDir, "site");
            Directory.CreateDirectory(siteDir);

            SaverLog.Init(dataDir);
            SaverLog.Write($"=== launch v{Version} mode={mode} screens={Screen.AllScreens.Length} ===");
            foreach (Screen s in Screen.AllScreens)
                SaverLog.Write($"  screen {(s.Primary ? "*" : " ")} bounds={s.Bounds} wa={s.WorkingArea}");

            using (Stream src = Assembly.GetExecutingAssembly().GetManifestResourceStream("applebee-acres.html"))
            using (FileStream dst = File.Create(Path.Combine(siteDir, "applebee-acres.html")))
            {
                src.CopyTo(dst);
            }

            // If the monitors form one clean side-by-side row (same height, no gaps),
            // span a single wide farm across all of them; otherwise one farm per screen.
            Screen[] screens = Screen.AllScreens;
            bool span = screens.Length > 1;
            if (span)
            {
                int top = screens[0].Bounds.Top, height = screens[0].Bounds.Height;
                int minL = int.MaxValue, maxR = int.MinValue, widthSum = 0;
                foreach (Screen s in screens)
                {
                    if (s.Bounds.Top != top || s.Bounds.Height != height) { span = false; break; }
                    widthSum += s.Bounds.Width;
                    if (s.Bounds.Left < minL) minL = s.Bounds.Left;
                    if (s.Bounds.Right > maxR) maxR = s.Bounds.Right;
                }
                if (span && widthSum != maxR - minL) span = false;
            }
            SaverLog.Write($"span={span}");

            Form main = null;
            if (span)
            {
                Rectangle union = screens[0].Bounds;
                foreach (Screen s in screens) union = Rectangle.Union(union, s.Bounds);
                SaverLog.Write($"span union bounds={union}");
                main = new SaverForm(union, dataDir, siteDir, true);
            }
            else
            {
                foreach (Screen screen in screens)
                {
                    var form = new SaverForm(screen.Bounds, dataDir, siteDir, false);
                    if (main == null) main = form; else form.Show();
                }
            }

            // The saver USED to exit on a display change and let the OS relaunch it clean. That has now
            // failed to cure the tear twice, which suggests either the triggering event never fires on
            // this hardware, or the relaunch races the still-waking display. So this build keeps the
            // process ALIVE and re-fits in place (see SaverForm.Watchdog): re-assert the spanning bounds
            // and rebuild the page surface whenever the geometry changes OR drifts — event-independent.
            EventHandler onDisplayChange = (s, e) =>
            {
                SaverLog.Write("event: DisplaySettingsChanged");
                (main as SaverForm)?.RequestHeal("DisplaySettingsChanged");
            };
            SystemEvents.DisplaySettingsChanged += onDisplayChange;
            try { Application.Run(main); }
            finally { SystemEvents.DisplaySettingsChanged -= onDisplayChange; SaverLog.Write("=== exit ==="); }
        }
    }

    internal sealed class SaverForm : Form
    {
        private static Task<CoreWebView2Environment> _envTask;
        private readonly string _dataDir;
        private readonly string _siteDir;
        private readonly bool _span;
        private Point? _firstMouse;
        private CoreWebView2 _core;
        private System.Windows.Forms.Timer _watch;
        private bool _needReload;
        private int _lastReloadTick = -100000;
        private readonly int _bootTick = Environment.TickCount;
        private bool _displayOn = true;

        // The page swallows input, so it reports activity back to the host.
        private const string ExitScript = @"
(function () {
  var ox = null, oy = null;
  function bail() {
    try { if (window.__acresExit) { window.__acresExit(); return; } } catch (e) {}
    try { window.chrome.webview.postMessage('exit'); } catch (e) {}
  }
  addEventListener('mousemove', function (e) {
    if (ox === null) { ox = e.screenX; oy = e.screenY; return; }
    if (Math.abs(e.screenX - ox) > 5 || Math.abs(e.screenY - oy) > 5) bail();
  });
  addEventListener('mousedown', bail);
  addEventListener('keydown', bail);
})();";

        public SaverForm(Rectangle bounds, string dataDir, string siteDir, bool span)
        {
            _dataDir = dataDir;
            _siteDir = siteDir;
            _span = span;
            FormBorderStyle = FormBorderStyle.None;
            StartPosition = FormStartPosition.Manual;
            Bounds = bounds;
            TopMost = true;
            ShowInTaskbar = false;
            BackColor = Color.FromArgb(10, 12, 22);
            KeyPreview = true;
            KeyDown += (s, e) => Application.Exit();
            MouseDown += (s, e) => Application.Exit();
            MouseMove += OnAnyMouseMove;
            Load += async (s, e) => await InitWebViewAsync();
        }

        private void OnAnyMouseMove(object sender, MouseEventArgs e)
        {
            if (_firstMouse == null) { _firstMouse = e.Location; return; }
            if (Math.Abs(e.X - _firstMouse.Value.X) > 5 || Math.Abs(e.Y - _firstMouse.Value.Y) > 5)
                Application.Exit();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            Cursor.Hide();
            // Event-independent self-heal: every ~1.5s, re-assert the correct spanning geometry and,
            // if a display change was flagged, rebuild the page surface. Only the spanning form (the
            // one that tears) needs it; it does nothing while geometry is stable, so no flicker.
            if (_span)
            {
                _watch = new System.Windows.Forms.Timer { Interval = 1500 };
                _watch.Tick += (s, ev) => Watchdog();
                _watch.Start();
            }
        }

        // Log + flag a surface rebuild on the next watchdog tick (the actual reload runs on the UI
        // thread from the timer, debounced, so bursts of wake events don't reload repeatedly).
        public void RequestHeal(string reason)
        {
            SaverLog.Write($"heal requested: {reason}");
            _needReload = true;
        }

        private void Watchdog()
        {
            try
            {
                Rectangle union = Screen.AllScreens[0].Bounds;
                foreach (Screen s in Screen.AllScreens) union = Rectangle.Union(union, s.Bounds);
                if (union != Bounds)
                {
                    SaverLog.Write($"watchdog: bounds drift {Bounds} -> {union}; re-applying");
                    Bounds = union;                 // WebView2 is Dock.Fill, so it follows
                    _needReload = true;
                }
                if (_needReload && _core != null && Environment.TickCount - _lastReloadTick > 5000)
                {
                    _lastReloadTick = Environment.TickCount;
                    _needReload = false;
                    SaverLog.Write($"watchdog: reloading page to re-fit the surface (clientSize={ClientSize})");
                    try { _core.Reload(); } catch (Exception ex) { SaverLog.Write("reload failed: " + ex.Message); }
                }
            }
            catch (Exception ex) { SaverLog.Write("watchdog error: " + ex.Message); }
        }

        // ---- monitor power notifications (log the off/on transitions; heal on wake) ----
        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr RegisterPowerSettingNotification(IntPtr hRecipient, ref Guid PowerSettingGuid, int Flags);
        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool UnregisterPowerSettingNotification(IntPtr Handle);

        private static readonly Guid GuidConsoleDisplayState = new Guid("6fe69556-704a-47a0-8f24-c28d936fda47");
        private const int DeviceNotifyWindowHandle = 0;
        private const int WmPowerBroadcast = 0x0218;
        private const int PbtPowerSettingChange = 0x8013;

        [StructLayout(LayoutKind.Sequential)]
        private struct PowerBroadcastSetting { public Guid PowerSetting; public int DataLength; public int Data; }

        private IntPtr _powerNotify = IntPtr.Zero;

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            Guid g = GuidConsoleDisplayState;
            _powerNotify = RegisterPowerSettingNotification(Handle, ref g, DeviceNotifyWindowHandle);
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            if (_powerNotify != IntPtr.Zero) { UnregisterPowerSettingNotification(_powerNotify); _powerNotify = IntPtr.Zero; }
            base.OnHandleDestroyed(e);
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WmPowerBroadcast && (int)m.WParam == PbtPowerSettingChange)
            {
                var ps = Marshal.PtrToStructure<PowerBroadcastSetting>(m.LParam);
                if (ps.PowerSetting == GuidConsoleDisplayState)
                {
                    int state = ps.Data & 0xFF;                 // 0 = off, 1 = on, 2 = dimmed
                    SaverLog.Write($"event: display power = {(state == 0 ? "OFF" : state == 1 ? "ON" : "DIM")}");
                    if (state == 1 && !_displayOn && Environment.TickCount - _bootTick > 4000)
                        RequestHeal("display woke");
                    if (state == 0) _displayOn = false; else if (state == 1) _displayOn = true;
                }
            }
            base.WndProc(ref m);
        }

        private async Task InitWebViewAsync()
        {
            try
            {
                var wv = new WebView2
                {
                    Dock = DockStyle.Fill,
                    DefaultBackgroundColor = Color.FromArgb(10, 12, 22)
                };
                Controls.Add(wv);

                if (_envTask == null)
                    _envTask = CoreWebView2Environment.CreateAsync(null, Path.Combine(_dataDir, "webview2"));
                CoreWebView2Environment env = await _envTask;
                await wv.EnsureCoreWebView2Async(env);

                CoreWebView2 core = wv.CoreWebView2;
                _core = core;
                core.Settings.AreDefaultContextMenusEnabled = false;
                core.Settings.AreDevToolsEnabled = false;
                core.Settings.IsZoomControlEnabled = false;
                core.Settings.IsStatusBarEnabled = false;
                // a WebView2 render/GPU process crash is another way the farm can go blank/torn — catch it
                core.ProcessFailed += (s, e) => { SaverLog.Write("event: WebView2 ProcessFailed " + e.ProcessFailedKind); RequestHeal("ProcessFailed"); };
                core.NavigationCompleted += (s, e) => SaverLog.Write($"nav completed ok={e.IsSuccess} clientSize={ClientSize}");
                await core.AddScriptToExecuteOnDocumentCreatedAsync(ExitScript);
                core.WebMessageReceived += (s, e) => Application.Exit();

                // Serve the page from a virtual https origin so its weather fetch()
                // to api.open-meteo.com is plain https-to-https (no file:// quirks).
                core.SetVirtualHostNameToFolderMapping(
                    "acres.local", _siteDir, CoreWebView2HostResourceAccessKind.Allow);
                core.Navigate("https://acres.local/applebee-acres.html?saver=1");
                SaverLog.Write("webview initialised, navigating");
            }
            catch (Exception ex)
            {
                SaverLog.Write("webview init FAILED: " + ex.Message);
                Controls.Clear();
                Controls.Add(new Label
                {
                    Dock = DockStyle.Fill,
                    ForeColor = Color.White,
                    BackColor = Color.FromArgb(10, 12, 22),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Font = new Font("Segoe UI", 14f),
                    Text = "Applebee Acres needs the Microsoft Edge WebView2 runtime.\n" +
                           "Install it from https://aka.ms/webview2 and try again.\n\n" + ex.Message
                });
            }
        }
    }
}
