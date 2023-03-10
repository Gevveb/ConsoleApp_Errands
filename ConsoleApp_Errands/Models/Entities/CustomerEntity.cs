using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp_Errands.Models.Entities
{
    [Index(nameof(Email), IsUnique = true)]
    public class CustomerEntity
    {
        [Key]
        public int Id { get; set; } 

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; } = null!;

        [Required]
        [StringLength(50)]
        public string LastName { get; set; } = null!;

        [Required]
        [StringLength(100)]
        public string Email { get; set; } = null!;

        [Required]
        [Column(TypeName = "char(13)")]
        public string PhoneNumber { get; set; } = null!;

        [Required]
        public int AddressId { get; set; }
        public AddressEntity Address { get; set; } = null!;

        public string DisplayName => $"{FirstName} {LastName}";

        public virtual ICollection<ErrandsEntity> Errands { get; set; } = null!;
    }
}
