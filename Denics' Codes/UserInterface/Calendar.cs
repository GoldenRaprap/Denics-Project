using Denics.Administrator;
using Denics;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Globalization;

namespace Denics.UserInterface
{
    public partial class Calendar : Form
    {
        private readonly CallDatabase _db = new CallDatabase();
        private readonly SqlConnection _con;
        private DateTime _selectedDate = DateTime.MinValue;
        private int _selectedServiceId = -1;

        // display time grid range (adjust if needed)
        private static readonly TimeSpan GridStart = TimeSpan.FromHours(8);
        private static readonly TimeSpan GridEnd = TimeSpan.FromHours(17);
        private static readonly TimeSpan GridStep = TimeSpan.FromHours(1);

        public Calendar()
        {
            InitializeComponent();
            _con = new SqlConnection(_db.getDatabaseStringName());

            // wire events
            Load += Calendar_Load;
            MonthPicker.ValueChanged += MonthPicker_ValueChanged;
            ServiceType_cmbbx.SelectedIndexChanged += ServiceType_cmbbx_SelectedIndexChanged;
            DoctorAvailable_list.ItemSelectionChanged += DoctorAvailable_list_ItemSelectionChanged;

            // make ListView show columns
            DoctorAvailable_list.View = View.Details;
            DoctorAvailable_list.FullRowSelect = true;
            DoctorAvailable_list.Columns.Clear();
            DoctorAvailable_list.Columns.Add("Doctor", -2, HorizontalAlignment.Left);


            // Sidebar click functions
            HomeButton.Click += HomeButton_Click;
            PatientButton.Click += PatientButton_Click;
            DoctorButton.Click += DoctorButton_Click;
            AvailabilityButton.Click += AvailabilityButton_Click;
            AppointmentButton.Click += AppointmentButton_Click;
            ServicesButton.Click += ServicesButton_Click;
        }

        private void Calendar_Load(object sender, EventArgs e)
        {
            // default month = today
            MonthPicker.Value = DateTime.Today;
            LoadAvailableServices();
            RenderCalendar(MonthPicker.Value);
        }
        private void HomeButton_Click(object sender, EventArgs e)
        {
            HomePage homePage = new HomePage();
            homePage.Show();
            this.Hide();
        }

        private void PatientButton_Click(object sender, EventArgs e)
        {
            UserProfile userProfile = new UserProfile();
            userProfile.Show();
            this.Hide();
        }

        private void DoctorButton_Click(object sender, EventArgs e)
        {
            Doctors doctors = new Doctors();
            doctors.Show();
            this.Hide();
        }

        private void AvailabilityButton_Click(object sender, EventArgs e)
        {
            Calendar calendar = new Calendar();
            calendar.Show();
            this.Hide();
        }

        private void AppointmentButton_Click(object sender, EventArgs e)
        {
            UserBookingPage userBookingPage = new UserBookingPage();
            userBookingPage.Show();
            this.Hide();
        }

        private void ServicesButton_Click(object sender, EventArgs e)
        {
            AvailableServices availableServices = new AvailableServices();
            availableServices.Show();
            this.Hide();
        }



        private void MonthPicker_ValueChanged(object sender, EventArgs e)
        {
            RenderCalendar(MonthPicker.Value);
        }

        private void ServiceType_cmbbx_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ServiceType_cmbbx.SelectedItem is ComboBoxItem item)
            {
                _selectedServiceId = item.Id;
                // allow picking a date when service is selected
                RenderCalendar(MonthPicker.Value);
            }
            else
            {
                _selectedServiceId = -1;
            }

            // clear prior selections
            _selectedDate = DateTime.MinValue;
            DoctorAvailable_list.Items.Clear();
            ClearTimeAvailability();
        }

        private void LoadAvailableServices()
        {
            ServiceType_cmbbx.Items.Clear();

            const string sql = @"
                SELECT service_id, service_name
                FROM Services
                WHERE status IS NOT NULL AND LOWER(status) IN ('available','true','1')
                   OR status = 'Available'"; // tolerant check

            try
            {
                if (_con.State != ConnectionState.Open) _con.Open();

                using var cmd = new SqlCommand(sql, _con);
                using var r = cmd.ExecuteReader();
                while (r.Read())
                {
                    var id = Convert.ToInt32(r["service_id"]);
                    var name = r["service_name"].ToString();
                    ServiceType_cmbbx.Items.Add(new ComboBoxItem(id, name));
                }

                if (ServiceType_cmbbx.Items.Count > 0)
                    ServiceType_cmbbx.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading services: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (_con.State == ConnectionState.Open) _con.Close();
            }
        }

        private void RenderCalendar(DateTime month)
        {
            // ensure container exists
            if (CalendarView == null) return;

            CalendarView.Controls.Clear();

            // prepare sizing based on the CalendarView client area so the generated calendar fits and does not overlap
            var containerWidth = Math.Max(240, CalendarView.ClientSize.Width - 8);
            var containerHeight = Math.Max(160, CalendarView.ClientSize.Height - 8);

            // compute first day cell: month first day
            var firstOfMonth = new DateTime(month.Year, month.Month, 1);
            // convert DayOfWeek to Sunday-based index (Sunday=0)
            int firstColumn = (int)firstOfMonth.DayOfWeek;

            int daysInMonth = DateTime.DaysInMonth(month.Year, month.Month);

            // total cells excluding header
            int totalCells = firstColumn + daysInMonth;
            int weeksNeeded = (int)Math.Ceiling(totalCells / 7.0);
            weeksNeeded = Math.Max(1, weeksNeeded); // at least one week row

            int headerHeight = 28;
            // compute available height for week rows and ensure a reasonable minimum per row
            int remainingHeight = Math.Max(weeksNeeded * 24, containerHeight - headerHeight);
            int rowHeight = Math.Max(24, remainingHeight / weeksNeeded);

            // create table that will be sized to fit the CalendarView client area
            var calendarTbl = new TableLayoutPanel
            {
                ColumnCount = 7,
                RowCount = 1 + weeksNeeded, // header + weeks
                AutoSize = false,
                Size = new Size(containerWidth, headerHeight + rowHeight * weeksNeeded),
                BackColor = Color.Transparent,
                CellBorderStyle = TableLayoutPanelCellBorderStyle.Single,
                Margin = new Padding(0),
                Location = new Point(0, 0)
            };

            calendarTbl.ColumnStyles.Clear();
            for (int c = 0; c < 7; c++)
                calendarTbl.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F / 7F));

            // header row style
            calendarTbl.RowStyles.Clear();
            calendarTbl.RowStyles.Add(new RowStyle(SizeType.Absolute, headerHeight));
            for (int r = 0; r < weeksNeeded; r++)
                calendarTbl.RowStyles.Add(new RowStyle(SizeType.Absolute, rowHeight));

            // header row labels: Sunday..Saturday (start with Sunday)
            var dayNames = new[] { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };
            for (int c = 0; c < 7; c++)
            {
                var lbl = new Label
                {
                    Text = dayNames[c],
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Font = new Font("Segoe UI", 8F, FontStyle.Bold),
                    BackColor = Color.LightSteelBlue,
                    AutoSize = false
                };
                calendarTbl.Controls.Add(lbl, c, 0);
            }

            // We'll query closed weekdays/dates once for this render
            var (closedWeekdays, closedDates) = GetClosedWeekdays();

            int currentDay = 1;
            for (int week = 0; week < weeksNeeded; week++)
            {
                for (int dow = 0; dow < 7; dow++)
                {
                    int row = week + 1; // row 0 is header
                    var cellPanel = new Panel
                    {
                        Dock = DockStyle.Fill,
                        Margin = new Padding(1),
                        BorderStyle = BorderStyle.None,
                        Tag = null // will hold date if in month
                    };

                    if (week == 0 && dow < firstColumn)
                    {
                        // empty before first day
                        cellPanel.BackColor = Color.FromArgb(245, 245, 245);
                    }
                    else if (currentDay > daysInMonth)
                    {
                        // empty after month end
                        cellPanel.BackColor = Color.FromArgb(245, 245, 245);
                    }
                    else
                    {
                        var thisDate = new DateTime(month.Year, month.Month, currentDay);
                        cellPanel.Tag = thisDate;

                        var lblDate = new Label
                        {
                            Text = currentDay.ToString(),
                            Dock = DockStyle.Top,
                            Height = 18,
                            TextAlign = ContentAlignment.TopRight,
                            Font = new Font("Segoe UI", 9F, FontStyle.Regular),
                            BackColor = Color.Transparent,
                            AutoSize = false
                        };

                        // determine if this date (or its weekday) is closed via Date table
                        bool isClosed = IsWeekdayClosed(thisDate, closedWeekdays, closedDates);

                        // treat any past date (before today) as closed / not available
                        if (thisDate.Date < DateTime.Today)
                        {
                            isClosed = true;
                        }

                        // highlight today: ensure today's cell is darker green
                        if (thisDate.Date == DateTime.Today)
                        {
                            cellPanel.BackColor = Color.FromArgb(170, 250, 170);
                        }
                        else
                        {
                            // previously white cells are now a light green for better visibility
                            cellPanel.BackColor = Color.FromArgb(200, 250, 200);
                        }

                        // override if closed
                        if (isClosed)
                        {
                            cellPanel.BackColor = Color.FromArgb(255, 200, 200); // red-ish
                        }

                        // clickable only if not closed and a service selected
                        var btn = new Button
                        {
                            Text = "Select",
                            Dock = DockStyle.Bottom,
                            Height = 22,
                            Enabled = !isClosed && _selectedServiceId > 0,
                            Tag = thisDate
                        };
                        btn.Click += DayButton_Click;

                        // show small availability hint (optional): count of available doctors for the day & selected service
                        var hint = new Label
                        {
                            Text = string.Empty,
                            Dock = DockStyle.Fill,
                            TextAlign = ContentAlignment.MiddleCenter,
                            Font = new Font("Segoe UI", 8F),
                            AutoSize = false
                        };

                        // add controls (order so date label is on top, hint center, button bottom)
                        cellPanel.Controls.Add(hint);
                        cellPanel.Controls.Add(btn);
                        cellPanel.Controls.Add(lblDate);

                        // cache hint by requesting count (synchronous). If DB is slow consider async.
                        if (!isClosed && _selectedServiceId > 0)
                        {
                            int availDoctors = CountAvailableDoctorsForDate(thisDate, _selectedServiceId);
                            hint.Text = availDoctors > 0 ? $"{availDoctors} doctor(s)" : "No doctors";

                            // If no doctors available for this service/date treat as closed/unavailable
                            if (availDoctors == 0)
                            {
                                cellPanel.BackColor = Color.FromArgb(255, 200, 200); // red-ish
                                btn.Enabled = false;
                            }
                        }
                        else if (isClosed)
                        {
                            hint.Text = "Closed";
                        }
                        else
                        {
                            hint.Text = "Choose service";
                        }

                        currentDay++;
                    }

                    calendarTbl.Controls.Add(cellPanel, dow, row);
                }
            }

            // ensure calendar table sits inside the CalendarView and does not exceed its bounds
            calendarTbl.MaximumSize = new Size(CalendarView.ClientSize.Width, CalendarView.ClientSize.Height);
            calendarTbl.MinimumSize = new Size(Math.Min(240, CalendarView.ClientSize.Width), headerHeight + rowHeight);
            calendarTbl.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            CalendarView.Controls.Add(calendarTbl);
            CalendarView.PerformLayout();
        }

        private void DayButton_Click(object? sender, EventArgs e)
        {
            if (sender is Button btn && btn.Tag is DateTime dt)
            {
                _selectedDate = dt.Date;
                LoadDoctorsForSelectedDate();
                ClearTimeAvailability();
            }
        }

        private void LoadDoctorsForSelectedDate()
        {
            DoctorAvailable_list.Items.Clear();

            if (_selectedServiceId <= 0)
            {
                MessageBox.Show("Please select a service first.", "Service Required", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            const string sql = @"
                SELECT DISTINCT d.doctor_id, d.full_name
                FROM Doctors d
                INNER JOIN DoctorServices ds ON d.doctor_id = ds.doctor_id
                INNER JOIN Services s ON ds.service_id = s.service_id
                WHERE s.service_id = @serviceId
                  AND d.is_active = 1
                  AND EXISTS (
                      SELECT 1 FROM Availability a
                      WHERE a.doctor_id = d.doctor_id
                        AND a.[date] = @date
                        AND (a.status IS NULL OR LOWER(a.status) IN ('available','true','1'))
                        AND (a.is_blocked = 0 OR a.is_blocked IS NULL)
                  )";

            try
            {
                if (_con.State != ConnectionState.Open) _con.Open();

                using var cmd = new SqlCommand(sql, _con);
                cmd.Parameters.AddWithValue("@serviceId", _selectedServiceId);
                cmd.Parameters.AddWithValue("@date", _selectedDate.Date);

                using var r = cmd.ExecuteReader();
                while (r.Read())
                {
                    var id = Convert.ToInt32(r["doctor_id"]);
                    var name = r["full_name"].ToString();
                    var listItem = new ListViewItem(name) { Tag = id };
                    DoctorAvailable_list.Items.Add(listItem);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading available doctors: " + ex.Message, "DB Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (_con.State == ConnectionState.Open) _con.Close();
            }
        }

        private void DoctorAvailable_list_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (e.IsSelected && e.Item != null && e.Item.Tag is int doctorId)
            {
                RenderTimeAvailability(doctorId, _selectedDate);
            }
            else
            {
                ClearTimeAvailability();
            }
        }

        private void RenderTimeAvailability(int doctorId, DateTime date)
        {
            ClearTimeAvailability();

            // gather availability entries for the doctor & date
            const string availSql = @"
                SELECT hour_slot, is_blocked, status
                FROM Availability
                WHERE doctor_id = @doctorId AND [date] = @date
                ORDER BY hour_slot";

            // gather appointments
            const string apptSql = @"
                SELECT appointment_time
                FROM Appointments
                WHERE doctor_id = @doctorId AND appointment_date = @date
                  AND status IN ('pending','confirmed','reschedule')";

            var availableSlots = new Dictionary<TimeSpan, (bool isBlocked, string status)>();
            var bookedSlots = new HashSet<TimeSpan>();

            try
            {
                if (_con.State != ConnectionState.Open) _con.Open();

                using (var cmd = new SqlCommand(availSql, _con))
                {
                    cmd.Parameters.AddWithValue("@doctorId", doctorId);
                    cmd.Parameters.AddWithValue("@date", date);
                    using var r = cmd.ExecuteReader();
                    while (r.Read())
                    {
                        var hour = ParseTimeSpanFromDbValue(r["hour_slot"]);
                        var isBlocked = !r.IsDBNull(r.GetOrdinal("is_blocked")) && Convert.ToBoolean(r["is_blocked"]);
                        var status = r.IsDBNull(r.GetOrdinal("status")) ? string.Empty : r["status"].ToString();
                        availableSlots[hour] = (isBlocked, status ?? string.Empty);
                    }
                }

                using (var cmd = new SqlCommand(apptSql, _con))
                {
                    cmd.Parameters.AddWithValue("@doctorId", doctorId);
                    cmd.Parameters.AddWithValue("@date", date);
                    using var r = cmd.ExecuteReader();
                    while (r.Read())
                    {
                        var apptTime = ParseTimeSpanFromDbValue(r["appointment_time"]);
                        bookedSlots.Add(apptTime);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading time availability: " + ex.Message, "DB Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            finally
            {
                if (_con.State == ConnectionState.Open) _con.Close();
            }

            // build time range
            var times = new List<TimeSpan>();
            for (var t = GridStart; t <= GridEnd; t = t.Add(GridStep))
                times.Add(t);

            // prepare TimeAvailability: left column = time, right column = status
            TimeAvailability.Controls.Clear();
            TimeAvailability.ColumnCount = 2;
            TimeAvailability.RowCount = times.Count;
            TimeAvailability.ColumnStyles.Clear();
            TimeAvailability.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            TimeAvailability.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
            TimeAvailability.RowStyles.Clear();

            for (int i = 0; i < times.Count; i++)
            {
                TimeAvailability.RowStyles.Add(new RowStyle(SizeType.Absolute, 28F));
                var timeLabel = new Label
                {
                    Text = DateTime.Today.Add(times[i]).ToString("h:mm tt"),
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleLeft,
                    Padding = new Padding(6, 0, 0, 0),
                    Font = new Font("Segoe UI", 9F)
                };

                string statusText = "Not Available";
                Color bg = Color.FromArgb(240, 240, 240);

                var key = times[i];
                if (availableSlots.TryGetValue(key, out var meta) && !meta.isBlocked &&
                    (string.IsNullOrWhiteSpace(meta.status) || meta.status.Equals("Available", StringComparison.OrdinalIgnoreCase)))
                {
                    // available but check appointments
                    if (bookedSlots.Contains(key))
                    {
                        statusText = "Booked";
                        bg = Color.FromArgb(255, 210, 210);
                    }
                    else
                    {
                        statusText = "Available";
                        bg = Color.FromArgb(207, 226, 243);
                    }
                }
                else if (availableSlots.TryGetValue(key, out var meta2) && (meta2.isBlocked || meta2.status.Equals("Blocked", StringComparison.OrdinalIgnoreCase)))
                {
                    statusText = "Blocked";
                    bg = Color.FromArgb(220, 220, 220);
                }
                else
                {
                    statusText = "Unavailable";
                    bg = Color.FromArgb(245, 245, 245);
                }

                var statusLabel = new Label
                {
                    Text = statusText,
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleCenter,
                    BackColor = bg,
                    Font = new Font("Segoe UI", 9F)
                };

                // if slot is available, make it clickable so user can store selection to AppointmentCart
                if (statusText.Equals("Available", StringComparison.OrdinalIgnoreCase))
                {
                    var slot = times[i]; // capture for closure
                    statusLabel.Cursor = Cursors.Hand;
                    statusLabel.Tag = slot;
                    statusLabel.Click += (s, e) =>
                    {
                        try
                        {
                            OnTimeSlotSelected(doctorId, date, slot);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Failed to select time slot: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    };
                }

                TimeAvailability.Controls.Add(timeLabel, 0, i);
                TimeAvailability.Controls.Add(statusLabel, 1, i);
            }

            TimeAvailability.PerformLayout();
        }

        private void ClearTimeAvailability()
        {
            TimeAvailability.Controls.Clear();
            TimeAvailability.RowStyles.Clear();
            TimeAvailability.RowCount = 0;
        }

        // When user clicks a time slot that is available, capture selection into the AppointmentCart.
        // This lets the UserBookingPage auto-load those values for fast booking.
        private void OnTimeSlotSelected(int doctorId, DateTime date, TimeSpan time)
        {
            // get selected service name
            string serviceName = (ServiceType_cmbbx.SelectedItem as ComboBoxItem)?.Text ?? string.Empty;

            // find doctor name (selected item preferred, otherwise search list)
            string doctorName = string.Empty;
            if (DoctorAvailable_list.SelectedItems.Count > 0)
            {
                doctorName = DoctorAvailable_list.SelectedItems[0].Text;
            }
            else
            {
                foreach (ListViewItem it in DoctorAvailable_list.Items)
                {
                    if (it.Tag is int id && id == doctorId)
                    {
                        doctorName = it.Text;
                        break;
                    }
                }
            }

            if (string.IsNullOrWhiteSpace(serviceName))
            {
                MessageBox.Show("Please select a service first.", "Service Required", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrWhiteSpace(doctorName))
            {
                MessageBox.Show("Please select a doctor first.", "Doctor Required", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // store to AppointmentCart (use invariant date/time formats)
            AppointmentCart.SetServiceType(serviceName);
            AppointmentCart.SetDoctor(doctorName);
            AppointmentCart.SetDate(date.ToString("yyyy-MM-dd"));
            AppointmentCart.SetTime(time.ToString(@"hh\:mm"));

            MessageBox.Show($"Selection saved: {serviceName} with {doctorName} on {date:yyyy-MM-dd} at {time:hh\\:mm}\nOpen Booking page to continue.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // helper: returns set of weekday names and explicit dates that are closed based on Date table
        // supports rows where [day] is a weekday name (e.g. "Monday") OR a concrete date string (e.g. "27/1/2025")
        private (HashSet<string> closedWeekdays, HashSet<DateTime> closedDates) GetClosedWeekdays()
        {
            var closedWeekdays = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            var closedDates = new HashSet<DateTime>();
            const string sql = "SELECT [day], day_avaialbility FROM [Date]";

            try
            {
                if (_con.State != ConnectionState.Open) _con.Open();
                using var cmd = new SqlCommand(sql, _con);
                using var r = cmd.ExecuteReader();
                while (r.Read())
                {
                    var dayObj = r["day"];
                    var dayStr = dayObj is DBNull ? string.Empty : dayObj.ToString()?.Trim() ?? string.Empty;
                    var avail = r["day_avaialbility"] is DBNull ? string.Empty : r["day_avaialbility"].ToString();

                    if (string.IsNullOrWhiteSpace(dayStr)) continue;
                    if (!string.Equals(avail, "Closed", StringComparison.OrdinalIgnoreCase)) continue;

                    // Try parse explicit date first (support common formats)
                    if (TryParseDateFlexible(dayStr, out var parsedDate))
                    {
                        closedDates.Add(parsedDate.Date);
                    }
                    else
                    {
                        // treat as weekday name (e.g. "Monday"). Store the name so we can match DayOfWeek.ToString().
                        closedWeekdays.Add(dayStr);
                    }
                }
            }
            catch
            {
                // ignore errors here; safer to leave no closures if DB fails
            }
            finally
            {
                if (_con.State == ConnectionState.Open) _con.Close();
            }

            return (closedWeekdays, closedDates);
        }

        // parse with flexible fallbacks (TryParse, then several common exact formats)
        private static bool TryParseDateFlexible(string input, out DateTime result)
        {
            result = default;
            if (string.IsNullOrWhiteSpace(input)) return false;

            // try default parse with current culture
            if (DateTime.TryParse(input, out result)) return true;

            // try invariant
            if (DateTime.TryParse(input, CultureInfo.InvariantCulture, DateTimeStyles.None, out result)) return true;

            // try common explicit formats
            var formats = new[] { "d/M/yyyy", "dd/MM/yyyy", "M/d/yyyy", "MM/dd/yyyy", "yyyy-M-d", "yyyy-MM-dd" };
            if (DateTime.TryParseExact(input, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out result)) return true;

            return false;
        }

        private bool IsWeekdayClosed(DateTime date, HashSet<string> closedWeekdays, HashSet<DateTime> closedDates)
        {
            // first check explicit closed dates
            if (closedDates.Contains(date.Date)) return true;

            // then check recurring weekday closures (if table contains weekday names)
            var name = date.DayOfWeek.ToString();
            return closedWeekdays.Contains(name);
        }

        private int CountAvailableDoctorsForDate(DateTime date, int serviceId)
        {
            const string sql = @"
                SELECT COUNT(DISTINCT d.doctor_id) AS cnt
                FROM Doctors d
                INNER JOIN DoctorServices ds ON d.doctor_id = ds.doctor_id
                INNER JOIN Services s ON ds.service_id = s.service_id
                INNER JOIN Availability a ON a.doctor_id = d.doctor_id
                WHERE s.service_id = @serviceId
                  AND a.[date] = @date
                  AND (a.status IS NULL OR LOWER(a.status) IN ('available','true','1'))
                  AND (a.is_blocked = 0 OR a.is_blocked IS NULL)
                  AND d.is_active = 1";

            try
            {
                if (_con.State != ConnectionState.Open) _con.Open();
                using var cmd = new SqlCommand(sql, _con);
                cmd.Parameters.AddWithValue("@serviceId", serviceId);
                cmd.Parameters.AddWithValue("@date", date.Date);
                var res = cmd.ExecuteScalar();
                return res == null || res == DBNull.Value ? 0 : Convert.ToInt32(res);
            }
            catch
            {
                return 0;
            }
            finally
            {
                if (_con.State == ConnectionState.Open) _con.Close();
            }
        }

        private static TimeSpan ParseTimeSpanFromDbValue(object dbVal)
        {
            if (dbVal == null || dbVal == DBNull.Value) return TimeSpan.Zero;
            if (dbVal is TimeSpan ts) return ts;
            if (dbVal is string s)
            {
                if (TimeSpan.TryParse(s, out var parsedTs)) return parsedTs;
                if (DateTime.TryParse(s, out var dt)) return dt.TimeOfDay;
            }
            if (dbVal is DateTime dtVal) return dtVal.TimeOfDay;
            return TimeSpan.Zero;
        }

        // small helper type to store ID+Text inside ComboBox
        private sealed class ComboBoxItem
        {
            public int Id { get; }
            public string Text { get; }
            public ComboBoxItem(int id, string text) { Id = id; Text = text; }
            public override string ToString() => Text;
        }

        private void bookbtn_Click(object sender, EventArgs e)
        {
            UserBookingPage book = new UserBookingPage();
            book.Show();
            this.Hide();
        }
    }
}
