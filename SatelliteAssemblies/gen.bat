@echo OFF
REM Please run from a .NET Visual Studio command prompt 
REM or from a shell with resgen.exe, csc.exe, and al.exe on PATH
resgen Resource.txt
resgen Resource.en-CA.txt
resgen Resource.fr-FR.txt
resgen Resource.de-DE.txt
csc Program.cs -res:Resource.resources
al -target:lib -embed:Resource.en-CA.resources -culture:en-CA -out:en-CA\Program.resources.dll
al -target:lib -embed:Resource.fr-FR.resources -culture:fr-FR -out:fr-FR\Program.resources.dll
al -target:lib -embed:Resource.de-DE.resources -culture:de-DE -out:de-DE\Program.resources.dll
Program.exe
Program.exe en-CA
Program.exe fr-FR
Program.exe de-DE
