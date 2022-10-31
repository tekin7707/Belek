using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Belek.Frontends.Web.Models.Catalogs
{
    public class CategoryViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public int Status { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}