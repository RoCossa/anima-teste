using System.Threading.Tasks;

namespace Web.Api.Core.Interfaces
{
    public interface IUseCaseRequestHandler<in TUseCaseRequest, out TUseCaseResponse> where TUseCaseRequest : IUseCaseRequest<TUseCaseResponse>
    {
        Task<bool> Handle(TUseCaseRequest message, IOutputPort<TUseCaseResponse> outputPort);
    }

    public interface IUseCaseHandler<in TUseCaseRequest, out TUseCaseResponse> where TUseCaseRequest : IUseCaseRequest<TUseCaseResponse>
    {
        bool Handle(TUseCaseRequest message, IOutputPort<TUseCaseResponse> outputPort);
    }
}
