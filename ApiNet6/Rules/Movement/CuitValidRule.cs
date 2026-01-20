using ApiNet6.Models;
using ApiNet6.Rules;
namespace ApiNet6.Rules.Movement;

public class CuitValidRule : IRule<MovementRequest>
{
    public string ErrorMessage => "El CUIT noes valido";
    
    public Task<bool> IsValidAsync(MovementRequest movement)
    {
        // Validar que no esté vacío
        if (string.IsNullOrWhiteSpace(movement.Cuit))
            return Task.FromResult(false);
        
        // Validar que tenga 11 caracteres
        if (movement.Cuit.Length != 11)
            return Task.FromResult(false);
        
        // Validar que solo contenga números
        if (!movement.Cuit.All(char.IsDigit))
            return Task.FromResult(false);
        
        return Task.FromResult(true);
    }
}