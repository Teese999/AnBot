using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnBot
{
    public class Click
    {
        public Click(uint count)
        {
            Date = DateTime.Today;
            Count = count;
        }

        public DateTime Date { get; set; } 
        public uint Count { get; set; }
    }
}
