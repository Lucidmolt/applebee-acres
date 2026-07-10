using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.WinForms;

namespace ApplebeeAcres
{
    internal static class Program
    {
        [STAThread]
        private static void Main(string[] args)
        {
            // Screensaver protocol: /s run, /c configure, /p <hwnd> mini-preview.
            // No args (double-click) also runs fullscreen so it's easy to test.
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

            Form main = null;
            if (span)
            {
                Rectangle union = screens[0].Bounds;
                foreach (Screen s in screens) union = Rectangle.Union(union, s.Bounds);
                main = new SaverForm(union, dataDir, siteDir);
            }
            else
            {
                foreach (Screen screen in screens)
                {
                    var form = new SaverForm(screen.Bounds, dataDir, siteDir);
                    if (main == null) main = form; else form.Show();
                }
            }
            Application.Run(main);
        }
    }

    internal sealed class SaverForm : Form
    {
        private static Task<CoreWebView2Environment> _envTask;
        private readonly string _dataDir;
        private readonly string _siteDir;
        private Point? _firstMouse;

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

        public SaverForm(Rectangle bounds, string dataDir, string siteDir)
        {
            _dataDir = dataDir;
            _siteDir = siteDir;
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
                core.Settings.AreDefaultContextMenusEnabled = false;
                core.Settings.AreDevToolsEnabled = false;
                core.Settings.IsZoomControlEnabled = false;
                core.Settings.IsStatusBarEnabled = false;
                await core.AddScriptToExecuteOnDocumentCreatedAsync(ExitScript);
                core.WebMessageReceived += (s, e) => Application.Exit();

                // Serve the page from a virtual https origin so its weather fetch()
                // to api.open-meteo.com is plain https-to-https (no file:// quirks).
                core.SetVirtualHostNameToFolderMapping(
                    "acres.local", _siteDir, CoreWebView2HostResourceAccessKind.Allow);
                core.Navigate("https://acres.local/applebee-acres.html?saver=1");
            }
            catch (Exception ex)
            {
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
