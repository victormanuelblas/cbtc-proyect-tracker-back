public class CustomerType
{
    public int CustomerTypeId { get; private set; }
    public string Description { get; private set; } = null!;

    internal CustomerType() {}
}
