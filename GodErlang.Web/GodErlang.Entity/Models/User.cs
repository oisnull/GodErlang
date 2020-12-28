using System;
using System.Collections.Generic;

namespace GodErlang.Entity.Models
{
    public partial class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNum { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string HeadImage { get; set; }
        public UserState State { get; set; }
        public UserSex Sex { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }
    }
}
