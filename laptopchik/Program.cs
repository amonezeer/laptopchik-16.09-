using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

public abstract class Device
{
    public decimal Price { get; set; }
    public string Manufacturer { get; set; }
    public string Category { get; set; }
    public int YearOfRelease { get; set; }
    public int Warranty { get; set; }
    public string Model { get; set; }

    public Device(decimal price, string manufacturer, string category, int yearOfRelease, int warranty, string model)
    {
        Price = price;
        Manufacturer = manufacturer;
        Category = category;
        YearOfRelease = yearOfRelease;
        Warranty = warranty;
        Model = model;
    }

    public override string ToString()
    {
        return $"{Category} {Model} by {Manufacturer}, Price: {Price:C}, Year: {YearOfRelease}";
    }
}

public class Laptop : Device
{
    public Laptop(decimal price, string manufacturer, int yearOfRelease, int warranty, string model)
        : base(price, manufacturer, "Laptop", yearOfRelease, warranty, model) { }
}

public class Planshet : Device
{
    public Planshet(decimal price, string manufacturer, int yearOfRelease, int warranty, string model)
        : base(price, manufacturer, "planshetik", yearOfRelease, warranty, model) { }
}

public class MobilePhone : Device
{
    public MobilePhone(decimal price, string manufacturer, int yearOfRelease, int warranty, string model)
        : base(price, manufacturer, "Mobile Phone", yearOfRelease, warranty, model) { }
}

public class Charger : Device
{
    public Charger(decimal price, string manufacturer, int yearOfRelease, int warranty, string model)
        : base(price, manufacturer, "Charger", yearOfRelease, warranty, model) { }
}

public class Chehol : Device
{
    public Chehol(decimal price, string manufacturer, int yearOfRelease, int warranty, string model)
        : base(price, manufacturer, "chehol ", yearOfRelease, warranty, model) { }
}
public class Store
{
    private List<Device> devices = new List<Device>();

    public void AddDevice(Device device)
    {
        devices.Add(device);
    }

    public IEnumerable<Device> FindByPriceRange(decimal minPrice, decimal maxPrice)
    {
        return devices.Where(d => d.Price >= minPrice && d.Price <= maxPrice);
    }
    public IEnumerable<Device> this[int year]
    {
        get
        {
            return devices.Where(d => d.YearOfRelease == year);
        }
    }
    public IEnumerable<Device> this[string modelPattern]
    {
        get
        {
            Regex regex = new Regex(modelPattern, RegexOptions.IgnoreCase);
            return devices.Where(d => regex.IsMatch(d.Model));
        }
    }
    public IEnumerable<Device> FindByDeviceType(Type deviceType)
    {
        return devices.Where(d => d.GetType() == deviceType);
    }
}

public class Program
{
    public static void Main()
    {
        Store store = new Store();

        store.AddDevice(new Laptop(60000, "ASUS", 2023, 2, "ROG Strix G16"));
        store.AddDevice(new Laptop(12000, "HP", 2021, 1, "Pavilion 14"));
        store.AddDevice(new Planshet(10000, "Apple", 2019, 1, "iPad Air"));
        store.AddDevice(new MobilePhone(900, "Samsung", 2023, 1, "Galaxy S23"));
        store.AddDevice(new Charger(50, "Baseus", 2022, 1, "Bipow 30000 mAh"));
        store.AddDevice(new Chehol(20, "MagCase", 2022, 1, "Сarbonoviy cheholchik dlya iphone 13 pro max "));

        Console.BackgroundColor = ConsoleColor.DarkGray;
        Console.WriteLine("|-----------------------------------|");
        Console.BackgroundColor = ConsoleColor.DarkGreen;
        Console.WriteLine("\nустройвства в диапазоне 1500-15000:");
        foreach (var device in store.FindByPriceRange(500, 1300))
        {
            Console.WriteLine(device);
        }
        Console.BackgroundColor = ConsoleColor.DarkRed;
        Console.WriteLine("\nустройства по году выпуска 2022:");
        foreach (var device in store[2022])
        {
            Console.WriteLine(device);
        }
        Console.BackgroundColor = ConsoleColor.DarkBlue;
        Console.WriteLine("\nпоиск модели гелекси':");
        foreach (var device in store["Galaxy"])
        {
            Console.WriteLine(device);
        }
        Console.BackgroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine("\nпоиск всех ноутбуков:");
        foreach (var device in store.FindByDeviceType(typeof(Laptop)))
        {
            Console.WriteLine(device);
        }
        Console.BackgroundColor = ConsoleColor.DarkGray;
        Console.WriteLine("\n|-----------------------------------|");
    }
}
