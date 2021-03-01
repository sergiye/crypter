@ECHO OFF
set src=%~1
ECHO Decrypt file %src%
Crypter.exe -src "%src%" -r -m -p ? -d
pause