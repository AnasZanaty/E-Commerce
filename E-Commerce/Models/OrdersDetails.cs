using E_Commerce.Data.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce.Models
{
    public class OrdersDetails : IBaseEntity
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public double Price { get; set; }

        //Nav prop

        public int OrderId { get; set; } // first declaring FK
        [ForeignKey(nameof(OrderId))]
        public virtual order order { get; set; } //second do the object

        public int ProductId { get; set; }
        [ForeignKey(nameof(ProductId))]

        public virtual product Product { get; set; }


    }
}
