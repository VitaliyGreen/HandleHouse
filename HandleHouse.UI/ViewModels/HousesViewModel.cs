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
    class HousesViewModel : Screen
    {
        public BindableCollection<House> Houses { get; set; }

        public HousesViewModel()
        {
            Houses = new BindableCollection<House>(DataAccess.GetHouses());
        }
    }
}
