
dotnet build -c release

$confirmation = Read-Host "Copy to EXILED Plugin folder [y/n] "
if ($confirmation -eq 'y') {
    Copy-Item -Path obj/release/ZeitvertreibCommon.dll -Destination "$($env:USERPROFILE)\AppData\Roaming\EXILED\Plugins\ZeitvertreibCommon.dll" -Recurse -force
}

