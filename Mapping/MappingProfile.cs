using AutoMapper;
using DataServices.Model;
using Messages;

namespace Fushan.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Domain to Resource
            //CreateMap<Music, MusicResource>();
            //CreateMap<Artist, ArtistResource>();

            //// Resource to Domain
            //CreateMap<MusicResource, Music>();
            //CreateMap<SaveMusicResource, Music>();
            //CreateMap<ArtistResource, Artist>();
            //CreateMap<SaveArtistResource, Artist>();
            CreateMap<RegistrationRequest, AppUser>()
                .ForMember(u => u.UserName, opt => opt.MapFrom(ur => ur.Email));
        }
    }
}
