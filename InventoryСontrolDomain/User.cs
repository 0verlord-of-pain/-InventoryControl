using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace InventoryСontrol.Domain
{
    public class User : IdentityUser
    {
        public List<PreOrder> PreOrders { get; set; }
        public static User Create(string email)
        {
            return new User
            {
                UserName = email,
                Email = email
            };
        }
    }
}