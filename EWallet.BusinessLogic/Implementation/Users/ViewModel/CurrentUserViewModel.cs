using System;
using System.Collections.Generic;

namespace EWallet.BusinessLogic.Implementation.Users.ViewModel
{
    public class CurrentUserViewModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }

        public bool IsAuthenticated { get; set; }
        public string Role { get; set; }


    }
}
