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
.nuget\nuget.exe restore Bebbs.Harmonize.sln

REM Build
%WINDIR%\Microsoft.NET\Framework\v4.0.30319\msbuild Bebbs.Harmonize.sln /p:RunOctoPack=true /p:OctoPackPublishPackageToFileShare=..\Services /p:Configuration="%config%" /m /v:M /fl /flp:LogFile=msbuild.log;Verbosity=Normal /nr:false
