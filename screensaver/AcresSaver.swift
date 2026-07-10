import ScreenSaver
import WebKit

@objc(AcresSaverView)
final class AcresSaverView: ScreenSaverView {
    private var webView: WKWebView?

    override init?(frame: NSRect, isPreview: Bool) {
        super.init(frame: frame, isPreview: isPreview)
        wantsLayer = true
        layer?.backgroundColor = NSColor(srgbRed: 0.039, green: 0.047, blue: 0.086, alpha: 1).cgColor
    }

    required init?(coder: NSCoder) {
        super.init(coder: coder)
    }

    // Attach the web view only once we live in a real window — creating it in init,
    // before the saver host has a backing window, leaves WebKit's GPU process unbound
    // and the view renders black on modern macOS.
    override func viewDidMoveToWindow() {
        super.viewDidMoveToWindow()
        guard window != nil, webView == nil else { return }

        let wv = WKWebView(frame: bounds, configuration: WKWebViewConfiguration())
        wv.autoresizingMask = [.width, .height]
        // WebKit's occlusion tracking decides saver-level windows are "not visible" and
        // suspends the page — rAF stops and the layer tree detaches, leaving a grey view.
        // Verified on this machine (macOS 26): with detection off, the page runs and renders.
        let occSel = NSSelectorFromString("_setWindowOcclusionDetectionEnabled:")
        if wv.responds(to: occSel), let m = wv.method(for: occSel) {
            typealias F = @convention(c) (AnyObject, Selector, ObjCBool) -> Void
            unsafeBitCast(m, to: F.self)(wv, occSel, false)
        }
        if #available(macOS 12.0, *) {
            wv.underPageBackgroundColor = NSColor(srgbRed: 0.039, green: 0.047, blue: 0.086, alpha: 1)
        }
        addSubview(wv)
        webView = wv

        // The saver process reads its own bundle and hands WebKit the string directly —
        // the WebContent sandbox never touches the filesystem, so the page always commits.
        // (The page is fully self-contained; the weather fetch is CORS-permitted by Open-Meteo
        // from the null origin, and falls back to simulated weather otherwise.)
        if let url = Bundle(for: Self.self).url(forResource: "applebee-acres", withExtension: "html"),
           let html = try? String(contentsOf: url, encoding: .utf8) {
            wv.loadHTMLString(html, baseURL: nil)
        }
    }

    override func animateOneFrame() { /* the page's JS drives all animation */ }
    override var hasConfigureSheet: Bool { false }
}
