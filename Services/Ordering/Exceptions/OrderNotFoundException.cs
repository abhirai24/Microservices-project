namespace Ordering.Exceptions
{
    public class OrderNotFoundException
    {
        public OrderNotFoundException(string name, Object key) : base($"Entity {name} - {key} is not found.")
        {

        }
    }
}
