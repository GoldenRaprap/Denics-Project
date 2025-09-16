namespace Denics.Administrator
{
    partial class PatientsPage
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
            ClientsTable = new DataGridView();
            txtboxfName = new TextBox();
            txtboxlName = new TextBox();
            txtboxemail = new TextBox();
            txtboxnumber = new TextBox();
            txtaddress = new TextBox();
            labelfName = new Label();
            labellName = new Label();
            labelemail = new Label();
            labelnum = new Label();
            labeladdress = new Label();
            buttonAdd = new Button();
            buttonEdit = new Button();
            buttonDelete = new Button();
            Password = new Label();
            txtboxPassword = new TextBox();
            Rolelabel = new Label();
            RoleCmbox = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)ClientsTable).BeginInit();
            SuspendLayout();
            // 
            // ClientsTable
            // 
            ClientsTable.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            ClientsTable.Location = new Point(442, 29);
            ClientsTable.Name = "ClientsTable";
            ClientsTable.Size = new Size(518, 598);
            ClientsTable.TabIndex = 0;
            // 
            // txtboxfName
            // 
            txtboxfName.Location = new Point(136, 47);
            txtboxfName.Name = "txtboxfName";
            txtboxfName.Size = new Size(239, 23);
            txtboxfName.TabIndex = 1;
            // 
            // txtboxlName
            // 
            txtboxlName.Location = new Point(136, 115);
            txtboxlName.Name = "txtboxlName";
            txtboxlName.Size = new Size(239, 23);
            txtboxlName.TabIndex = 2;
            // 
            // txtboxemail
            // 
            txtboxemail.Location = new Point(136, 178);
            txtboxemail.Name = "txtboxemail";
            txtboxemail.Size = new Size(239, 23);
            txtboxemail.TabIndex = 3;
            // 
            // txtboxnumber
            // 
            txtboxnumber.Location = new Point(136, 301);
            txtboxnumber.Name = "txtboxnumber";
            txtboxnumber.Size = new Size(239, 23);
            txtboxnumber.TabIndex = 4;
            // 
            // txtaddress
            // 
            txtaddress.Location = new Point(136, 359);
            txtaddress.Name = "txtaddress";
            txtaddress.Size = new Size(239, 23);
            txtaddress.TabIndex = 5;
            // 
            // labelfName
            // 
            labelfName.AutoSize = true;
            labelfName.Location = new Point(136, 29);
            labelfName.Name = "labelfName";
            labelfName.Size = new Size(67, 15);
            labelfName.TabIndex = 6;
            labelfName.Text = "First Name:";
            // 
            // labellName
            // 
            labellName.AutoSize = true;
            labellName.Location = new Point(136, 97);
            labellName.Name = "labellName";
            labellName.Size = new Size(66, 15);
            labellName.TabIndex = 7;
            labellName.Text = "Last Name:";
            // 
            // labelemail
            // 
            labelemail.AutoSize = true;
            labelemail.Location = new Point(136, 160);
            labelemail.Name = "labelemail";
            labelemail.Size = new Size(39, 15);
            labelemail.TabIndex = 8;
            labelemail.Text = "Email:";
            // 
            // labelnum
            // 
            labelnum.AutoSize = true;
            labelnum.Location = new Point(136, 283);
            labelnum.Name = "labelnum";
            labelnum.Size = new Size(99, 15);
            labelnum.TabIndex = 9;
            labelnum.Text = "Contact Number:";
            // 
            // labeladdress
            // 
            labeladdress.AutoSize = true;
            labeladdress.Location = new Point(136, 341);
            labeladdress.Name = "labeladdress";
            labeladdress.Size = new Size(52, 15);
            labeladdress.TabIndex = 10;
            labeladdress.Text = "Address:";
            // 
            // buttonAdd
            // 
            buttonAdd.Location = new Point(136, 476);
            buttonAdd.Name = "buttonAdd";
            buttonAdd.Size = new Size(67, 36);
            buttonAdd.TabIndex = 11;
            buttonAdd.Text = "ADD";
            buttonAdd.UseVisualStyleBackColor = true;
            // 
            // buttonEdit
            // 
            buttonEdit.Location = new Point(209, 476);
            buttonEdit.Name = "buttonEdit";
            buttonEdit.Size = new Size(67, 36);
            buttonEdit.TabIndex = 12;
            buttonEdit.Text = "EDIT";
            buttonEdit.UseVisualStyleBackColor = true;
            // 
            // buttonDelete
            // 
            buttonDelete.Location = new Point(308, 476);
            buttonDelete.Name = "buttonDelete";
            buttonDelete.Size = new Size(67, 36);
            buttonDelete.TabIndex = 13;
            buttonDelete.Text = "DELETE";
            buttonDelete.UseVisualStyleBackColor = true;
            // 
            // Password
            // 
            Password.AutoSize = true;
            Password.Location = new Point(136, 221);
            Password.Name = "Password";
            Password.Size = new Size(63, 15);
            Password.TabIndex = 15;
            Password.Text = "Password: ";
            // 
            // txtboxPassword
            // 
            txtboxPassword.Location = new Point(136, 239);
            txtboxPassword.Name = "txtboxPassword";
            txtboxPassword.Size = new Size(239, 23);
            txtboxPassword.TabIndex = 14;
            // 
            // Rolelabel
            // 
            Rolelabel.AutoSize = true;
            Rolelabel.Location = new Point(136, 404);
            Rolelabel.Name = "Rolelabel";
            Rolelabel.Size = new Size(36, 15);
            Rolelabel.TabIndex = 17;
            Rolelabel.Text = "Role: ";
            // 
            // RoleCmbox
            // 
            RoleCmbox.FormattingEnabled = true;
            RoleCmbox.Location = new Point(136, 422);
            RoleCmbox.Name = "RoleCmbox";
            RoleCmbox.Size = new Size(122, 23);
            RoleCmbox.TabIndex = 18;
            // 
            // PatientsPage
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(984, 661);
            Controls.Add(RoleCmbox);
            Controls.Add(Rolelabel);
            Controls.Add(Password);
            Controls.Add(txtboxPassword);
            Controls.Add(buttonDelete);
            Controls.Add(buttonEdit);
            Controls.Add(buttonAdd);
            Controls.Add(labeladdress);
            Controls.Add(labelnum);
            Controls.Add(labelemail);
            Controls.Add(labellName);
            Controls.Add(labelfName);
            Controls.Add(txtaddress);
            Controls.Add(txtboxnumber);
            Controls.Add(txtboxemail);
            Controls.Add(txtboxlName);
            Controls.Add(txtboxfName);
            Controls.Add(ClientsTable);
            Name = "PatientsPage";
            Text = "PatientsPage";
            Load += PatientsPage_Load;
            ((System.ComponentModel.ISupportInitialize)ClientsTable).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView ClientsTable;
        private TextBox txtboxfName;
        private TextBox txtboxlName;
        private TextBox txtboxemail;
        private TextBox txtboxnumber;
        private TextBox txtaddress;
        private Label labelfName;
        private Label labellName;
        private Label labelemail;
        private Label labelnum;
        private Label labeladdress;
        private Button buttonAdd;
        private Button buttonEdit;
        private Button buttonDelete;
        private Label Password;
        private TextBox txtboxPassword;
        private Label Rolelabel;
        private ComboBox RoleCmbox;
    }
}