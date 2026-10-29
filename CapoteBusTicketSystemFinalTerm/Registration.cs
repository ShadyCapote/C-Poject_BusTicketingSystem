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
    public partial class Registration : Form
    {
        public Registration()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string name = txtName.Text.Trim(); 
            string lastName = txtLastName.Text.Trim();
            string email = txtEmail.Text.Trim();
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            // Here you can add code to handle the registration logic

            if(name == "" || lastName == "" ||  email == "" || username == "" || password == "")
            {
                MessageBox.Show("Please fill in all fields.");
                lblStatus.ForeColor = Color.Red;
                return;
            }


        for(int i = 0; i < PersonalRecord.idCounter; i++)
            {
                if(PersonalRecord.userNames[i] == username)
                {
                    MessageBox.Show("Username already exists. Please choose a different username.");
                    lblStatus.ForeColor = Color.Red;
                    return;
                }
            }
            int index = PersonalRecord.Count;
            PersonalRecord.names[index] = name;
            PersonalRecord.emails[index] = email;
            PersonalRecord.userNames[index] = username;
            PersonalRecord.passwords[index] = password;
            PersonalRecord.Count++;

            MessageBox.Show("Registration successful!");

            Form1 frm = new Form1();
            frm.Show();
            this.Hide();
        }


              
    }
    
}
