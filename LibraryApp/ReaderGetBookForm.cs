using LibraryApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LibraryApp
{
    public partial class ReaderGetBookForm : Form
       
    {
        LibraryContext db = new LibraryContext();
        public ReaderGetBookForm()
        {
            InitializeComponent();
        }



        private void button1_Click(object sender, EventArgs e)
        {
            string readername = txtReaderName.Text;
            string bookname = cmbBooks.Text;
            DateTime getbook = dtGetDate.Value;
            DateTime returnbook = dtReturnDate.Value;
            if(Utilities.IsEmpty(new string[] { readername, bookname }))
            {
                if (getbook < returnbook)
                {
                    Reader read = new Reader();
                    read.FullName = readername;
                    db.Readers.Add(read);
                    db.SaveChanges();
                    int bookId = db.Books.FirstOrDefault(x => x.Name == bookname).BookId;
                    ReaderToBook rtb = new ReaderToBook();
                    rtb.ReaderId = read.ReaderId;
                    rtb.BookId = bookId;
                    rtb.GetBookDate = getbook;
                    rtb.ReturnBookDate = returnbook;
                    db.ReaderToBooks.Add(rtb);
                    db.SaveChanges();
                    MessageBox.Show("Hikmet peyda oldu");
                    FillDataGrid();
                }
            }
        }

        private void ReaderGetBookForm_Load(object sender, EventArgs e)
        {
            FillBookCombo();
            FillDataGrid();
        }

        public void FillDataGrid()
        {
            dtgReaderBookList.DataSource = db.ReaderToBooks.Select(x => new
            {
                BookName = x.Book.Name,
                ReaderName = x.Reader.FullName,
                ExpireDate = (x.ReturnBookDate - x.GetBookDate).Days + "day"

            }).ToList();

        }
        public void FillBookCombo()
        {
            cmbBooks.Items.AddRange(db.Books.Select(X => X.Name).ToArray());
        }
    }
}
