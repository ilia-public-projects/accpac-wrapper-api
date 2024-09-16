namespace Wrapper.Models
{
    public interface IBatchModel<T> where T : class
    {
        List<T> Headers { get; }
    }
}
