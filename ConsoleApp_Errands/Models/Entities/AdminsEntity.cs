using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_Errands.Models.Entities
{
    public class AdminsEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string AdminFirstName { get; set; } = null!;
        [Required]
        [StringLength(50)]
        public string AdminLastName { get; set; } = null!;
    }
}
