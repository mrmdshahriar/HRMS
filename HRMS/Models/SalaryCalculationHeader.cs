using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRMS.Models
{
    public class SalaryCalculationHeader
    {
        public long Id { get; set; }
        public long EmployeeId { get; set; }
        public string SalaryMonth { get; set; }
        public DateTime? SalaryDate { get; set; }
        public decimal? BasicSalary { get; set; }
        public int? OThours { get; set; }
        public decimal? OTAmount { get; set; }
        public decimal? TotalSalaryAmount { get; set; }
    }
}