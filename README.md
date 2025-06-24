# Lunch Roulette MCP Server

A Model Context Protocol (MCP) server for managing lunch restaurant choices and picking random restaurants. This server helps you decide where to eat lunch by maintaining a list of restaurants and tracking visit statistics.

## Features

### Available Tools

1. **GetRestaurants** - List all available restaurants
2. **AddRestaurant** - Add a new restaurant to your choices
3. **PickRandomRestaurant** - Randomly select a restaurant for lunch
4. **GetVisitStatistics** - View statistics about how often you've visited each restaurant

### Pre-loaded Restaurants

The server comes pre-loaded with 10 trendy restaurants from the West Hollywood area:

- **Guelaguetza** - Oaxacan Mexican (3014 W Olympic Blvd)
- **Republique** - French Bistro (624 S La Brea Ave)
- **Night + Market WeHo** - Thai Street Food (9041 Sunset Blvd)
- **Gracias Madre** - Vegan Mexican (8905 Melrose Ave)
- **The Ivy** - Californian (113 N Robertson Blvd)
- **Catch LA** - Seafood (8715 Melrose Ave)
- **Cecconi's** - Italian (8764 Melrose Ave)
- **Earls Kitchen + Bar** - Global Comfort Food (8730 W Sunset Blvd)
- **Pump Restaurant** - Mediterranean (8948 Santa Monica Blvd)
- **Craig's** - American Contemporary (8826 Melrose Ave)

## Setup

### Prerequisites
- .NET 9.0 SDK or later
- MCP-compatible client (VS Code with GitHub Copilot, Claude Desktop, etc.)

### Building
```bash
dotnet build
```

### Running
```bash
dotnet run
```

## Configuration

### VS Code with GitHub Copilot

Add this to your MCP settings:

```json
{
    "inputs": [],
    "servers": {
        "lunchroulette": {
            "type": "stdio",
            "command": "dotnet",
            "args": [
                "run",
                "--project",
                "C:\\GitHub\\LunchRouletteMCP\\LunchTimeMCP\\LunchTimeMCP.csproj"
            ],
            "env": {}
        }
    }
}
```

### Claude Desktop

Add this to your `claude_desktop_config.json`:

```json
{
    "mcpServers": {
        "lunchroulette": {
            "command": "dotnet",
            "args": [
                "run",
                "--project",
                "C:\\GitHub\\LunchRouletteMCP\\LunchTimeMCP\\LunchTimeMCP.csproj"
            ]
        }
    }
}
```

## Usage Examples

### Getting All Restaurants
Use the `GetRestaurants` tool to see all available restaurants in your list.

### Adding a New Restaurant
Use the `AddRestaurant` tool with parameters:
- **name**: The name of the restaurant
- **location**: The address or location
- **foodType**: The type of cuisine (e.g., "Italian", "Mexican", "Thai")

### Picking a Random Restaurant
Use the `PickRandomRestaurant` tool to randomly select a restaurant for lunch. This will also increment the visit counter for that restaurant.

### Checking Visit Statistics
Use the `GetVisitStatistics` tool to see:
- How many times you've visited each restaurant
- Total number of restaurants
- Total number of visits
- Restaurants sorted by visit frequency

## Data Storage

The server stores restaurant data and visit statistics in:
- **Windows**: `%APPDATA%\\LunchTimeMCP\\restaurants.json`
- **macOS/Linux**: `~/.config/LunchTimeMCP/restaurants.json`

This ensures your data persists between server restarts.

## Architecture

The server is built using:
- **.NET 9.0** - Runtime platform
- **Microsoft.Extensions.Hosting** - Application hosting framework
- **ModelContextProtocol** - MCP server implementation
- **System.Text.Json** - JSON serialization

### Key Components

- **RestaurantService** - Core business logic for managing restaurants and visit tracking
- **RestaurantTools** - MCP tool implementations
- **Restaurant/RestaurantVisitInfo** - Data models
- **RestaurantContext** - JSON serialization context

## Example Interactions

1. **"Show me all restaurants"** → Calls `GetRestaurants` tool
2. **"Add a new Italian restaurant called Mario's on Main Street"** → Calls `AddRestaurant` tool
3. **"Pick a random restaurant for lunch"** → Calls `PickRandomRestaurant` tool
4. **"Show me visit statistics"** → Calls `GetVisitStatistics` tool

## Contributing

Feel free to extend the server with additional features like:
- Restaurant ratings
- Cuisine preferences
- Location-based filtering
- Integration with restaurant APIs
- Opening hours tracking
