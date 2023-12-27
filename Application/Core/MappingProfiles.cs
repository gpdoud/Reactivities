
using AutoMapper;
using Domain;


namespace Application;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        // added the var config ... wrapper.
        var config = new MapperConfiguration(cfg => {
            cfg.CreateMap<Activity, Activity>();

            cfg.CreateMap<Activity, ActivityDto>()
                .ForMember(d => d.HostUsername, o => o.MapFrom(s => s.Attendees.FirstOrDefault(x => x.IsHost).AppUser.UserName));
            
            //cfg.CreateMap<List<Activity>, List<ActivityDto>>();

            cfg.CreateMap<ActivityAttendee, Profile>()
                .ForMember(d => d.DisplayName, o => o.MapFrom(s => s.AppUser.DisplayName))
                .ForMember(d => d.Username, o => o.MapFrom(s => s.AppUser.UserName))
                .ForMember(d => d.Bio, o => o.MapFrom(s => s.AppUser.Bio));
        });
    }
}
