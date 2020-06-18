#Publisher for game 'What Happened to Site-13?'
Write-Host "======================"
Write-Host "=Site-13 Publish Tool="
Write-Host "=" -NoNewline
Write-Host "     Ver:" -NoNewline
Write-Host "1.2.0.0    " -Foreground "Green" -NoNewline
Write-Host "="
Write-Host "======================"
$SKU0=Resolve-Path "..\Build\x64.Uncompressed"
$SKU1=Resolve-Path "..\ExternalTools"
$SKU2=Resolve-Path "..\SDK"
$Version="0.1.86.0"
$Version=$args[0]
Set-Location ..
Set-Location Build
mkdir $Version -Force
mkdir $Version\Sources -Force
Set-Location ..
Set-Location Publish
Copy-Item '.\Site-13 Uninstaller.exe' ..\Build\x64.Uncompressed
#Copy some libs
Write-Host "==========================" -Foreground "Green"
Write-Host "=Copying Prebuilt Libs...=" -Foreground "Green"
Write-Host "==========================" -Foreground "Green"
.\Site-13-Prebuilts.exe ..\PrebuildLibs\Rule.txt
#Set permissions
Write-Host "========================" -Foreground "Green"
Write-Host "=Setting Permissions...=" -Foreground "Green"
Write-Host "========================" -Foreground "Green"
.\Chmod777.ps1 ..\Build\x64.Uncompressed\
$TargetFile=$ExecutionContext.SessionState.Path.GetUnresolvedProviderPathFromPSPath("..\Build\$Version\Sources\Install.wim")
$TargetFolder=$ExecutionContext.SessionState.Path.GetUnresolvedProviderPathFromPSPath("..\Build\$Version\")
$TargetISO=$ExecutionContext.SessionState.Path.GetUnresolvedProviderPathFromPSPath("..\Build\Site-13.$Version.iso")
Write-Host 'Packaging into WIM with WimEditKit.exe' -Foreground "Green"
#Make WIM file with WIMEditKit
.\WimEditKit.exe -Add-Image:Site-13-Main-Game?$SKU0 -Add-Image:Ext-Tools?$SKU1 -Add-Image:Site-13-SDK?$SKU2 -WIMFile:$TargetFile
#Copy Installer
Copy-Item -Path ./Installer/* -Container -Destination ../Build/$Version/ -Force -Recurse
Write-Host 'Packaging into ISO with ISOCreatorCLI.exe' -Foreground "Green"
#Package To ISO
.\ISOCreatorCLI.exe $TargetFolder "Site-13.$Version" $TargetISO
Write-Host "============" -Foreground "Green"
Write-Host "= Finished =" -Foreground "Green"
Write-Host "============" -Foreground "Green"