@ECHO OFF
set src=%~1
Crypter.exe -src "%src%" -r -m -p ? -d
pause