@echo off
setlocal
reg delete "HKCU\Control Panel\Desktop" /v SCRNSAVE.EXE /f >nul 2>&1
reg add "HKCU\Control Panel\Desktop" /v ScreenSaveActive /t REG_SZ /d 0 /f >nul
rd /s /q "%LOCALAPPDATA%\ApplebeeAcres" 2>nul
echo Applebee Acres removed for %USERNAME%.
pause
endlocal
