param(
    [string]$ProjectDir
)

if (-not $ProjectDir -or $ProjectDir -eq "") {
    $ProjectDir = Split-Path -Parent $MyInvocation.MyCommand.Path
}

Write-Host "=== GroundTimeUuidGui :: OpenFlights .dat -> CSV converter ==="

$root    = $ProjectDir
$dataDir = Join-Path $root "data"

$airportsDat  = Join-Path $dataDir "airports.dat"
$airlinesDat  = Join-Path $dataDir "airlines.dat"

$airportsCsv  = Join-Path $root "airports.csv"
$operatorsCsv = Join-Path $root "operators.csv"

#
# Airports: airports.dat -> airports.csv
# Expected output schema for airports.csv:
#   code_type,code,canonical_icao,name
# where:
#   - code_type: ICAO or IATA
#   - code:      alias (e.g. KMIA, MIA)
#   - canonical_icao: primary ICAO-like code used for grouping
#   - name:      display text (no commas to keep parsing simple)
#
if (Test-Path $airportsDat) {
    Write-Host "Generating airports.csv from" $airportsDat

    $lines = Get-Content $airportsDat
    $outAirports = @()
    $outAirports += "code_type,code,canonical_icao,name"

    foreach ($line in $lines) {
        if ([string]::IsNullOrWhiteSpace($line)) { continue }

        $parts = $line.Split(',')
        if ($parts.Count -lt 7) { continue }

        $name    = $parts[1].Trim('"')
        $city    = $parts[2].Trim('"')
        $country = $parts[3].Trim('"')
        $iata    = $parts[4].Trim('"')
        $icao    = $parts[5].Trim('"')

        # Choose a canonical code: prefer ICAO, then IATA
        $canonical = $null
        if ($icao -and $icao -ne "\N") {
            $canonical = $icao
        } elseif ($iata -and $iata -ne "\N") {
            $canonical = $iata
        }

        if (-not $canonical) { continue }

        # Build a simple display name and strip commas so the CSV remains 4 columns.
        $display = "$name - $city - $country"
        $display = $display.Replace(',', ' ')

        if ($icao -and $icao -ne "\N") {
            $outAirports += ("ICAO,{0},{1},{2}" -f $icao, $canonical, $display)
        }

        if ($iata -and $iata -ne "\N") {
            $outAirports += ("IATA,{0},{1},{2}" -f $iata, $canonical, $display)
        }
    }

    $outAirports | Set-Content -Path $airportsCsv -Encoding UTF8
} else {
    Write-Host "NOTE: airports.dat not found at $airportsDat (download from https://openflights.org/ and place it there)."
}

#
# Operators: airlines.dat -> operators.csv
# Expected output schema for operators.csv:
#   code_type,code,canonical_icao,name
# where:
#   - code_type: ICAO or IATA
#   - code:      alias (e.g. SIA, SQ)
#   - canonical_icao: primary ICAO-like code used for grouping
#   - name:      display text (no commas to keep parsing simple)
#
if (Test-Path $airlinesDat) {
    Write-Host "Generating operators.csv from" $airlinesDat

    $lines = Get-Content $airlinesDat
    $outOperators = @()
    $outOperators += "code_type,code,canonical_icao,name"

    foreach ($line in $lines) {
        if ([string]::IsNullOrWhiteSpace($line)) { continue }

        $parts = $line.Split(',')
        if ($parts.Count -lt 8) { continue }

        $name    = $parts[1].Trim('"')
        $alias   = $parts[2].Trim('"')
        $iata    = $parts[3].Trim('"')
        $icao    = $parts[4].Trim('"')
        $country = $parts[6].Trim('"')

        # Choose canonical ICAO-like identifier: prefer ICAO, then IATA.
        $canonical = $null
        if ($icao -and $icao -ne "\N") {
            $canonical = $icao
        } elseif ($iata -and $iata -ne "\N") {
            $canonical = $iata
        }

        if (-not $canonical) { continue }

        $display = "$name - $country"
        $display = $display.Replace(',', ' ')

        if ($icao -and $icao -ne "\N") {
            $outOperators += ("ICAO,{0},{1},{2}" -f $icao, $canonical, $display)
        }

        if ($iata -and $iata -ne "\N") {
            $outOperators += ("IATA,{0},{1},{2}" -f $iata, $canonical, $display)
        }

        if ($alias -and $alias -ne "\N") {
            $outOperators += ("ALIAS,{0},{1},{2}" -f $alias, $canonical, $display)
        }
    }

    $outOperators | Set-Content -Path $operatorsCsv -Encoding UTF8
} else {
    Write-Host "NOTE: airlines.dat not found at $airlinesDat (download from https://openflights.org/ and place it there)."
}

Write-Host "OpenFlights conversion complete."
