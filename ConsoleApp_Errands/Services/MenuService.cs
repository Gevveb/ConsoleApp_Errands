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

            customer.Status = "not started";


            //Lägg till ett ärende i databasen.
            await DbService.SaveAsync(customer);

        }

        public async Task ListAllErrandsAsync()
        {
            //Hämta alla ärenden från databasen.
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
                //Hämta ett specifikt ärende från databasen.
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
                    Console.WriteLine("Enter information in the fields you want to update. \n");

                    Console.Write("Add a new Comment / Update Comment: ");
                    customer.Comment = Console.ReadLine() ?? "";

                    Console.WriteLine("Change Status / Update Status: ");
                    Console.Write($"Enter 1 for \"Ongoing\" or 2 for \"Completed\": ");
                    var answer = Console.ReadLine();
                    if(answer == "1")                  
                        customer.Status = "Ongoing";                    
                    else if(answer == "2") 
                    {
                        customer.Status = "Completed";
                    }
                    customer.UpdateDate= new DateTime();
                        

                    //Uppdatera status och lägga till kommentar på ett specifikt ärende.
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
                //Ta bort ett specifikt ärende från databasen.
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
                Console.WriteLine("No email address provided.");
                Console.WriteLine("");
            }

        }
    }
}
