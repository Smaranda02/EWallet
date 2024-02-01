using Microsoft.AspNetCore.Mvc;
using EWallet.BusinessLogic.Implementation.Users.ViewModel;

namespace EWallet.WebApp.Code.Base
{
    public class BaseController : Controller
    {
        protected readonly CurrentUserViewModel CurrentUserViewModel;

        public BaseController(ControllerDependencies dependencies)
            : base()
        {
            CurrentUserViewModel = dependencies.CurrentUserViewModel;
        }
    }
}
