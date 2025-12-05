using Denics;
using Denics.Administrator;
using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Denics.Administrator
{
    public partial class SchedulePage : Form
    {
        // Use CallDatabase instance to retrieve the connection string
        private readonly string _connectionString = new CallDatabase().getDatabaseStringName();

        private readonly string[] _dayNames = { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };

        // Eight rows/hours by default (change as needed)
        private readonly TimeSpan[] _hours = {
            TimeSpan.Parse("09:00"), TimeSpan.Parse("10:00"), TimeSpan.Parse("11:00"), TimeSpan.Parse("12:00"),
            TimeSpan.Parse("13:00"), TimeSpan.Parse("14:00"), TimeSpan.Parse("15:00"), TimeSpan.Parse("16:00")
        };

        private const int MinHeaderHeight = 4;

        // combine/highlight mode fields (single declaration)
        private bool _combineMode = false;
        private readonly DataGridViewCellStyle _highlightStyle = new DataGridViewCellStyle { BackColor = Color.Yellow, ForeColor = Color.Black };

        public SchedulePage()
        {
            InitializeComponent();
            // center the page on screen
            this.StartPosition = FormStartPosition.CenterScreen;

            // Navigation button event wiring
            ReportButton.Click += ReportButton_Click;
            HomeButton.Click += HomeButton_Click;
            ServicesButton.Click += ServicesButton_Click;
            AvailabilityButton.Click += AvailabilityButton_Click;
            AppointmentButton.Click += AppointmentButton_Click;
            PatientButton.Click += PatientButton_Click;
            DoctorButton.Click += DoctorButton_Click;

            // Event wiring
            this.Load += SchedulePage_Load;
            btnView.Click += btnView_Click;
            btnSave.Click += btnSave_Click;

            // Ensure single subscription to CellClick
            scheduleGrid.CellClick -= scheduleGrid_CellClick;
            scheduleGrid.CellClick += scheduleGrid_CellClick;

            // subscribe CellPainting so merged drawing runs
            scheduleGrid.CellPainting -= scheduleGrid_CellPainting;
            scheduleGrid.CellPainting += scheduleGrid_CellPainting;

            UpcommingClosedStore_dtgrd.SelectionChanged -= UpcommingClosedStore_dtgrd_SelectionChanged;
            UpcommingClosedStore_dtgrd.SelectionChanged += UpcommingClosedStore_dtgrd_SelectionChanged;
            UpcommingClosedStore_dtgrd.CellClick -= UpcommingClosedStore_dtgrd_CellClick;
            UpcommingClosedStore_dtgrd.CellClick += UpcommingClosedStore_dtgrd_CellClick;

            // enable combine mode so painting runs automatically on view and clicks
            SetCombineMode(true);
        }

        private void SchedulePage_Load(object? sender, EventArgs e)
        {
            try
            {
                weekPickerDateTime.Format = DateTimePickerFormat.Custom;
                weekPickerDateTime.CustomFormat = "MMMM, yyyy";
                ConfigureGrid();
                PopulateEmptyGrid();
                LoadDate();

                // Initialize the small legend showing the three colors
                ConfigureLegends();

                // hide headers and time column visually but keep model intact
                scheduleGrid.ColumnHeadersVisible = false;
                if (scheduleGrid.Columns.Contains("Time"))
                    scheduleGrid.Columns["Time"].Visible = false;

                // defensive: ensure header height is valid
                scheduleGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
                scheduleGrid.ColumnHeadersHeight = Math.Max(MinHeaderHeight, scheduleGrid.ColumnHeadersHeight);

                LoadIntoDoctorsComboBox();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Load error: " + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigureGrid()
        {
            scheduleGrid.AllowUserToAddRows = false;
            scheduleGrid.AutoGenerateColumns = false;
            scheduleGrid.RowHeadersVisible = false;
            scheduleGrid.SelectionMode = DataGridViewSelectionMode.CellSelect;
            scheduleGrid.MultiSelect = false;
            scheduleGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            scheduleGrid.Columns.Clear();

            // Time column (kept for indexing / hour values — hidden later)
            var timeCol = new DataGridViewTextBoxColumn
            {
                Name = "Time",
                HeaderText = "Time",
                DataPropertyName = "Time",
                ReadOnly = true,
                Width = 80,
                Frozen = true,
                SortMode = DataGridViewColumnSortMode.NotSortable,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            };
            scheduleGrid.Columns.Add(timeCol);

            // Day columns
            foreach (var d in _dayNames)
            {
                var col = new DataGridViewTextBoxColumn
                {
                    Name = d,
                    HeaderText = d,
                    DataPropertyName = d,
                    ReadOnly = true, // keep read-only so users cannot type; programmatic updates still allowed
                    SortMode = DataGridViewColumnSortMode.NotSortable,
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                };
                scheduleGrid.Columns.Add(col);
            }

            scheduleGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            scheduleGrid.Columns["Time"].Resizable = DataGridViewTriState.False;
            scheduleGrid.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        // load Date table to datagrid named UpcommingClosedStore_dtgrd
        private void LoadDate()
        {
            try
            {
                using Microsoft.Data.SqlClient.SqlConnection con = new Microsoft.Data.SqlClient.SqlConnection(_connectionString);
                con.Open();

                // Select upcoming dates that are marked "Closed", ordered from nearest to furthest
                string sql = @"
                    SELECT [day], [day_avaialbility]
                    FROM [Date]
                    WHERE [day_avaialbility] = @status
                      AND [day] >= @today
                    ORDER BY [day] ASC";

                using var cmd = new Microsoft.Data.SqlClient.SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@status", "Closed");
                cmd.Parameters.AddWithValue("@today", DateTime.Today);

                using var sda = new Microsoft.Data.SqlClient.SqlDataAdapter(cmd);
                var dt = new DataTable();
                sda.Fill(dt);

                UpcommingClosedStore_dtgrd.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading dates: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PopulateEmptyGrid()
        {
            var dt = new DataTable();
            dt.Columns.Add("Time", typeof(string));
            foreach (var d in _dayNames) dt.Columns.Add(d, typeof(string));

            // Use exactly _hours.Length rows
            foreach (var h in _hours)
            {
                var row = dt.NewRow();
                row["Time"] = h.ToString(@"hh\:mm");
                foreach (var d in _dayNames) row[d] = "";
                dt.Rows.Add(row);
            }

            scheduleGrid.DataSource = dt;

            // style Time column if shown later
            if (scheduleGrid.Columns.Contains("Time"))
            {
                scheduleGrid.Columns["Time"].DefaultCellStyle.BackColor = Color.LightGray;
                scheduleGrid.Columns["Time"].DefaultCellStyle.SelectionBackColor = Color.LightGray;
            }

            InitCellTagsToZero();
            // keep grid read-only for users
            MakeDayColumnsReadOnly();
        }

        private void InitCellTagsToZero()
        {
            if (scheduleGrid.Rows == null) return;
            for (int r = 0; r < scheduleGrid.Rows.Count; r++)
            {
                if (scheduleGrid.Rows[r].IsNewRow) continue;
                for (int c = 0; c < scheduleGrid.Columns.Count; c++)
                    scheduleGrid[c, r].Tag = 0;
            }
        }

        private void MakeDayColumnsReadOnly()
        {
            foreach (DataGridViewColumn col in scheduleGrid.Columns)
                col.ReadOnly = true;
            scheduleGrid.CurrentCell = null;
        }

        private void LoadIntoDoctorsComboBox()
        {
            try
            {
                using (var con = new Microsoft.Data.SqlClient.SqlConnection(_connectionString))
                using (var cmd = new Microsoft.Data.SqlClient.SqlCommand("SELECT doctor_id, full_name FROM Doctors ORDER BY full_name", con))
                {
                    var adapter = new Microsoft.Data.SqlClient.SqlDataAdapter(cmd);
                    var dt = new DataTable();
                    adapter.Fill(dt);
                    comboBoxDoctors.DataSource = dt;
                    comboBoxDoctors.DisplayMember = "full_name";
                    comboBoxDoctors.ValueMember = "doctor_id";
                    comboBoxDoctors.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load doctors: " + ex.ToString(), "DB Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Cell click cycles 0 -> 1 -> 2 -> 3 -> 0
        private void scheduleGrid_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
                var col = scheduleGrid.Columns[e.ColumnIndex];
                if (col == null || string.Equals(col.Name, "Time", StringComparison.OrdinalIgnoreCase)) return;

                var cell = scheduleGrid[e.ColumnIndex, e.RowIndex];
                if (cell == null) return;

                int state = 0;
                if (cell.Tag is int si) state = si;
                else if (cell.Tag is string ss && int.TryParse(ss, out var pi)) state = pi;

                state = (state + 1) % 4;

                // map state -> status string
                string status = state switch
                {
                    1 => "Available",
                    2 => "On Break",
                    3 => "Not Available",
                    _ => ""
                };

                // update cell state and style
                cell.Value = "";
                cell.ToolTipText = status;
                cell.Tag = state;                   // set tag first so restore logic reads correct value
                ApplyCellStyleByStatus(cell, status);

                // repaint column so merged painting updates
                scheduleGrid.InvalidateColumn(e.ColumnIndex);

                // NOTE: do NOT call HighlightMatchingCells here.
                // HighlightMatchingCells clears/restores styles for all cells which
                // caused other cells to jump to different states/colors during clicks.
            }
            catch (Exception ex)
            {
                MessageBox.Show("Cell click error: " + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // update ApplyCellStyleByStatus to set tooltip (defensive) and keep original styling behavior
        private void ApplyCellStyleByStatus(DataGridViewCell cell, string status)
        {
            // always set tooltip so users can hover to read the hidden label
            cell.ToolTipText = status ?? "";

            switch (status)
            {
                case "Available":
                    cell.Style.BackColor = Color.Blue; ; cell.Style.ForeColor = Color.White; break;
                case "On Break":
                    cell.Style.BackColor = Color.LightBlue; cell.Style.ForeColor = Color.White; break;
                case "Not Available":
                    cell.Style.BackColor = Color.Gray; cell.Style.ForeColor = Color.White; break;
                default:
                    cell.Style.BackColor = Color.White; cell.Style.ForeColor = Color.Black; break;
            }
        }

        // btnView: load DB schedule into grid
        private void btnView_Click(object? sender, EventArgs e)
        {
            try
            {
                if (comboBoxDoctors.SelectedValue == null || comboBoxDoctors.SelectedIndex < 0)
                {
                    MessageBox.Show("Please select a doctor first.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (!int.TryParse(comboBoxDoctors.SelectedValue.ToString(), out int doctorId))
                {
                    MessageBox.Show("Invalid doctor selected.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                // compute weekStart / weekEnd first
                DateTime selectedDate = weekPickerDateTime.Value;
                DateTime weekStart = selectedDate.AddDays(-(int)selectedDate.DayOfWeek); // Sunday
                DateTime weekEnd = weekStart.AddDays(6); // Saturday

                // build label with explicit day numbers to avoid culture-specific short date formats
                if (weekStart.Year == weekEnd.Year)
                {
                    if (weekStart.Month == weekEnd.Month)
                    {
                        // same month, same year -> "November 9-15, 2025"
                        labelWeekRange.Text = $"Week of {weekStart:MMMM} {weekStart.Day}-{weekEnd.Day}, {weekEnd:yyyy}";
                    }
                    else
                    {
                        // same year, different months -> "Week of November 30 - December 6, 2025"
                        labelWeekRange.Text = $"Week of {weekStart:MMMM} {weekStart.Day} - {weekEnd:MMMM} {weekEnd.Day}, {weekEnd:yyyy}";
                    }
                }
                else
                {
                    // different years -> "Week of December 28, 2025 - January 3, 2026"
                    labelWeekRange.Text = $"Week of {weekStart:MMMM} {weekStart.Day}, {weekStart:yyyy} - {weekEnd:MMMM} {weekEnd.Day}, {weekEnd:yyyy}";
                }

                var cmdText = "SELECT date, hour_slot, status, is_blocked FROM Availability " +
                              "WHERE doctor_id = @doctorId AND date BETWEEN @start AND @end";

                var scheduleDict = new Dictionary<(string day, TimeSpan hour), (string status, bool isBlocked)>(new DayTimeComparer());

                using (var conn = new Microsoft.Data.SqlClient.SqlConnection(_connectionString))
                using (var cmd = new Microsoft.Data.SqlClient.SqlCommand(cmdText, conn))
                {
                    cmd.Parameters.Add("@doctorId", SqlDbType.Int).Value = doctorId;
                    cmd.Parameters.Add("@start", SqlDbType.Date).Value = weekStart;
                    cmd.Parameters.Add("@end", SqlDbType.Date).Value = weekEnd;

                    conn.Open();
                    using (var rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            DateTime date = rdr.GetDateTime(0);
                            string rawDay = date.DayOfWeek.ToString();

                            TimeSpan hourSlot = TimeSpan.Zero;
                            if (!rdr.IsDBNull(1))
                            {
                                try { hourSlot = rdr.GetTimeSpan(1); }
                                catch { TimeSpan.TryParse(rdr.GetValue(1).ToString(), out hourSlot); }
                            }

                            var status = rdr.IsDBNull(2) ? "" : rdr.GetString(2);
                            var isBlocked = !rdr.IsDBNull(3) && rdr.GetBoolean(3);

                            var dayNormalized = NormalizeDayName(rawDay);
                            if (dayNormalized == null) continue;
                            scheduleDict[(dayNormalized, hourSlot)] = (status, isBlocked);
                        }
                    }
                }

                // Clear grid and fill based on lookup
                var dt = scheduleGrid.DataSource as DataTable;
                if (dt == null) return;

                for (int r = 0; r < dt.Rows.Count; r++)
                {
                    foreach (var day in _dayNames) dt.Rows[r][day] = "";
                }

                for (int r = 0; r < _hours.Length && r < scheduleGrid.Rows.Count; r++)
                {
                    var hour = _hours[r];
                    for (int c = 0; c < _dayNames.Length; c++)
                    {
                        var colIndex = 1 + c;
                        if (scheduleDict.TryGetValue((_dayNames[c], hour), out var entry))
                        {
                            var statusValue = entry.status ?? "";
                            if (entry.isBlocked && string.IsNullOrWhiteSpace(statusValue)) statusValue = "Not Available";

                            var cell = scheduleGrid[colIndex, r];
                            // hide text, but keep status accessible via tooltip and styling
                            cell.Value = "";
                            cell.ToolTipText = statusValue;
                            ApplyCellStyleByStatus(cell, statusValue);
                            cell.Tag = StatusToClickCount(statusValue);
                        }
                        else
                        {
                            var cell = scheduleGrid[colIndex, r];
                            cell.Value = "";
                            cell.ToolTipText = "";
                            cell.Style.BackColor = Color.White;
                            cell.Style.ForeColor = Color.Black;
                            cell.Tag = 0;
                        }
                    }
                }

                // Defensive: keep read-only state but DO NOT wipe the tags we just set
                // MakeDayColumnsReadOnly(); // keep this
                MakeDayColumnsReadOnly();

                // Invalidate the entire grid or specific columns to trigger CellPainting
                SetCombineMode(true);              // turn on automatic merge/painting for the populated data
                scheduleGrid.Invalidate();         // force CellPainting to redraw merged areas
            }
            catch (Exception ex)
            {
                MessageBox.Show("View error: " + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int StatusToClickCount(string status)
        {
            return status switch
            {
                "Available" => 1,
                "On Break" => 2,
                "Not Available" => 3,
                _ => 0,
            };
        }

        // Robust save: uses actual grid rows/columns, skips invalid indexes and new rows
        private void btnSave_Click(object? sender, EventArgs e)
        {
            try
            {
                if (comboBoxDoctors.SelectedValue == null || comboBoxDoctors.SelectedIndex < 0)
                {
                    MessageBox.Show("Please select a doctor first.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (!int.TryParse(comboBoxDoctors.SelectedValue.ToString(), out int doctorId))
                {
                    MessageBox.Show("Invalid doctor selected.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Get selected week start date from week picker
                DateTime selectedDate = weekPickerDateTime.Value;
                DateTime weekStart = selectedDate.AddDays(-(int)selectedDate.DayOfWeek); // Sunday

                // Map day names to actual dates in the selected week
                var dayToDateMap = new Dictionary<string, DateTime>();
                for (int i = 0; i < _dayNames.Length; i++)
                {
                    dayToDateMap[_dayNames[i]] = weekStart.AddDays(i);
                }

                var rowsToSave = new List<(DateTime date, TimeSpan hour, string status, bool isBlocked)>();

                bool hasTimeColumn = scheduleGrid.Columns.Contains("Time");
                int firstDayColIndex = hasTimeColumn ? 1 : 0;

                for (int r = 0; r < scheduleGrid.Rows.Count; r++)
                {
                    var gridRow = scheduleGrid.Rows[r];
                    if (gridRow.IsNewRow) continue;

                    TimeSpan hourSlot = TimeSpan.Zero;
                    bool haveHour = false;

                    if (hasTimeColumn)
                    {
                        var timeCell = scheduleGrid["Time", r];
                        if (timeCell != null && timeCell.Value != null)
                        {
                            var tsStr = timeCell.Value.ToString();
                            if (TimeSpan.TryParse(tsStr, out var parsed) || TimeSpan.TryParseExact(tsStr, new[] { @"hh\:mm", @"hh\:mm\:ss" }, null, out parsed))
                            {
                                hourSlot = parsed;
                                haveHour = true;
                            }
                        }
                    }
                    else
                    {
                        if (r < _hours.Length) { hourSlot = _hours[r]; haveHour = true; }
                    }

                    for (int c = firstDayColIndex; c < scheduleGrid.Columns.Count; c++)
                    {
                        var col = scheduleGrid.Columns[c];
                        if (col == null || string.Equals(col.Name, "Time", StringComparison.OrdinalIgnoreCase)) continue;

                        var cell = scheduleGrid[c, r];
                        if (cell == null) continue;

                        // Prefer tooltip (status stored there), otherwise fallback to Value
                        var status = (cell.ToolTipText ?? cell.Value?.ToString() ?? "").Trim();
                        bool isBlocked = string.Equals(status, "Not Available", StringComparison.OrdinalIgnoreCase);

                        if (string.IsNullOrEmpty(status) && !isBlocked) continue;
                        if (!haveHour) continue;

                        if (!dayToDateMap.TryGetValue(col.Name, out DateTime date)) continue;

                        rowsToSave.Add((date, hourSlot, status, isBlocked));
                    }
                }

                using (var conn = new Microsoft.Data.SqlClient.SqlConnection(_connectionString))
                {
                    conn.Open();
                    using (var tran = conn.BeginTransaction())
                    {
                        try
                        {
                            using (var delCmd = new Microsoft.Data.SqlClient.SqlCommand("DELETE FROM Availability WHERE doctor_id = @doc AND date BETWEEN @start AND @end", conn, tran))
                            {
                                delCmd.Parameters.Add("@doc", SqlDbType.Int).Value = doctorId;
                                delCmd.Parameters.Add("@start", SqlDbType.Date).Value = weekStart;
                                delCmd.Parameters.Add("@end", SqlDbType.Date).Value = weekStart.AddDays(6);
                                delCmd.ExecuteNonQuery();
                            }

                            using (var ins = new Microsoft.Data.SqlClient.SqlCommand(
                                "INSERT INTO Availability (doctor_id, date, hour_slot, status, is_blocked) VALUES (@doc, @date, @hour, @status, @isBlocked)",
                                conn, tran))
                            {
                                ins.Parameters.Add(new Microsoft.Data.SqlClient.SqlParameter("@doc", SqlDbType.Int));
                                ins.Parameters.Add(new Microsoft.Data.SqlClient.SqlParameter("@date", SqlDbType.Date));
                                ins.Parameters.Add(new Microsoft.Data.SqlClient.SqlParameter("@hour", SqlDbType.Time));
                                ins.Parameters.Add(new Microsoft.Data.SqlClient.SqlParameter("@status", SqlDbType.NVarChar, 200));
                                ins.Parameters.Add(new Microsoft.Data.SqlClient.SqlParameter("@isBlocked", SqlDbType.Bit));

                                foreach (var r in rowsToSave)
                                {
                                    ins.Parameters["@doc"].Value = doctorId;
                                    ins.Parameters["@date"].Value = r.date;
                                    ins.Parameters["@hour"].Value = r.hour;
                                    ins.Parameters["@status"].Value = r.status ?? "";
                                    ins.Parameters["@isBlocked"].Value = r.isBlocked ? 1 : 0;
                                    ins.ExecuteNonQuery();
                                }
                            }

                            tran.Commit();
                        }
                        catch
                        {
                            tran.Rollback();
                            throw;
                        }
                    }
                }

                MessageBox.Show("Schedule saved to Availability table successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Save error: " + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private string? NormalizeDayName(string rawDay)
        {
            if (string.IsNullOrWhiteSpace(rawDay)) return null;
            var map = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { "sun", "Sunday" }, { "sunday", "Sunday" },
                { "mon", "Monday" }, { "monday", "Monday" },
                { "tue", "Tuesday" }, { "tues", "Tuesday" }, { "tuesday", "Tuesday" },
                { "wed", "Wednesday" }, { "wednesday", "Wednesday" },
                { "thu", "Thursday" }, { "thur", "Thursday" }, { "thursday", "Thursday" },
                { "fri", "Friday" }, { "friday", "Friday" },
                { "sat", "Saturday" }, { "saturday", "Saturday" }
            };
            return map.TryGetValue(rawDay.Trim(), out var full) ? full : null;
        }

        private void btnClear_Click(object? sender, EventArgs e)
        {
            try
            {
                if (comboBoxDoctors.SelectedValue == null || comboBoxDoctors.SelectedIndex < 0)
                {
                    MessageBox.Show("Please select a doctor first.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (!int.TryParse(comboBoxDoctors.SelectedValue.ToString(), out int doctorId))
                {
                    MessageBox.Show("Invalid doctor selected.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var confirm = MessageBox.Show("Clear this doctor's schedule to default and save the change?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirm != DialogResult.Yes) return;

                var dt = scheduleGrid.DataSource as DataTable;
                if (dt != null)
                {
                    for (int r = 0; r < dt.Rows.Count && r < scheduleGrid.Rows.Count; r++)
                    {
                        foreach (var day in _dayNames) dt.Rows[r][day] = "";
                    }
                }

                for (int r = 0; r < scheduleGrid.Rows.Count; r++)
                {
                    if (scheduleGrid.Rows[r].IsNewRow) continue;
                    for (int c = 0; c < scheduleGrid.Columns.Count; c++)
                    {
                        var col = scheduleGrid.Columns[c];
                        if (col == null) continue;
                        if (string.Equals(col.Name, "Time", StringComparison.OrdinalIgnoreCase)) continue;

                        var cell = scheduleGrid[c, r];
                        if (cell == null) continue;
                        cell.Value = "";
                        cell.Tag = 0;
                        cell.Style.BackColor = Color.White;
                        cell.Style.ForeColor = Color.Black;
                    }
                }

                using (var conn = new Microsoft.Data.SqlClient.SqlConnection(_connectionString))
                {
                    conn.Open();
                    using (var tran = conn.BeginTransaction())
                    {
                        try
                        {
                            using (var delCmd = new Microsoft.Data.SqlClient.SqlCommand("DELETE FROM Availability WHERE doctor_id = @doc", conn, tran))
                            {
                                delCmd.Parameters.Add("@doc", System.Data.SqlDbType.Int).Value = doctorId;
                                delCmd.ExecuteNonQuery();
                            }

                            tran.Commit();
                        }
                        catch
                        {
                            tran.Rollback();
                            throw;
                        }
                    }
                }

                MessageBox.Show("Schedule cleared and saved for the selected doctor.", "Cleared", MessageBoxButtons.OK, MessageBoxIcon.Information);

                InitCellTagsToZero();
                MakeDayColumnsReadOnly();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Clear error: " + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void HomeButton_Click(object? sender, EventArgs e)
        {
            MainAdminPage homeButton = new MainAdminPage();
            homeButton.Show();
            this.Hide();
        }

        private void PatientButton_Click(object? sender, EventArgs e)
        {
            PatientsPage patientsPage = new PatientsPage();
            patientsPage.Show();
            this.Hide();
        }

        private void DoctorButton_Click(object? sender, EventArgs e)
        {
            DoctorPage doctorsPage = new DoctorPage();
            doctorsPage.Show();
            this.Hide();
        }

        private void AvailabilityButton_Click(object? sender, EventArgs e)
        {
            SchedulePage availabilityPage = new SchedulePage();
            availabilityPage.Show();
            this.Hide();
        }

        private void AppointmentButton_Click(object? sender, EventArgs e)
        {
            AppointmentPage appointmentPage = new AppointmentPage();
            appointmentPage.Show();
            this.Hide();
        }

        private void ServicesButton_Click(object? sender, EventArgs e)
        {
            ServicesPage servicesPage = new ServicesPage();
            servicesPage.Show();
            this.Hide();
        }

        private void ReportButton_Click(object? sender, EventArgs e)
        {
            ReportPage reportsPage = new ReportPage();
            reportsPage.Show();
            this.Hide();
        }


        // check DayPicker to add closed dates to Date table
        private void Close_btn_Click(object? sender, EventArgs e)
        {
            try
            {
                // Replace 'DayPicker' with the actual DateTimePicker control name on your form if different.
                DateTime selectedDate = DayPicker?.Value.Date ?? DateTime.Today;

                using (var conn = new Microsoft.Data.SqlClient.SqlConnection(_connectionString))
                using (var cmd = conn.CreateCommand())
                {
                    conn.Open();

                    // Insert if missing, otherwise update the availability to "Closed"
                    cmd.CommandText = @"
                    IF EXISTS (SELECT 1 FROM [Date] WHERE [day] = @day)
                        UPDATE [Date] SET [day_avaialbility] = @status WHERE [day] = @day;
                    ELSE
                    INSERT INTO [Date] ([day], [day_avaialbility]) VALUES (@day, @status);";

                    cmd.Parameters.AddWithValue("@day", selectedDate);
                    cmd.Parameters.AddWithValue("@status", "Closed");

                    cmd.ExecuteNonQuery();
                }

                // Refresh the grid
                LoadDate();

                MessageBox.Show($"Date {selectedDate:yyyy-MM-dd} marked as Closed.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving closed date: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Reopen previously closed existing data from Date table
        private void ReOpen_btn_Click(object? sender, EventArgs e)
        {
            try
            {
                // Collect dates from selected rows (or current row if none selected)
                var dates = new HashSet<DateTime>();
                if (UpcommingClosedStore_dtgrd.SelectedRows.Count > 0)
                {
                    foreach (DataGridViewRow row in UpcommingClosedStore_dtgrd.SelectedRows)
                    {
                        if (row.IsNewRow) continue;
                        var cellVal = row.Cells["day"]?.Value ?? row.Cells[0]?.Value;
                        if (cellVal != null && DateTime.TryParse(cellVal.ToString(), out var d))
                            dates.Add(d.Date);
                    }
                }
                else if (UpcommingClosedStore_dtgrd.CurrentRow != null)
                {
                    var cellVal = UpcommingClosedStore_dtgrd.CurrentRow.Cells["day"]?.Value ?? UpcommingClosedStore_dtgrd.CurrentRow.Cells[0]?.Value;
                    if (cellVal != null && DateTime.TryParse(cellVal.ToString(), out var d))
                        dates.Add(d.Date);
                }

                if (dates.Count == 0)
                {
                    MessageBox.Show("Please select at least one closed date to reopen.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                using (var conn = new Microsoft.Data.SqlClient.SqlConnection(_connectionString))
                {
                    conn.Open();
                    using (var tran = conn.BeginTransaction())
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.Transaction = tran;
                        cmd.CommandText = "UPDATE [Date] SET [day_avaialbility] = @status WHERE [day] = @day";
                        cmd.Parameters.Add(new Microsoft.Data.SqlClient.SqlParameter("@status", System.Data.SqlDbType.NVarChar, 50));
                        cmd.Parameters.Add(new Microsoft.Data.SqlClient.SqlParameter("@day", System.Data.SqlDbType.Date));

                        try
                        {
                            foreach (var d in dates)
                            {
                                cmd.Parameters["@status"].Value = "Open";
                                cmd.Parameters["@day"].Value = d;
                                cmd.ExecuteNonQuery();
                            }

                            tran.Commit();
                        }
                        catch
                        {
                            tran.Rollback();
                            throw;
                        }
                    }
                }

                LoadDate(); // refresh grid
                MessageBox.Show($"Reopened {dates.Count} date(s).", "Reopened", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error reopening dates: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpcommingClosedStore_dtgrd_SelectionChanged(object? sender, EventArgs e)
        {
            // Prefer selected row(s); fall back to CurrentRow
            if (UpcommingClosedStore_dtgrd.SelectedRows.Count > 0)
            {
                SetDayPickerFromRow(UpcommingClosedStore_dtgrd.SelectedRows[0]);
            }
            else if (UpcommingClosedStore_dtgrd.CurrentRow != null)
            {
                SetDayPickerFromRow(UpcommingClosedStore_dtgrd.CurrentRow);
            }
        }

        private void UpcommingClosedStore_dtgrd_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var row = UpcommingClosedStore_dtgrd.Rows[e.RowIndex];
            SetDayPickerFromRow(row);

            if (_combineMode && UpcommingClosedStore_dtgrd.Columns.Contains("day_avaialbility"))
            {
                var statusCell = row.Cells["day_avaialbility"];
                string status = statusCell?.Value?.ToString() ?? "";
                if (!string.IsNullOrEmpty(status))
                    HighlightMatchingCells(status);
            }
        }

        private void SetDayPickerFromRow(DataGridViewRow row)
        {
            if (row == null) return;

            // Try column named "day" first, otherwise use first column
            var cellValue = row.Cells["day"]?.Value ?? row.Cells[0]?.Value;
            if (cellValue == null) return;

            if (DateTime.TryParse(cellValue.ToString(), out var date))
            {
                try
                {
                    DayPicker.Value = date.Date;
                }
                catch (ArgumentOutOfRangeException)
                {
                    // If the date is outside DayPicker range, clamp to Min/Max
                    if (date < DayPicker.MinDate) DayPicker.Value = DayPicker.MinDate;
                    else if (date > DayPicker.MaxDate) DayPicker.Value = DayPicker.MaxDate;
                }
            }
        }

        // helper to set combine mode (call from a toggle button or menu)
        private void SetCombineMode(bool enable)
        {
            _combineMode = enable;
            if (!enable) ClearCombinedHighlights();
        }

        // convert state(int) -> status string (keeps parity with StatusToClickCount)
        private string StatusFromState(int state)
        {
            return state switch
            {
                1 => "Available",
                2 => "On Break",
                3 => "Not Available",
                _ => ""
            };
        }

        private void ClearCombinedHighlights()
        {
            // restore scheduleGrid styles from the numeric state stored in Tag
            for (int r = 0; r < scheduleGrid.Rows.Count; r++)
            {
                if (scheduleGrid.Rows[r].IsNewRow) continue;
                for (int c = 0; c < scheduleGrid.Columns.Count; c++)
                {
                    var cell = scheduleGrid[c, r];
                    if (cell == null) continue;
                    if (cell.Tag is int si)
                        ApplyCellStyleByStatus(cell, StatusFromState(si));
                    else
                        ApplyCellStyleByStatus(cell, "");
                }
            }

            // restore UpcommingClosedStore_dtgrd rows (if they hold a status column)
            if (UpcommingClosedStore_dtgrd.Columns.Contains("day_avaialbility"))
            {
                int idx = UpcommingClosedStore_dtgrd.Columns["day_avaialbility"].Index;
                for (int r = 0; r < UpcommingClosedStore_dtgrd.Rows.Count; r++)
                {
                    var cell = UpcommingClosedStore_dtgrd[idx, r];
                    if (cell == null) continue;
                    cell.Style.BackColor = Color.White;
                    cell.Style.ForeColor = Color.Black;
                }
            }
        }

        // highlight all cells in both grids that match the provided status text
        private void HighlightMatchingCells(string status)
        {
            ClearCombinedHighlights();
            if (string.IsNullOrEmpty(status)) return;

            // scheduleGrid: use ToolTipText (we stored status there) or Value fallback
            for (int r = 0; r < scheduleGrid.Rows.Count; r++)
            {
                if (scheduleGrid.Rows[r].IsNewRow) continue;
                for (int c = 0; c < scheduleGrid.Columns.Count; c++)
                {
                    var cell = scheduleGrid[c, r];
                    if (cell == null) continue;
                    string cellStatus = cell.ToolTipText ?? cell.Value?.ToString() ?? "";
                    if (string.Equals(cellStatus, status, StringComparison.OrdinalIgnoreCase))
                    {
                        // visual highlight while preserving tag/state
                        cell.Style.BackColor = _highlightStyle.BackColor;
                        cell.Style.ForeColor = _highlightStyle.ForeColor;
                    }
                }
            }

            // UpcommingClosedStore_dtgrd: if it has "day_avaialbility" column, match statuses there
            if (UpcommingClosedStore_dtgrd.Columns.Contains("day_avaialbility"))
            {
                int idx = UpcommingClosedStore_dtgrd.Columns["day_avaialbility"].Index;
                for (int r = 0; r < UpcommingClosedStore_dtgrd.Rows.Count; r++)
                {
                    var cell = UpcommingClosedStore_dtgrd[idx, r];
                    if (cell == null) continue;
                    string v = (cell.Value?.ToString() ?? "");
                    if (string.Equals(v, status, StringComparison.OrdinalIgnoreCase))
                    {
                        cell.Style.BackColor = _highlightStyle.BackColor;
                        cell.Style.ForeColor = _highlightStyle.ForeColor;
                    }
                }
            }
        }

        // Add this painting handler to the class (new method)
        private void scheduleGrid_CellPainting(object? sender, DataGridViewCellPaintingEventArgs e)
        {
            // only operate on data cells and non-"Time" columns
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            var col = scheduleGrid.Columns[e.ColumnIndex];
            if (col == null || string.Equals(col.Name, "Time", StringComparison.OrdinalIgnoreCase)) return;

            // safety: ensure Graphics is present
            if (e.Graphics == null) return;

            // get status stored in ToolTipText (or Value fallback)
            string status = scheduleGrid[e.ColumnIndex, e.RowIndex].ToolTipText
                            ?? scheduleGrid[e.ColumnIndex, e.RowIndex].Value?.ToString()
                            ?? "";

            if (string.IsNullOrEmpty(status))
            {
                // default paint for empty cells
                return;
            }

            // find contiguous block of rows with same status for this column
            int start = e.RowIndex;
            while (start > 0)
            {
                string prev = scheduleGrid[e.ColumnIndex, start - 1].ToolTipText
                              ?? scheduleGrid[e.ColumnIndex, start - 1].Value?.ToString()
                              ?? "";
                if (!string.Equals(prev, status, StringComparison.OrdinalIgnoreCase)) break;
                start--;
            }

            // only paint once for the top cell of the block
            if (start != e.RowIndex)
            {
                e.Handled = true; // skip painting for non-top rows in the block
                return;
            }

            int end = e.RowIndex;
            while (end + 1 < scheduleGrid.Rows.Count)
            {
                string next = scheduleGrid[e.ColumnIndex, end + 1].ToolTipText
                              ?? scheduleGrid[e.ColumnIndex, end + 1].Value?.ToString()
                              ?? "";
                if (!string.Equals(next, status, StringComparison.OrdinalIgnoreCase)) break;
                end++;
            }

            // compute union rectangle covering all rows in block 
            Rectangle unionRect = scheduleGrid.GetCellDisplayRectangle(e.ColumnIndex, start, true);
            for (int r = start + 1; r <= end; r++)
            {
                unionRect = Rectangle.Union(unionRect, scheduleGrid.GetCellDisplayRectangle(e.ColumnIndex, r, true));
            }

            // paint background using the style for the top cell
            var topCell = scheduleGrid[e.ColumnIndex, start];
            using (var backBrush = new SolidBrush(topCell.Style.BackColor))
                e.Graphics.FillRectangle(backBrush, unionRect);

            // optional border to show merged area
            using (var pen = new Pen(Color.Black))
                e.Graphics.DrawRectangle(pen, unionRect.X, unionRect.Y, unionRect.Width - 1, unionRect.Height - 1);

            // keep the cell value empty (we already use ToolTipText for the status)
            e.Handled = true;
        }

        private void ConfigureLegends()
        {
            if (Legends == null) return;

            Legends.AllowUserToAddRows = false;
            Legends.AllowUserToResizeColumns = false;
            Legends.AllowUserToResizeRows = false;
            Legends.RowHeadersVisible = false;
            Legends.ColumnHeadersVisible = false;
            Legends.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            Legends.MultiSelect = false;
            Legends.ReadOnly = true;
            Legends.BorderStyle = BorderStyle.None;
            Legends.BackgroundColor = this.BackColor;

            Legends.Columns.Clear();
            var col = new DataGridViewTextBoxColumn
            {
                Name = "ColorBox",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            };
            Legends.Columns.Add(col);

            Legends.Rows.Clear();
            // Add four rows: Blue, LightBlue, Gray, (no color)
            Legends.Rows.Add(4);

            int rowCount = 4;
            int h = (Legends.Height > 0) ? Legends.Height / rowCount : 30;
            for (int i = 0; i < rowCount; i++)
            {
                Legends.Rows[i].Height = h;
                Legends.Rows[i].Cells[0].Value = string.Empty;
                // ensure selection doesn't change appearance
                Legends.Rows[i].Cells[0].Style.SelectionBackColor = Legends.Rows[i].Cells[0].Style.BackColor;
                Legends.Rows[i].Cells[0].Style.SelectionForeColor = Legends.Rows[i].Cells[0].Style.ForeColor;
            }

            // Set the colors: Blue, LightBlue, Gray
            Legends.Rows[0].Cells[0].Style.BackColor = Color.Blue;
            Legends.Rows[0].Cells[0].Style.ForeColor = Color.White;

            Legends.Rows[1].Cells[0].Style.BackColor = Color.LightBlue;
            Legends.Rows[1].Cells[0].Style.ForeColor = Color.White;

            Legends.Rows[2].Cells[0].Style.BackColor = Color.Gray;
            Legends.Rows[2].Cells[0].Style.ForeColor = Color.White;

            // Fourth row: no color — use the legend's background (visually appears "no color")
            Legends.Rows[3].Cells[0].Style.BackColor = Legends.BackgroundColor;
            Legends.Rows[3].Cells[0].Style.ForeColor = Color.Black;

            Legends.ClearSelection();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }


    }
    class DayTimeComparer : IEqualityComparer<(string day, TimeSpan hour)>
    {
        public bool Equals((string day, TimeSpan hour) x, (string day, TimeSpan hour) y)
            => string.Equals(x.day, y.day, StringComparison.OrdinalIgnoreCase) && x.hour == y.hour;
        public int GetHashCode((string day, TimeSpan hour) obj)
            => StringComparer.OrdinalIgnoreCase.GetHashCode(obj.day) ^ obj.hour.GetHashCode();
    }
}

