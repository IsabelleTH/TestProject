using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestProject.Models
{
    public class TestProtocolUserViewModel
    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }
        public int UserId { get; set; }
        public int TestId { get; set; }
        public DateTime TestDate { get; set; }
        public int TestScore { get; set; }
        public string City { get; set; }
    }
}