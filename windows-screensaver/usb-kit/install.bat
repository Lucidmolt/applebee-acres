@echo off
setlocal
title Applebee Acres screensaver setup

set "SRC=%~dp0ApplebeeAcres.scr"
set "DESTDIR=%LOCALAPPDATA%\ApplebeeAcres"
set "DEST=%DESTDIR%\ApplebeeAcres.scr"

if not exist "%SRC%" (
  echo Could not find ApplebeeAcres.scr next to this script.
  pause
  exit /b 1
)

echo Installing Applebee Acres for user %USERNAME% ...
if not exist "%DESTDIR%" mkdir "%DESTDIR%"
copy /y "%SRC%" "%DEST%" >nul

rem Clear mark-of-the-web in case this arrived by email/Teams instead of USB
powershell -NoProfile -Command "Unblock-File -LiteralPath '%DEST%'" >nul 2>&1

reg add "HKCU\Control Panel\Desktop" /v SCRNSAVE.EXE /t REG_SZ /d "%DEST%" /f >nul
reg add "HKCU\Control Panel\Desktop" /v ScreenSaveActive /t REG_SZ /d 1 /f >nul
reg add "HKCU\Control Panel\Desktop" /v ScreenSaveTimeOut /t REG_SZ /d 180 /f >nul
reg add "HKCU\Control Panel\Desktop" /v ScreenSaverIsSecure /t REG_SZ /d 0 /f >nul

rem Registry writes alone don't reach the running session (it re-reads them at
rem next sign-in). Broadcast the new values live so idle-start works immediately.
set "PS1=%TEMP%\acres-spi.ps1"
> "%PS1%" echo $t=Add-Type -MemberDefinition '[DllImport("user32.dll")]public static extern bool SystemParametersInfo(uint u,uint p,IntPtr v,uint f);' -Name SPI -PassThru
>>"%PS1%" echo [void]$t::SystemParametersInfo(15,180,[IntPtr]::Zero,3)
>>"%PS1%" echo [void]$t::SystemParametersInfo(17,1,[IntPtr]::Zero,3)
powershell -NoProfile -ExecutionPolicy Bypass -File "%PS1%" >nul 2>&1
del "%PS1%" >nul 2>&1

echo.
echo Done. The farm starts after 3 idle minutes.
echo (Timer and password-on-wake: Settings ^> Personalization ^> Lock screen ^> Screen saver.)
echo.
choice /c YN /m "See it now"
if errorlevel 2 goto end
start "" "%DEST%" /s
:end
endlocal
