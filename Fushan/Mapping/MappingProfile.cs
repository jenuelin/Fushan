using AutoMapper;
using DataServices.Model;
using Messages;
using Messages.Department;
using Messages.User;

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
            CreateMap<CreateUpdateUserRequest, AppUser>();
            CreateMap<GetDepartmentsRequest, Department>();
            CreateMap<Department, DepartmentModel>();
            CreateMap<CreateUpdateDepartmentRequest, Department>();
            CreateMap<RegistrationRequest, AppUser>();
            CreateMap<AppUserModel, AppUser>();
            CreateMap<AppUser, AppUserModel>()
                .ForMember(u => u.EmploymentStatusString, opt => opt.MapFrom(ur => ur.EmploymentStatus))
                .ForMember(u => u.EmploymentStatus, opt => opt.MapFrom(ur => (int)ur.EmploymentStatus))
                .ForMember(u => u.EmployeeCategoryString, opt => opt.MapFrom(ur => ur.EmployeeCategory))
                .ForMember(u => u.EmployeeCategory, opt => opt.MapFrom(ur => (int)ur.EmployeeCategory))
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