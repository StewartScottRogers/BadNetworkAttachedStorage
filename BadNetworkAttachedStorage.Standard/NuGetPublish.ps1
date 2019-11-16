$ScriptDir = Split-Path $script:MyInvocation.MyCommand.Path
Write-Host "Current script directory is $ScriptDir"
dotnet nuget push $ScriptDir\bin\Release\BadNetworkAttachedStorage.Standard.1.0.1.nupkg -k oy2kpgvvvnvqe55fcg4qg2hmgfln5cieziv56wlxksyvj4 -s https://api.nuget.org/v3/index.json