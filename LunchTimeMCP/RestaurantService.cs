using System.Text.Json;
using System.Text.Json.Serialization;

namespace LunchTimeMCP;

public class RestaurantService
{
    private readonly string dataFilePath;
    private List<Restaurant> restaurants = new();
    private Dictionary<string, int> visitCounts = new();

    public RestaurantService()
    {
        // Store data in user's app data directory
        var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        var appDir = Path.Combine(appDataPath, "LunchTimeMCP");
        Directory.CreateDirectory(appDir);
        
        dataFilePath = Path.Combine(appDir, "restaurants.json");
        LoadData();
        
        // Initialize with trendy West Hollywood restaurants if empty
        if (restaurants.Count == 0)
        {
            InitializeWithTrendyRestaurants();
            SaveData();
        }
    }

    public async Task<List<Restaurant>> GetRestaurantsAsync()
    {
        return await Task.FromResult(restaurants.ToList());
    }

    public async Task<Restaurant> AddRestaurantAsync(string name, string location, string foodType)
    {
        var restaurant = new Restaurant
        {
            Id = Guid.NewGuid().ToString(),
            Name = name,
            Location = location,
            FoodType = foodType,
            DateAdded = DateTime.UtcNow
        };

        restaurants.Add(restaurant);
        SaveData();
        
        return await Task.FromResult(restaurant);
    }

    public async Task<Restaurant?> PickRandomRestaurantAsync()
    {
        if (restaurants.Count == 0)
            return null;

        var random = new Random();
        var selectedRestaurant = restaurants[random.Next(restaurants.Count)];
        
        // Track the visit
        if (visitCounts.ContainsKey(selectedRestaurant.Id))
            visitCounts[selectedRestaurant.Id]++;
        else
            visitCounts[selectedRestaurant.Id] = 1;

        SaveData();
        
        return await Task.FromResult(selectedRestaurant);
    }

    public async Task<Dictionary<string, RestaurantVisitInfo>> GetVisitStatsAsync()
    {
        var stats = new Dictionary<string, RestaurantVisitInfo>();
        
        foreach (var restaurant in restaurants)
        {
            var visitCount = visitCounts.GetValueOrDefault(restaurant.Id, 0);
            stats[restaurant.Name] = new RestaurantVisitInfo
            {
                Restaurant = restaurant,
                VisitCount = visitCount,
                LastVisited = visitCount > 0 ? DateTime.UtcNow : null // In a real app, you'd track actual visit dates
            };
        }

        return await Task.FromResult(stats);
    }

    public async Task<FormattedRestaurantStats> GetFormattedVisitStatsAsync()
    {
        var stats = await GetVisitStatsAsync();
        
        var formattedStats = stats.Values
            .OrderByDescending(x => x.VisitCount)
            .Select(stat => new FormattedRestaurantStat
            {
                Restaurant = stat.Restaurant.Name,
                Location = stat.Restaurant.Location,
                FoodType = stat.Restaurant.FoodType,
                VisitCount = stat.VisitCount,
                TimesEaten = stat.VisitCount == 0 ? "Never" : 
                            stat.VisitCount == 1 ? "Once" : 
                            $"{stat.VisitCount} times"
            })
            .ToList();

        return new FormattedRestaurantStats
        {
            Message = "Restaurant visit statistics:",
            Statistics = formattedStats,
            TotalRestaurants = stats.Count,
            TotalVisits = stats.Values.Sum(x => x.VisitCount)
        };
    }

    private void LoadData()
    {
        if (!File.Exists(dataFilePath))
            return;

        try
        {
            var json = File.ReadAllText(dataFilePath);
            var data = JsonSerializer.Deserialize<RestaurantData>(json, RestaurantContext.Default.RestaurantData);
            
            if (data != null)
            {
                restaurants = data.Restaurants ?? new List<Restaurant>();
                visitCounts = data.VisitCounts ?? new Dictionary<string, int>();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading data: {ex.Message}");
        }
    }

    private void SaveData()
    {
        try
        {
            var data = new RestaurantData
            {
                Restaurants = restaurants,
                VisitCounts = visitCounts
            };
            
            var json = JsonSerializer.Serialize(data, RestaurantContext.Default.RestaurantData);
            File.WriteAllText(dataFilePath, json);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving data: {ex.Message}");
        }
    }

    private void InitializeWithTrendyRestaurants()
    {
        var trendyRestaurants = new List<Restaurant>
        {
            new() { Id = Guid.NewGuid().ToString(), Name = "Guelaguetza", Location = "3014 W Olympic Blvd", FoodType = "Oaxacan Mexican", DateAdded = DateTime.UtcNow },
            new() { Id = Guid.NewGuid().ToString(), Name = "Republique", Location = "624 S La Brea Ave", FoodType = "French Bistro", DateAdded = DateTime.UtcNow },
            new() { Id = Guid.NewGuid().ToString(), Name = "Night + Market WeHo", Location = "9041 Sunset Blvd", FoodType = "Thai Street Food", DateAdded = DateTime.UtcNow },
            new() { Id = Guid.NewGuid().ToString(), Name = "Gracias Madre", Location = "8905 Melrose Ave", FoodType = "Vegan Mexican", DateAdded = DateTime.UtcNow },
            new() { Id = Guid.NewGuid().ToString(), Name = "The Ivy", Location = "113 N Robertson Blvd", FoodType = "Californian", DateAdded = DateTime.UtcNow },
            new() { Id = Guid.NewGuid().ToString(), Name = "Catch LA", Location = "8715 Melrose Ave", FoodType = "Seafood", DateAdded = DateTime.UtcNow },
            new() { Id = Guid.NewGuid().ToString(), Name = "Cecconi's", Location = "8764 Melrose Ave", FoodType = "Italian", DateAdded = DateTime.UtcNow },
            new() { Id = Guid.NewGuid().ToString(), Name = "Earls Kitchen + Bar", Location = "8730 W Sunset Blvd", FoodType = "Global Comfort Food", DateAdded = DateTime.UtcNow },
            new() { Id = Guid.NewGuid().ToString(), Name = "Pump Restaurant", Location = "8948 Santa Monica Blvd", FoodType = "Mediterranean", DateAdded = DateTime.UtcNow },
            new() { Id = Guid.NewGuid().ToString(), Name = "Craig's", Location = "8826 Melrose Ave", FoodType = "American Contemporary", DateAdded = DateTime.UtcNow }
        };

        restaurants.AddRange(trendyRestaurants);
    }
}

public partial class Restaurant
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public string FoodType { get; set; } = string.Empty;
    public DateTime DateAdded { get; set; }
}

public partial class RestaurantVisitInfo
{
    public Restaurant Restaurant { get; set; } = new();
    public int VisitCount { get; set; }
    public DateTime? LastVisited { get; set; }
}

public partial class RestaurantData
{
    public List<Restaurant> Restaurants { get; set; } = new();
    public Dictionary<string, int> VisitCounts { get; set; } = new();
}

public class FormattedRestaurantStat
{
    public string Restaurant { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public string FoodType { get; set; } = string.Empty;
    public int VisitCount { get; set; }
    public string TimesEaten { get; set; } = string.Empty;
}

public class FormattedRestaurantStats
{
    public string Message { get; set; } = string.Empty;
    public List<FormattedRestaurantStat> Statistics { get; set; } = new();
    public int TotalRestaurants { get; set; }
    public int TotalVisits { get; set; }
}

[JsonSerializable(typeof(List<Restaurant>))]
[JsonSerializable(typeof(Restaurant))]
[JsonSerializable(typeof(RestaurantVisitInfo))]
[JsonSerializable(typeof(Dictionary<string, RestaurantVisitInfo>))]
[JsonSerializable(typeof(RestaurantData))]
[JsonSerializable(typeof(FormattedRestaurantStat))]
[JsonSerializable(typeof(FormattedRestaurantStats))]
internal sealed partial class RestaurantContext : JsonSerializerContext
{
}
