using EWallet.Entities.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EWallet.BusinessLogic.Implementation.Spendings.ViewModel
{
    public class SpendingViewModel
    {
        public int Id { get; set; }

        public decimal Amount { get; set; }

        public int SpendingCategoryId { get; set; }

        public string SpendingDescription { get; set; }

        public List<SelectListItem> SpendingCategories { get; set; } = new();
        public string SpendingCategoryName { get; set; }

        public int? RecurrenceTypeId { get; set; }
        public int? RecurringNumber { get; set; }
        public string? RecurrenceTypeName { get; set; }



    }
}
