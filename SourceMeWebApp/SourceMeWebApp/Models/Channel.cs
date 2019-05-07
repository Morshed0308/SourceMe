using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SourceMeWebApp.Models
{
    public class Channel
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Url { get; set; }
    }
}
