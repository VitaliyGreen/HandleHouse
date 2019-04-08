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
    class Query8ViewModel : INotifyPropertyChanged
    {
        private readonly HouseContext _currentContext;

        private string _selectedSettlement;
        public string SelectedSettlement
        {
            get => _selectedSettlement;
            set
            {
                _selectedSettlement = value;
                Streets = new BindableCollection<string>(
                    _currentContext.Houses.Include("Settlement").Where(h => h.Settlement.Name == SelectedSettlement)
                        .Select(h => h.Street).Distinct());
                NotifyOfPropertyChanged(nameof(Streets));
                NotifyOfPropertyChanged();
            }
        }

        private string _selectedStreet;
        public string SelectedStreet
        {
            get => _selectedStreet;
            set
            {
                _selectedStreet = value;
                NotifyOfPropertyChanged(nameof(SelectedStreet));
            }
        }

        public BindableCollection<string> Settlements
        {
            get
            {
                return new BindableCollection<string>(_currentContext.Settlements.Select(s => s.Name).ToList().Distinct());
            }
        }
        public BindableCollection<string> Streets { get; set; }

        private BindableCollection<Info> _infos;
        public BindableCollection<Info> Infos
        {
            get => _infos;
            set
            {
                _infos = value;
                NotifyOfPropertyChanged();
            }
        }


        public Searching SearchingInfo { get; set; }

        public Query8ViewModel(HouseContext db)
        {
            _currentContext = db;
            SearchingInfo = new Searching(this);
        }

        private void SearchInfo()
        {
            var HouseFurniture = new List<(House, List<Furniture>)>();
            foreach (House house in _currentContext.Houses.Include("Settlement"))
            {
                List<Furniture> furnitureInHouse = new List<Furniture>();
                foreach (Furniture furniture in _currentContext.Furniture.Include("House"))
                {
                    if (furniture.House.TechnicalPassportNumber == house.TechnicalPassportNumber)
                        furnitureInHouse.Add(furniture);
                }
                HouseFurniture.Add((house, furnitureInHouse));
            }
            List<Info> info = new List<Info>();
            foreach ((House, List<Furniture>) tuple in HouseFurniture)
            {
                try
                {
                    Owner owner = _currentContext.Owners
                        .Include("House")
                        .Where(o => o.House.TechnicalPassportNumber == tuple.Item1.TechnicalPassportNumber)
                        .First();
                    info.Add(new Info(owner.FullName, FindMaxCost(tuple.Item2).Name, FindMaxCost(tuple.Item2).Cost));
                }
                catch (Exception e)
                {
                    continue;
                }
            }

            Infos = new BindableCollection<Info>(info);
        }

        private Furniture FindMaxCost(List<Furniture> list)
        {
            if (list.Count == 0)
            {
                throw new InvalidOperationException("Empty list");
            }

            Furniture furniture = null;
            int maxValue = int.MinValue;
            foreach (Furniture item in list)
            {
                int value = item.Cost;
                if (value > maxValue)
                {
                    furniture = item;
                }
            }
            return furniture;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        [NotifyPropertyChangedInvocator]
        protected virtual void NotifyOfPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        internal class Searching : ICommand
        {
            private Query8ViewModel model;

            public Searching(Query8ViewModel model)
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
                return !String.IsNullOrWhiteSpace(model.SelectedSettlement);
            }

            public void Execute(object parameter)
            {
                model.SearchInfo();
            }

            public event EventHandler CanExecuteChanged;
        }
    }

    public class Info
    {
        public string Name { get; set; }
        public string Furniture { get; set; }
        public int Cost { get; set; }

        public Info()
        {
            
        }

        public Info(string name, string furniture, int cost)
        {
            Name = name;
            Furniture = furniture;
            Cost = cost;
        }
    }
}
