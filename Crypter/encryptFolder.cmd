@ECHO OFF
set src=%~1
ECHO Encrypt file %src%
Crypter.exe -src "%src%" -r -m -p ?
pause