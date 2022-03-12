using System;
using System.Collections.Generic;

#nullable disable

namespace LibraryApp.Models
{
    public partial class Reader
    {
        public Reader()
        {
            ReaderToBooks = new HashSet<ReaderToBook>();
        }

        public int ReaderId { get; set; }
        public string FullName { get; set; }

        public virtual ICollection<ReaderToBook> ReaderToBooks { get; set; }
    }
}
