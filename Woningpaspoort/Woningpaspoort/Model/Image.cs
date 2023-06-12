using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Woningpaspoort.Model
{
    public class Image
    {
        public int ImageId { get; set; }
        public required string URL { get; set; }
        public required string ExternalURL { get; set; }
        public required string Description { get; set; }
        public required bool Thumbnail { get; set; }
        public string? CreatedBy { get; set; }
        public Stream? File { get; set; }
    }
}
