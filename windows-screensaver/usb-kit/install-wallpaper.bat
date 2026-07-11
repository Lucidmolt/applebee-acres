@echo off
setlocal
title Applebee Acres live wallpaper setup

rem Sets up the farm as an animated DESKTOP BACKGROUND (via Lively Wallpaper).
rem This is separate from install.bat, which sets up the idle SCREENSAVER.
rem Run both if you want the farm while working (this) AND when idle (install.bat).

where winget >nul 2>&1
if errorlevel 1 (
  echo.
  echo Windows Package Manager ^(winget^) was not found on this PC.
  echo Get "App Installer" from the Microsoft Store ^(it provides winget^) and re-run,
  echo or just install "Lively Wallpaper" from the Store yourself, then re-run this.
  echo.
  pause
  exit /b 1
)

powershell -NoProfile -ExecutionPolicy Bypass -File "%~dp0acres-wallpaper.ps1"

echo.
pause
endlocal
