Source code for Shamir Secret Sharing by group Holdet consisting of `alsk` and `vson`.

The code is written in C# with the .NET framework version 8.0.

To run the console application do:
```
cd .\ShamirSecretSharing\
dotnet run
```

We have written the classes Polynomial, Point and an extension method for the BigInteger class which can be found in the Utilities project.

## Different .NET versions

It is possible to change the targeted version of .NET by modifying ShamirSecretSharing.csproj and Utilities.csproj to target whatever you have installed. 
Though please note we have only tested the program on .NET 8.0.
