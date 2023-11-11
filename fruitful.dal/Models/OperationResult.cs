namespace fruitful.dal.Models;

public class OperationResult<T>
{
    public bool Success { get; set; }
    public T Data { get; set; }
}