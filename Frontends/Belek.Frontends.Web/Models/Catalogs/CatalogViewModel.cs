using Belek.Frontends.Web.Models.Catalogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Belek.Frontends.Web.Models.Catalogs
{
    public class CatalogViewModel
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public string ShortDescription
        {
            get => Description?.Length > 100 ? Description?.Substring(0, 100) + "..." : Description ?? "";
        }

        public double Price { get; set; }

        public string? UserId { get; set; }

        public string? Picture { get; set; }

        public string? PictureUrl { get; set; }

        public string? ThumbUrl { get; set; }

        public DateTime CreatedTime { get; set; }

        public int? CategoryId { get; set; }

        public CategoryViewModel? Category { get; set; }
    }
}