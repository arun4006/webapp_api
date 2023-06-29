using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Webapp_api.Dtos
{
    public class MovieDtos
    {
        public int Id { get; set; }
        [Required]
        [StringLength(200)]
        public string Name { get; set; }

  
        public byte GenreId { get; set; }

        public DateTime? DateAdded { get; set; }

        public DateTime ReleaseDate { get; set; }
        [Range(1, 20)]
        public byte NumberInStock { get; set; }
    }
}