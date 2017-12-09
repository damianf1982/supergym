using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoxRoles.Models
{
    public class Profiles
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public int GymNumber { get; set; }
        public string Notes { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}