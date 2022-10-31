using Belek.Services.Catalog.Domain.Enums;
using Belek.Services.Catalog.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Belek.Services.Catalog.App.Dtos
{
    public class CatalogDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string? Picture { get; set; }
        public StatusEnum Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public string UserId { get; set; }
        public string? PictureUrl => string.IsNullOrEmpty(Picture)?"": $"https://tekin001.s3.eu-north-1.amazonaws.com/{Picture}";
        public string? ThumbUrl => string.IsNullOrEmpty(Picture) ? "" : $"https://tekin001.s3.eu-north-1.amazonaws.com/thumbs/{Picture}";
    }
}
