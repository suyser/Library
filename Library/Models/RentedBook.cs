using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models
{
    public class RentedBook
    {
        public int Rented_Books_ID { get; set; }
        public int Book_ID { get; set; }
        public int User_ID { get; set; }
        public DateTime Time_rent { get; set; }
        public DateTime Time_start_rent { get; set; }


    }
}
