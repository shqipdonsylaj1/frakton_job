using AutoMapper;
using Core.Entities.Identity;
using IdentityAPI.DTO;

namespace IdentityAPI.Extensions
{
    public class MappingProfiles : Profile
    {
        #region Constructor
        public MappingProfiles()
        {
            CreateMap<RegisterDTO, Users>();
        }
        #endregion
    }
}
