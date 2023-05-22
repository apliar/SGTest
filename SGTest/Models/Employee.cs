using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGTest.Models
{
    [Table("employees")]
    internal class Employee
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        public int? department { get; set; }

        [ForeignKey("department")]
        public Department? Department { get; set; }

        [Column("fullname")]
        public string FullName { get; set; }

        [Column("login")]
        public string? Login { get; set; }

        [Column("password")]
        public string? Password { get; set; }

        public int? jobtitle { get; set; }

        [ForeignKey("jobtitle")]
        public JobTitle? JobTitle { get; set; }

        public override bool Equals(object? obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Employee emp = (Employee)obj;
                return (department == emp.department) 
                    && (Login == emp.Login)
                    && (Password == emp.Password)
                    && (jobtitle == emp.jobtitle);
            }
        }
    }
}
