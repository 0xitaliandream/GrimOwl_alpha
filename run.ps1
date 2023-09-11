# Passo 1: Compilare e costruire il progetto del server
dotnet build ".\GrimOwlRiptideServer\GrimOwlRiptideServer.csproj"
if ($LASTEXITCODE -ne 0) {
    Write-Host "La compilazione del server è fallita. Premere un tasto per continuare..."
    $null = Read-Host
    exit 1
}

# Passo 1.1: Compilare e costruire il progetto del client
dotnet build ".\GrimOwlConsoleGuiClient\GrimOwlConsoleGuiClient.csproj"
if ($LASTEXITCODE -ne 0) {
    Write-Host "La compilazione del client è fallita. Premere un tasto per continuare..."
    $null = Read-Host
    exit 1
}

# Passo 2: Imposta i percorsi relativi per i client e il server
$serverPath = ".\GrimOwlRiptideServer\bin\Debug\net7.0\GrimOwlRiptideServer.exe"
$clientPath = ".\GrimOwlConsoleGuiClient\bin\Debug\net7.0\GrimOwlConsoleGuiClient.exe"

# Passo 3: Avvia il server
Start-Process -FilePath $serverPath -ArgumentList "1", "2"

# Pausa per permettere al server di iniziare
Start-Sleep -Seconds 2

# Passo 4: Avvia la prima istanza del client con parametri
Start-Process -FilePath $clientPath -ArgumentList "1", "1"

# Pausa per permettere all'utente di vedere cosa sta succedendo
Start-Sleep -Seconds 1

# Passo 5: Avvia la seconda istanza del client con parametri
Start-Process -FilePath $clientPath -ArgumentList "2", "1"
