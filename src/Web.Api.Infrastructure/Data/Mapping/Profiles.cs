using AutoMapper;
using Web.Api.Core.Domain.Entities;
using Web.Api.Infrastructure.Data.Entities;


namespace Web.Api.Infrastructure.Data.Mapping
{
    public class DataProfile : Profile
    {
        public DataProfile()
        {
            // CADASTRAR OS MAPPINGS AQUI
            //CreateMap<Usuario, Aluno().ConstructUsing(uint => new Aluno {});
        }
    }
}
