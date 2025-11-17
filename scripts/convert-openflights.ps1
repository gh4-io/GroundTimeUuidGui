param(
    [string]$ProjectDir
)

if (-not $ProjectDir -or $ProjectDir -eq "") {
    $ProjectDir = Split-Path -Parent $MyInvocation.MyCommand.Path
}

Write-Host "=== GroundTimeUuidGui :: OpenFlights .dat -> CSV (Rev21) ==="

$root    = $ProjectDir
$dataDir = Join-Path $root "data"

$airportsDat  = Join-Path $dataDir "airports.dat"
$airlinesDat  = Join-Path $dataDir "airlines.dat"

$airportsCsv  = Join-Path $root "airports.csv"
$operatorsCsv = Join-Path $root "operators.csv"

#
# Helper: sanitize text so we don't have commas in text fields
#
function Sanitize-Text([string]$value) {
    if (-not $value) { return "" }
    $v = $value.Trim('"')
    # Replace commas with spaces so we can safely split on comma later.
    return $v.Replace(",", " ")
}

#
# AIRPORTS
# Generate airports.csv with OpenFlights-like columns:
#   AirportID,Name,City,Country,IATA,ICAO
#
if (Test-Path $airportsDat) {
    Write-Host "Generating airports.csv from $airportsDat"

    $airportHeader = @(
        "AirportID","Name","City","Country",
        "IATA","ICAO","Latitude","Longitude",
        "Altitude","TimezoneOffset","DST","TzTimeZone",
        "Type","Source"
    )

    $airports = Import-Csv -Path $airportsDat -Header $airportHeader

    $outLines = @()
    $outLines += "AirportID,Name,City,Country,IATA,ICAO"

    foreach ($a in $airports) {
        $iata = $a.IATA
        $icao = $a.ICAO

        # Skip entries that have neither ICAO nor IATA
        if ([string]::IsNullOrWhiteSpace($iata) -and [string]::IsNullOrWhiteSpace($icao)) {
            continue
        }

        $airportId = $a.AirportID.Trim('"')
        $name      = Sanitize-Text $a.Name
        $city      = Sanitize-Text $a.City
        $country   = Sanitize-Text $a.Country
        $iata      = $iata.Trim('"')
        $icao      = $icao.Trim('"')

        $line = "{0},{1},{2},{3},{4},{5}" -f `
            $airportId, $name, $city, $country, $iata, $icao

        $outLines += $line
    }

    $outLines | Set-Content -Path $airportsCsv -Encoding UTF8
}
else {
    Write-Host "NOTE: airports.dat not found at $airportsDat"
}

#
# AIRLINES / OPERATORS
# Generate operators.csv with OpenFlights-like columns:
#   AirlineID,Name,Alias,IATA,ICAO,Country
#
if (Test-Path $airlinesDat) {
    Write-Host "Generating operators.csv from $airlinesDat"

    $airlineHeader = @(
        "AirlineID","Name","Alias","IATA",
        "ICAO","Callsign","Country","Active"
    )

    $airlines = Import-Csv -Path $airlinesDat -Header $airlineHeader

    $outLines = @()
    $outLines += "AirlineID,Name,Alias,IATA,ICAO,Country"

    foreach ($o in $airlines) {

        $iata = $o.IATA
        $icao = $o.ICAO
        $alias = $o.Alias

        # Skip entries that have neither ICAO nor IATA
        if ([string]::IsNullOrWhiteSpace($iata) -and [string]::IsNullOrWhiteSpace($icao)) {
            continue
        }

        $airlineId = $o.AirlineID.Trim('"')
        $name      = Sanitize-Text $o.Name
        $alias     = Sanitize-Text $alias
        $iata      = $iata.Trim('"')
        $icao      = $icao.Trim('"')
        $country   = Sanitize-Text $o.Country

        $line = "{0},{1},{2},{3},{4},{5}" -f `
            $airlineId, $name, $alias, $iata, $icao, $country

        $outLines += $line
    }

    $outLines | Set-Content -Path $operatorsCsv -Encoding UTF8
}
else {
    Write-Host "NOTE: airlines.dat not found at $airlinesDat"
}

Write-Host "OpenFlights conversion complete (Rev21)."
