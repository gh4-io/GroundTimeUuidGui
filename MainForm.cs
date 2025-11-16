using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace GroundTimeUuidGui
{
    public partial class MainForm : Form
    {
        // DNS namespace GUID as per RFC 4122
        private static readonly Guid NamespaceDns = Guid.Parse("6ba7b810-9dad-11d1-80b4-00c04fd430c8");

        // These dictionaries are loaded from CSVs if present, or fall back to built-in sets.
        private static readonly IReadOnlyDictionary<string, OperatorInfo> OperatorDirectory =
            OperatorDirectoryProvider.LoadOperators();

        private static readonly IReadOnlyDictionary<string, AirportInfo> AirportDirectory =
            AirportDirectoryProvider.LoadAirports();

        private bool _darkMode = false;

        public MainForm()
        {
            InitializeComponent();

            // Hide developer-only airport source label in normal builds.
            lblAirportSource.Visible = false;

            // Hook events for smart behavior and uppercase enforcement
            txtCustomerCode.TextChanged += UppercaseTextBox_TextChanged;
            txtReg.TextChanged += UppercaseTextBox_TextChanged;
            txtStation.TextChanged += UppercaseTextBox_TextChanged;

            // Max lengths
            txtCustomerCode.MaxLength = 5;  // covers ICAO (3), IATA (2), and internal like DHLUK (5)
            txtStation.MaxLength = 4;       // ICAO 4, IATA 3
            txtReg.MaxLength = 8;           // typical max for registrations including hyphen

            txtDate.TextChanged += TxtDate_TextChanged;
            txtTime.TextChanged += TxtTime_TextChanged;

            monthCalendar.Visible = false;

            // Info icon hover events
            infoIcon.MouseEnter += infoIcon_MouseEnter;
            infoIcon.MouseLeave += infoIcon_MouseLeave;
            infoIcon.Click += infoIcon_Click;

            // Theme icon hover events + click
            themeIcon.MouseEnter += themeIcon_MouseEnter;
            themeIcon.MouseLeave += themeIcon_MouseLeave;
            themeIcon.Click += themeIcon_Click;

            // Slightly larger icons
            infoIcon.Font = new Font(infoIcon.Font.FontFamily, infoIcon.Font.Size + 2, infoIcon.Font.Style);
            themeIcon.Font = new Font(themeIcon.Font.FontFamily, themeIcon.Font.Size + 2, themeIcon.Font.Style);

            // Apply initial theme (light)
            ApplyTheme();
        }

        #region Info icon handlers

        private void infoIcon_MouseEnter(object? sender, EventArgs e)
        {
            infoIcon.ForeColor = Color.DimGray;
            infoIcon.Cursor = Cursors.Hand;
        }

        private void infoIcon_MouseLeave(object? sender, EventArgs e)
        {
            infoIcon.ForeColor = Color.Silver;
            infoIcon.Cursor = Cursors.Default;
        }

        private void infoIcon_Click(object? sender, EventArgs e)
        {
            using (var about = new AboutForm())
            {
                about.ShowDialog(this);
            }
        }

        #endregion


        #region Theme (dark mode) handlers

        private void themeIcon_MouseEnter(object? sender, EventArgs e)
        {
            themeIcon.ForeColor = Color.DimGray;
            themeIcon.Cursor = Cursors.Hand;
        }

        private void themeIcon_MouseLeave(object? sender, EventArgs e)
        {
            themeIcon.ForeColor = Color.Silver;
            themeIcon.Cursor = Cursors.Default;
        }

        private void themeIcon_Click(object? sender, EventArgs e)
        {
            _darkMode = !_darkMode;
            ApplyTheme();
        }

        private void ApplyTheme()
        {
            if (_darkMode)
            {
                // Chrome-like dark theme
                this.BackColor = Color.FromArgb(32, 33, 36);   // #202124
                this.ForeColor = Color.FromArgb(232, 234, 237); // #E8EAED

                foreach (Control ctl in GetAllControls(this))
                {
                    ApplyDarkThemeToControl(ctl);
                }

                // Dulled info text in dark mode
                lblAirportInfo.ForeColor = Color.FromArgb(154, 160, 166);   // #9AA0A6
                lblOperatorInfo.ForeColor = Color.FromArgb(154, 160, 166);  // #9AA0A6
            }
            else
            {
                // Standard light theme
                this.BackColor = SystemColors.Control;
                this.ForeColor = SystemColors.ControlText;

                foreach (Control ctl in GetAllControls(this))
                {
                    ApplyLightThemeToControl(ctl);
                }

                // Dulled info text in light mode (like original v11)
                lblAirportInfo.ForeColor = SystemColors.GrayText;
                lblOperatorInfo.ForeColor = SystemColors.GrayText;
            }

            // Ensure icons keep their accent color
            var iconLight = SystemColors.GrayText;
            var iconDark = Color.FromArgb(189, 193, 198); // #BDC1C6

            infoIcon.ForeColor = _darkMode ? iconDark : iconLight;
            themeIcon.ForeColor = _darkMode ? iconDark : iconLight;
        }

        private static IEnumerable<Control> GetAllControls(Control root)
        {
            foreach (Control c in root.Controls)
            {
                yield return c;
                foreach (Control child in GetAllControls(c))
                {
                    yield return child;
                }
            }
        }

        private static void ApplyDarkThemeToControl(Control ctl)
        {
            // Chrome-like dark colors
            // Background: #202124 (32,33,36) and #303134 (48,49,52)
            // Primary text: #E8EAED (232,234,237)
            // Secondary text: #9AA0A6 (154,160,166)
            switch (ctl)
            {
                case TextBox or ComboBox:
                    ctl.BackColor = Color.FromArgb(32, 33, 36);
                    ctl.ForeColor = Color.FromArgb(232, 234, 237);
                    break;

                case Button or CheckBox:
                    ctl.BackColor = Color.FromArgb(48, 49, 52);
                    ctl.ForeColor = Color.FromArgb(232, 234, 237);
                    break;

                case Label:
                    ctl.BackColor = Color.Transparent;
                    // Info labels get dulled; others get primary text color
                    if (ctl.Name == "lblAirportInfo" || ctl.Name == "lblOperatorInfo")
                    {
                        ctl.ForeColor = Color.FromArgb(154, 160, 166);
                    }
                    else
                    {
                        ctl.ForeColor = Color.FromArgb(232, 234, 237);
                    }
                    break;

                case Panel or GroupBox:
                    ctl.BackColor = Color.FromArgb(32, 33, 36);
                    ctl.ForeColor = Color.FromArgb(232, 234, 237);
                    break;

                default:
                    // leave other controls mostly alone
                    break;
            }
        }

        private static void ApplyLightThemeToControl(Control ctl)
        {
            switch (ctl)
            {
                case TextBox or ComboBox:
                    ctl.BackColor = SystemColors.Window;
                    ctl.ForeColor = SystemColors.WindowText;
                    break;

                case Label:
                    ctl.BackColor = SystemColors.Control;
                    // Info labels use GrayText to appear dulled, like original v11
                    if (ctl.Name == "lblAirportInfo" || ctl.Name == "lblOperatorInfo")
                    {
                        ctl.ForeColor = SystemColors.GrayText;
                    }
                    else
                    {
                        ctl.ForeColor = SystemColors.ControlText;
                    }
                    break;

                case Button or CheckBox or Panel or GroupBox:
                    ctl.BackColor = SystemColors.Control;
                    ctl.ForeColor = SystemColors.ControlText;
                    break;

                default:
                    // leave other controls on their defaults
                    break;
            }
        }

        #endregion


        #region Event wiring helpers

        private void UppercaseTextBox_TextChanged(object? sender, EventArgs e)
        {
            if (sender is TextBox tb)
            {
                int selStart = tb.SelectionStart;
                int selLength = tb.SelectionLength;
                string upper = tb.Text.ToUpperInvariant();
                if (tb.Text != upper)
                {
                    tb.Text = upper;
                    tb.SelectionStart = selStart;
                    tb.SelectionLength = selLength;
                }

                if (tb == txtStation)
                {
                    UpdateAirportInfo(upper);
                }
                else if (tb == txtCustomerCode)
                {
                    UpdateOperatorInfo(upper);
                }

                TryAutoGenerate();
            }
        }

        private void TxtDate_TextChanged(object? sender, EventArgs e)
        {
            // Auto-format 8 digits as MM/dd/yyyy when no separators are present
            string raw = txtDate.Text.Replace("/", "").Replace("-", "").Trim();
            if (raw.Length == 8 && txtDate.Text.IndexOf('/') == -1 && txtDate.Text.IndexOf('-') == -1)
            {
                string mm = raw.Substring(0, 2);
                string dd = raw.Substring(2, 2);
                string yyyy = raw.Substring(4);
                txtDate.Text = $"{mm}/{dd}/{yyyy}";
                txtDate.SelectionStart = txtDate.Text.Length;
            }

            TryAutoGenerate();
        }

        private void TxtTime_TextChanged(object? sender, EventArgs e)
        {
            // Smart handling: if user types 4 digits like 1400, convert to 14:00
            string raw = txtTime.Text.Replace(":", "").Trim();

            if (raw.Length == 4 && !txtTime.Text.Contains(":"))
            {
                if (int.TryParse(raw, out int _))
                {
                    int hour = int.Parse(raw.Substring(0, 2));
                    int minute = int.Parse(raw.Substring(2, 2));

                    if (hour >= 0 && hour <= 23 && minute >= 0 && minute <= 59)
                    {
                        string formatted = $"{hour:00}:{minute:00}";
                        if (txtTime.Text != formatted)
                        {
                            txtTime.Text = formatted;
                            txtTime.SelectionStart = txtTime.Text.Length;
                        }
                    }
                }
            }

            TryAutoGenerate();
        }

        private void btnCalendar_Click(object sender, EventArgs e)
        {
            monthCalendar.Visible = !monthCalendar.Visible;

            if (monthCalendar.Visible)
            {
                // Position calendar near the date textbox and ensure it stays within the window
                Point desired = new Point(txtDate.Left, txtDate.Bottom + 4);
                int x = desired.X;
                int y = desired.Y;

                if (x + monthCalendar.Width > ClientSize.Width)
                    x = ClientSize.Width - monthCalendar.Width - 5;
                if (y + monthCalendar.Height > ClientSize.Height)
                    y = ClientSize.Height - monthCalendar.Height - 5;
                if (x < 0) x = 0;
                if (y < 0) y = 0;

                monthCalendar.Location = new Point(x, y);
                monthCalendar.BringToFront();

                if (DateTime.TryParseExact(txtDate.Text, "MM/dd/yyyy", CultureInfo.InvariantCulture,
                        DateTimeStyles.None, out DateTime dt))
                {
                    monthCalendar.SetDate(dt);
                }
                else
                {
                    monthCalendar.SetDate(DateTime.Today);
                }
            }
        }

        private void monthCalendar_DateSelected(object sender, DateRangeEventArgs e)
        {
            txtDate.Text = e.Start.ToString("MM/dd/yyyy");
            monthCalendar.Visible = false;
            TryAutoGenerate();
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            // Manual generation: allow unknown operator/station; still prefer ICAO when known
            GenerateUuidFromInputs(autoMode: false);
        }

        #endregion

        #region Info labels

        private void UpdateAirportInfo(string code)
        {
            code = code.Trim();
            if (code.Length == 0)
            {
                lblAirportInfo.Text = "";
                lblAirportSource.Text = "";
                return;
            }

            if (AirportDirectory.TryGetValue(code, out AirportInfo? info))
            {
                lblAirportInfo.Text = info.Name;
                // Developer-only source tag so you can see whether this station
                // came from embedded data, external CSV, or the built-in fallback.
                lblAirportSource.Text = $"[{info.Source}]";
            }
            else
            {
                lblAirportInfo.Text = "UNKNOWN STATION";
                lblAirportSource.Text = "[unmapped]";
            }
        }

        private void UpdateOperatorInfo(string code)
        {
            code = code.Trim();
            if (code.Length == 0)
            {
                lblOperatorInfo.Text = "";
                return;
            }

            if (OperatorDirectory.TryGetValue(code, out OperatorInfo? info))
            {
                lblOperatorInfo.Text = info.Name;
            }
            else
            {
                lblOperatorInfo.Text = "UNKNOWN OPERATOR";
            }
        }


        #endregion

        #region UUID generation logic

        /// <summary>
        /// Auto-generate UUID whenever all fields are valid AND
        /// both operator and station are known (in their directories).
        /// If anything is unknown, auto-mode does nothing and user must press the button.
        /// </summary>
        private void TryAutoGenerate()
        {
            GenerateUuidFromInputs(autoMode: true);
        }

        private void GenerateUuidFromInputs(bool autoMode)
        {
            string customerRaw = txtCustomerCode.Text.Trim().ToUpperInvariant();
            string reg = txtReg.Text.Trim().ToUpperInvariant();
            string stationRaw = txtStation.Text.Trim().ToUpperInvariant();
            string dateText = txtDate.Text.Trim();
            string timeText = txtTime.Text.Trim();

            if (string.IsNullOrEmpty(customerRaw) ||
                string.IsNullOrEmpty(reg) ||
                string.IsNullOrEmpty(stationRaw) ||
                string.IsNullOrEmpty(dateText) ||
                string.IsNullOrEmpty(timeText))
            {
                if (!autoMode)
                {
                    MessageBox.Show("ALL FIELDS ARE REQUIRED.", "MISSING DATA",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                return;
            }

            if (stationRaw.Length > 4)
            {
                if (!autoMode)
                {
                    MessageBox.Show("STATION CODE MUST BE 4 CHARACTERS OR FEWER.", "INVALID STATION",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                return;
            }

            if (!DateTime.TryParseExact(
                    dateText + " " + timeText,
                    new[] { "MM/dd/yyyy HH:mm", "M/d/yyyy HH:mm" },
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out DateTime dt))
            {
                if (!autoMode)
                {
                    MessageBox.Show("DATE/TIME COULD NOT BE PARSED. USE MM/DD/YYYY AND HH:MM.", "INVALID DATE/TIME",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                return;
            }

            bool operatorKnown = OperatorDirectory.TryGetValue(customerRaw, out OperatorInfo? opInfo);
            bool stationKnown = AirportDirectory.TryGetValue(stationRaw, out AirportInfo? apInfo);

            if (autoMode)
            {
                // In auto mode, require BOTH operator and station to be known
                if (!operatorKnown || !stationKnown)
                {
                    return;
                }
            }

            // Canonical ICAO preference: when known, use canonical ICAO code; otherwise fall back to user input.
            string customerCodeForSeed = operatorKnown ? opInfo!.CanonicalIcao : customerRaw;
            string stationCodeForSeed = stationKnown ? apInfo!.CanonicalIcao : stationRaw;

            string iso = dt.ToString("yyyy-MM-dd'T'HH:mm'Z'", CultureInfo.InvariantCulture);

            string seed = $"{customerCodeForSeed}_{reg}_{iso}_{stationCodeForSeed}";
            txtIsoPreview.Text = seed;

            Guid uuid = GenerateUuid5(NamespaceDns, seed);
            txtUuid.Text = uuid.ToString();
        }

        /// <summary>
        /// Computes a UUIDv5 from a namespace GUID and a name string, per RFC 4122:
        /// - Convert namespace GUID to big-endian
        /// - Concatenate namespace bytes + UTF-8(name)
        /// - SHA-1 hash the buffer
        /// - Take first 16 bytes, set version and variant bits
        /// - Convert back to little-endian Guid for .NET
        /// </summary>
        private static Guid GenerateUuid5(Guid namespaceId, string name)
        {
            // 1. Convert namespace GUID to RFC 4122 network byte order (big-endian fields)
            byte[] nsBytes = namespaceId.ToByteArray();
            Swap(nsBytes, 0, 3);
            Swap(nsBytes, 1, 2);
            Swap(nsBytes, 4, 5);
            Swap(nsBytes, 6, 7);

            // 2. Concatenate namespace bytes + name bytes
            byte[] nameBytes = Encoding.UTF8.GetBytes(name);
            byte[] toHash = new byte[nsBytes.Length + nameBytes.Length];
            Buffer.BlockCopy(nsBytes, 0, toHash, 0, nsBytes.Length);
            Buffer.BlockCopy(nameBytes, 0, toHash, nsBytes.Length, nameBytes.Length);

            // 3. SHA-1 hash
            using (var sha1 = SHA1.Create())
            {
                byte[] hash = sha1.ComputeHash(toHash);

                // 4. First 16 bytes of the hash become the GUID payload
                byte[] newGuid = new byte[16];
                Array.Copy(hash, 0, newGuid, 0, 16);

                // 5. Set version (5) and variant (RFC 4122)
                newGuid[6] = (byte)((newGuid[6] & 0x0F) | (5 << 4));
                newGuid[8] = (byte)((newGuid[8] & 0x3F) | 0x80);

                // 6. Convert back to little-endian layout used by System.Guid
                Swap(newGuid, 0, 3);
                Swap(newGuid, 1, 2);
                Swap(newGuid, 4, 5);
                Swap(newGuid, 6, 7);

                return new Guid(newGuid);
            }
        }

        private static void Swap(byte[] bytes, int i, int j)
        {
            byte temp = bytes[i];
            bytes[i] = bytes[j];
            bytes[j] = temp;
        }

        #endregion
    }
}
