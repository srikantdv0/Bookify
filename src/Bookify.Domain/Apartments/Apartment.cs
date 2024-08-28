using  Bookify.Domain.Abstractions;
namespace Bookify.Domain.Apartments;

public sealed class Apartments : Entity
{
    public Apartments(
        Guid id,
        Name name,
        Description description,
        Address address,
        Money price,
        Money cleaningfee,
        List<Amenity> amenities
        ) : base(id)
    {
        Name = name;
        Description = description;
        Address = address;
        Price = price;
        CleaningFee = cleaningfee;
        Amenities = amenities;
    }
    public Name Name {get;private set;}
    public Description Description {get;private set;}
    public Address Address {get; private set;}
    public Money Price {get; private set;}
    public Money CleaningFee {get; private set;}
    public DateTime? LastBookedOnUTC {get;private set;}
    public List<Amenity> Amenities{get;private set;} = new();

}