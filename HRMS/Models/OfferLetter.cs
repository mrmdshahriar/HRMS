using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HRMS.Models
{
    public class OfferLetter
    {

        [Key]
        public int PK_OfferLetterId { get; set; }
        public string EmployeeId { get; set; }
        public string DesignationId { get; set; }
        public string DepartmentId { get; set; }
        public DateTime JoiningDate { get; set; }
        public string Salary { get; set; }

    }
}
