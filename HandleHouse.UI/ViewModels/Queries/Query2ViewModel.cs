using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Caliburn.Micro;
using HandleHouse.Data;
using HandleHouse.Data.Models;
using HandleHouse.UI.Annotations;

namespace HandleHouse.UI.ViewModels.Queries
{
    class Query2ViewModel : INotifyPropertyChanged
    {
        private readonly HouseContext _currentContext;

        public BindableCollection<Furniture> Furniture { get; set; }
        public BindableCollection<House> Houses { get; set; }

        private House _selectedHouse;
        public House SelectedHouse
        {
            get { return _selectedHouse; }
            set
            {
                _selectedHouse = value;
                NotifyOfPropertyChanged();
            }
        }
        private DateTime _setDate;
        public DateTime SetDate
        {
            get { return _setDate; }
            set
            {
                _setDate = value;
                NotifyOfPropertyChanged();
            }
        }
        
        public Query2ViewModel(HouseContext db)
        {
            _currentContext = db;
            Houses = new BindableCollection<House>(_currentContext.Houses.Include("Settlement").ToList());
        }

        private void SearchFurniture()
        {
            Furniture = new BindableCollection<Furniture>(
                _currentContext.Furniture.Where(f => f.House.TechnicalPassportNumber == SelectedHouse.TechnicalPassportNumber &&
                                                     f.SetDate > SetDate).ToList());
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
            private Query2ViewModel model;
            public Searching(Query2ViewModel model)
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
                return model.SelectedHouse != null;
            }

            public void Execute(object parameter)
            {
                throw new NotImplementedException();
            }

            public event EventHandler CanExecuteChanged;
        }
    }
}
