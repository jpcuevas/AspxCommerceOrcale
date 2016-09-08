using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SageFrame.Scheduler
{
    public class ScheduleDate
    {
        public int ScheduleID { get; set; }
        public DateTime Schedule_Date { get; set; }
        public bool IsExecuted { get; set; }
    }
}