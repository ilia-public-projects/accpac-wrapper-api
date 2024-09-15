using AutoMapper;
using Wrapper.API.DTO.GLDTOs.Models;
using Wrapper.Models.Accpac.GLModels.GLSetupModels;

namespace Wrapper.API.Mapping
{
    public class GLSetupProfile:Profile
    {
        public GLSetupProfile()
        {
            CreateMap<GLAccountModel, ResponseGLAccountModel>();
        }
    }
}
