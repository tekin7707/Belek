using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Belek.Services.Catalog.App.Dtos
{
    public class CatalogUpdateDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        public string UserId { get; set; }

        public string? Picture { get; set; }

        public int CategoryId { get; set; }

        public DateTime? UpdateDate { get; set; }
    }
}