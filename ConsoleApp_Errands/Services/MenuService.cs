using ConsoleApp_Errands.Models;


namespace ConsoleApp_Errands.Services
{
    internal class MenuService
    {
        public async Task CreateNewContactAsync()
        {
            var customer = new ErrandModel();

            Console.Write("Add a new case");
            Console.Write("");

            Console.Write("Customers First Name: ");
            customer.FirstName = Console.ReadLine() ?? "";

            Console.Write("Customers Last Name: ");
            customer.LastName = Console.ReadLine() ?? "";

            Console.Write("Customers Email: ");
            customer.Email = Console.ReadLine() ?? "";

            Console.Write("Customers Phone Number: ");
            customer.PhoneNumber = Console.ReadLine() ?? "";

            Console.Write("Customers Address: ");
            Console.Write("");

            Console.Write("Street Name: ");
            customer.StreetName = Console.ReadLine() ?? "";

            Console.Write("Postal Code: ");
            customer.PostalCode = Console.ReadLine() ?? "";

            Console.Write("City: ");
            customer.City = Console.ReadLine() ?? "";

            Console.Write("Case: ");
            customer.Error = Console.ReadLine() ?? "";

            Console.Write("Description: ");
            customer.Description = Console.ReadLine() ?? "";

            Console.Write("Admin First Name: ");
            customer.AdminFirstName = Console.ReadLine() ?? "";

            Console.Write("Admin Last Name: ");
            customer.AdminLastName = Console.ReadLine() ?? "";


            customer.Comment = "Comment";

            customer.Status = "unopened";


            //save customer to database
            await DbService.SaveAsync(customer);

        }

        public async Task ListAllErrandsAsync()
        {
            //get all customers+address from database
            var errands = await DbService.GetAllAsync();

            if (errands.Any())
            {

                foreach (ErrandModel errand in errands)
                {
                    Console.WriteLine($"Customer number: {errand.Id}");
                    Console.WriteLine($"Customer Name: {errand.FirstName} {errand.LastName}");
                    Console.WriteLine($"Email: {errand.Email}");
                    Console.WriteLine($"Phone Number: {errand.PhoneNumber}");
                    Console.WriteLine($"Address: {errand.StreetName}, {errand.PostalCode} {errand.City}");
                    Console.WriteLine($"Case: {errand.Error}");
                    Console.WriteLine($"Description: {errand.Description} ");
                    Console.WriteLine($"Comment: {errand.Comment} ");
                    Console.WriteLine($"Admin Name: {errand.AdminFirstName}, {errand.AdminLastName}");
                    Console.WriteLine($"Creation Date: {errand.CreationDate}");
                    Console.WriteLine($"Update Date: {errand.UpdateDate}");
                    Console.WriteLine($"Status: {errand.Status}");
                    Console.WriteLine("");  
                }
        }
            else
            {
                Console.WriteLine("There are no customers in the database.");
                Console.WriteLine("");
            }

}

        public async Task ListSpecificErrandAsync()
        {
            Console.Write("Enter the email address of the customer to reach the case: ");
            var email = Console.ReadLine();

            if (!string.IsNullOrEmpty(email))
            {
                //get specific customer+address from database
                var errand = await DbService.GetAsync(email);

                if (errand != null)
                {
                    Console.WriteLine($"Customer number: {errand.Id}");
                    Console.WriteLine($"Customer Name: {errand.FirstName} {errand.LastName}");
                    Console.WriteLine($"Email: {errand.Email}");
                    Console.WriteLine($"Phone Number: {errand.PhoneNumber}");
                    Console.WriteLine($"Address: {errand.StreetName}, {errand.PostalCode} {errand.City}");
                    Console.WriteLine($"Case: {errand.Error}");
                    Console.WriteLine($"Description: {errand.Description} ");
                    Console.WriteLine($"Comment: {errand.Comment} ");
                    Console.WriteLine($"Admin Name: {errand.AdminFirstName}, {errand.AdminLastName}");
                    Console.WriteLine($"Creation Date: {errand.CreationDate}");
                    Console.WriteLine($"Update Date: {errand.UpdateDate}");
                    //Console.WriteLine($"Address: {errand.AdminId}, {errand.CustomerId}");
                    Console.WriteLine($"Status: {errand.Status}");
                    Console.WriteLine("");
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine($"No customer with the specified email address {email} was found.");
                    Console.WriteLine("");
                }
            }
            else
            {
                Console.WriteLine("No email address provided.");
                Console.WriteLine("");
            }

        }

        public async Task UpdateSpecificErrandAsync()
        {
            Console.Write("Enter the email address of the customer to reach the case: ");
            var email = Console.ReadLine();

            if (!string.IsNullOrEmpty(email))
            {

                var customer = await DbService.GetAsync(email);
                if (customer != null)
                {
                    Console.WriteLine("Skriv in information på de fält som du vill uppdatera. \n");

                    Console.Write("Add a Comment: ");
                    customer.Comment = Console.ReadLine() ?? "";

                    Console.Write("Change Status: ");
                    var answer = Console.ReadLine();
                    if(answer == "1")                  
                        customer.Status = "Opened";                    
                    else if(answer == "2") 
                    {
                        customer.Status = "Closed";
                    }
                    customer.UpdateDate= new DateTime();
                        

                    //update specific customer from database
                    await DbService.UpdateAsync(customer);
                }
                else
                {
                    Console.WriteLine($"No customer with the specified email address {email} was found.");
                    Console.WriteLine("");
                }

            }
            else
            {
                Console.WriteLine("No email address provided.");
                Console.WriteLine("");
            }

        }


        public async Task DeleteSpecificErrandAsync()
        {
            Console.Write("Enter the email address of the customer to delete the case: ");
            var email = Console.ReadLine();

            if (!string.IsNullOrEmpty(email))
            {
                Console.Write("Do you really want to delete the case? (y = yes/n = no)");
                var answer = Console.ReadLine();
                //delete specific customer from database
                switch(answer)
                {
                    case "y":
                        await DbService.DeleteAsync(email);
                        Console.WriteLine("The case is removed: ");
                        break;
                    case "n":
                        Console.WriteLine("The case is not removed: ");
                        break;
                }
                    
                
            }
            else
            {
                Console.WriteLine($"Ingen e-postadressen angiven.");
                Console.WriteLine("");
            }

        }
    }
}


//public async Task UpdateSpecificContactAsync()
//{
//    Console.Write("Ange e-postadress på kunden: ");
//    var email = Console.ReadLine();

//    if (!string.IsNullOrEmpty(email))
//    {

//        var customer = await DbService.GetAsync(email);
//        if (customer != null)
//        {
//            Console.WriteLine("Skriv in information på de fält som du vill uppdatera. \n");

//            Console.Write("Förnamn: ");
//            customer.FirstName = Console.ReadLine() ?? null!;

//            Console.Write("Efternamn: ");
//            customer.LastName = Console.ReadLine() ?? null!;

//            Console.Write("E-postadress: ");
//            customer.Email = Console.ReadLine() ?? null!;

//            Console.Write("Telefonnummer: ");
//            customer.PhoneNumber = Console.ReadLine() ?? null!;

//            Console.Write("Gatuadress: ");
//            customer.StreetName = Console.ReadLine() ?? null!;

//            Console.Write("Postnummer: ");
//            customer.PostalCode = Console.ReadLine() ?? null!;

//            Console.Write("Stad: ");
//            customer.City = Console.ReadLine() ?? null!;

//            Console.Write("Change Status: ");
//            var answer = Console.ReadLine();
//            if (answer == "1")
//            {
//                customer.Status = "Öppnad";
//            }
//            if (answer == "2")
//            {
//                customer.Status = "Stängd";
//            }


//            //update specific customer from database
//            await DbService.UpdateAsync(customer);
//        }
//        else
//        {
//            Console.WriteLine($"Hittade inte någon kund med den angivna e-postadressen.");
//            Console.WriteLine("");
//        }