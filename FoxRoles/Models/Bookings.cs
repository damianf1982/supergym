using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoxRoles.Models
{
    public class Bookings
    {
        public int Id { get; set; }
        public int GymSessionId { get; set; }
        public string GymSessionName { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}