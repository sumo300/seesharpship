@echo off

setlocal

pushd ..
pushd SeeSharpShip

set config=Release

copy SeeSharpShip.nuspec.%config% SeeSharpShip.nuspec
nuget pack SeeSharpShip.csproj -Build -IncludeReferencedProjects -Properties Configuration=%config%;OutputPath=.\bin\%config%
del SeeSharpShip.nuspec

popd
popd