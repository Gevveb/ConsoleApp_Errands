using ConsoleApp_Errands.Contexts;
using ConsoleApp_Errands.Models;
using ConsoleApp_Errands.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp_Errands.Services
{
    internal class DbService
    {
        private static DataContext _context = new DataContext();

        public static async Task SaveAsync(ErrandModel Errand)
        {

            var _errandEntity = new ErrandsEntity
            {
                Id= Errand.Id,
                Error = Errand.Error,
                Description= Errand.Description,
                AdminId= Errand.AdminId,
                Comment= Errand.Comment,
                CreationDate= Errand.CreationDate,
                UpdateDate= Errand.UpdateDate,
                Status= Errand.Status,
            };
         

            var _customerEntity = await _context.Customers.FirstOrDefaultAsync(x => x.FirstName == Errand.FirstName && x.LastName == Errand.LastName && x.PhoneNumber == Errand.PhoneNumber && x.Email == Errand.Email);
            if (_customerEntity != null)
                _errandEntity.CustomerId = _customerEntity.Id;
            else
                _errandEntity.Customer = new CustomerEntity
                {
                    FirstName = Errand.FirstName,
                    LastName = Errand.LastName,
                    Email = Errand.Email,
                    PhoneNumber = Errand.PhoneNumber,

                };

            var _adminEntity = await _context.Admins.FirstOrDefaultAsync(x => x.AdminFirstName == Errand.AdminFirstName && x.AdminLastName == Errand.AdminLastName);
            if (_adminEntity != null)
                _errandEntity.AdminId = _adminEntity.Id;
            else
                _errandEntity.Admin = new AdminsEntity
                {
                    AdminFirstName = Errand.AdminFirstName,
                    AdminLastName = Errand.AdminLastName,
                };

            var _addressEntity = await _context.Addresses.FirstOrDefaultAsync(x => x.StreetName == Errand.StreetName && x.PostalCode == Errand.PostalCode && x.City == Errand.City);
            if (_addressEntity != null)
                _errandEntity.Customer.AddressId = _addressEntity.Id;
            else
                _errandEntity.Customer.Address = new AddressEntity
                {
                    StreetName = Errand.StreetName,
                    PostalCode = Errand.PostalCode,
                    City = Errand.City
                };

            _context.Add(_errandEntity);
            await _context.SaveChangesAsync();
        }

        public static async Task<IEnumerable<ErrandModel>> GetAllAsync()
        {
            var _errands = new List<ErrandModel>();

            foreach (var _errand in await _context.Errands.Include(x => x.Customer.Address).Include(x => x.Admin).ToListAsync())
                _errands.Add(new ErrandModel
                {
                    Id = _errand.Id,
                    FirstName = _errand.Customer.FirstName,
                    LastName = _errand.Customer.LastName,
                    Email = _errand.Customer.Email,
                    Error = _errand.Error,
                    CustomerId = _errand.CustomerId,
                    Description = _errand.Description,
                    Comment = _errand.Comment,
                    PhoneNumber = _errand.Customer.PhoneNumber,
                    StreetName = _errand.Customer.Address.StreetName,
                    PostalCode = _errand.Customer.Address.PostalCode,
                    City = _errand.Customer.Address.City,
                    AdminId = _errand.AdminId,
                    AdminFirstName = _errand.Admin.AdminFirstName,
                    AdminLastName = _errand.Admin.AdminLastName,
                    CreationDate = _errand.CreationDate,
                    UpdateDate = _errand.UpdateDate,
                    Status = _errand.Status
                });

            return _errands;
        }

        public static async Task<ErrandModel> GetAsync(string email)
        {
            var _errand = await _context.Errands.Include(x => x.Customer.Address).Include(x => x.Admin).FirstOrDefaultAsync(x => x.Customer.Email == email);
            if (_errand != null)
                return new ErrandModel
                {
                    Id = _errand.Id,
                    FirstName = _errand.Customer.FirstName,
                    LastName = _errand.Customer.LastName,
                    Email = _errand.Customer.Email,
                    PhoneNumber = _errand.Customer.PhoneNumber,
                    Error = _errand.Error,
                    Description = _errand.Description,
                    Comment = _errand.Comment,
                    StreetName = _errand.Customer.Address.StreetName,
                    PostalCode = _errand.Customer.Address.PostalCode,
                    City = _errand.Customer.Address.City,
                    AdminFirstName = _errand.Admin.AdminFirstName,
                    AdminLastName = _errand.Admin.AdminLastName
                };

            else
                return null!;
        }

        public static async Task UpdateAsync(ErrandModel errand)
        {
            var _errandEntity = await _context.Errands.FirstOrDefaultAsync(x => x.Id == errand.Id);
            if (_errandEntity != null)
            {

                if (!string.IsNullOrEmpty(errand.Error))
                    _errandEntity.Error = errand.Error;

                if (!string.IsNullOrEmpty(errand.Comment))
                    _errandEntity.Comment = errand.Comment;

                if (!string.IsNullOrEmpty(errand.Description))
                    _errandEntity.Description = errand.Description;

                if (!string.IsNullOrEmpty(errand.Status))
                    _errandEntity.Status = errand.Status;


                _context.Update(_errandEntity);
                await _context.SaveChangesAsync();

            }
        }

        public static async Task DeleteAsync(string email)
        {
            var errand = await _context.Errands.Include(x => x.Customer).Include(x => x.Admin).FirstOrDefaultAsync(x => x.Customer.Email == email);
            if (errand != null)
            {
                _context.Remove(errand);
                await _context.SaveChangesAsync();
            }
        }
    }
}


//public static async Task UpdateAsync(ErrandModel customer)
//{
//    var _errandEntity = await _context.Errands.Include(x => x.Customer.Address).Include(x => x.Admin).FirstOrDefaultAsync(x => x.Id == customer.Id);
//    if (_errandEntity != null)
//    {
//        if (!string.IsNullOrEmpty(customer.FirstName))
//            _errandEntity.Customer.FirstName = customer.FirstName;

//        if (!string.IsNullOrEmpty(customer.LastName))
//            _errandEntity.Customer.LastName = customer.LastName;

//        if (!string.IsNullOrEmpty(customer.Email))
//            _errandEntity.Customer.Email = customer.Email;

//        if (!string.IsNullOrEmpty(customer.PhoneNumber))
//            _errandEntity.Customer.PhoneNumber = customer.PhoneNumber;

//        if (!string.IsNullOrEmpty(customer.StreetName) || !string.IsNullOrEmpty(customer.PostalCode) || !string.IsNullOrEmpty(customer.City))
//        {
//            var _addressEntity = await _context.Addresses.FirstOrDefaultAsync(x => x.StreetName == customer.StreetName && x.PostalCode == customer.PostalCode && x.City == customer.City);
//            if (_addressEntity != null)
//                _errandEntity.Customer.AddressId = _addressEntity.Id;
//            else
//                _errandEntity.Customer.Address = new AddressEntity
//                {
//                    StreetName = customer.StreetName,
//                    PostalCode = customer.PostalCode,
//                    City = customer.City
//                };
//        }

//        _context.Update(_errandEntity);
//        await _context.SaveChangesAsync();

//    }
//}











//public static async Task UpdateAsync(ErrandModel errand)
//{
//    var _errandEntity = await _context.Errands.Include(x => x.Customer.Address).Include(x => x.Admin).FirstOrDefaultAsync(x => x.Id == errand.Id);
//    if (_errandEntity != null)
//    {

//        if (!string.IsNullOrEmpty(errand.Error))
//            _errandEntity.Error = errand.Error;

//        if (!string.IsNullOrEmpty(errand.Comment))
//            _errandEntity.Comment = errand.Comment;

//        if (!string.IsNullOrEmpty(errand.Description))
//            _errandEntity.Description = errand.Description;

//        if (!string.IsNullOrEmpty(errand.Status))
//            _errandEntity.Status = errand.Status;


//        _errandEntity.UpdateDate = errand.UpdateDate;

//        _errandEntity.CreationDate = errand.CreationDate;

//        var _customerEntity = await _context.Customers.FirstOrDefaultAsync(x => x.FirstName == errand.FirstName && x.LastName == errand.LastName && x.PhoneNumber == errand.PhoneNumber && x.Email == errand.Email);
//        if (_customerEntity != null)
//            _errandEntity.CustomerId = _customerEntity.Id;
//        else
//            _errandEntity.Customer = new CustomerEntity
//            {
//                FirstName = errand.FirstName,
//                LastName = errand.LastName,
//                Email = errand.Email,
//                PhoneNumber = errand.PhoneNumber,

//            };

//        var _adminEntity = await _context.Admins.FirstOrDefaultAsync(x => x.AdminFirstName == errand.AdminFirstName && x.AdminLastName == errand.AdminLastName);
//        if (_adminEntity != null)
//            _errandEntity.AdminId = _adminEntity.Id;
//        else
//            _errandEntity.Admin = new AdminsEntity
//            {
//                AdminFirstName = errand.AdminFirstName,
//                AdminLastName = errand.AdminLastName,
//            };

//        if (!string.IsNullOrEmpty(errand.StreetName) || !string.IsNullOrEmpty(errand.PostalCode) || !string.IsNullOrEmpty(errand.City))
//        {
//            var _addressEntity = await _context.Addresses.FirstOrDefaultAsync(x => x.StreetName == errand.StreetName && x.PostalCode == errand.PostalCode && x.City == errand.City);
//            if (_addressEntity != null)
//                _errandEntity.Customer.AddressId = _addressEntity.Id;
//            else
//                _errandEntity.Customer.Address = new AddressEntity
//                {
//                    StreetName = errand.StreetName,
//                    PostalCode = errand.PostalCode,
//                    City = errand.City
//                };
//        }

//        _context.Update(_errandEntity);
//        await _context.SaveChangesAsync();

//    }
//}