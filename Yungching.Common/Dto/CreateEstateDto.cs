using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yungching.Common.Dto
{
    public class CreateEstateDto
    {
        public int? Id { get; set; }
        public int MembershipId { get; set; }
        //public string? Number { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int Price { get; set; }
        public int Type { get; set; }
        public int Range { get; set; }
        //public bool? Status { get; set; }
        //public DateTime? CreateAt { get; set; }
        //public DateTime? UpdateAt { get; set; }

    }
}
