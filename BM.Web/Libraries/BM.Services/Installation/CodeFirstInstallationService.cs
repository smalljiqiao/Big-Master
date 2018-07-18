using BM.Core.Data;
using BM.Core.Domain.Users;
using BM.Core.Installation;

namespace BM.Services.Installation
{
    public partial class CodeFirstInstallationService:IInstallationService
    {
        private readonly IRepository<User> _useRepository;

        public CodeFirstInstallationService(IRepository<User> userRepository)
        {
            this._useRepository = userRepository;
        }

        public virtual void InstallData()
        {

        }
    }
}
