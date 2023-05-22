using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGTest.Models
{
    [Table("departments")]
    internal class Department
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("parent_id")]
        public int? ParentId { get; set; }

        public int? manager_id { get; set; }

        [ForeignKey("manager_id")]
        public Employee? Manager { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("phone")]
        public string? Phone { get; set; }

        public override bool Equals(object? obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Department dep = (Department)obj;
                return (manager_id == dep.manager_id)
                    && (ParentId == dep.ParentId)
                    && (Phone == dep.Phone);
            }
        }
    }
}
