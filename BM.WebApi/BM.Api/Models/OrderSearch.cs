using System;
using System.ComponentModel.DataAnnotations;

namespace BM.Api.Models
{
    public partial class OrderSearch
    {
        [StringLength(16)]
        public string OrderID { get; set; }

        [StringLength(20)]
        public string UserName { get; set; }

        public byte? Sex { get; set; }

        public DateTime? BirthDay { get; set; }

        [StringLength(20)]
        public string ManName { get; set; }

        public DateTime? ManBirthDay { get; set; }

        [StringLength(20)]
        public string WomanName { get; set; }

        public DateTime? WomanBirthDay { get; set; }
    }
}
