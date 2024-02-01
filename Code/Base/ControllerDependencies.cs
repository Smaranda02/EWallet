using EWallet.BusinessLogic.Implementation.Users.ViewModel;

namespace EWallet.WebApp.Code.Base
{
    public class ControllerDependencies
    {
        public CurrentUserViewModel CurrentUserViewModel { get; set; }

        public ControllerDependencies(CurrentUserViewModel CurrentUserViewModel)
        {
            this.CurrentUserViewModel = CurrentUserViewModel;
        }
    }
}
