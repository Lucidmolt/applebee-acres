// Soak test: ~12 simulated years at x16, heap growth watched for leaks.
const fs = require('fs');
const html = fs.readFileSync('/Users/lucid/Creative/applebee-acres.html', 'utf8');
const src = html.match(/<script>([\s\S]*)<\/script>/)[1];

const handlers = { win: {}, cv: {} };
const noopProxy = () => new Proxy(function(){}, {
  get: (t, k) => (k === Symbol.toPrimitive ? () => 0 : noopProxy()),
  apply: () => noopProxy(), set: () => true,
});
const ctx = new Proxy({}, { get: () => noopProxy(), set: () => true });
global.document = {
  getElementById: (id) => (id === 's'
    ? { width: 384, height: 216, getContext: () => ctx,
        addEventListener: (ev, fn) => { handlers.cv[ev] = fn; },
        getBoundingClientRect: () => ({ left: 0, top: 0, width: 768, height: 432 }) }
    : { textContent: '' }),
  body: { classList: { toggle: () => {} } },
};
global.addEventListener = (ev, fn) => { handlers.win[ev] = fn; };
let rafFn = null;
global.requestAnimationFrame = (fn) => { rafFn = fn; };
global.setInterval = () => 0;
global.fetch = () => Promise.reject(new Error('offline'));

eval(src);

const key = (k) => handlers.win.keydown({ key: k, preventDefault: () => {} });
key('='); key('='); key('='); key('=');   // sim mode, x16

const hud = global.document.getElementById('hud');
let ts = 0;
const YEAR_FRAMES = 13500;                // ~1 sim year at x16, 16.7ms frames
global.gc && global.gc();
const heap0 = process.memoryUsage().heapUsed;
console.log('start heap:', (heap0/1048576).toFixed(1), 'MB');

for (let yr = 1; yr <= 12; yr++) {
  for (let i = 0; i < YEAR_FRAMES; i++) {
    ts += 16.7; const f = rafFn; rafFn = null; f(ts);
    if (!rafFn) throw new Error('rAF dropped in year ' + yr);
  }
  global.gc && global.gc();
  const h = process.memoryUsage().heapUsed;
  console.log('sim-year', yr, '| heap', (h/1048576).toFixed(1), 'MB');
}
const heap1 = process.memoryUsage().heapUsed;
const growth = (heap1 - heap0) / 1048576;
console.log('total heap growth over 12 sim-years:', growth.toFixed(1), 'MB');
if (growth > 40) { console.log('FAIL: heap growth suggests a leak'); process.exit(1); }
console.log('SOAK OK');
process.exit(0);
