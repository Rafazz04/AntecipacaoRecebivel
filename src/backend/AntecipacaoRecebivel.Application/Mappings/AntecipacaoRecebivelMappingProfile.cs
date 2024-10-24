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

        CreateMap<CarrinhoCreateResponseDTO, Carrinho>().ReverseMap();
        CreateMap<CarrinhoCheckoutReadResponseDTO, Carrinho>().ReverseMap();
    }
}
