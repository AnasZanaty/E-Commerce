using E_Commerce.Data.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Models
{
    public class category : IBaseEntity
    {
        public category() //cause i used icollection
        {
            products = new HashSet<product>();
        }
        public int Id { get; set; }
        [Required(ErrorMessage ="Name is required")]
        [StringLength(20, ErrorMessage ="this{0} is specifies between{2},{1}",MinimumLength =5)]
        [Display(Name="Category Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
        //nav prop
        public ICollection <product> products { get; set; }       
    }
}
