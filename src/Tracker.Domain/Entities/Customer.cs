namespace Tracker.Domain.Entities;

public class Customer
{
    public int CustomerId { get; private set; }
    public string Name { get; private set; } = null!;
    public string DocmNumber{ get; private set; } = null!;
    public string? Email { get; private set; }
    public string? Phone { get; private set; }
    public int CustomerTypeId { get; private set; }
    public CustomerType CustomerType { get; private set; }

    protected Customer() { }

    public Customer(string name, string docmNumber, string email, string? phone, CustomerType customerType)
    {
        Name = name;
        DocmNumber = docmNumber;
        Email = email;
        Phone = phone;
        CustomerType = customerType;
        CustomerTypeId = customerType?.CustomerTypeId ?? 0;
    }
}
