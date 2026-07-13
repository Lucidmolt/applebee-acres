// Headless smoke test for applebee-acres.html (live + sim modes)
const fs = require('fs');
const html = fs.readFileSync('/Users/lucid/Creative/applebee-acres.html', 'utf8');
const m = html.match(/<script>([\s\S]*)<\/script>/);
if (!m) { console.error('no script found'); process.exit(1); }
const src = m[1];

// --- stubs ---
const handlers = { win: {}, cv: {} };
const noopProxy = () => new Proxy(function(){}, {
  get: (t, k) => (k === Symbol.toPrimitive ? () => 0 : noopProxy()),
  apply: () => noopProxy(),
  set: () => true,
});
const ctx = new Proxy({}, { get: () => noopProxy(), set: () => true });
const cvEl = {
  width: 384, height: 216,
  getContext: () => ctx,
  addEventListener: (ev, fn) => { handlers.cv[ev] = fn; },
  getBoundingClientRect: () => ({ left: 0, top: 0, width: 768, height: 432 }),
};
const hudEl = { textContent: '' };
global.document = {
  getElementById: (id) => (id === 's' ? cvEl : hudEl),
  body: { classList: { toggle: () => {} } },
};
global.addEventListener = (ev, fn) => { handlers.win[ev] = fn; };
let rafFn = null;
global.requestAnimationFrame = (fn) => { rafFn = fn; };
global.setInterval = () => 0;                                  // don't keep process alive
global.fetch = () => Promise.reject(new Error('offline'));     // prove weather fails soft

// --- run ---
eval(src);

const key = (k) => handlers.win.keydown({ key: k, preventDefault: () => {} });
const keyUp = (k) => handlers.win.keyup && handlers.win.keyup({ key: k });
const click = (x, y) => handlers.cv.click({ clientX: x * 2, clientY: y * 2 });

let ts = 0;
const step = (n, dt = 16.7) => {
  for (let i = 0; i < n; i++) { ts += dt; const f = rafFn; rafFn = null; f(ts); if (!rafFn) throw new Error('rAF not re-queued'); }
};

step(300);                                   // live mode boot (real July date)
console.log('live hud:', hudEl.textContent);
click(200, 156); click(120, 178); click(175, 134); click(50, 40); // plants; lot + sky are no-ops
key(' '); keyUp(' '); step(30); key(' '); keyUp(' ');   // pause/resume (fires on release now)
key(' '); key('d'); keyUp(' '); step(120);   // secret chord: Space+D starts the duck hunt
for (let i = 0; i < 6; i++) { click(120 + i * 20, 40); step(60); } step(400);
console.log('space+D chord ran');
key('=');                                    // exit live -> sim
step(60);
console.log('sim hud :', hudEl.textContent);
key('='); key('='); key('=');                // x16
step(4700);                                  // ~ a full sim year
console.log('year hud:', hudEl.textContent);
for (const k of ['1','2','3','4','1']) { key(k); step(900); } // long enough for combine + cart + baler
key('2'); key('z'); step(700);               // See & Spray pass over seeded weeds
console.log('equipment ran');
key('2'); step(200);                         // summer field for the burn
for (const k of ['f','t','d','r','c']) key(k); // force fire, tumbleweed, dust devil, rainbow, duster
step(3600);                                  // full fire+rescue cycle, art mow, oddities
console.log('events ran, hud:', hudEl.textContent);
key('3'); step(600);                         // autumn -> harvest makes bales for the ufo
for (const k of ['n','s','b','e','m','w','u']) key(k); // train, sale, balloon, deer, snowman, fireworks, ufo
step(3600);                                  // sale cycle, train crossing, abduction
console.log('more events ran');
key('2'); step(200);                         // summer: farm & dealership on-shift
for (const k of ['j',',','.','/',';','\'']) key(k); // family + dealership personality wave
step(2600);                                  // town round-trip, catch, bike, bay-out, service drop, trade-in
console.log('family/dealership wave ran');
for (const k of ['H','P','C']) key(k); step(500);   // hawk, pheasant, cattle herd (demo keys force them)
key('3'); step(600); key('C'); step(600);           // autumn/stubble cattle grazing + drift + spook paths
console.log('livestock & hawk ran');
key('U'); step(1600);                               // mid-day cattle abduction: saucer in, beam, cow gone, zip out
console.log('cow abduction ran');
key('G'); step(1600);                               // the prairie lizard rises, roars, and is escorted off by helicopters
console.log('kaiju ran');
for (const k of ['a','o','x','v','g','y','i','8']) key(k); // aurora, comet, rocket, meteor shower, crop circle, eclipse, satellite, blood moon
step(2400);                                  // full eclipse transit + rocket plume fade
console.log('strange sky ran');
key('q'); step(400);                         // asteroid strike + crater + debris settle
console.log('asteroid ran');
key('7'); step(120);                         // interactive duck hunt: hunter walks on
for (let i = 0; i < 6; i++) { click(120 + i * 20, 40); step(80); } // five shells (+1 past empty)
step(900);                                   // ducks fall, hunter ambles off
console.log('duck hunt ran');
key('9'); step(1400);                        // ambient self-playing hunt: walks on, auto-shoots, ambles off
console.log('ambient hunt ran');
key('0'); step(900);                         // protest gathers, pickets, disperses
console.log('protest ran');
key('k'); step(280);                         // network offline: Keating catches fire (demo toggle)
console.log('network fire ran');
key('k'); step(120);                         // network restored: fire dies down
key('l'); step(200);                         // back to live
console.log('back live:', hudEl.textContent);
key('h'); key('-'); step(500);
setTimeout(() => { console.log('OK'); process.exit(0); }, 50); // let rejected fetch settle
