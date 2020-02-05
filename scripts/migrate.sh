#!/bin/bash
cd ../src/Stocky.Net
dotnet ef database drop
dotnet ef migrations remove
dotnet ef migrations add init
dotnet ef database update
