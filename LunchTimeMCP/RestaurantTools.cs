using System.ComponentModel;
using System.Text.Json;
using ModelContextProtocol.Server;

namespace LunchTimeMCP;

[McpServerToolType]
public sealed class RestaurantTools
{
    private readonly RestaurantService restaurantService;

    public RestaurantTools(RestaurantService restaurantService)
    {
        this.restaurantService = restaurantService;
    }

    [McpServerTool, Description("Get a list of all restaurants available for lunch.")]
    public async Task<string> GetRestaurants()
    {
        var restaurants = await restaurantService.GetRestaurantsAsync();
        return JsonSerializer.Serialize(restaurants, RestaurantContext.Default.ListRestaurant);
    }

    [McpServerTool, Description("Add a new restaurant to the lunch options.")]
    public async Task<string> AddRestaurant(
        [Description("The name of the restaurant")] string name,
        [Description("The location/address of the restaurant")] string location,
        [Description("The type of food served (e.g., Italian, Mexican, Thai, etc.)")] string foodType)
    {
        var restaurant = await restaurantService.AddRestaurantAsync(name, location, foodType);
        return JsonSerializer.Serialize(restaurant, RestaurantContext.Default.Restaurant);
    }

    [McpServerTool, Description("Pick a random restaurant from the available options for lunch.")]
    public async Task<string> PickRandomRestaurant()
    {
        var selectedRestaurant = await restaurantService.PickRandomRestaurantAsync();
        
        if (selectedRestaurant == null)
        {
            return JsonSerializer.Serialize(new { message = "No restaurants available. Please add some restaurants first!" });
        }

        return JsonSerializer.Serialize(new { 
            message = $"🍽️ Time for lunch at {selectedRestaurant.Name}!",
            restaurant = selectedRestaurant 
        });
    }

    [McpServerTool, Description("Get statistics about how many times each restaurant has been visited.")]
    public async Task<string> GetVisitStatistics()
    {
        var stats = await restaurantService.GetVisitStatsAsync();
        
        var formattedStats = stats.Values
            .OrderByDescending(x => x.VisitCount)
            .Select(stat => new {
                restaurant = stat.Restaurant.Name,
                location = stat.Restaurant.Location,
                foodType = stat.Restaurant.FoodType,
                visitCount = stat.VisitCount,
                timesEaten = stat.VisitCount == 0 ? "Never" : 
                            stat.VisitCount == 1 ? "Once" : 
                            $"{stat.VisitCount} times"
            })
            .ToList();

        return JsonSerializer.Serialize(new {
            message = "Restaurant visit statistics:",
            statistics = formattedStats,
            totalRestaurants = stats.Count,
            totalVisits = stats.Values.Sum(x => x.VisitCount)
        });
    }
}
