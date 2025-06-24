# Lunch Roulette MCP Server Examples

This document provides examples of how to interact with the Lunch Roulette MCP server.

## Tool Examples

### 1. GetRestaurants

**Purpose**: Get a list of all available restaurants

**MCP Call**:
```json
{
    "jsonrpc": "2.0",
    "id": 1,
    "method": "tools/call",
    "params": {
        "name": "GetRestaurants",
        "arguments": {}
    }
}
```

**Example Response**:
```json
[
    {
        "id": "abc123",
        "name": "Guelaguetza",
        "location": "3014 W Olympic Blvd",
        "foodType": "Oaxacan Mexican",
        "dateAdded": "2025-06-24T12:00:00Z"
    },
    {
        "id": "def456",
        "name": "Republique",
        "location": "624 S La Brea Ave",
        "foodType": "French Bistro",
        "dateAdded": "2025-06-24T12:00:00Z"
    }
]
```

### 2. AddRestaurant

**Purpose**: Add a new restaurant to your lunch options

**MCP Call**:
```json
{
    "jsonrpc": "2.0",
    "id": 2,
    "method": "tools/call",
    "params": {
        "name": "AddRestaurant",
        "arguments": {
            "name": "Mario's Pizza",
            "location": "456 Main Street",
            "foodType": "Italian"
        }
    }
}
```

**Example Response**:
```json
{
    "id": "ghi789",
    "name": "Mario's Pizza",
    "location": "456 Main Street",
    "foodType": "Italian",
    "dateAdded": "2025-06-24T14:30:00Z"
}
```

### 3. PickRandomRestaurant

**Purpose**: Randomly select a restaurant for lunch (and track the visit)

**MCP Call**:
```json
{
    "jsonrpc": "2.0",
    "id": 3,
    "method": "tools/call",
    "params": {
        "name": "PickRandomRestaurant",
        "arguments": {}
    }
}
```

**Example Response**:
```json
{
    "message": "🍽️ Time for lunch at Night + Market WeHo!",
    "restaurant": {
        "id": "def456",
        "name": "Night + Market WeHo",
        "location": "9041 Sunset Blvd",
        "foodType": "Thai Street Food",
        "dateAdded": "2025-06-24T12:00:00Z"
    }
}
```

### 4. GetVisitStatistics

**Purpose**: View statistics about restaurant visits

**MCP Call**:
```json
{
    "jsonrpc": "2.0",
    "id": 4,
    "method": "tools/call",
    "params": {
        "name": "GetVisitStatistics",
        "arguments": {}
    }
}
```

**Example Response**:
```json
{
    "message": "Restaurant visit statistics:",
    "statistics": [
        {
            "restaurant": "Night + Market WeHo",
            "location": "9041 Sunset Blvd",
            "foodType": "Thai Street Food",
            "visitCount": 3,
            "timesEaten": "3 times"
        },
        {
            "restaurant": "Guelaguetza",
            "location": "3014 W Olympic Blvd",
            "foodType": "Oaxacan Mexican",
            "visitCount": 2,
            "timesEaten": "Twice"
        },
        {
            "restaurant": "Mario's Pizza",
            "location": "456 Main Street",
            "foodType": "Italian",
            "visitCount": 0,
            "timesEaten": "Never"
        }
    ],
    "totalRestaurants": 11,
    "totalVisits": 5
}
```

## Natural Language Examples

When using with GitHub Copilot or Claude, you can use natural language:

### Adding Restaurants
- "Add a new Thai restaurant called Spicy Basil on 5th Avenue"
- "I want to add Mario's Italian Kitchen at 123 Oak Street to my lunch options"

### Getting Information
- "Show me all the restaurants I can choose from"
- "What restaurants do I have available for lunch?"

### Picking Restaurants
- "Pick a random restaurant for lunch"
- "Choose where I should eat today"
- "Surprise me with a lunch spot"

### Statistics
- "Show me which restaurants I visit most often"
- "What are my lunch statistics?"
- "Which places haven't I been to yet?"

## Integration with AI Assistants

The MCP server works seamlessly with AI assistants. You can have conversations like:

**You**: "I'm craving Mexican food today, what options do I have?"

**AI**: *Uses GetRestaurants tool, filters results*
"I found these Mexican restaurants in your list:
- Guelaguetza (Oaxacan Mexican) at 3014 W Olympic Blvd
- Gracias Madre (Vegan Mexican) at 8905 Melrose Ave

Would you like me to pick one randomly or would you prefer to choose?"

**You**: "Add a new sushi place called Yamashiro on Hollywood Boulevard"

**AI**: *Uses AddRestaurant tool*
"I've added Yamashiro (Japanese) at Hollywood Boulevard to your restaurant list!"

**You**: "I can't decide where to eat, pick something for me"

**AI**: *Uses PickRandomRestaurant tool*
"🍽️ Time for lunch at Catch LA! It's a seafood restaurant at 8715 Melrose Ave. Enjoy your meal!"
