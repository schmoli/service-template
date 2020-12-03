#!/bin/bash

mkdir -p bin

# install dotnet-format if it doesn't exist on the PATH
TOOL=`command -v dotnet-format`
if [ -z "$TOOL" ]
then
  echo "Not found, installing locally"
  dotnet tool install --tool-path ./bin dotnet-format
  TOOL='./bin/dotnet-format'
fi
echo "Found tool at $TOOL"


# pass all arguments to dotnet format
$TOOL ./src --folder $*
