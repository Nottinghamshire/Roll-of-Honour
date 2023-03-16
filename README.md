# Roll of Honour

A website dedicated to those who were lost during the Great War and the Second World War within Nottinghamshire. Written in .NET 7, using a simple N-Tier + Practical Clean Arch concoction.

## Preparing to run

- Install .NET 7
- Add a reference an Azure App Configuration and a database (optionally configure with your own DB provider or app configuration service/file)
- Run using:
```bash
$ dotnet watch --verbose run --project .\src\RollOfHonour.Web\RollOfHonour.Web.csproj
```