using Microsoft.AspNetCore.Mvc;
using ApiNet6.Services;

namespace ApiNet6.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HarmonizedController : ControllerBase
{
    private readonly HarmonizedService _harmonized;

    public HarmonizedController(HarmonizedService harmonized)
    {
        _harmonized = harmonized;
    }

    [HttpPost("send")]
    public async Task<IActionResult> Send()  
    {
        try
        {
            using var reader = new StreamReader(Request.Body);
            var jsonString = await reader.ReadToEndAsync();
            
            var ticket = await _harmonized.SendStringAsync(jsonString);
            
            return Ok(new 
            { 
                message = "Ticket guardado exitosamente en BD",
                data = ticket 
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new 
            { 
                message = "Error al procesar el ticket", 
                error = ex.Message 
            });
        }
    }
}