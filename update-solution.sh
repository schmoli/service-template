#!/bin/bash
rm -f main.sln
dotnet new sln -n main >/dev/null
find ./src -type f -name *.csproj | xargs dotnet sln add
