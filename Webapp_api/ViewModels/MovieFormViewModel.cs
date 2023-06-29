using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Webapp_api.Models;

namespace Webapp_api.ViewModels
{
    public class MovieFormViewModel
    {
        public IEnumerable<Genre> Genre { get; set; }
        public Movies Movies { get; set; }
    }
}