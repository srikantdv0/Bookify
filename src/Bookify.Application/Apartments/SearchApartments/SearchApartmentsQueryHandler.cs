using Bookify.Application.Abstractions.Data;
using Bookify.Application.Abstractions.Messaging;
using Bookify.Domain.Abstractions;

namespace Bookify.Application.Apartments.SearchApartments;
internal sealed class SearchApartmentsQueryHandler 
            : IQueryHandler<SearchApartmentsQuery,IReadOnlyList<ApartmentResponse>>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;
    public SearchApartmentsQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }
    public async Task<Result<IReadOnlyList<ApartmentResponse>>> Handle(SearchApartmentsQuery request, CancellationToken cancellationToken)
    {
      if(request.StartDate > request.EndDate)
      {
        return new List<ApartmentResponse>();
      }
      using var connection = _sqlConnectionFactory.CreateConnection();

      const string sql = """
            SELECT
             a.id AS Id,
             a.name AS Name,
             a.description AS Description,
             a.price_amount AS Price,
             a.Price_currency AS Currency,
             a.address_country AS Country,
             a.address_state AS State,
             a.address_zip_code AS ZipCode,
             a.address_city AS City,
             a.address_street AS Street
      """;
    }
}