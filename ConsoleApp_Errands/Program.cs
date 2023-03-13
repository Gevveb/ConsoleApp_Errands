using ConsoleApp_Errands.Services;

var menu = new MenuService();

var Ended = true;

while (Ended)
{
    Console.Clear();
    Console.WriteLine("Choose one of the following options (1-6): ");
    Console.WriteLine("");
    Console.WriteLine("1. Create a new case");
    Console.WriteLine("2. Show all cases");
    Console.WriteLine("3. Show a specific case");
    Console.WriteLine("4. Open and comment on the case/ update the case");
    Console.WriteLine("5. Delete a specific case");
    Console.WriteLine("6. Close the app");


    switch (Console.ReadLine())
    {
        case "1":
            Console.Clear();
            await menu.CreateNewContactAsync();
            break;

        case "2":
            Console.Clear();
            await menu.ListAllErrandsAsync();
            break;

        case "3":
            Console.Clear();
            await menu.ListSpecificErrandAsync();
            break;

        case "4":
            Console.Clear();
            await menu.UpdateSpecificErrandAsync();
            break;

        case "5":
            Console.Clear();
            await menu.DeleteSpecificErrandAsync();
            break;
        case "6":
            Console.Clear();
            Ended = false;
            break;
    }

    Console.WriteLine("\nPress any key to continue...");
    Console.ReadKey();
}