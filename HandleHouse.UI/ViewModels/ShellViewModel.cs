using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;
using HandleHouse.Data;
using HandleHouse.Data.Models;

namespace HandleHouse.UI.ViewModels
{
    class ShellViewModel : Conductor<object>
    {
        public ShellViewModel()
        {}

        public void ActivateSettlementsView()
        {
            ActivateItem(new SettlementsViewModel());
        }

        public void ActivateHousesView()
        {
            ActivateItem(new HousesViewModel());
        }

        public void ActivateWorksView()
        {
            ActivateItem(new WorksViewModel());
        }

        public void ActivateFurnitureView()
        {
            ActivateItem(new FurnitureViewModel());
        }

        public void ActivatePeopleView()
        {
            ActivateItem(new PeopleViewModel());
        }

        public void ActivateOwnersView()
        {
            ActivateItem(new OwnersViewModel());
        }
    }
}
