using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MedsWebApp.Models
{
    public class Manufacturer : BaseModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public List<Medicine> Medicines { get; set; }
    }
}
