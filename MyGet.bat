@echo Off
set config=%1
if "%config%" == "" (
   set config=Release
)

set version=
if not "%PackageVersion%" == "" (
   set version=-Version %PackageVersion%
)

REM Package restore
tools\nuget.exe restore Bebbs.Harmonize.sln -OutputDirectory %cd%\packages -NonInteractive

REM Build
%WINDIR%\Microsoft.NET\Framework\v4.0.30319\msbuild Bebbs.Harmonize.sln /p:RunOctoPack=true /p:OctoPackPublishPackageToFileShare=..\Services /p:Configuration="%config%" /m /v:M /fl /flp:LogFile=msbuild.log;Verbosity=Normal /nr:false
