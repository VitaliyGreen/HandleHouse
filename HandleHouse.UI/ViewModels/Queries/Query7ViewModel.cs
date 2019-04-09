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
    class Query7ViewModel : INotifyPropertyChanged
    {
        private readonly HouseContext _currentContext;

        private int _roomNumber;
        public int RoomNumber
        {
            get => _roomNumber;
            set
            {
                int num = -1;
                if (Int32.TryParse(value.ToString(), out num) && value >= 0)
                {
                    _roomNumber = value;
                    NotifyOfPropertyChanged();
                    NotifyOfPropertyChanged(nameof(ViewHeader));
                }
                else
                    MessageBox.Show("Incorrect number input");
            }
        }
        private string _street;
        public string Street
        {
            get => _street;
            set
            {
                _street = value;
                NotifyOfPropertyChanged();
                NotifyOfPropertyChanged(nameof(ViewHeader));
            }
        }
        private int _cost;
        public int Cost
        {
            get { return _cost; }
            set
            {
                int num = -1;
                if (Int32.TryParse(value.ToString(), out num) && value >= 0)
                {
                    _cost = value;
                    NotifyOfPropertyChanged();
                    NotifyOfPropertyChanged(nameof(ViewHeader));
                }
                else
                    MessageBox.Show("Incorrect cost input");
            }
        }

        public string ViewHeader => $"Searching houses with {RoomNumber} rooms and on {Street} street, where cost is lower than {Cost}";

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

        public Searching SearchingHouses { get; set; }

        public Query7ViewModel(HouseContext db)
        {
            _currentContext = db;
            SearchingHouses = new Searching(this);
        }

        private void SearchHouses()
        {
            List<House> result = new List<House>();
            List<House> selectedHouses = _currentContext.Houses
                .Where(house => house.Street == Street && house.RoomNumber > RoomNumber).ToList();
            List<Work> selectedWorks = _currentContext.Works.Include("House").Where(work => work.Cost < Cost).ToList();
            foreach (Work work in selectedWorks)
            {
                result.AddRange(selectedHouses.FindAll(house => house.TechnicalPassportNumber == work.House.TechnicalPassportNumber).ToList());
            }
            Houses = new BindableCollection<House>(result.Distinct());
        }

        public event PropertyChangedEventHandler PropertyChanged;
        [NotifyPropertyChangedInvocator]
        protected virtual void NotifyOfPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        internal class Searching : ICommand
        {
            private Query7ViewModel model;

            public Searching(Query7ViewModel model)
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
                return !String.IsNullOrWhiteSpace(model.Street) && model.Cost > 0 && model.RoomNumber > 0;
            }

            public void Execute(object parameter)
            {
                model.SearchHouses();
            }

            public event EventHandler CanExecuteChanged;
        }
    }
}
