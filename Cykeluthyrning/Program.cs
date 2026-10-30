using Cykeluthyrning;                            // knyter ihop program med övriga filer
using System.Runtime.CompilerServices;

List<RegularBike> regularBikeList = new()       // tillgängliga vanliga cyklar
{
    new("Cykel ", 1, true, 50m),
    new("Cykel ", 2, true, 50m),
    new("Cykel ", 3, true, 50m)
};

List<ElectricBike> electricBikeList = new()     // Tillgängliga el cyklar
{
    new("EL Cykel ", 1, true, 80, 90m),
    new("EL Cykel ", 2, true, 20, 90m),
    new("EL Cykel ", 3, true, 40, 90m)
};

List<Customer> customerList = new()           // 3st förinlagda medlemar
{
    new("Christian", 38, true, 1),
    new("Martin", 34, true, 2),
    new("Vito", 17, true, 3),
    new("Anna", 14,false,4)
};


while (true)
{
    Console.WriteLine();
    Console.WriteLine("===== Chrilles Cykeluthyrning ==================");             //min meny och en loop som gör att man kommer tillbaka. 
    Console.Write("1: Visa enheter. ");
    Console.Write("2: Registrera kund. ");
    Console.Write("3: Hyr ut. ");
    Console.WriteLine();
    Console.Write("4: Lämna tillbaka. ");
    Console.Write("5: Ladda. ");
    Console.Write("6: Kund lista. ");
    Console.WriteLine();
    Console.WriteLine("7: Avsluta. ");
    Console.WriteLine();

    Console.Write("Välj ett alternativ: ");
    string choice = Console.ReadLine() ?? "";






    if (choice == "1")                                // Val 1 för att se alla enheter
    {
        Console.WriteLine("===== Alla enheter =============================");
        foreach (RegularBike bike in regularBikeList)
        {
            Console.WriteLine($"Enhet:    {bike.Name} | Pris: {bike.BikeHourPrice} kr/h | Ledig: {(bike.RegularAvailable ? "Ja " : "Nej")}");  //ternary operator letade jag fram.
        }
        foreach (ElectricBike bike in electricBikeList)
        {
            Console.WriteLine($"Enhet: {bike.Name} | Pris: {bike.BikeHourPrice} kr/h | Ledig: {(bike.ElectricAvailable ? "Ja " : "Nej")}  | Batteri: {bike.BatteryLevel}%");
        }
    }






    else if (choice == "2")
    {
        Console.WriteLine("===== Registrera dig ===========================");
        Console.Write("Ange namn: ");
        string name = Console.ReadLine()!;

        Console.Write("Ange ålder: ");
        int age = int.Parse(Console.ReadLine()!);            // mindes att int skulle vara där och att int.parse skulle vara med men tog hjälp. var osäker.

        Console.Write("Vill du bli medlem(Ja/Nej): ");
        string memberAnswer = Console.ReadLine() ?? "";
        bool isMember = memberAnswer.Trim().Equals("Ja", StringComparison.OrdinalIgnoreCase);   // Tog hjälp från trim, kom ej ihåg sista delen!

        int customerNumber = customerList.Count + 1;
        Customer newCustomer = new Customer(name, age, isMember, customerNumber);               // Tog lite hjälp här
        customerList.Add(newCustomer);
        Console.WriteLine($"Ny användare: {newCustomer.Name}, {newCustomer.Age}år, medlemsID:{newCustomer.CustomerNumber}");
    }





    else if (choice == "3")
    {
        Console.WriteLine("===== Hyr en enhet =============================");
        Console.Write("Ange ditt kund ID: ");
        if (!int.TryParse(Console.ReadLine(), out int customerNumber))
        {
            Console.WriteLine("Ogiltigt kundnummer! Ange endast siffror.");
            continue;
        }
        Console.WriteLine();
        Customer? customer = customerList.FirstOrDefault(c => c.CustomerNumber == customerNumber);   // Hade lite koll för vi gått genom precis men tog lite hjälp ändå.

        if (customer == null)
        {
            Console.WriteLine("Kunden finns ej!");
            continue;
        }
        Console.WriteLine("===== Alla enheter =============================");
        int bikeChoiceNumber = 1;                                               // Tog lite hjälp med att få till bikeChoiceNumber men viste att den skulle starta på 1. var positionen på själva Int men nu är den på rätt ställe.
        foreach (RegularBike bike in regularBikeList)                           // Copy paste från "Visa Cyklar" med liten ändring.
        {
            Console.WriteLine($"{bikeChoiceNumber}:    {bike.Name} | Pris: {bike.BikeHourPrice} kr/h | Ledig: {(bike.RegularAvailable ? "Ja " : "Nej")}");  //ternary operator letade jag fram.
            bikeChoiceNumber++;
        }
        foreach (ElectricBike bike in electricBikeList)
        {
            Console.WriteLine($"{bikeChoiceNumber}: {bike.Name} | Pris: {bike.BikeHourPrice} kr/h | Ledig: {(bike.ElectricAvailable ? "Ja " : "Nej")}  | Batteri: {bike.BatteryLevel}%");
            bikeChoiceNumber++;
        }
        Console.Write("Välj enhet nr: ");

        if (!int.TryParse(Console.ReadLine(), out int bikeChoice))
        {
            Console.WriteLine("Ange ett giltigt nummer!");
            continue;
        }

        int totalBikes = regularBikeList.Count + electricBikeList.Count;

        if (bikeChoice < 1 || bikeChoice > totalBikes)
        {
            Console.WriteLine("Ogiltigt val!");
            continue;
        }
        if (customer.Age < 15)
        {
            Console.WriteLine();
            Console.WriteLine("Du måste vara minst 15 år för att hyra en cykel!");
            continue;
        }
        if (bikeChoice <= regularBikeList.Count)
        {
            RegularBike selecteRegulardBike = regularBikeList[bikeChoice - 1];    // Mycket hjälp här.
            if (selecteRegulardBike.RegularAvailable)
            {
                selecteRegulardBike.RegularAvailable = false;
                selecteRegulardBike.RentedByCustomerNumber = customerNumber;
                Console.WriteLine($"Du valde: {selecteRegulardBike.Name}");
            }
            else
            {
                Console.WriteLine($"Hyrs av kund id: {selecteRegulardBike.RentedByCustomerNumber}");
            }
        }
        else
        {
            int electricIndex = bikeChoice - regularBikeList.Count - 1;
            ElectricBike selectedElectricBike = electricBikeList[electricIndex];     // Kontrollerar att den är ledig

            if (customer.Age < 18)
            {
                Console.WriteLine();
                Console.WriteLine("Du måste vara minst 18 år för att hyra en el cykel!");
                continue;
            }
            if (selectedElectricBike.BatteryLevel <= 30)
            {
                Console.WriteLine();
                Console.WriteLine("Elcykeln har för lite batteri och måste laddas först!");
                continue;
            }
            if (selectedElectricBike.ElectricAvailable)
            {
                selectedElectricBike.ElectricAvailable = false;
                selectedElectricBike.RentedByCustomerNumber = customerNumber;
                Console.WriteLine($"Du valde {selectedElectricBike.Name}");
            }
            else
            {
                Console.WriteLine($"Hyrs av kund id: {selectedElectricBike.RentedByCustomerNumber}");
            }
        }
    }







    else if (choice == "4")
    {
        Console.WriteLine("===== Lämna tillbaka cykel =====================");
        Console.Write("Ange kundnummer: ");

        if (!int.TryParse(Console.ReadLine(), out int customerNumber))                         //(customer == null)
        {
            Console.WriteLine("Ogiltligt kundenummer!");
            continue;
        }
        Customer? customer = customerList.FirstOrDefault(
        c => c.CustomerNumber == customerNumber
        );

        if (customer == null)
        {
            Console.WriteLine("Kunden finns inte!");
            continue;
        }

        // Hitta kundens hyrda cyklar
        var rentedRegularBike = regularBikeList
            .Where(b => b.RentedByCustomerNumber == customerNumber)
            .ToList();

        var rentedElectricBike = electricBikeList
            .Where(b => b.RentedByCustomerNumber == customerNumber)
            .ToList();

        Console.WriteLine("===== Dina hyrda cyklar ========================");
        int returnBikeNumber = 1;
        int totalRentedBikes = rentedRegularBike.Count + rentedElectricBike.Count;
        if (totalRentedBikes == 0)                                      // hjälp med rätt plats
        {
            Console.WriteLine("Du har inga uthyrda enheter!");
            continue;
        }
        foreach (RegularBike bike in rentedRegularBike)               // vi använder rentedRegularBike för att inte se alla bara den som kunden har
        {
            Console.WriteLine($"{returnBikeNumber}. {bike.Name}");
            returnBikeNumber++;                                      //Hjälp här med
        }
        foreach (ElectricBike bike in rentedElectricBike)            // vi vi använder rentedElectricBike inte electricBikeList för vi vill inte ha ut hela listan
        {
            Console.WriteLine($"{returnBikeNumber}. {bike.Name}");
            returnBikeNumber++;

        }
        Console.Write("Välj cykel att lämna tillbaka: ");

        if (!int.TryParse(Console.ReadLine(), out int returnChoice))      //Välj en att lämna tillbaka
        {
            Console.WriteLine("Du måste ange ett nummer!");
            continue;
        }
        if (returnChoice < 1 || returnChoice > totalRentedBikes)
        {
            Console.WriteLine("Ogiltigt cykelval!");
            continue;
        }
        Console.Write("Ange antal hyrda timmar: ");

        if (!int.TryParse(Console.ReadLine(), out int rentedHours) || rentedHours <= 0)
        {
            Console.WriteLine("Ogiltigt antal timmar!");
            continue;
        }
        if (returnChoice <= rentedRegularBike.Count)
        {
            RegularBike selectedRegularBike = rentedRegularBike[returnChoice - 1];
            selectedRegularBike.RegularAvailable = true;
            selectedRegularBike.RentedByCustomerNumber = null;
            decimal totalPrice = selectedRegularBike.BikeHourPrice * rentedHours;
            decimal discount = 0m;

            if (customer.IsMember)
            {
                discount = totalPrice * 0.10m;
            }

            decimal finalPrice = totalPrice - discount;

            Console.WriteLine();
            Console.Write($"{selectedRegularBike.Name} har återlämnats! ");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("===== Kvitto ===================================");
            Console.WriteLine($"Namn: {customer.Name}");
            Console.WriteLine($"Ålder: {customer.Age} år");
            Console.WriteLine($"Kund-ID: {customer.CustomerNumber}");
            Console.WriteLine();
            Console.WriteLine($"Enhet: {selectedRegularBike.Name}");
            Console.WriteLine($"Ordinarie pris:          {totalPrice:F2} kr");
            Console.WriteLine($"Medlemsrabatt (-10%):   -{discount:F2} kr");
            Console.WriteLine($"Att betala:              {finalPrice:F2} kr");
        }
        else
        {
            int electricIndex = returnChoice - rentedRegularBike.Count - 1;     // Här ska vi återlämna elcykeln

            ElectricBike selectedElectricBike = rentedElectricBike[electricIndex];
            selectedElectricBike.ElectricAvailable = true;                             // Gör cykeln tillgänglig igen
            selectedElectricBike.RentedByCustomerNumber = null;                        // Tar bort kopplingen till kunden.
            selectedElectricBike.UseBattery(rentedHours);
            decimal totalPrice = selectedElectricBike.BikeHourPrice * rentedHours;
            decimal discount = 0m;

            if (customer.IsMember)
            {
                discount = totalPrice * 0.10m;
            }

            decimal finalPrice = totalPrice - discount;

            Console.WriteLine();
            Console.Write($"{selectedElectricBike.Name} har återlämnats! ");
            Console.Write($"Batteri kvar: {selectedElectricBike.BatteryLevel}%");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("===== Kvitto ===================================");
            Console.WriteLine($"Namn: {customer.Name}");
            Console.WriteLine($"Ålder: {customer.Age} år");
            Console.WriteLine($"Kund-ID: {customer.CustomerNumber}");
            Console.WriteLine();
            Console.WriteLine($"Cykel: {selectedElectricBike.Name}");
            Console.WriteLine($"Ordinarie pris:          {totalPrice:F2} kr");
            Console.WriteLine($"Medlemsrabatt (-10%):   -{discount:F2} kr");
            Console.WriteLine($"Att betala:              {finalPrice:F2} kr");


        }
    }
    else if (choice == "5")
    {
        Console.WriteLine("===== Ladda el cykel ===========================");
        int chargeBikeNumber = 1;
        foreach (ElectricBike bike in electricBikeList)
        {
            Console.WriteLine($"{chargeBikeNumber}. {bike.Name} | Batteri: {bike.BatteryLevel}%");
            chargeBikeNumber++;
        }
        if (!int.TryParse(Console.ReadLine(), out int chargeChoice))
            {
                Console.WriteLine("Ogiltigt nummer!");
                continue;
            }
        if (chargeChoice < 1 || chargeChoice > electricBikeList.Count)
            {
                Console.WriteLine("Ogiltigt cykelnummer!");
                continue;
            }
            ElectricBike selectedBike = electricBikeList[chargeChoice - 1];
            if (!selectedBike.ElectricAvailable)                               // ! betyder att vi stoppar laddningen när cykeln inte är ledig
            {
                Console.WriteLine("Cykeln är uthyrd och kan inte laddas!");
                continue;
            }
            if (selectedBike.BatteryLevel == 100)
            {
                Console.WriteLine("Batteriet är redan fulladdat!");
                continue;

            }
            selectedBike.ChargeBike();
            Console.WriteLine($"Elcykeln {selectedBike.Name} är nu laddad till {selectedBike.BatteryLevel}%!");
    }
    
    else if (choice == "6")
    {
        Console.WriteLine("===== Registrerade kunder ======================");
        foreach (Customer customer in customerList)
        {
            Console.WriteLine(
                $"ID: {customer.CustomerNumber} | " +
                $"Namn: {customer.Name} | " +
                $"Ålder: {customer.Age} | " +
                $"Medlem: {(customer.IsMember ? "Ja" : "Nej")}");

            var customerRegularBikes = regularBikeList.Where(b => b.RentedByCustomerNumber == customer.CustomerNumber).ToList();
            var customerElectricBikes = electricBikeList.Where(b => b.RentedByCustomerNumber == customer.CustomerNumber).ToList();

            foreach (RegularBike bike in customerRegularBikes)
            {
                Console.WriteLine($"Hyr: {bike.Name}");
            }
            foreach (ElectricBike bike in customerElectricBikes)
            {
                Console.WriteLine($"Hyr: {bike.Name}");
                Console.WriteLine();
            }
            if (customerRegularBikes.Count == 0 && customerElectricBikes.Count == 0)
            {
                Console.WriteLine("Hyr: Ingen cykel");
                Console.WriteLine();
            }
        }

    }
    else if (choice =="7")
    {
        Console.WriteLine("Avslutar program!");
        break;
    }
}

