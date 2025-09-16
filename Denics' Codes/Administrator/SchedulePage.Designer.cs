namespace Denics.Administrator
{
    partial class SchedulePage
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Save = new Button();
            comboBox1 = new ComboBox();
            Status = new ComboBox();
            Day = new ComboBox();
            Time = new ComboBox();
            Display = new Button();
            TableSchedule = new TableLayoutPanel();
            SuspendLayout();
            // 
            // Save
            // 
            Save.Location = new Point(665, 80);
            Save.Name = "Save";
            Save.Size = new Size(94, 29);
            Save.TabIndex = 4;
            Save.Text = "Save";
            Save.UseVisualStyleBackColor = true;
            Save.Click += Save_Click;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(115, 23);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(248, 28);
            comboBox1.TabIndex = 5;
            comboBox1.Text = "Doctors";
            // 
            // Status
            // 
            Status.FormattingEnabled = true;
            Status.Items.AddRange(new object[] { "Available", "Not Available", "Break" });
            Status.Location = new Point(475, 80);
            Status.Name = "Status";
            Status.Size = new Size(151, 28);
            Status.TabIndex = 8;
            Status.Text = "Status";
            // 
            // Day
            // 
            Day.FormattingEnabled = true;
            Day.Items.AddRange(new object[] { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" });
            Day.Location = new Point(115, 80);
            Day.Name = "Day";
            Day.Size = new Size(151, 28);
            Day.TabIndex = 9;
            Day.Text = "Day";
            // 
            // Time
            // 
            Time.FormattingEnabled = true;
            Time.Items.AddRange(new object[] { "09:00", "10:00", "11:00", "12:00", "13:00", "14:00", "15:00", "16:00", "17:00 " });
            Time.Location = new Point(299, 80);
            Time.Name = "Time";
            Time.Size = new Size(151, 28);
            Time.TabIndex = 10;
            Time.Text = "Time";
            // 
            // Display
            // 
            Display.Location = new Point(425, 23);
            Display.Name = "Display";
            Display.Size = new Size(94, 29);
            Display.TabIndex = 11;
            Display.Text = "Display";
            Display.UseVisualStyleBackColor = true;
            Display.Click += button1_Click;
            // 
            // TableSchedule
            // 
            TableSchedule.ColumnCount = 8;
            TableSchedule.ColumnStyles.Add(new ColumnStyle());
            TableSchedule.ColumnStyles.Add(new ColumnStyle());
            TableSchedule.ColumnStyles.Add(new ColumnStyle());
            TableSchedule.ColumnStyles.Add(new ColumnStyle());
            TableSchedule.ColumnStyles.Add(new ColumnStyle());
            TableSchedule.ColumnStyles.Add(new ColumnStyle());
            TableSchedule.ColumnStyles.Add(new ColumnStyle());
            TableSchedule.ColumnStyles.Add(new ColumnStyle());
            TableSchedule.Location = new Point(115, 138);
            TableSchedule.Name = "TableSchedule";
            TableSchedule.RowCount = 10;
            TableSchedule.RowStyles.Add(new RowStyle());
            TableSchedule.RowStyles.Add(new RowStyle());
            TableSchedule.RowStyles.Add(new RowStyle());
            TableSchedule.RowStyles.Add(new RowStyle());
            TableSchedule.RowStyles.Add(new RowStyle());
            TableSchedule.RowStyles.Add(new RowStyle());
            TableSchedule.RowStyles.Add(new RowStyle());
            TableSchedule.RowStyles.Add(new RowStyle());
            TableSchedule.RowStyles.Add(new RowStyle());
            TableSchedule.RowStyles.Add(new RowStyle());
            TableSchedule.Size = new Size(595, 251);
            TableSchedule.TabIndex = 12;
            // 
            // SchedulePage
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(TableSchedule);
            Controls.Add(Display);
            Controls.Add(Time);
            Controls.Add(Day);
            Controls.Add(Status);
            Controls.Add(comboBox1);
            Controls.Add(Save);
            Name = "SchedulePage";
            Text = "SchedulePage";
            Load += SchedulePage_Load;
            ResumeLayout(false);
        }

        #endregion
        private Button Save;
        private ComboBox comboBox1;
        private ComboBox Status;
        private ComboBox Day;
        private ComboBox Time;
        private Button Display;
        private TableLayoutPanel TableSchedule;
    }
}