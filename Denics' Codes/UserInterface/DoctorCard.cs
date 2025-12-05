using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Denics.UserInterface
{
    public partial class DoctorCard : UserControl
    {
        private ToolTip toolTip = new ToolTip();

        public DoctorCard()
        {
            InitializeComponent();
        }

        public int DoctorId { get; set; }

        public event EventHandler<ViewScheduleEventArgs> ViewScheduleRequested;

        protected virtual void OnViewScheduleRequested(int doctorId)
            => ViewScheduleRequested?.Invoke(this, new ViewScheduleEventArgs(doctorId));

        public string FullName
        {
            get => lblName.Text;
            set => lblName.Text = value;
        }

        public string Email
        {
            get => lblEmail.Text;
            set
            {
                lblEmail.Text = value;
                toolTip.SetToolTip(lblEmail, value); // show full email on hover
            }
        }
        public string PhoneNumber
        {
            get => lblPhone.Text;
            set => lblPhone.Text = value;
        }

        public string[] Services
        {
            set
            {
                flowLayoutPanelServices.Controls.Clear();
                foreach (string service in value)
                {
                    Label lbl = new Label
                    {
                        Text = service,
                        AutoSize = true,
                        Margin = new Padding(2)
                    };
                    flowLayoutPanelServices.Controls.Add(lbl);
                }
            }
        }

        public Image ProfilePhoto
        {
            get => pictureBox1.Image;
            set => pictureBox1.Image = value;
        }

        private void schedbtn_Click(object sender, EventArgs e)
        {
            // Raise event with DoctorId. Parent form should handle showing the central schedule panel.
            OnViewScheduleRequested(DoctorId);
        }
    }

    // Simple EventArgs to carry doctor id
    public class ViewScheduleEventArgs : EventArgs
    {
        public int DoctorId { get; }
        public ViewScheduleEventArgs(int doctorId) => DoctorId = doctorId;
    }
}
