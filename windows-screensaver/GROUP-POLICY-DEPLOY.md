# Deploying Applebee Acres to the store PCs via Group Policy

## 0. Test on one PC first
Copy `ApplebeeAcres.scr` to any Windows 10/11 x64 machine and double-click it — it should
run fullscreen (all monitors) and exit the moment you move the mouse or press a key.
If it shows a "needs WebView2" message instead of the farm, that machine is missing the
Microsoft Edge WebView2 runtime (rare — it ships with Edge). Deploy Microsoft's Evergreen
runtime from https://aka.ms/webview2 (MSI available for GPO software installation).

## 1. Stage the file on a share
Copy `ApplebeeAcres.scr` to a share readable by **Domain Computers**, e.g.
`\\SERVER\deploy$\ApplebeeAcres.scr`

## 2. Copy it onto each machine (Group Policy Preferences)
In a GPO linked to the store computers' OU:

**Computer Configuration → Preferences → Windows Settings → Files → New → File**
- Action: **Replace**
- Source: `\\SERVER\deploy$\ApplebeeAcres.scr`
- Destination: `C:\Windows\ApplebeeAcres.scr`

GPP runs as SYSTEM, so it can write to `C:\Windows`. A local copy is deliberate —
running a screensaver directly off a UNC path is slow and flaky. "Replace" also means
future rebuilds update every PC automatically on the next policy refresh.

## 3. Force it as the screensaver
In a GPO linked to the users (or the same GPO if users are in scope):

**User Configuration → Policies → Administrative Templates → Control Panel → Personalization**
- **Enable screen saver**: Enabled
- **Force specific screen saver**: `C:\Windows\ApplebeeAcres.scr`
- **Screen saver timeout**: `180` seconds (3 min — adjust to taste)
- **Password protect the screen saver**:
  - Enabled for staff workstations (locks on wake)
  - Disabled for showroom/lobby display machines
  - If you want both behaviors, use two GPOs filtered by security group or OU.

## 4. Apply and verify
`gpupdate /force` on a test machine, then leave it idle (or run the .scr manually).
Everything else rolls out within the normal ~90-minute refresh.

## Scripted alternative (no GPO)
The user-policy registry equivalents, if you'd rather push with a script or Intune:
```
HKCU\Software\Policies\Microsoft\Windows\Control Panel\Desktop
  SCRNSAVE.EXE        REG_SZ  C:\Windows\ApplebeeAcres.scr
  ScreenSaveActive    REG_SZ  1
  ScreenSaveTimeOut   REG_SZ  180
  ScreenSaverIsSecure REG_SZ  0   (1 = require password on wake)
```

## Notes
- The .scr is fully self-contained (.NET embedded, farm page embedded); the only
  dependency is the WebView2 runtime noted above. x64 only.
- Live weather calls `https://api.open-meteo.com` — allow that domain if you filter
  outbound traffic. Machines without internet quietly fall back to simulated weather.
- Each user's WebView2 cache lives in `%LOCALAPPDATA%\ApplebeeAcres` (small).
- If you run AppLocker/WDAC, add a hash or path rule for `C:\Windows\ApplebeeAcres.scr`.
- To change the farm (location, colors, scenery): edit `Creative/applebee-acres.html`
  on the Mac, run `windows-screensaver/build.sh`, and re-stage the new .scr on the share.
