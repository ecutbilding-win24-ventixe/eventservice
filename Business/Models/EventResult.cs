namespace Business.Models;

public class EventResult<T> : ServiceResult
{
    public T? Result { get; set; }
}

public class EventResult : ServiceResult
{
}

