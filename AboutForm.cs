using System;
using System.Windows.Forms;

namespace GroundTimeUuidGui
{
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();
            rtbAbout.Text = GetAboutText();
            rtbAbout.SelectionStart = 0;
            rtbAbout.SelectionLength = 0;
        }

        private string GetAboutText()
        {
            return
@"GROUND TIME UUID GENERATOR
Version 1.0

PURPOSE
This tool generates consistent, deterministic UUIDs for aircraft ground time records
to be consumed by AMOS (Swiss-AS) via the transferGroundTime interface (APN-1403).

It is designed so that different data sources (customers, internal systems, imports)
produce the SAME UUID for the SAME ground time event, even if they use slightly
different codes (IATA vs ICAO, internal shorthand, etc.).

UUID STRATEGY (HIGH LEVEL)
- UUID Version 5 (namespace-based, SHA-1, RFC 4122)
- Namespace UUID (DNS): 6ba7b810-9dad-11d1-80b4-00c04fd430c8
- Seed format:
    ICAO_OPERATOR + ""_"" + AIRCRAFT_REG + ""_"" + ISO_DATETIME_UTC + ""_"" + ICAO_STATION

Example seed:
    DHK_GDHLU_2025-11-15T14:00Z_KCVG

This produces a stable, collision-resistant UUID for each unique combination of:
- Operator
- Aircraft registration
- Scheduled arrival (UTC)
- Station (airport)

UUID STRATEGY (HOW IT IS HASHED IN CODE)
Implementation (GenerateUuid5 method in MainForm.cs):
1. Take the namespace GUID and convert it to RFC 4122 network byte order (big-endian fields).
2. Concatenate the namespace bytes with the UTF-8 bytes of the seed string.
3. Compute SHA-1 over that combined buffer.
4. Take the first 16 bytes of the SHA-1 hash as the raw UUID payload.
5. Set the UUID version bits to 5 and the variant bits to RFC 4122.
6. Convert the bytes back into the little-endian layout used by System.Guid.

In pseudo-code:

    bytes = BigEndianBytes(namespaceGuid) + Utf8Bytes(seed)
    hash  = SHA1(bytes)
    uuidBytes = hash[0..15]
    uuidBytes[6] = (uuidBytes[6] & 0x0F) | (5 << 4)  // version 5
    uuidBytes[8] = (uuidBytes[8] & 0x3F) | 0x80      // RFC 4122 variant

    // convert to .NET Guid with proper byte swaps
    guid = new Guid(ToLittleEndian(uuidBytes))

CANONICAL ICAO NORMALIZATION
Operator and airport codes are normalized to canonical ICAO codes before going
into the UUID seed.

The app supports two lookup modes:
- Built-in defaults for common operators and stations
- Full data-driven mode via CSV files next to the EXE:
    operators.csv and airports.csv

CSV formats:
- operators.csv:
    code_type,code,canonical_icao,name
- airports.csv:
    code_type,code,canonical_icao,name

You can populate these with full ICAO + IATA lists from your own data sources
and the app will automatically use them on startup.

INPUT BEHAVIOR
- CUSTOMER CODE
    * Uppercase only
    * Max length: 5 characters
    * Looks up operator and shows its name, or 'UNKNOWN OPERATOR'

- AIRCRAFT REGISTRATION
    * Uppercase only
    * Max length: 8 characters
    * Wide enough for typical registrations (e.g. N123AB, 9V-SWA)

- STATION
    * Uppercase only
    * Max length: 4 characters
    * Looks up airport and shows full name, or 'UNKNOWN STATION'

- SCHEDULED DATE
    * Smart parsing: typing 11142025 becomes 11/14/2025
    * Calendar button shows an overlay MonthCalendar (never clipped by the form)

- SCHEDULED TIME
    * Smart parsing: typing 1400 becomes 14:00
    * Validated as 24-hour time (00:00 through 23:59)

AUTO UUID GENERATION
The UUID is automatically generated and updated whenever:
- All fields are filled
- Date and time parse correctly
- Operator and station are BOTH known in their dictionaries

If operator or station are unknown:
- Auto-generation does nothing
- You may still click the 'GENERATE UUID' button to create one manually
  (using raw codes for any unknown values).

REBUILD NOTES (FOR FUTURE YOU)
To rebuild this tool from scratch, you need:
- A .NET 10.0 Windows Forms project (WinExe)
- MainForm with:
    * CUSTOMER CODE, AIRCRAFT REGISTRATION, STATION, DATE, TIME inputs
    * Operator and airport info labels (gray text)
    * ISO SCHEDULED ARRIVAL (UTC) preview field
    * UUID output field
    * 'GENERATE UUID' button
    * Calendar button that shows a MonthCalendar overlay
    * A subtle top-right info icon (🛈) that opens this About dialog

- Logic:
    * All text fields forced to uppercase
    * Max lengths:
        - Customer: 5
        - Station: 4
        - Registration: 8
    * OperatorDirectoryProvider:
        - Attempts to load operators.csv next to the EXE
        - Falls back to built-in minimal set if not present
    * AirportDirectoryProvider:
        - Attempts to load airports.csv next to the EXE
        - Falls back to built-in minimal set if not present
    * UUID v5 function using the DNS namespace GUID above
    * Seed: ICAO_OPERATOR + '_' + REG + '_' + ISO_UTC + '_' + ICAO_STATION
    * Auto-generate only when all required fields valid AND both operator
      and station are known

CREDITS
- Domain design and requirements:
    Jason Grace (CVG 145 Planning / Operations Management)

- Implementation assistance and architecture:
    ChatGPT 5.1 (OpenAI)
    Assisted with:
      * UUID v5 design
      * Canonical ICAO normalization model
      * Windows Forms UI layout
      * Auto-generation rules
      * Data-driven code/airport lookups via CSV
      * Rebuild documentation inside this dialog";
        }
    }
}
