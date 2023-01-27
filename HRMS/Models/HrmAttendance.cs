using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HRMS.Models
{
    public class HrmAttendance
    {
        [Key]
        public int Id { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<System.DateTime> TimeIn { get; set; }
        public Nullable<System.DateTime> TimeOut { get; set; }
        public Nullable<bool> IsPresent { get; set; }
        public Nullable<bool> IsAbsent { get; set; }
        public Nullable<bool> IsLeave { get; set; }
        public string LeaveType { get; set; }
        public Nullable<bool> IsHoliday { get; set; }
        public string Holiday { get; set; }
        public Nullable<bool> IsHalfDay { get; set; }
        public Nullable<bool> IsLate { get; set; }
        public Nullable<bool> IsEarly { get; set; }
        public Nullable<int> Department { get; set; }
        public Nullable<int> Employee { get; set; }
        public string Month { get; set; }

    }
}