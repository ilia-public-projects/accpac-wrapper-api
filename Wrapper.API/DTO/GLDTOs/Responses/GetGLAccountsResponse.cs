using AutoMapper;
using Wrapper.API.DTO.GLDTOs.Models;
using Wrapper.Models.Accpac.GLModels.GLSetupModels;

namespace Wrapper.API.DTO.GLDTOs.Responses
{
    /// <summary>
    /// Represents the response containing a list of active General Ledger (GL) accounts.
    /// </summary>
    public class GetGLAccountsResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetGLAccountsResponse"/> class.
        /// Maps the source list of <see cref="GLAccountModel"/> to the response list of <see cref="ResponseGLAccountModel"/> using the provided mapper.
        /// </summary>
        /// <param name="mapper">The mapper used to map the source list of GL accounts to the response models.</param>
        /// <param name="source">The source list of GL account models to be mapped.</param>
        public GetGLAccountsResponse(
                IMapper mapper, List<GLAccountModel> source
            )
        {
            Accounts = mapper.Map<List<ResponseGLAccountModel>>(source);
        }

        /// <summary>
        /// Gets or sets the list of active General Ledger (GL) accounts, represented by <see cref="ResponseGLAccountModel"/> objects.
        /// </summary>
        public List<ResponseGLAccountModel> Accounts { get; set; }
    }

}
