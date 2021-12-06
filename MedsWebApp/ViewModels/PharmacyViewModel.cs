using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedsWebApp.ViewModels
{
    public class PharmacyViewModel : BaseViewModel
    {
        public string Name { get; internal set; }
        public string Phone { get; internal set; }
        public string Address { get; internal set; }
        public string WorkTime { get; internal set; }
        
    }
}
