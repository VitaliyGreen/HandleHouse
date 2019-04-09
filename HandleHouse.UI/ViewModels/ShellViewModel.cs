using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;
using HandleHouse.Data;
using HandleHouse.Data.Models;
using HandleHouse.UI.ViewModels.Queries;

namespace HandleHouse.UI.ViewModels
{
    public class ShellViewModel : Conductor<object>
    {
        private HouseContext context;

        public ShellViewModel()
        {
            context = new HouseContext();
        }

        public void ActivateSettlementsView()
        {
            ActivateItem(new SettlementsViewModel(context));
        }
        public void ActivateHousesView()
        {
            ActivateItem(new HousesViewModel());
        }
        public void ActivateWorksView()
        {
            ActivateItem(new WorksViewModel(context));
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

        public void ActivateQuery1()
        {
            ActivateItem(new Query1ViewModel(context));
        }
        public void ActivateQuery2()
        {
            ActivateItem(new Query2ViewModel(context));
        }
        public void ActivateQuery3()
        {
            ActivateItem(new Query3ViewModel(context));
        }
        public void ActivateQuery4()
        {
            ActivateItem(new Query4ViewModel(context));
        }
        public void ActivateQuery5()
        {
            ActivateItem(new Query5ViewModel(context));
        }
        public void ActivateQuery6()
        {
            ActivateItem(new Query6ViewModel(context));
        }
        public void ActivateQuery7()
        {
            ActivateItem(new Query7ViewModel(context));
        }
        public void ActivateQuery8()
        {
            ActivateItem(new Query8ViewModel(context));
        }
        public void ActivateQuery9()
        {
            ActivateItem(new Query9ViewModel(context));
        }
    }
}
