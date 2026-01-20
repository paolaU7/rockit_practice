namespace ApiNet6.Rules;

public interface IRule<T>
{
    Task<bool> IsValidAsync(T entity);

    string ErrorMessage {get;}
}