using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bytez.business.Dto.Category
{
    public class UpdateCategoryDto
    {
        public string? Id { get; set; }
        public string? Name { get; set; }

        public int Count { get; set; }
    }
}
