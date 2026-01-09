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
        
        var jsonString = JsonSerializer.Deserialize<string>(rawData, options);
        var movement = JsonSerializer.Deserialize<MovementRequest>(jsonString!, options);
        
        return movement!;
    }
}