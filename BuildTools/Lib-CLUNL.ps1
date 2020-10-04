Write-Output "Build CLUNL..."
Set-Location ..
Set-Location CLUNL
Set-Location "Creeper Lv`'s Universal dotNet Library"
dotnet build -c:Release
Copy-Item "bin/Release/netstandard2.0/CLUNL.dll" "../../Assets/Plugins/CLUNL.dll" -Force
Set-Location ../../BuildTools/
Write-Output "Build CLUNL... Done"