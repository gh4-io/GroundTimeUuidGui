Place OpenFlights source files here if you want the build to regenerate airports.csv/operators.csv.

Expected files:
  - airports.dat
  - airlines.dat

The MSBuild target 'GenerateOpenFlightsCsv' will run scripts/convert-openflights.ps1
before resources are prepared and will update the root-level airports.csv and
operators.csv files accordingly.
