# Reverse Polish notation calculator in .NET C#

Pass RPN expression as one string as an argument or run without arguments for REPL. Calculator also has basic error handler. 

## Examples

### Argument expression example

```powershell
dotnet run "5 3 +"
8

dotnet run "10 2 - 5 +"
13
```

### REPL examples

```powershell
dotnet run
>> 5 3 +
8
>> 10 2 a +
... 2 a + 
      ^ got unknown operator
>> 10 2 - * 5 + 
... 2 - * 5 + ...
        ^ operator requires 2 arguments
```

P.S.I still hate OOP (don't know it well), so code improvements are welcomed