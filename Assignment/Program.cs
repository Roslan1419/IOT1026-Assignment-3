using System;
using System.Collections.Generic;
using System.Text;

public class InventoryItem
{
    public string Name { get; set; }
    public double Weight { get; set; }
    public double Volume { get; set; }

    public InventoryItem(string name, double weight, double volume)
    {
        Name = name;
        Weight = weight;
        Volume = volume;
    }
}

public class Arrow : InventoryItem
{
    public Arrow() : base("Arrow", 0.1, 0.05)
    {
    }
}

public class Bow : InventoryItem
{
    public Bow() : base("Bow", 1, 4)
    {
    }
}

public class Rope : InventoryItem
{
    public Rope() : base("Rope", 1, 1.5)
    {
    }
}

public class Water : InventoryItem
{
    public Water() : base("Water", 2, 3)
    {
    }
}

public class Food : InventoryItem
{
    public Food() : base("Food", 1, 0.5)
    {
    }
}

public class Sword : InventoryItem
{
    public Sword() : base("Sword", 5, 3)
    {
    }
}

public class Pack
{
    private List<InventoryItem> items;
    public int MaxCount { get; }
    public double MaxVolume { get; }
    public double MaxWeight { get; }

    public Pack(int maxCount, double maxVolume, double maxWeight)
    {
        items = new List<InventoryItem>();
        MaxCount = maxCount;
        MaxVolume = maxVolume;
        MaxWeight = maxWeight;
    }

    public bool AddItem(InventoryItem item)
    {
        if (items.Count >= MaxCount || GetTotalVolume() + item.Volume > MaxVolume || GetTotalWeight() + item.Weight > MaxWeight)
        {
            return false;
        }

        items.Add(item);
        return true;
    }

    public double GetTotalVolume()
    {
        return items.Sum(item => item.Volume);
    }

    public double GetTotalWeight()
    {
        return items.Sum(item => item.Weight);
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine($"Pack: maxCount={MaxCount}, maxVolume={MaxVolume}, maxWeight={MaxWeight}");
        sb.AppendLine("Items:");
        foreach (var item in items)
        {
            sb.AppendLine($"{item.Name}: weight={item.Weight}, volume={item.Volume}");
        }
        return sb.ToString();
    }
}

public static class PackTester
{
    public static void AddEquipment(Pack pack)
    {
        while (true)
        {
            Console.WriteLine("\nChoose an item to add to the pack:");
            Console.WriteLine("1. Arrow");
            Console.WriteLine("2. Bow");
            Console.WriteLine("3. Rope");
            Console.WriteLine("4. Water");
            Console.WriteLine("5. Food");
            Console.WriteLine("6. Sword");
            Console.WriteLine("0. Exit");

            int choice = GetChoice(0, 6);

            if (choice == 0)
            {
                break;
            }

            InventoryItem item = null;

            switch (choice)
            {
                case 1:
                    item = new Arrow();
                    break;
                case 2:
                    item = new Bow();
                    break;
                case 3:
                    item = new Rope();
                    break;
                case 4:
                    item = new Water();
                    break;
                case 5:
                    item = new Food();
                    break;
                case 6:
                    item = new Sword();
                    break;
            }

            if (pack.AddItem(item))
            {
                Console.WriteLine("Item added to the pack.");
            }
            else
            {
                Console.WriteLine("Cannot add item to the pack. Exceeds constraints.");
            }

            Console.WriteLine(pack);
        }
    }

    private static int GetChoice(int min, int max)
    {
        int choice;
        while (!int.TryParse(Console.ReadLine(), out choice) || choice < min || choice > max)
        {
            Console.WriteLine("Invalid choice. Please try again.");
        }
        return choice;
    }
}

class Program
{
    static void Main(string[] args)
    {
        const int PackMaxItems = 10;
        const double PackMaxVolume = 20;
        const double PackMaxWeight = 30;
        Pack pack = new Pack(PackMaxItems, PackMaxVolume, PackMaxWeight);
        PackTester.AddEquipment(pack);

        Console.WriteLine("Program finished. Press any key to exit.");
        Console.ReadKey();
    }
}
