using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using HandleHouse.Data;
using HandleHouse.Data.Models;

namespace HandleHouse.UI.ViewModels
{
    class SettlementsViewModel : Screen
    {
        public BindableCollection<Settlement> Settlements { get; set; }

        private Settlement _selectedSettlement;

        public Settlement SelectedSettlement
        {
            get { return _selectedSettlement; }
            set { _selectedSettlement = value; }
        }


        public SettlementsViewModel()
        {
            Settlements = new BindableCollection<Settlement>(DataAccess.GetSettlements());
        }
    }
}
