# Przygotowanie
$path = "C:\Users\janbi\Desktop\PD-3-TANKS\data_neural_network.txt"

If (![System.IO.File]::Exists($path)) {
    # Program w Ruby - generacja startowego kodu JS sieci neuronowej opartej na randomowych wartościach
    ruby random_network_generator.rb 
}

$i = 1

$startDate = Get-Date
$EndDate = $startDate.AddMinutes(60)
do {
    # Program w C# - sterowanie czołgami i obsługa danych 
    ## przygotowanie wywołania
    $app = 'C:\Users\janbi\Desktop\PD-3-TANKS\NeuralHelp.exe'
    ## wywołanie
    & $app
    ## przechwycenie wartości LastExitCode; C# zwraca 1 lub 2 w zależności od koloru wygranego czołgu
    Write-Host "Wygral $LastExitCode"

    $winningTank = $LastExitCode
    If ($winningTank -eq 0) {
        continue
    }
	
	Copy-Item "C:\Users\janbi\Desktop\PD-3-TANKS\data_neural_network.txt" -Destination "C:\Users\janbi\Desktop\PD-3-TANKS\iterations\iteration$i.txt"

    # Program w R 
    $app = "C:\Program Files\R\R-3.5.2\bin\Rscript.exe"
    & $app network_training.R $winningTank $i

    # Program w Ruby - generacja kodu JS uczącej się sieci neuronowej opartej na podanych wartościach
    ruby learning_network_generator.rb 

    Write-Host "Zakonczono iteracje $i"
    
    $currentTime = Get-Date
    $timeLeft = NEW-TIMESPAN –Start $currentTime –End $EndDate
    Write-Host "Pozostalo czasu $timeLeft"
	$i = $i + 1
} while ($startDate.AddMinutes(60) -gt (Get-Date))