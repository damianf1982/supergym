using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoxRoles.Models
{
    public class GymSessions
    {
        public int Id { get; set; }
        public string GymSessionName { get; set; }
        public int RemainingPlaces { get; set; }
    }
}