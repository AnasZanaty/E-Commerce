using E_Commerce.Data.Base;

namespace E_Commerce.Models
{
    public class CartItem : IBaseEntity
    {
        public int Id { get; set; }
        public product Product { get; set; }
        public int Amount { get; set; }

        public string CartId { get; set; }

    }
}
