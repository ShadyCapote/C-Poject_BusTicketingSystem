using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CapoteBusTicketSystemFinalTerm
{
    public partial class MainMenu : Form
    {

        int ticketCount = 0;    
        public MainMenu()
        {
            InitializeComponent();
        }

        private void MainMenu_Load(object sender, EventArgs e)
        {
            lblUsername.Text = StoreInfo.CurrentUser;

            lblID.Text = PersonalRecord.idCounter.ToString();
            dgvDisplay.ColumnCount = 5;
            string[] columnNames = new string[] { "ID Number", "Passenger", "Route", "Seat No.", "Departure Time" };
            for (int x = 0; x < columnNames.Length; x++)
            {
                dgvDisplay.Columns[x].Name = columnNames[x];
            }

            cmbRoute.Items.Clear();
            cmbRoute.Items.Add("Manila to Cebu");
            cmbRoute.Items.Add("Manila to Davao");
            cmbRoute.Items.Add("Cebu to Davao");
            cmbRoute.Items.Add("Davao to Iloilo");
           


            Timer timer = new Timer();  
            timer.Interval = 1000;
            timer.Tick += (s, ev) =>
            {
                dtpDate.Value = DateTime.Now;
                dtpTime.Value = DateTime.Now;
            };
            timer.Start();

            
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {

            if(txtPassenger.Text == "" || cmbRoute.Text == "" || txtSelectSeat.Text == "" || txtDepartureTime.Text == "")
            {
                MessageBox.Show("Please fill in all fields.", "warning");
                return;
            }

            int i = PersonalRecord.indexCounter;
            PersonalRecord.id[i] = PersonalRecord.idCounter;

            PersonalRecord.passenger[i] = txtPassenger.Text;
            PersonalRecord.route[i] = cmbRoute.Text;
            PersonalRecord.seatNo[i] = txtSelectSeat.Text;
            PersonalRecord.departureTime[i] = txtDepartureTime.Text;



            dgvDisplay.Rows.Add(PersonalRecord.idCounter, txtPassenger.Text, cmbRoute.Text , txtSelectSeat.Text, txtDepartureTime.Text);

            PersonalRecord.idCounter++;
            PersonalRecord.indexCounter++;
            

             

            lblDate.Text = dtpDate.Value.ToShortDateString();
            lblName.Text = txtPassenger.Text;
            lblRoute.Text = cmbRoute.Text;
            lblSeatNo.Text = txtSelectSeat.Text;
            lblDepartureTime.Text = txtDepartureTime.Text;
            lblTime.Text = dtpTime.Value.ToShortTimeString();   
            lblBus.Text= cmbBus.Text;
            lblIDNum.Text = lblID.Text;

           lblDate.Visible = true;
            lblName.Visible = true;
            lblRoute.Visible = true;
            lblSeatNo.Visible = true;
            lblDepartureTime.Visible = true;
            lblTime.Visible = true;
            lblBus.Visible = true;
            lblIDNum.Visible = true;
            

            MessageBox.Show("Ticket submitted successfully!", "Success");
        }
        
       

        string browseLocator;
        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog fileOpen = new OpenFileDialog();
                //fileOpen.Title = "Open Image File";

                //fileOpen.Filter = "Image Files (*.jpg; *.jpeg; *.gif; *.bmp;*.png) | *.jpg; *.jpeg; *.gif; *.gif;*.bmp;*.png";

                if (fileOpen.ShowDialog() == DialogResult.OK)
                {
                    pbxPrint.Image = Image.FromFile(fileOpen.FileName);
                    browseLocator = fileOpen.FileName;
                }
                fileOpen.Dispose();
            }
            catch
            {
                MessageBox.Show("Error Uploading Image");
            }
            print(panel3);

        }

        private void pbxPrint_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(pbxPrint, "Print");
        }

        private void print(Panel pn4)
        {
            printPreviewDialog1.Document = printDocument1;
            printDocument1.PrintPage += new PrintPageEventHandler(printDocument_PrintPage);
            printPreviewDialog1.ShowDialog();
        }

        private void printDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            Bitmap bm = new Bitmap(panel3.Width, panel3.Height);
            panel3.DrawToBitmap(bm, new Rectangle(0, 0, panel3.Width, panel3.Height));
            Rectangle pagearea = e.PageBounds;
            e.Graphics.DrawImage(bm, (pagearea.Width / 2) - (this.panel3.Width / 2), this.panel3.Location.Y);

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string name = txtPassenger.Text.Trim();
            if (name == "")
            {
                MessageBox.Show("Enter passenger name to delete", "Warning");
                return;
            }

            bool found = false;
            for (int i = 0; i < PersonalRecord.indexCounter; i++)
            {
                if (PersonalRecord.passenger[i] == name)
                {
                    for (int j = 0; j < PersonalRecord.indexCounter - 1; j++)
                    {
                        PersonalRecord.id[j] = PersonalRecord.id[j + 1];
                        PersonalRecord.passenger[j] = PersonalRecord.passenger[j + 1];
                        PersonalRecord.route[j] = PersonalRecord.route[j + 1];
                        PersonalRecord.seatNo[j] = PersonalRecord.seatNo[j + 1];
                        PersonalRecord.departureTime[j] = PersonalRecord.departureTime[j + 1];
                    }
                    PersonalRecord.indexCounter--;
                    found = true;
                    RefreshGrid();
                    MessageBox.Show("Ticket deleted!", "Success");
                    ClearFields();

                    break;
                }
            }
           if(!found)
            {
                MessageBox.Show("Passenger not found.", "Error");
            }


    
        }
        private void RefreshGrid()
        {
            dgvDisplay.Rows.Clear();
            for(int i = 0; i < PersonalRecord.indexCounter; i++)
            {
                dgvDisplay.Rows.Add(PersonalRecord.id[i], PersonalRecord.passenger[i], PersonalRecord.route[i], PersonalRecord.seatNo[i], PersonalRecord.departureTime[i]);
            }
        }

        private void ClearFields()
        {
            txtPassenger.Text = "";
            cmbRoute.Text = "";
            txtSelectSeat.Text = "";
            txtDepartureTime.Text = "";

        }

        private void btnRecord_Click(object sender, EventArgs e)
        {
            Record viewRecord = new Record();
            viewRecord.Show();
            this.Hide();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string name = txtPassenger.Text.Trim();
            
            if(name == "")
            {
                MessageBox.Show("Enter Passenger Name to Update.", "Warning");
                return;
            }

            bool found = false;

            for(int i = 0; i < PersonalRecord.indexCounter;i++)
            {
                if (PersonalRecord.passenger[i] == name)
                {
                    PersonalRecord.route[i] = cmbRoute.Text;
                    PersonalRecord.seatNo[i] = txtSelectSeat.Text;
                    PersonalRecord.departureTime[i] = txtDepartureTime.Text;
                   found = true;
                    RefreshGrid();
                    MessageBox.Show("Ticket Updated!", "Success");
                    ClearFields();
                    break;
                }
            }
        }

       

        private void btnPay_Click(object sender, EventArgs e)
        {
           if(txtPayment.Text == "")
            {
                MessageBox.Show("Please enter payment amount.");
                return;
            }
           double payment = double.Parse(txtPayment.Text);
            double fare = double.Parse(lblFare.Text);

            if(payment < fare)
            {
                MessageBox.Show("Insufficient payment! Fare is ₱" + lblFare.Text);
                return;
            }

            double change = payment - fare;
            lblChange.Text = "₱" + change.ToString("0.00");
            MessageBox.Show("Payment Successful! Change: ₱" + change.ToString("0.00"));
        }

        private void cmbRoute_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cmbRoute.Text)
            {
                case "Manila to Cebu":
                    lblFare.Text = "850";
                    break;
                case "Manila to Davao":
                    lblFare.Text = "1200";
                    break;
                case "Cebu to Davao":
                    break;
                case "Davao to Iloilo":
                    lblFare.Text = "750";
                    break;
                default:
                    lblFare.Text = "0";
                    break;
            }

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }   
}
