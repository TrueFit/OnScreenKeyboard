using AutoMapper;
using Model = Keyboard.Common.Models;
using Keyboard.Domain;

namespace Keyboard.Business.Logic
{
    public static class MappingHandler
    {
        public static IMapper BuildMapper()
        {
            var mapConfiguration = new MapperConfiguration(cfg => {
                cfg.CreateMap<DvrKey, Model.Key>();
                cfg.CreateMap<DvrKeyboard, Model.Keyboard>()
                   .ForMember(obj => obj.Keys, opts => opts.MapFrom(src => src.DvrKeys))
                   .ForMember(obj => obj.Type, opts => opts.MapFrom(src => src.Type));
            });
            return mapConfiguration.CreateMapper();
        }
    }
}