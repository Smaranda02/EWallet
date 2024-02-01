using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.ComponentModel.DataAnnotations;

namespace EWallet.BusinessLogic.Implementation.Users
{
    public class RegistrationViewModel
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        [Display(Name = "Confirmed Password")]
        public string ConfirmedPassword { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Birth Date ")]
        public DateTime BirthDate { get; set; }

    }
}
