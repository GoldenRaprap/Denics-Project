namespace Denics.UserInterface
{
    partial class DoctorCard
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pictureBox1 = new PictureBox();
            lblName = new Label();
            lblEmail = new Label();
            lblPhone = new Label();
            lblServices = new Label();
            flowLayoutPanelServices = new FlowLayoutPanel();
            flowLayoutPanelSchedule = new FlowLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.BackgroundImageLayout = ImageLayout.Zoom;
            pictureBox1.Location = new Point(40, 12);
            pictureBox1.Margin = new Padding(3, 2, 3, 2);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(98, 118);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // lblName
            // 
            lblName.Location = new Point(3, 135);
            lblName.Name = "lblName";
            lblName.Size = new Size(177, 15);
            lblName.TabIndex = 1;
            lblName.Text = "Name";
            lblName.TextAlign = ContentAlignment.TopCenter;
            // 
            // lblEmail
            // 
            lblEmail.AutoEllipsis = true;
            lblEmail.Location = new Point(3, 150);
            lblEmail.MaximumSize = new Size(219, 38);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(177, 15);
            lblEmail.TabIndex = 2;
            lblEmail.Text = "Email";
            lblEmail.TextAlign = ContentAlignment.TopCenter;
            // 
            // lblPhone
            // 
            lblPhone.Location = new Point(3, 165);
            lblPhone.Name = "lblPhone";
            lblPhone.Size = new Size(177, 15);
            lblPhone.TabIndex = 3;
            lblPhone.Text = "Phone Number";
            lblPhone.TextAlign = ContentAlignment.TopCenter;
            // 
            // lblServices
            // 
            lblServices.AutoSize = true;
            lblServices.Location = new Point(3, 195);
            lblServices.Name = "lblServices";
            lblServices.Size = new Size(55, 15);
            lblServices.TabIndex = 4;
            lblServices.Text = "Services: ";
            // 
            // flowLayoutPanelServices
            // 
            flowLayoutPanelServices.Location = new Point(13, 212);
            flowLayoutPanelServices.Margin = new Padding(3, 2, 3, 2);
            flowLayoutPanelServices.Name = "flowLayoutPanelServices";
            flowLayoutPanelServices.Size = new Size(154, 158);
            flowLayoutPanelServices.TabIndex = 5;
            // 
            // flowLayoutPanelSchedule
            // 
            flowLayoutPanelSchedule.AutoScroll = true;
            flowLayoutPanelSchedule.Location = new Point(15, 335);
            flowLayoutPanelSchedule.Name = "flowLayoutPanelSchedule";
            flowLayoutPanelSchedule.Size = new Size(118, 100);
            flowLayoutPanelSchedule.TabIndex = 7;
            flowLayoutPanelSchedule.Visible = false;
            // 
            // DoctorCard
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(flowLayoutPanelServices);
            Controls.Add(lblServices);
            Controls.Add(lblPhone);
            Controls.Add(lblEmail);
            Controls.Add(lblName);
            Controls.Add(pictureBox1);
            Margin = new Padding(3, 2, 3, 2);
            Name = "DoctorCard";
            Size = new Size(183, 384);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private Label lblName;
        private Label lblEmail;
        private Label lblPhone;
        private Label lblServices;
        private FlowLayoutPanel flowLayoutPanelServices;
        private FlowLayoutPanel flowLayoutPanelSchedule;
    }
}
