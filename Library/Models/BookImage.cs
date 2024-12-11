using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models
{
    public class BookImage
    {
        public int Image_ID { get; set; }
        public int Book_Info_ID { get; set; }
        public byte[] Image { get; set; }
    }
}
