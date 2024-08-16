@echo off
setlocal

REM Search for the string "internal static void SigScan" recursively in all files in the current directory
for /r %%f in (*) do (
    findstr /c:"internal static void SigScan" "%%f" >nul
    if not errorlevel 1 (
        echo Found in "%%f"
    )
)

pause

endlocal
