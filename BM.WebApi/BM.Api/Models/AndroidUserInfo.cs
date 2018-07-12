using System;
using System.ComponentModel.DataAnnotations;

namespace BM.Api.Models
{
    public partial class AndroidUserInfo
    {
        public Guid ID { get; set; }

        [StringLength(50)]
        public string AndroidID { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
