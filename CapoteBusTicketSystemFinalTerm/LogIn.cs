using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapoteBusTicketSystemFinalTerm
{
    public partial class LogIn : Form
    {
        public LogIn()
        {
            InitializeComponent();
        }

        private void btnLogIn_Click(object sender, EventArgs e)
        {
            string name = txtName.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (name == "" || password == "")
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }

            bool found = false;
            for (int i = 0; i < PersonalRecord.Count; i++)
            {
                if (PersonalRecord.userNames[i] == name && PersonalRecord.passwords[i] == password)
                {
                    found = true;
                    StoreInfo.CurrentUser = name;
                    StoreInfo.CurrentFirstName = PersonalRecord.names[i];
                    StoreInfo.CurrentLastName = PersonalRecord.lastNames[i];

                    MessageBox.Show("Welcome," + PersonalRecord.firstNames[i] + " " + PersonalRecord.lastNames[i] + "!", "Login Success");

                    MainMenu mainMenu = new MainMenu();
                    mainMenu.Show();
                    this.Close();
                    break;

                }
            }

            if (!found)
            {
                MessageBox.Show("Invalid username or password. Please try again.", "Login Failed"); return;
            }

        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Form1 frm = new Form1();
            frm.Show();
            this.Close();
        }

        private void cbPass_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = !cbPass.Checked;
        }

        private void LogIn_Load(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = true;
            cbPass.Checked = false;
        }
    }
}
