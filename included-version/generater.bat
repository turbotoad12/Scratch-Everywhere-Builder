@echo off
setlocal enabledelayedexpansion

echo ^<ItemGroup^>

for /r "%cd%" %%F in (*) do (
    rem Skip .cs and .csproj files
    if /I not "%%~xF"==".cs" (
        if /I not "%%~xF"==".csproj" (
            
            rem Build a relative path
            set "file=%%F"
            set "rel=!file:%cd%\=!"

            echo   ^<Content Include="!rel!" /^>
        )
    )
)

echo ^</ItemGroup^>

endlocal
