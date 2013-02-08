@echo off

pushd ..
pushd SeeSharpShip

nuget pack SeeSharpShip.csproj -Prop Configuration=Release %1

popd
popd