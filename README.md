Harmonize
=========

C# port of PyHarmony in reusable wrapper

Change Bebbs.Harmonize.Console.Settings.Provider to provide correct credentials and IP address of Harmony Hub.

The console app is currently configured to turn my amp on but it is extremely simple to change this functionality. First you must determine the device ID you want to control by examining the harmony configuration information returned in Bebbs.Harmonize.Harmony.Services.XmppService.ExtractHarmonyConfiguration method.

More documentation to follow.
