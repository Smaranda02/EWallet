using AutoMapper;
using EWallet.BusinessLogic.Implementation.Users.ViewModel;
using EWallet.DataAccess;

namespace EWallet.BusinessLogic
{
    public class ServiceDependencies
    {
        public IMapper Mapper { get; set; }
        public UnitOfWork UnitOfWork { get; set; }
        public CurrentUserViewModel CurrentUserViewModel { get; set; }

        public ServiceDependencies(IMapper mapper, UnitOfWork unitOfWork, CurrentUserViewModel currentUserViewModel)
        {
            Mapper = mapper;
            UnitOfWork = unitOfWork;
            CurrentUserViewModel = currentUserViewModel;
        }
    }
}
