using System.Linq;
using InventoryСontrol.Application.CQRS.Items.Views;

namespace InventoryСontrol.Application.Mapper.Item
{
    public sealed class RegisterViews : AutoMapper.Profile
    {
        public RegisterViews()
        {
            CreateMap<Domain.Item, ItemView>()
                .ForMember(dest => dest.Categories,
                    dest => dest.
                        MapFrom(src => src.Categories
                            .Select(i => i.Category.Name)
                            .ToArray()));
        }
    }
}