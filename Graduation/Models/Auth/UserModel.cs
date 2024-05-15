using System.ComponentModel.DataAnnotations;
using System;

namespace Graduation.Models.Auth
{
    public class UserModel
    {
        public string UserId { get; set; }
        [MaxLength(50)]
        public string FirstName { get; set; }
        [MaxLength(50)]
        public string LastName { get; set; }
        [MaxLength(50)]
        public string UserName { get; set; }
        [MaxLength(100)]
        [EmailAddress]
        public string Email { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime birthday { get; set; }
    }
}
