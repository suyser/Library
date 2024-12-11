using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models
{
    public class BookRentUser
    {
        public int Book_ID { get; set; }
        public int Book_Info_ID { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public DateTime TimeStartRent { get; set; } // Дата начала аренды
        public DateTime TimeRent { get; set; }      // Дата окончания аренды
    }
}
