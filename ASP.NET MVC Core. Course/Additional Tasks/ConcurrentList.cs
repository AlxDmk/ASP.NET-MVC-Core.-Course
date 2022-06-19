namespace ASP.NET_MVC_Core._Course.Additional_Tasks;

public class ConcurrentList<T>: List<T>
{
    private readonly object _locker = new();

    public new void Add(T item)
    {
        lock (_locker)
        {
            base.Add(item);
        }
    }

    public new void Remove(T item)
    {
        lock (_locker)
        {
            base.Remove(item);
        }
    }

    public new void Clear()
    {
        lock (_locker)
        {
            base.Clear();
        }
    }
}