
namespace ConsoleApp_Errands.Models
{
    internal class ErrandModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public int CustomerId { get; set; }
        public string Error { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Comment { get; set; } = null!;

        public int AddressId { get; set; }
        public string StreetName { get; set; } = null!;
        public string PostalCode { get; set; } = null!;
        public string City { get; set; } = null!;

        public int AdminId { get; set; }
        public string AdminFirstName { get; set; } = null!;
        public string AdminLastName { get; set; } = null!;


        public string AdminDisplayName => $"{AdminFirstName} {AdminLastName}";

        public DateTime CreationDate { get; set; } = DateTime.Now;
        public DateTime UpdateDate { get; set; } = DateTime.Now;

        public string Status { get; set; } = null!;
    }

}

