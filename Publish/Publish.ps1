#Publisher for game 'What Happened to Site-13?'
$SKU0=Resolve-Path "..\Build\x64"
$SKU1=Resolve-Path "..\ExternalTools"
$SKU2=Resolve-Path "..\SDK"
$Version="0.1.82.7"
$Version=$args[0]
Set-Location ..
Set-Location Build
mkdir $Version
mkdir $Version\Sources
Set-Location ..
Set-Location Publish
Copy-Item '.\Site-13 Uninstaller.exe' ..\Build\x64
$TargetFile=$ExecutionContext.SessionState.Path.GetUnresolvedProviderPathFromPSPath("..\Build\$Version\Sources\Install.wim")
$TargetFolder=$ExecutionContext.SessionState.Path.GetUnresolvedProviderPathFromPSPath("..\Build\$Version\")
$TargetISO=$ExecutionContext.SessionState.Path.GetUnresolvedProviderPathFromPSPath("..\Build\Site-13.$Version.iso")
Write-Host 'Packaging into WIM with WimEditKit.exe'
#Make WIM file with WIMEditKit
.\WimEditKit.exe -Add-Image:Site-13-Main-Game?$SKU0 -Add-Image:Ext-Tools?$SKU1 -Add-Image:Site-13-SDK?$SKU2 -WIMFile:$TargetFile
#Copy Installer
Copy-Item -Path ./Installer/* -Container -Destination ../Build/$Version/ -Force -Recurse
Write-Host 'Packaging into ISO with ISOCreatorCLI.exe'
#Package To ISO
.\ISOCreatorCLI.exe $TargetFolder "Site-13.$Version" $TargetISO