using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_Errands.Models.Entities
{
    public class ErrandsEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Error { get; set; } = null!;

        [Required]
        [StringLength(150)]
        public string Description { get; set; } = null!;

        public string Comment { get; set; } = null!;

        [Required]
        public DateTime CreationDate { get; set; } = DateTime.Now;

        [Required]
        public DateTime UpdateDate { get; set; } = DateTime.Now;

        [Required]
        public int AdminId { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [Required]
        public string Status { get; set; } = null!;

        public AdminsEntity Admin { get; set; } = null!;

        public CustomerEntity Customer { get; set; } = null!;
    }
}
