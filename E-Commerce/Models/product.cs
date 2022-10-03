using E_Commerce.Data;
using E_Commerce.Data.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce.Models
{
    public class product : IBaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int price { get; set; }
        public ProductBrand ProductBrand { get; set; }
        public string Description { get; set; }
        public string ImgUrl { get; set; }
        //nav prop
        public int categoryId { get; set; }

        [ForeignKey (nameof (categoryId))]
        public category category { get; set; }
       
    }
}
