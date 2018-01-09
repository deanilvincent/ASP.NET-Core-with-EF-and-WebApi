using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreWithWebApi.Models
{
    public class EmployeeDetails
    {
        [Key]
        public int EmpId { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        [DisplayName("Date Hired")]
        public DateTime DateHired { get; set; }
    }
}