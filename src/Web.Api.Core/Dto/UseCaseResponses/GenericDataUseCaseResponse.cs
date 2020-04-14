using System.Collections.Generic;
using Web.Api.Core.Interfaces;

namespace Web.Api.Core.Dto.UseCaseResponses
{
    public class GenericDataUseCaseResponse<T> : UseCaseResponseMessage 
    {
        public T Data { get; }
        public IEnumerable<string> Errors {  get; }

        public GenericDataUseCaseResponse(IEnumerable<string> errors, bool success=false, string message=null) : base(success, message)
        {
            Errors = errors;
        }

        public GenericDataUseCaseResponse(IEnumerable<string> errors, T data, bool success=false, string message=null) : base(success, message)
        {
            Errors = errors;
            Data = data;
        }

        public GenericDataUseCaseResponse(T data, bool success = false, string message = null) : base(success, message)
        {
            Data = data;
        }
    }
}
