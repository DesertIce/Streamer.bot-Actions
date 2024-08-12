REM Basically automating the steps described here: https://help.elgato.com/hc/en-us/articles/15525011385229-Elgato-Stream-Deck-How-to-use-with-OBS-Studio-Portable-Mode

SET InstallerRoot=C:\ProgramData\obs-studio\plugins

REM Change this if you use another path
SET PortableRoot=C:\Program Files\obs-studio

REM Copy the binary
xcopy "%InstallerRoot%\StreamDeckPlugin\bin\64bit\*.*" "%PortableRoot%\obs-plugins\64bit" /Y

REM Create UI Folder
mkdir "%PortableRoot%\data\obs-plugins\StreamDeckPlugin"

SET PortableLocalized="%PortableRoot%\data\obs-plugins\StreamDeckPlugin\"

xcopy "%InstallerRoot%\StreamDeckPlugin\data\StreamDeckPluginQt6.dll" %PortableLocalized% /Y
xcopy "%InstallerRoot%\StreamDeckPlugin\data\StreamDeckPluginQt6.pdb" %PortableLocalized% /Y
xcopy "%InstallerRoot%\StreamDeckPlugin\data\locale\" %PortableLocalized%\locale\ /E /Y