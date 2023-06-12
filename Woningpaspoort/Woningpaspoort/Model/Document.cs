using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Woningpaspoort.Model
{
    public class Document
    {
        public int DocumentId { get; set; }
        public required string URL { get; set; }
        public required string ExternalURL { get; set; }
        public required string Description { get; set; }
    }
}
