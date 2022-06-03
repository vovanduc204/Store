using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.ValueObjects
{
    public class Publication
    {
        public int Edition { get; set; }
        public int Year { get; set; }

        public Publication(int edition, int year)
        {
            Edition = edition;
            Year = year;
        }
    }
}
