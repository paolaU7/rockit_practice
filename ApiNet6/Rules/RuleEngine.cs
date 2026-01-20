using System.ComponentModel.DataAnnotations;
using ApiNet6.Models;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;

namespace ApiNet6.Rules;

public class RuleEngine<T>
{
    private readonly List<IRule<T>> _rules = new();

    public void AddRule(IRule<T> rule)
    {
        _rules.Add(rule);
    }
    public async Task<ValidationResult> ValidateAsync(T entity)
    {
        var errors = new List<string>();

        foreach (var rule in _rules)
        {
            if (!await rule.IsValidAsync(entity))
            {
                errors.Add(rule.ErrorMessage);
            }
        }

        return new ValidationResult
        {
            IsValid = errors.Count == 0,
            Errors = errors
        };
    }
}

public class ValidationResult
{
    public bool IsValid { get; set; }
    public List<string> Errors { get; set; } = new();
}