using MedsWebApp.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MedsWebApp.Models
{
    public class Category : BaseModel, IViewModel<CategoryViewModel>
    {
        [Required]
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public string Description { get; set; }
        public List<Medicine> Medicines { get; set; }

        public CategoryViewModel AsViewModel() => new CategoryViewModel
        {
            Id = Id,
            Name = Name,
            ParentId = ParentId,
            Description = Description
        };
    }
}
