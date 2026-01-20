using System.Text.Json;
using ApiNet6.Models;

namespace ApiNet6.Services;

public class RockitService
{
    public MovementRequest StringToObject(string rawData)
    {
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        
        var movement = JsonSerializer.Deserialize<MovementRequest>(rawData, options);
        
        return movement!;
    }
}