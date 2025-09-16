namespace Denics.Administrator
{
    partial class ServicesPage
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
            Servicestb = new DataGridView();
            Addbtn = new Button();
            editbtn = new Button();
            txtServiceName = new TextBox();
            txtServiceID = new TextBox();
            removebtn = new Button();
            ((System.ComponentModel.ISupportInitialize)Servicestb).BeginInit();
            SuspendLayout();
            // 
            // Servicestb
            // 
            Servicestb.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            Servicestb.Location = new Point(640, 133);
            Servicestb.Margin = new Padding(3, 2, 3, 2);
            Servicestb.Name = "Servicestb";
            Servicestb.RowHeadersWidth = 51;
            Servicestb.Size = new Size(293, 369);
            Servicestb.TabIndex = 0;
            // 
            // Addbtn
            // 
            Addbtn.Location = new Point(291, 264);
            Addbtn.Margin = new Padding(3, 2, 3, 2);
            Addbtn.Name = "Addbtn";
            Addbtn.Size = new Size(82, 22);
            Addbtn.TabIndex = 1;
            Addbtn.Text = "Add";
            Addbtn.UseVisualStyleBackColor = true;
            Addbtn.Click += Addbtn_Click;
            // 
            // editbtn
            // 
            editbtn.Location = new Point(467, 264);
            editbtn.Margin = new Padding(3, 2, 3, 2);
            editbtn.Name = "editbtn";
            editbtn.Size = new Size(82, 22);
            editbtn.TabIndex = 3;
            editbtn.Text = "Edit";
            editbtn.UseVisualStyleBackColor = true;
            editbtn.Click += editbtn_Click;
            // 
            // txtServiceName
            // 
            txtServiceName.Location = new Point(349, 173);
            txtServiceName.Margin = new Padding(3, 2, 3, 2);
            txtServiceName.Name = "txtServiceName";
            txtServiceName.Size = new Size(200, 23);
            txtServiceName.TabIndex = 5;
            // 
            // txtServiceID
            // 
            txtServiceID.Location = new Point(349, 133);
            txtServiceID.Margin = new Padding(3, 2, 3, 2);
            txtServiceID.Name = "txtServiceID";
            txtServiceID.Size = new Size(56, 23);
            txtServiceID.TabIndex = 6;
            // 
            // removebtn
            // 
            removebtn.Location = new Point(379, 264);
            removebtn.Margin = new Padding(3, 2, 3, 2);
            removebtn.Name = "removebtn";
            removebtn.Size = new Size(82, 22);
            removebtn.TabIndex = 7;
            removebtn.Text = "Remove";
            removebtn.UseVisualStyleBackColor = true;
            removebtn.Click += removebtn_Click_1;
            // 
            // ServicesPage
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(984, 661);
            Controls.Add(removebtn);
            Controls.Add(txtServiceID);
            Controls.Add(txtServiceName);
            Controls.Add(editbtn);
            Controls.Add(Addbtn);
            Controls.Add(Servicestb);
            Name = "ServicesPage";
            Text = "ServicesPage";
            Load += ServicesPage_Load;
            ((System.ComponentModel.ISupportInitialize)Servicestb).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView Servicestb;
        private Button Addbtn;
        private Button editbtn;
        private TextBox txtServiceName;
        private TextBox txtServiceID;
        private Button removebtn;
    }
}