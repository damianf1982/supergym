using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoxRoles.Models
{
    public class ViewGymsModel
    {
        public int GymSessionsId { get; set; }
        public string SessionName { get; set; }
        public string SessionDay { get; set; }
        public string SessionHour { get; set; }
        public int SessionWeek { get; set; }
        public int RemainingSlots { get; set; }


    }
}