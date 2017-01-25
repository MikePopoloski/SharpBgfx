Push-Location ../bgfx
&../bx/tools/bin/windows/genie.exe --with-shared-lib --with-tools vs2017

& 'C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\MSBuild\15.0\Bin\MSBuild.exe' .\.build\projects\vs2017\bgfx.sln /t:rebuild /p:configuration=Debug /p:platform=x64
if ($LastExitCode -ne 0) {
	Pop-Location
	Return
}

& 'C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\MSBuild\15.0\Bin\MSBuild.exe' .\.build\projects\vs2017\bgfx.sln /t:rebuild /p:configuration=Release /p:platform=x64
if ($LastExitCode -ne 0) {
	Pop-Location
	Return
}

Copy-Item .build\win64_vs2017\bin\bgfx-shared-libDebug.dll ../SharpBgfx/external/bgfx/bgfx_debug.dll
Copy-Item .build\win64_vs2017\bin\bgfx-shared-libRelease.dll ../SharpBgfx/external/bgfx/bgfx.dll

Copy-Item .build\win64_vs2017\bin\shadercRelease.exe ../SharpBgfx/tools/shaderc.exe
Copy-Item .build\win64_vs2017\bin\geometrycRelease.exe ../SharpBgfx/tools/geometryc.exe
Copy-Item .build\win64_vs2017\bin\texturecRelease.exe ../SharpBgfx/tools/texturec.exe
Copy-Item .build\win64_vs2017\bin\texturevRelease.exe ../SharpBgfx/tools/texturev.exe

Pop-Location

..\Amalgamate\Amalgamate\bin\Release\Amalgamate.exe SharpBgfx SharpBgfx.cs LICENSE