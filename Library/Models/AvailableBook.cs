using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models
{
    public class AvailableBook
    {
        public int Available_Books_ID { get; set; }
        public int Book_ID { get; set; }
        public int Book_Info_ID { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
    }
}
