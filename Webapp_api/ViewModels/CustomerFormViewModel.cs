using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Webapp_api.Models;

namespace Webapp_api.ViewModels
{
    public class CustomerFormViewModel
    {
        public IEnumerable<MembershipType> MembershipType { get; set; }
        public Customers Customers { get; set; }
        
    }
}