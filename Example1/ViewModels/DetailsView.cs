using Example1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Example1.ViewModels
{
    public class DetailsView
    {
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public Friend Friend { get; set; }
    }
}
