using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models
{
    public class Book
    {
        public int Book_Info_ID { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public int Quantity { get; set; } 
    }
}
