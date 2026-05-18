namespace ETL;

public interface ILoader<T>
{
    void Load(IEnumerable<T> items);
}