# GroundTimeUuidGui – Changelog

## Rev 20
- Fixed airport/airport code mapping so that OpenFlights `airports.dat` is converted into a proper `airports.csv` in the format `code_type,code,canonical_icao,name`, ensuring lookups like KMIA/KCVG work reliably.
- Fixed operator mapping by converting OpenFlights `airlines.dat` into `operators.csv` with the same schema, allowing aliases and both ICAO/IATA codes to resolve to a canonical operator.
- Added a developer-only airport source label (`lblAirportSource`) in the main form that shows where a station entry came from (`[embedded]`, `[external]`, or `[built-in]`), without changing the core Rev 11 visual style.
- Extended `AirportInfo` to include a `Source` property so the UI and diagnostics can see whether an entry was loaded from embedded CSV, external CSV, or the built-in dictionary.
- Kept all existing Rev 11–17 styling intact, including UUID appearance, info text dulling, and Chrome-style dark mode.

## Rev 19
- Introduced layered data loading for airports and operators:
  - Internal (embedded) CSV is generated at build time from the OpenFlights `.dat` files when present.
  - External `airports.csv` / `operators.csv` next to the EXE override or supplement the embedded data (external rows win; missing external rows fall back to internal).
- Ensured that the EXE can run even without external data by falling back to a small built-in dictionary of common stations and operators.
- Preserved Rev 17 UI/UX and dark mode styling while adding the new data layering behavior.

## Rev 17 (context)
- Restored and locked in the Rev 11 visual style for UUID display and info text.
- Implemented hashed schedule string handling in place of raw ISO timestamps.
- Polished the dark mode theme to match Chrome’s dark color palette.
- Adjusted input field widths (Station=55, CustomerCode=55, Registration=80) and relabeled the registration field to “Ser Air”.
- Slightly increased the size of the info and dark-mode toggle icons and ensured they sit adjacent in the header area.
