using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Caliburn.Micro;
using HandleHouse.Data;
using HandleHouse.Data.Models;
using HandleHouse.UI.Annotations;

namespace HandleHouse.UI.ViewModels.Queries
{
    class Query4ViewModel : INotifyPropertyChanged
    {
        private readonly HouseContext _currentContext;
        
        public BindableCollection<string> Regions
        {
            get
            {
                return new BindableCollection<string>(_currentContext.Settlements.Select(s => s.Name).ToList().Distinct());
            }
        }
        private string _selectedRegion;
        public string SelectedRegion
        {
            get { return _selectedRegion; }
            set
            {
                _selectedRegion = value;
                NotifyOfPropertyChanged();
            }
        }
        private int _numberOfHouses;
        public int NumberOfHouses
        {
            get { return _numberOfHouses; }
            set {
                int num = -1;
                if (Int32.TryParse(value.ToString(), out num) && value >= 0)
                {
                    _numberOfHouses = value;
                    NotifyOfPropertyChanged();
                }
                else
                    MessageBox.Show("Incorrect number input");
            }
        }

        private BindableCollection<Settlement> _settlements;
        public BindableCollection<Settlement> Settlements
        {
            get => _settlements;
            set
            {
                _settlements = value; 
                NotifyOfPropertyChanged();
            }
        }

        public Searching SettlementsSearching { get; set; }

        public Query4ViewModel(HouseContext db)
        {
            _currentContext = db;
            SettlementsSearching = new Searching(this);
        }

        private void SearchSettlement()
        {
            List<Settlement> foundSettlements = new List<Settlement>();
            foreach (Settlement settlement in _currentContext.Settlements)
            {
                int i = 0;
                foreach (House house in _currentContext.Houses.Include("Settlement"))
                {
                    if (house.Settlement.Region == SelectedRegion && settlement.Region == SelectedRegion)
                        i++;
                }
                if (i >= NumberOfHouses)
                    foundSettlements.Add(settlement);
            }

            Settlements = new BindableCollection<Settlement>(foundSettlements);
            NotifyOfPropertyChanged();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        [NotifyPropertyChangedInvocator]
        protected virtual void NotifyOfPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        internal class Searching : ICommand
        {
            private Query4ViewModel model;

            public Searching(Query4ViewModel model)
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

            public bool CanExecute(object parameter)
            {
                return model.SelectedRegion != null && model.NumberOfHouses > 0;
            }

            public void Execute(object parameter)
            {
                model.SearchSettlement();
            }

            public event EventHandler CanExecuteChanged;
        }
    }
}
