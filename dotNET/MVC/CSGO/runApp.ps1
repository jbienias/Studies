$command1 = "dotnet publish -c Release"
$command2 = "cd .\bin\Release\netcoreapp2.0\publish"
$command3 = "dotnet CSGO.dll"
iex $command1
iex $command2
iex $command3