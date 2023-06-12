using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Woningpaspoort.Model
{
    public class Complex
    {
        public int ComplexId { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
    }
}
