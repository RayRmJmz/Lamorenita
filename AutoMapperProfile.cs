using AutoMapper;
using Lamorenita.Data_Entities;
using Lamorenita.Models;
using Microsoft.AspNetCore.Identity;

namespace Lamorenita
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            SetUserMappings();
            SetRoleMappings();
            //Product type
            CreateMap<ProductTypeEntity, ProductTypeViewModel>()
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Descripcion, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.FechaCreacion, opt => opt.MapFrom(src => src.Created))
                .ForMember(dest => dest.Activo, opt => opt.MapFrom(src => src.Active));

            CreateMap<ProductTypeCreateModel, ProductTypeEntity>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Nombre))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Descripcion));

            //Product
            CreateMap<ProductEntity, ProductViewModel>()
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.TipoProductoId, opt => opt.MapFrom(src => src.ProductTypeId))
                .ForMember(dest => dest.Descripcion, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Precio, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.Existencia, opt => opt.MapFrom(src => src.Stock))
                .ForMember(dest => dest.FechaCreacion, opt => opt.MapFrom(src => src.Created))
                .ForMember(dest => dest.Activo, opt => opt.MapFrom(src => src.Active))
                .ForMember(dest => dest.TipoProducto, opt => opt.MapFrom(src => src.ProductType.Name));


            CreateMap<ProductCreateModel, ProductEntity>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Nombre))
                .ForMember(dest => dest.ProductTypeId, opt => opt.MapFrom(src => src.TipoProductoId))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Descripcion))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Precio))
                .ForMember(dest => dest.Stock, opt => opt.MapFrom(src => src.Existencia));

            //Contact
            CreateMap<ContactEntity, ContactViewModel>()
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.PrimerApellido, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.SegundoApellido, opt => opt.MapFrom(src => src.SecondLastName))
                .ForMember(dest => dest.FechaCreacion, opt => opt.MapFrom(src => src.Created))
                .ForMember(dest => dest.Activo, opt => opt.MapFrom(src => src.Active));

            CreateMap<ContactCreateModel, ContactEntity>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Nombre))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.PrimerApellido))
                .ForMember(dest => dest.SecondLastName, opt => opt.MapFrom(src => src.SegundoApellido))
                .ForMember(dest => dest.Active, opt => opt.MapFrom(src => src.activo));

            //Store
            CreateMap<StoreEntity, StoreViewModel>()
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Descripcion, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.FechaCreacion, opt => opt.MapFrom(src => src.Created))
                .ForMember(dest => dest.Activo, opt => opt.MapFrom(src => src.Active));

            CreateMap<StoreCreateModel, StoreEntity>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Nombre))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Descripcion));

            //Direction
            CreateMap<DirectionEntity, DirectionViewModel>()
                .ForMember(dest => dest.Calle, opt => opt.MapFrom(src => src.StreetName))
                .ForMember(dest => dest.Numero, opt => opt.MapFrom(src => src.Number))
                .ForMember(dest => dest.Colonia, opt => opt.MapFrom(src => src.Colony))
                .ForMember(dest => dest.Municipio, opt => opt.MapFrom(src => src.Municipality))
                .ForMember(dest => dest.FechaCreacion, opt => opt.MapFrom(src => src.Created))
                .ForMember(dest => dest.Activo, opt => opt.MapFrom(src => src.Active));

            CreateMap<DirectionCreateModel, DirectionEntity>()
                .ForMember(dest => dest.StreetName, opt => opt.MapFrom(src => src.Calle))
                .ForMember(dest => dest.Number, opt => opt.MapFrom(src => src.Numero))
                .ForMember(dest => dest.Colony, opt => opt.MapFrom(src => src.Colonia))
                .ForMember(dest => dest.Municipality, opt => opt.MapFrom(src => src.Municipio));


            //Phone number
            CreateMap<PhoneNumberEntity, PhoneNumberViewModel>()
                .ForMember(dest => dest.NumeroTelefono, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.ContactoId, opt => opt.MapFrom(src => src.ContactId))
                .ForMember(dest => dest.FechaCreacion, opt => opt.MapFrom(src => src.Created));

            CreateMap<PhoneNumberCreateModel, PhoneNumberEntity>()
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.NumeroTelefono))
                .ForMember(dest => dest.ContactId, opt => opt.MapFrom(src => src.ContactoId));

        }


        private void SetRoleMappings()
        {
            CreateMap<IdentityRole, RoleViewModel>();
            CreateMap<IdentityRole, RoleSelectedModel>();
            CreateMap<IdentityRole, RoleUserViewModel>()
                .AfterMap((src, dest) =>
                {
                    dest.Selected = true;
                });
        }
        private void SetUserMappings()
        {
            CreateMap<ApplicationUser, UserViewModel>()
                .ForMember(dest => dest.Active, opt => opt.MapFrom(src => !src.LockoutEnabled));
            CreateMap<ApplicationUser, UserEditModel>();
            CreateMap<UserRegisterModel, ApplicationUser>();
            CreateMap<UserEditModel, ApplicationUser>();
            CreateMap<ApplicationUser, UserActiveModel>()
                .ForMember(dest => dest.Active, opt => opt.MapFrom(src => !src.LockoutEnabled));
            CreateMap<ApplicationUser, UserFullViewModel>()
                .ForMember(dest => dest.Active, opt => opt.MapFrom(src => !src.LockoutEnabled))
                .AfterMap((src, dest) =>
                {
                    if (dest.Roles != null)
                    {
                        foreach (var role in dest.Roles)
                        {
                            role.Selected = true;
                        }
                    }
                });
        }
    }
}
