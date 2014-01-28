@echo off

setlocal

pushd ..
pushd SeeSharpShip

set config=Debug

copy SeeSharpShip.nuspec.%config% SeeSharpShip.nuspec
nuget pack SeeSharpShip.csproj -Build -Symbols -Properties Configuration=%config%;OutputPath=.\bin\%config%
del SeeSharpShip.nuspec

popd
popd