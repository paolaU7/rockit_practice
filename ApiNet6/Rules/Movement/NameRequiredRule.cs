using ApiNet6.Models;
using ApiNet6.Rules;
namespace ApiNet6.Rules.Movement;

public class NameRequiredRule : IRule<MovementRequest>
{
    public string ErrorMessage => "El nombre esta vacio";

    public Task<bool> IsValidAsync(MovementRequest movement)
    {
        string nameStr = movement.Name.ToString();
        return Task.FromResult(!string.IsNullOrWhiteSpace(nameStr));
    }
}

