using AutoMapper;
using PastryShopAPI;
using PastryShopAPI.Data.Entities;
using PastryShopAPI.Models;
using PastryShopAPI.Models.Combos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PastryShopAPI.Data
{
    public class AtomapperProfile: Profile
    {
        public AtomapperProfile()
        {
            this.CreateMap<CategoryModel, CategoryEntity>()
                .ForMember(tm => tm.Name, te => te.MapFrom(m => m.Name))
                .ReverseMap();

            this.CreateMap<ProductModel, ProductEntity>()
                .ForMember(ent => ent.Category, mod => mod.MapFrom(modSrc => new CategoryEntity() { Id = modSrc.CategoryId }))
                .ReverseMap()
                .ForMember(mod => mod.CategoryId, ent => ent.MapFrom(entSrc => entSrc.Category.Id));

            // COMBO
            this.CreateMap<ComboModel, ComboEntity>()
               .ForMember(tm => tm.Name, te => te.MapFrom(m => m.Name))
               .ReverseMap();

            this.CreateMap<Product_ComboModel, Product_ComboEntity>()
               //.ForMember(tm => tm.Name, te => te.MapFrom(m => m.Name))
               .ReverseMap();

            /*this.CreateMap<TeamWithPlayerModel, TeamEntity>()
                .ReverseMap();*/

            /* BAD MAPPING: this.CreateMap<ProductModel, ProductEntity>()
                .ForMember(ent => ent.Category, mod => mod.MapFrom(modSrc => new CategoryEntity() { Id = modSrc.Id }))
                .ReverseMap()
                .ForMember(mod => mod.Id, ent => ent.MapFrom(entSrc => entSrc.Category.Id));// mod.CategoryId, ...*/



        }
    }
}
