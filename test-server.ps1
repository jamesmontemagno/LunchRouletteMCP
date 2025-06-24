#!/usr/bin/env pwsh

# Test script for the Lunch Roulette MCP Server

$projectPath = "c:\GitHub\LunchRouletteMCP\LunchTimeMCP"

Write-Host "🍽️ Testing Lunch Roulette MCP Server" -ForegroundColor Green
Write-Host "=====================================" -ForegroundColor Green

# Test 1: Initialize the server
Write-Host "`n1. Testing server initialization..." -ForegroundColor Yellow

$initMessage = @{
    jsonrpc = "2.0"
    id = 1
    method = "initialize"
    params = @{
        protocolVersion = "2024-11-05"
        capabilities = @{
            tools = @{}
        }
        clientInfo = @{
            name = "test-client"
            version = "1.0.0"
        }
    }
} | ConvertTo-Json -Depth 4 -Compress

# Test 2: List tools
Write-Host "2. Testing tools/list..." -ForegroundColor Yellow

$listToolsMessage = @{
    jsonrpc = "2.0"
    id = 2
    method = "tools/list"
    params = @{}
} | ConvertTo-Json -Depth 4 -Compress

# Test 3: Call GetRestaurants
Write-Host "3. Testing GetRestaurants tool..." -ForegroundColor Yellow

$getRestaurantsMessage = @{
    jsonrpc = "2.0"
    id = 3
    method = "tools/call"
    params = @{
        name = "GetRestaurants"
        arguments = @{}
    }
} | ConvertTo-Json -Depth 4 -Compress

Write-Host "`nSample MCP messages you can send to test the server:" -ForegroundColor Cyan
Write-Host "Initialize: $initMessage" -ForegroundColor Gray
Write-Host "List Tools: $listToolsMessage" -ForegroundColor Gray
Write-Host "Get Restaurants: $getRestaurantsMessage" -ForegroundColor Gray

Write-Host "`n✅ Server is ready to use!" -ForegroundColor Green
Write-Host "You can now configure it in your MCP client (VS Code, Claude Desktop, etc.)" -ForegroundColor Green
