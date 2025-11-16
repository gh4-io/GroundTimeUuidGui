# Ground Time UUID Generator

A small Windows desktop utility for generating **stable, deterministic UUIDs** for aircraft ground time records, designed to feed the **AMOS** (Swiss-AS) `transferGroundTime` (APN-1403) interface.

This tool helps an MRO / planning department ensure that **the same ground time event** always results in **the same UUID**, even when data comes from multiple systems or customers using different operator and station codes.

---

## ✈️ Problem This Solves

In a multi-customer aviation maintenance environment, you may receive planned ground times from:

- Internal planning tools  
- Customer planning departments  
- Third-party integrations  

Each of these could use different conventions:

- IATA vs ICAO operator codes (`SQ` vs `SIA`, `K4` vs `CKS`)  
- IATA vs ICAO airport codes (`SIN` vs `WSSS`, `CVG` vs `KCVG`)  
- Internal shorthands (e.g. `DHLUK`, `21`)  

If each system generates its own ID, you end up with:

- Duplicate representations of the same event  
- Collisions / confusion in AMOS  
- Painful reconciliation and reporting

This app gives you a single, **canonical UUID** for a ground time event based on:

- Operator  
- Aircraft registration  
- Scheduled arrival (UTC)  
- Station (airport)  

---

## 🔑 Core Idea

The app uses **UUID Version 5** (namespace-based SHA-1) with a stable namespace and a strict seed string format:

```text
ICAO_OPERATOR + "_" + AIRCRAFT_REG + "_" + ISO_DATETIME_UTC + "_" + ICAO_STATION
```

Example:

```text
DHK_GDHLU_2025-11-15T14:00Z_KCVG
```

Becomes a UUID like:

```text
fed1ba97-91c9-548b-902a-db7e0e2cc87d
```

Same input values → same UUID every time, regardless of where it’s generated.

---

## 🧮 How the UUID Is Hashed (Implementation Details)

The core implementation lives in `MainForm.cs` in the `GenerateUuid5` method. It follows **RFC 4122** for UUIDv5:

1. Take a fixed namespace UUID (the **DNS namespace**):

   ```text
   6ba7b810-9dad-11d1-80b4-00c04fd430c8
   ```

2. Convert the namespace GUID to **network byte order** (big-endian) to match RFC 4122.
3. Concatenate the namespace bytes with the UTF-8 bytes of the seed string:

   ```text
   bytes = BigEndianBytes(namespaceGuid) + Utf8Bytes(seed)
   ```

4. Compute **SHA-1** over that combined buffer:

   ```text
   hash = SHA1(bytes)
   ```

5. Take the **first 16 bytes** of the hash as the raw UUID payload.
6. Set the UUID **version bits** to `5` and the **variant bits** to “RFC 4122”.
7. Convert the bytes back into the **little-endian layout** used by `System.Guid` in .NET.

So in pseudo-code:

```csharp
bytes = BigEndian(namespaceGuid) + UTF8(seed);
hash = SHA1(bytes);
uuidBytes = hash[0..15];

uuidBytes[6] = (uuidBytes[6] & 0x0F) | (5 << 4); // version 5
uuidBytes[8] = (uuidBytes[8] & 0x3F) | 0x80;     // RFC 4122 variant

guid = new Guid(ToLittleEndian(uuidBytes));
```

The `seed` is always:

```text
ICAO_OPERATOR + "_" + AIRCRAFT_REG + "_" + ISO_DATETIME_UTC + "_" + ICAO_STATION
```

which is exactly the “Core Idea” documented above.

---

## 🧱 Features

### 1. Deterministic UUID v5 generation

- Uses the official **DNS namespace UUID**.
- UUID is derived from the normalized seed string.
- Collision-resistant and fully deterministic.

### 2. Canonical ICAO normalization (data-driven)

Operators and airports are normalized to **canonical ICAO codes** before being used in the UUID seed.

The app supports two modes:

1. **Built-in minimal set** (shipped in code) – covers your common customers and stations so the EXE works out-of-the-box.
2. **Full data-driven set** via CSV files – you can drop complete ICAO/IATA lists next to the EXE without recompiling.

**CSV formats (UTF-8 with header):**

- `operators.csv`

  ```text
  code_type,code,canonical_icao,name
  ICAO,DHK,DHK,DHL AIR UK
  IATA,SQ,SIA,SINGAPORE AIRLINES
  INTERNAL,DHLUK,DHK,DHL AIR UK
  ...
  ```

- `airports.csv`

  ```text
  code_type,code,canonical_icao,name
  ICAO,KCVG,KCVG,CINCINNATI/NORTHERN KENTUCKY INTL, USA
  IATA,CVG,KCVG,CINCINNATI/NORTHERN KENTUCKY INTL, USA
  ICAO,WSSS,WSSS,SINGAPORE CHANGI, SINGAPORE
  IATA,SIN,WSSS,SINGAPORE CHANGI, SINGAPORE
  ...
  ```

Only `code`, `canonical_icao`, and `name` are used for logic; `code_type` is informational.

> ⚠️ **Important:** The repo includes only a **small sample set** for illustration and to make the app usable out-of-the-box.
> To have **ALL** ICAO/IATA airports and operators, you must export those from an authoritative / licensed source
> (internal data warehouse, commercial dataset, etc.) into the CSV formats above and drop them next to the EXE.

### 3. Smart, planner-friendly input UX

- **Customer Code**
  - Uppercase enforced
  - Max length: **5**
  - Operator name displayed in gray if found; “UNKNOWN OPERATOR” otherwise

- **Aircraft Registration**
  - Uppercase enforced
  - Max length: **8** (e.g. `9V-SWA`, `G-DHLM`, `N123AB`)

- **Station**
  - Uppercase enforced
  - Max length: **4**
  - Airport name displayed in gray if found; “UNKNOWN STATION” otherwise

- **Scheduled Date**
  - Smart parsing:  
    - `11142025` → `11/14/2025`
  - Calendar button shows an overlay `MonthCalendar` (repositioned so it never gets clipped by the window)

- **Scheduled Time**
  - Smart parsing:  
    - `1400` → `14:00`
  - Validated as 24-hour time (00:00–23:59)

### 4. Auto UUID generation (with safety checks)

The UUID is **auto-generated and kept in sync** when all of the following are true:

- All fields are filled
- Date and time parse correctly
- Operator and station are **known** in the loaded dictionaries

If either operator or station is unknown:

- Auto-generation quietly does nothing
- You can still click **GENERATE UUID** to force a manual UUID using raw codes.

### 5. ISO preview

The app shows the **ISO 8601 UTC timestamp** used in the seed:

```text
YYYY-MM-DDTHH:MMZ
```

e.g. `2025-11-15T14:00Z`

This makes it easy to verify that the “scheduled arrival” you think you’re using matches what’s going into the AMOS payload and UUID.

### 6. Discreet About panel

- Subtle **🛈** icon in the top-right (light gray, darkens on hover)
- Click to open a structured **About** dialog that documents:
  - Purpose
  - UUID strategy
  - Normalization rules
  - CSV formats
  - Input behavior
  - Rebuild notes
  - Credits

---

## 🛠️ Tech Stack

- **Language:** C#  
- **Framework:** .NET **10.0** (target framework: `net10.0-windows`)  
- **UI:** Windows Forms  
- **Target:** `net10.0-windows`  

For a debug/dev build, you just need the .NET 10 SDK installed.  
For deployment, you can publish a **self-contained EXE**.

---

## 📦 Building From Source

### Prerequisites

- **.NET SDK 10.0 or later**
- Windows (WinForms-capable)

### Build & run (Debug)

From the folder containing `GroundTimeUuidGui.csproj`:

```powershell
dotnet build -c Debug
dotnet run
```

This will launch the WinForms app.

---

## 🚀 Publishing a Standalone EXE

To create a **single-file, self-contained** EXE for 64-bit Windows:

```powershell
dotnet publish -c Release -r win-x64 --self-contained true /p:PublishSingleFile=true
```

The output will be in:

```text
./bin/Release/net10.0-windows/win-x64/publish/
```

Inside you’ll find:

- `GroundTimeUuidGui.exe`  ← the EXE you can copy to any 64-bit Windows machine

You may optionally place:

- `operators.csv`
- `airports.csv`

in the same folder as the EXE to enable your **full reference tables**.

If the CSVs are missing or invalid, the app falls back to its built-in minimal dictionaries.

---

## 📚 Using Full Reference Tables (ALL ICAO/IATA)

To move from a curated set to a **complete list**:

1. Obtain authoritative code tables (ICAO/IATA operators and airports) from:
   - Your data warehouse
   - Internal reference tables
   - Vendor / subscription services  
   *(Make sure you comply with licensing terms.)*

2. Transform them to the CSV formats expected by the app:

   - `operators.csv`

     ```text
     code_type,code,canonical_icao,name
     ICAO,DHK,DHK,DHL AIR UK
     IATA,SQ,SIA,SINGAPORE AIRLINES
     INTERNAL,DHLUK,DHK,DHL AIR UK
     ...
     ```

   - `airports.csv`

     ```text
     code_type,code,canonical_icao,name
     ICAO,KCVG,KCVG,CINCINNATI/NORTHERN KENTUCKY INTL, USA
     IATA,CVG,KCVG,CINCINNATI/NORTHERN KENTUCKY INTL, USA
     ...
     ```

3. Drop both CSV files in the same folder as `GroundTimeUuidGui.exe`.

4. Start the app – it will automatically load the CSV-based dictionaries and use them for:
   - Display names
   - Canonical ICAO codes used in UUID seeds
   - Auto-generation rules (must be known to auto-generate).

> 🔎 **Note:** This repository does NOT include the full ICAO/IATA datasets because
> those are large and typically subject to licensing. The app is designed so you
> can plug in your own complete tables without changing the code.

---

## 🧩 Project Structure

- `GroundTimeUuidGui.csproj`  
  Project definition, target framework.

- `Program.cs`  
  Standard WinForms entry point (`ApplicationConfiguration.Initialize()` + `MainForm`).

- `ApplicationConfiguration.cs`  
  Minimal WinForms configuration (visual styles, text rendering).

- `MainForm.cs`  
  UI logic, UUID generation, normalization, auto-update rules.

- `MainForm.Designer.cs`  
  Layout of the main window.

- `OperatorInfo.cs` / `AirportInfo.cs`  
  Simple value objects for normalized entries (canonical ICAO + name).

- `OperatorDirectoryProvider.cs`  
  Loads operators from `operators.csv` (if present) or uses built-in defaults.

- `AirportDirectoryProvider.cs`  
  Loads airports from `airports.csv` (if present) or uses built-in defaults.

- `AboutForm.cs` / `AboutForm.Designer.cs`  
  About dialog layout and content.

- `operators.csv` / `airports.csv`  
  Sample CSVs you can replace with your full datasets.

- `README.md`  
  This file.

---

## 🔁 Rebuilding the Logic Elsewhere

If you ever need to re-implement this in another language (e.g., a web tool, integration script, or AMOS-side helper), the core algorithm is:

1. Normalize **operator** to canonical ICAO where possible.
2. Normalize **station** (airport) to canonical ICAO where possible.
3. Convert scheduled arrival to **UTC ISO 8601** (`yyyy-MM-ddTHH:mmZ`).
4. Build the seed:
   ```text
   {ICAO_OPERATOR}_{AIRCRAFT_REG}_{ISO_UTC}_{ICAO_STATION}
   ```
5. Compute **UUIDv5** using:
   - Namespace: `6ba7b810-9dad-11d1-80b4-00c04fd430c8`
   - Name: the seed string above.

Same seed → same UUID → easy reconciliation between systems.

---

## 👤 Credits

- **Domain design & requirements**  
  Jason Grace (CVG 145 Planning / Operations Management)

- **Implementation assistance & UUID architecture**  
  ChatGPT 5.1 (OpenAI)

---

## 📝 License

*(Add your chosen license here — MIT is a common, simple choice for internal tools.)*
