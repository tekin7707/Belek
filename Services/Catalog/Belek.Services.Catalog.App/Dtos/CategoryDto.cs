using Belek.Services.Catalog.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Belek.Services.Catalog.App.Dtos
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public StatusEnum Status { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
