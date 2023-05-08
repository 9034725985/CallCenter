# for learning purposes only

This is not production code. 
It is for learning only. 

```powershell
cd C:\Users\kushal\src\dotnet7\callCenter\CallCenter.Benchmarks\; date; dotnet clean; date; date; dotnet build; $now = (Get-Date).ToString("yyyyMMddHHmmss"); date | Out-File "C:\Users\kushal\src\dotnet7\CallCenter\CallCenter.Benchmarks\$now.txt"; dotnet run --configuration=Release --verbosity=minimal  | Out-File -Append "C:\Users\kushal\src\dotnet7\CallCenter\CallCenter.Benchmarks\$now.txt"; date  | Out-File -Append "C:\Users\kushal\src\dotnet7\CallCenter\CallCenter.Benchmarks\$now.txt";
```