using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LibraryApp
{
    public partial class AuthorForm : Form
    {
        public AuthorForm()
        {
            InitializeComponent();
        }

        private void btnAddAuthorName_Click(object sender, EventArgs e)
        {
            string authorName = txtAuthorName.Text;
            if (!string.IsNullOrWhiteSpace(authorName))
            {
                string conText = "Data Source=HOME-PC; Initial Catalog=Library; integrated security=true";
                SqlConnection con = new SqlConnection(conText);
                string myCom = $"insert into Authors values(N'{authorName}')";
                con.Open();
                SqlCommand com = new SqlCommand(myCom, con);
                com.ExecuteNonQuery();
                con.Close();
                lblError.Visible = false;
                txtAuthorName.Text = "";
                MessageBox.Show("Author added successfully");
            }
            else
            {
                lblError.Text = "Author name is fill";
                lblError.Visible = true;
            }
         
        }
    }
}
