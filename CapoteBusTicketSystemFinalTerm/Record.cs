using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapoteBusTicketSystemFinalTerm
{
    public partial class Record : Form
    {

        int searchIndex;
        bool isSearch = false;
        public Record()
        {
            InitializeComponent();
        }

        private void dgvViewList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int selectedrow;
                selectedrow = e.RowIndex;
                DataGridViewRow row = dgvViewList.Rows[selectedrow];

                int clickID = int.Parse(row.Cells[0].Value.ToString());

                for (int i = 0; i < PersonalRecord.indexCounter; i++)
                {
                    if (clickID == PersonalRecord.id[i])
                    {
                        searchIndex = i;
                        isSearch = true; break;
                    }
                }

                if (isSearch)
                {
                    lblIDDisplay.Text = PersonalRecord.id[searchIndex].ToString();
                    lblIDDisplay.Text = PersonalRecord.passenger[searchIndex];

                }
            }
            catch (Exception i)
            {
                MessageBox.Show("There's no error!" + i, "Error", MessageBoxButtons.OK);
            }
        }

        private void Record_Load(object sender, EventArgs e)
        {
            dgvViewList.ColumnCount = 5;
            string[] columnNames = new string[] { "ID Number", "Passenger", "Route", "Seat Number", "Departure Time" };
            for (int x = 0; x < columnNames.Length; x++)
            {
                dgvViewList.Columns[x].Name = columnNames[x];
                dgvViewList.Columns[x].HeaderText = columnNames[x];
            }
            RefreshGrid();
        }
        private void RefreshGrid()
        {
            dgvViewList.Rows.Clear();
            for (int i = 0; i < PersonalRecord.indexCounter; i++)
            {
                string[] row = new string[] { PersonalRecord.id[i].ToString(), PersonalRecord.passenger[i], PersonalRecord.route[i], PersonalRecord.seatNo[i], PersonalRecord.departureTime[i] };
                dgvViewList.Rows.Add(row);
            }
        }   

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchID = txtSearch.Text.Trim();

           if(searchID == "")
            {
                RefreshGrid();
                return; 
            }

           dgvViewList.Rows.Clear();

            for(int i = 0; i < PersonalRecord.indexCounter; i++)
            {
                if (PersonalRecord.passenger[i].ToLower().Contains(searchID.ToLower()) || PersonalRecord.id[i].ToString() == searchID)
                {
                   dgvViewList.Rows.Add(PersonalRecord.id[i].ToString(), PersonalRecord.passenger[i], PersonalRecord.route[i], PersonalRecord.seatNo[i], PersonalRecord.departureTime[i]);

                    lblIDDisplay.Text = PersonalRecord.id[i].ToString();
                }
            } 
            if(dgvViewList.Rows.Count == 0)
            {
                MessageBox.Show("No records found.", "Search", MessageBoxButtons.OK);
               lblIDDisplay.Text = "";
                
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnBooking_Click(object sender, EventArgs e)
        {
            MainMenu mainMenu = new MainMenu();
            mainMenu.Show();
            this.Hide();
        }

        private void dgvViewList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvViewList.Rows[e.RowIndex];
                lblIDDisplay.Text = row.Cells[0].Value.ToString();
            }
        }
    }
}
