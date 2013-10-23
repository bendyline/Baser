@echo off

IF "%1" == "r" (SET cla="/p:Configuration=Release")
IF "%1" == "rc" (SET cla="/p:Configuration=Release" & SET clb="/t:Rebuild")
IF "%1" == "c" (SET cla="/t:Rebuild")

pushd net
echo === Compiling Baser.net
msbuild /nologo /v:q %cla% %clb% %2 baser.net.csproj
popd

pushd script
echo === Compiling Baser.script
msbuild /nologo /v:q %cla% %clb% %2 baser.script.csproj
popd
