using AntecipacaoRecebivel.Application.DTOs.CarrinhoDTO.Create;
using AntecipacaoRecebivel.Application.DTOs.CarrinhoDTO.Read;
using AntecipacaoRecebivel.Application.DTOs.EmpresaDTO.Create;
using AntecipacaoRecebivel.Application.DTOs.NotaFiscalDTO.Create;
using AntecipacaoRecebivel.Domain.Entities;
using AutoMapper;

namespace AntecipacaoRecebivel.Application.Mappings;

public class AntecipacaoRecebivelMappingProfile : Profile
{
    public AntecipacaoRecebivelMappingProfile()
    {
        CreateMap<EmpresaCreateDTO, Empresa>().ReverseMap();

        CreateMap<NotaFiscalCreateRequestDTO, NotaFiscal>().ReverseMap();
        CreateMap<NotaFiscalCreateResponseDTO, NotaFiscal>().ReverseMap();

        CreateMap<Carrinho, CarrinhoCreateResponseDTO>()
            .ForMember(dest => dest.Cnpj, opt => opt.MapFrom(src => src.Empresa.Cnpj))
            .ForMember(dest => dest.LimiteCredito, opt => opt.MapFrom(src => src.LimiteDeCreditoDisponivel));

        CreateMap<CarrinhoCheckoutReadResponseDTO, Carrinho>().ReverseMap()
            .ForMember(dest => dest.Limite, opt => opt.MapFrom(src => src.LimiteDeCreditoDisponivel));
    }
}
