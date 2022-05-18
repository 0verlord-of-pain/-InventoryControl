﻿using System.ComponentModel.DataAnnotations;

namespace InventoryСontrol.Application.CQRS.UserAccounts.Validator
{
    public class EmailValidator
    {
        public static bool IsValid(string email)
        {
            return new EmailAddressAttribute().IsValid(email);
        }
    }
}