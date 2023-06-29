using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Webapp_api.Models
{
    public class Min18YearsIfAMember: ValidationAttribute

    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var customer = (Customers)validationContext.ObjectInstance;
            if (customer.MembershipTypeId == MembershipType.UnKnown || 
                customer.MembershipTypeId == MembershipType.PayAsYouGo)
            {
                return ValidationResult.Success;
            }
            if (customer.Birthdate == null)
            {
                return new ValidationResult("Birth date is Required");
            }
            var age = DateTime.Today.Year - customer.Birthdate.Value.Year;
            return (age > 18) 
                ? ValidationResult.Success
                : new ValidationResult("Customer shoud be atleast 18 years old to get membership");
        }
    }
}