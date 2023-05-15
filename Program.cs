using System;

class City
{
    public int CityId { get; set; }
    public string CityName { get; set; }
    public int InfectionLevel { get; set; }
    public int ContactCityId { get; set; }
}

class Program
{
    static void Main()
    {
        Console.Write("Enter the number of cities in the model: ");
        int numOfCities = int.Parse(Console.ReadLine());

        City[] cities = new City[numOfCities];

        for (int i = 0; i < numOfCities; i++)
        {
            Console.WriteLine($"City {i} details:");
            cities[i] = new City();

            Console.Write("City name: ");
            cities[i].CityName = Console.ReadLine();

            Console.Write("Number of cities contacted: ");
            cities[i].ContactCityId = int.Parse(Console.ReadLine());

            if (cities[i].ContactCityId < 0 || cities[i].ContactCityId > i)
            {
                Console.WriteLine("Invalid ID");
                i--;
            }
        }

        while (true)
        {
            Console.WriteLine("\nEnter an event during the COVID-19 outbreak ('Outbreak', 'Vaccinate', 'Lockdown', 'Spread', or 'Exit'):");
            string eventType = Console.ReadLine();

            if (eventType == "Outbreak" || eventType == "Vaccinate" || eventType == "Lockdown")
            {
                Console.Write("Enter the city ID where the event occurred: ");
                int eventCityId = int.Parse(Console.ReadLine());

                if (eventCityId >= 0 && eventCityId < numOfCities)
                {
                    ProcessEvent(eventType, eventCityId, cities);
                    DisplayCityDetails(cities);
                }
                else
                {
                    Console.WriteLine("Invalid ID");
                }
            }
            else if (eventType == "Spread")
            {
                SpreadInfection(cities);
                DisplayCityDetails(cities);
            }
            else if (eventType == "Exit")
            {
                break;
            }
            else
            {
                Console.WriteLine("Invalid event");
            }
        }
    }

    static void ProcessEvent(string eventType, int eventCityId, City[] cities)
    {
        if (eventType == "Outbreak")
        {
            cities[eventCityId].InfectionLevel += 2;

            if (cities[eventCityId].InfectionLevel > 3)
            {
                cities[eventCityId].InfectionLevel = 3;
            }

            int contactCityId = cities[eventCityId].ContactCityId;
            cities[contactCityId].InfectionLevel++;
        }
        else if (eventType == "Vaccinate")
        {
            cities[eventCityId].InfectionLevel = 0;
        }
        else if (eventType == "Lockdown")
        {
            cities[eventCityId].InfectionLevel--;
        }
    }

    static void SpreadInfection(City[] cities)
    {
        for (int i = 0; i < cities.Length; i++)
        {
            int contactCityId = cities[i].ContactCityId;

            if (cities[i].InfectionLevel < cities[contactCityId].InfectionLevel)
            {
                cities[i].InfectionLevel++;
            }
        }
    }

    static void[]