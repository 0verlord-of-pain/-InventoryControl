using System;
using System.Collections.Generic;

namespace InventoryСontrol.Application.CQRS.UserAccounts.Views
{
    public class UserAccountRoleView
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; }
    }
}