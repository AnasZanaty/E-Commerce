using E_Commerce.Data.Base;
using System.Collections.Generic;

namespace E_Commerce.Models
{
    public class order : IBaseEntity
    {
        public order() //ctor and hashset cause i used icollection
        {
         OrdersDetails = new HashSet<OrdersDetails>();
        }
        public int Id { get; set; }
        public string UserID { get; set; }

        public ICollection<OrdersDetails> OrdersDetails { get; set; }

    }
}
