@ECHO OFF
set srcfolder=%~1
Crypter.exe -src "%srcfolder%\*.*" -r -m -p 'password' -d
pause