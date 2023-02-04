using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRMS.Models
{
    public class SalaryCalculationDetail
    {
        public long Id { get; set; }
        public Nullable<long> SalaryHdrId { get; set; }
        public Nullable<int> AllowanceId { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public Nullable<decimal> Arrears { get; set; }
        public Nullable<decimal> BonusSetup { get; set; }
        public Nullable<decimal> LoanInstallment { get; set; }
        public Nullable<decimal> Total { get; set; }
    }
}