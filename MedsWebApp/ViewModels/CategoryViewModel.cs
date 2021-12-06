using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedsWebApp.ViewModels
{
    public class CategoryViewModel : BaseViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int? ParentId { get; set; }
    }
}
