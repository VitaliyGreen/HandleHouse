using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using Caliburn.Micro;
using HandleHouse.Data;
using HandleHouse.Data.Models;
using HandleHouse.UI.Annotations;
using MessageBox = System.Windows.Forms.MessageBox;

namespace HandleHouse.UI.ViewModels.Queries
{
    class Query1ViewModel : INotifyPropertyChanged
    {
        private readonly HouseContext _currentContext;
        private string _settlementName;
        public String SettlementName
        {
            get => _settlementName;
            set
            {
                _settlementName = value;
                NotifyOfPropertyChanged();
            }
        }
        private int _peopleNumber;
        public int PeopleNumber
        {
            get => _peopleNumber;
            set
            {
                if (value > 0 && value < 17)
                {
                    _peopleNumber = value;
                    NotifyOfPropertyChanged();
                }
                
                else
                    MessageBox.Show("Invalid input of people number");
            }
        }

        private BindableCollection<House> _houses;
        public BindableCollection<House> Houses
        {
            get => _houses;
            set
            {
                _houses = value;
                NotifyOfPropertyChanged();
            }
        }

        private SearchHouses _searchingHouses;
        public SearchHouses SearchingHouses => _searchingHouses;

        public Query1ViewModel(HouseContext db)
        {
            _currentContext = db;
            _searchingHouses = new SearchHouses(this);
        }
      
        private void SearchHousesMethod()
        {
            List<House> foundHouses = new List<House>();
            foreach (House house in _currentContext.Houses.Include("Settlement"))
            {
                int i = 0;
                foreach (Person person in _currentContext.People.Include("House"))
                {
                    if (person.House.TechnicalPassportNumber == house.TechnicalPassportNumber)
                        i++;
                }
                if (i >= PeopleNumber)
                    foundHouses.Add(house);
            }

            Houses = new BindableCollection<House>(foundHouses.Where(h => h.Settlement.Name == SettlementName).ToList()); 
            NotifyOfPropertyChanged();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void NotifyOfPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        internal class SearchHouses : ICommand
        {
            private Query1ViewModel model;
            public SearchHouses(Query1ViewModel model)
            {
                this.model = model;
                this.model.PropertyChanged += (s, e) =>
                {
                    if (this.CanExecuteChanged != null)
                    {
                        this.CanExecuteChanged(this, new EventArgs());
                    }
                };
            }
            public event EventHandler CanExecuteChanged;

            public bool CanExecute(object parameter)
            {
                int number;
                return (!String.IsNullOrEmpty(model.SettlementName) && Int32.TryParse(model.PeopleNumber.ToString(), out number));
            }

            public void Execute(object parameter)
            {
                model.SearchHousesMethod();
            }
        }
    }
}
