namespace ETL;

public interface IExtractor<T>
{
    IEnumerable<T> Extract();
}