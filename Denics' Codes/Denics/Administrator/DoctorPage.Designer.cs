namespace Denics.Administrator
{
    partial class DoctorPage
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
            dataGridView1 = new DataGridView();
            label1 = new Label();
            Save = new Button();
            Edit = new Button();
            Delete = new Button();
            txtfname = new TextBox();
            txtemail = new TextBox();
            txtpnum = new TextBox();
            txtDocid = new TextBox();
            MSchedule = new Button();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(421, 61);
            dataGridView1.Margin = new Padding(3, 4, 3, 4);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(481, 220);
            dataGridView1.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(39, 21);
            label1.Name = "label1";
            label1.Size = new Size(156, 20);
            label1.TabIndex = 1;
            label1.Text = "Calling table : Doctors";
            // 
            // Save
            // 
            Save.Location = new Point(12, 309);
            Save.Name = "Save";
            Save.Size = new Size(94, 29);
            Save.TabIndex = 2;
            Save.Text = "Save";
            Save.UseVisualStyleBackColor = true;
            Save.Click += Save_Click;
            // 
            // Edit
            // 
            Edit.Location = new Point(112, 309);
            Edit.Name = "Edit";
            Edit.Size = new Size(94, 29);
            Edit.TabIndex = 3;
            Edit.Text = "Edit";
            Edit.UseVisualStyleBackColor = true;
            Edit.Click += Edit_Click;
            // 
            // Delete
            // 
            Delete.Location = new Point(212, 309);
            Delete.Name = "Delete";
            Delete.Size = new Size(94, 29);
            Delete.TabIndex = 4;
            Delete.Text = "Delete";
            Delete.UseVisualStyleBackColor = true;
            Delete.Click += Delete_Click_1;
            // 
            // txtfname
            // 
            txtfname.Location = new Point(127, 105);
            txtfname.Name = "txtfname";
            txtfname.Size = new Size(276, 27);
            txtfname.TabIndex = 6;
            // 
            // txtemail
            // 
            txtemail.Location = new Point(127, 155);
            txtemail.Name = "txtemail";
            txtemail.Size = new Size(276, 27);
            txtemail.TabIndex = 7;
            // 
            // txtpnum
            // 
            txtpnum.Location = new Point(127, 211);
            txtpnum.Name = "txtpnum";
            txtpnum.Size = new Size(276, 27);
            txtpnum.TabIndex = 8;
            // 
            // txtDocid
            // 
            txtDocid.Location = new Point(165, 61);
            txtDocid.Name = "txtDocid";
            txtDocid.Size = new Size(41, 27);
            txtDocid.TabIndex = 9;
            // 
            // MSchedule
            // 
            MSchedule.Location = new Point(666, 356);
            MSchedule.Name = "MSchedule";
            MSchedule.Size = new Size(140, 29);
            MSchedule.TabIndex = 10;
            MSchedule.Text = "Manage Schedule";
            MSchedule.UseVisualStyleBackColor = true;
            MSchedule.Click += MSchedule_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(52, 61);
            label2.Name = "label2";
            label2.Size = new Size(25, 20);
            label2.TabIndex = 11;
            label2.Text = "Id:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(25, 108);
            label3.Name = "label3";
            label3.Size = new Size(79, 20);
            label3.TabIndex = 12;
            label3.Text = "Full Name:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(39, 162);
            label4.Name = "label4";
            label4.Size = new Size(49, 20);
            label4.TabIndex = 13;
            label4.Text = "Email:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(0, 214);
            label5.Name = "label5";
            label5.Size = new Size(121, 20);
            label5.TabIndex = 14;
            label5.Text = "Contact Number:";
            // 
            // Doctorpage
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(914, 600);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(MSchedule);
            Controls.Add(txtDocid);
            Controls.Add(txtpnum);
            Controls.Add(txtemail);
            Controls.Add(txtfname);
            Controls.Add(Delete);
            Controls.Add(Edit);
            Controls.Add(Save);
            Controls.Add(label1);
            Controls.Add(dataGridView1);
            Margin = new Padding(3, 4, 3, 4);
            Name = "Doctorpage";
            Text = "Doctors";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridView1;
        private Label label1;
        private Button Save;
        private Button Edit;
        private Button Delete;
        private TextBox txtfname;
        private TextBox txtemail;
        private TextBox txtpnum;
        private TextBox txtDocid;
        private Button MSchedule;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
    }
}