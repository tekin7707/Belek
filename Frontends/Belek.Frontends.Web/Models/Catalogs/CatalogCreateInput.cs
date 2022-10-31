using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Belek.Frontends.Web.Models.Catalogs
{
    public class CatalogCreateInput
    {
        [Display(Name = "Catalog name")]
        public string Name { get; set; }

        [Display(Name = "Catalog description")]
        public string Description { get; set; }

        [Display(Name = "Catalog price")]
        public double Price { get; set; }

        public string? Picture { get; set; }

        public string? UserId { get; set; }

        [Display(Name = "Catalog category")]
        public int CategoryId { get; set; }

        [Display(Name = "Catalog picture")]
        public IFormFile PhotoFormFile { get; set; }
    }
}