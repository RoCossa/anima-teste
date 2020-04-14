using System.Collections.Generic;

namespace Web.Api.Core.Dto.GatewayResponses.Repositories
{
  public sealed class DbGenericResponse<T> : BaseGatewayResponse
  {
    public T Data { get; }
    public DbGenericResponse(T data, bool success = false, IEnumerable<Error> errors = null) : base(success, errors)
    {
      Data = data;
    }
  }
}
