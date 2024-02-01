using EWallet.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EWallet.BusinessLogic.Implementation.Incomes.ViewModel
{
    public class IncomeViewModel
    {
        public int Id { get; set; }

        public decimal IncomeSum { get; set; }

        public string IncomeDescription { get; set; }

        public int? RecurrenceTypeId { get; set; }
        public int? RecurringNumber { get; set; }
        public string? RecurrenceTypeName { get; set; }
        public List<int>? PiggyBankIds { get; set; }

    }
}
