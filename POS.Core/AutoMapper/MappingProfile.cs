using AutoMapper;
using POS.Core.Models.InventoryDTOs;
using POS.Core.Models.RoleDTOs;
using POS.Core.Models.TaxDTOs;
using POS.Core.Models.UserDTOs;
using POS.Core.Models;
using POS.Core.Models.ProductDTOs;
using POS.Core.Dtos.BarcodeDTOs;
using POS.Core.Dtos.DiscountDTOs;
using POS.Core.Dtos.SaleItemDTOs;
using POS.Core.Dtos.SaleDTOs;

namespace POS.Core.AutoMapper
{
   

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //UserDto Mapping
            CreateMap<User, UserDto>();
            CreateMap<UserCreateDto, User>();
            CreateMap<UserUpdateDto, User>();

            //ProductDtos Mapping
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Product, ProductCreateDto>().ReverseMap();
            CreateMap<ProductUpdateDto, Product>();

            //RoleDtos Mapping
            CreateMap<Role, RoleDto>();
            CreateMap<RoleCreateDto, Role>();
            CreateMap<RoleUpdateDto, Role>();

            //Inventory Mapping
            CreateMap<Inventory, InventoryDto>();
            CreateMap<InventoryCreateDto, Inventory>();
            CreateMap<InventoryUpdateDto, Inventory>();

            //Tax Mapping
            CreateMap<Tax, TaxDto>();
            CreateMap<TaxCreateDto, Tax>();
            CreateMap<TaxUpdateDto, Tax>();

            //Barcode Mapping
            CreateMap<Barcode, BarcodeDto>();
            CreateMap<BarcodeCreateDto, Barcode>();
            CreateMap<BarcodeUpdateDto, Barcode>();

            //Discount Mapping
            CreateMap<Discount, DiscountDto>();
            CreateMap<DiscountCreateDto, Discount>();
            CreateMap<DiscountUpdateDto, Discount>();

            //SaleItem Mapping
            CreateMap<SaleItem, SaleItemDto>();
            CreateMap<SaleItemCreateDto, SaleItem>();
            CreateMap<SaleItemUpdateDto, SaleItem>();

            //Sale Mapping
            CreateMap<Sale, SaleDto>().ReverseMap();
            CreateMap<Sale, SaleCreateDto>().ReverseMap();
        }
    }
}
