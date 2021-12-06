using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedsWebApp.ViewModels
{
    public class DiscountViewModel : BaseViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DiscountStart { get; set; }
        public DateTime DiscountEnd { get; set; }
        public int Value { get; set; }
    }
}
