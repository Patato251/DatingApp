using System.Linq;
using AutoMapper;
using DatingApp.API.Dtos;
using DatingApp.API.Models;

namespace DatingApp.API.Helpers
{
  public class AutoMapperProfiles : Profile
  {
    public AutoMapperProfiles()
    {
      // Create Mapping profiles for the two User get's
      // Convention base, same prop names will be assigned to each other
      // List Mapping (with photo and age manual arrangement)
      CreateMap<User, UserForListDto>()
        .ForMember(dest => dest.PhotoUrl, opt =>
          opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url))
            .ForMember(dest => dest.Age, opt =>
              opt.MapFrom(src => src.DateOfBirth.CalculateAge()));
      // Single User Mapping (with photo and age manual arrangement)
      CreateMap<User, UserForDetailedDto>()
        .ForMember(dest => dest.PhotoUrl, opt =>
          opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url))
            .ForMember(dest => dest.Age, opt =>
              opt.MapFrom(src => src.DateOfBirth.CalculateAge()));
      // Photo Mapping
      CreateMap<Photo, PhotosForDetailedDto>();
    }

  }
}