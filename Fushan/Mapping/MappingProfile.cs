using AutoMapper;
using DataServices.Model;
using Messages;
using Messages.Account;

namespace Fushan.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<string, string>().ConvertUsing<NullStringConverter>();
            // Domain to Resource
            //CreateMap<Music, MusicResource>();
            //CreateMap<Artist, ArtistResource>();

            //// Resource to Domain
            //CreateMap<MusicResource, Music>();
            //CreateMap<SaveMusicResource, Music>();
            //CreateMap<ArtistResource, Artist>();
            CreateMap<CreateUpdateDepartmentRequest, Department>();
            CreateMap<RegistrationRequest, AppUser>()
                .ForMember(u => u.UserName, opt => opt.MapFrom(ur => ur.Email));
            CreateMap<AppUser, AppUserModel>()
                .ForMember(u => u.SexString, opt => opt.MapFrom(ur => ur.Sex))
                .ForMember(u => u.Sex, opt => opt.MapFrom(ur => (int)ur.Sex));

        }

        public static MapperConfiguration Config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        });

        public class NullStringConverter : ITypeConverter<string, string>
        {
            public string Convert(string source)
            {
                return source ?? string.Empty;
            }

            string ITypeConverter<string, string>.Convert(string source, string destination, ResolutionContext context)
            {
                return source ?? string.Empty;
            }
        }
    }
}
