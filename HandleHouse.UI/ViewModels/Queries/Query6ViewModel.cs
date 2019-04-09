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
    class Query6ViewModel : INotifyPropertyChanged
    {
        private readonly HouseContext _currentContext;

        private int _peopleNumber;
        public int PeopleNumber
        {
            get => _peopleNumber;
            set
            {
                int num = -1;
                if (Int32.TryParse(value.ToString(), out num) && value >= 0)
                {
                    _peopleNumber = value;
                    NotifyOfPropertyChanged();
                    NotifyOfPropertyChanged(nameof(ViewHeader));
                }
                else
                    MessageBox.Show("Incorrect number input");
            }
        }
        private int _peopleAge;
        public int PeopleAge
        {
            get => _peopleAge;
            set
            {
                int num = -1;
                if (Int32.TryParse(value.ToString(), out num) && value >= 0)
                {
                    _peopleAge = value;
                    NotifyOfPropertyChanged();
                    NotifyOfPropertyChanged(nameof(ViewHeader));
                }
                else
                    MessageBox.Show("Incorrect age input");
            }
        }

        public string ViewHeader => $"Searching for with more than {PeopleNumber} of inhabitants and age more than {PeopleAge}";
        
        private BindableCollection<Person> _people;
        public BindableCollection<Person> People
        {
            get => _people;
            set
            {
                _people = value;
                NotifyOfPropertyChanged();
            }
        }
        
        public Searching PeopleSearching { get; set; }
        
        public Query6ViewModel(HouseContext db)
        {
            _currentContext = db;
            PeopleSearching = new Searching(this);
        }

        private void SearchPeople()
        {
            List<Person> selectedPeople = new List<Person>();
            List<House> selectedHouses = new List<House>();
            var tuples = new List<(House, int, int)>();
            foreach (House house in _currentContext.Houses)
            {
                (House, int, int) tup;
                int count = 0;
                int age = 0;
                foreach (Person person in _currentContext.People.Include("House"))
                {
                    if (house.TechnicalPassportNumber == person.House.TechnicalPassportNumber)
                    {
                        count++;
                        TimeSpan diff = DateTime.Today - person.Birthday;
                        age += (new DateTime(1, 1, 1) + diff).Year;
                    }
                }

                tup.Item1 = house;
                tup.Item2 = count;
                tup.Item3 = age;
                tuples.Add(tup);
            }

            foreach ((House, int, int) tup in tuples)
            {
                if(tup.Item2 > PeopleNumber && (tup.Item3 / tup.Item2) > PeopleAge)
                    selectedHouses.Add(tup.Item1);
            }

            foreach (House house in selectedHouses)
            {
                selectedPeople.AddRange(
                    _currentContext.People
                        .Include("House")
                        .Where(p => p.House.TechnicalPassportNumber == house.TechnicalPassportNumber).ToList());
            }
            People = new BindableCollection<Person>(selectedPeople);
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
            private Query6ViewModel model;

            public Searching(Query6ViewModel model)
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
                return model.PeopleNumber > 0 && model.PeopleAge > 0;
            }

            public void Execute(object parameter)
            {
                model.SearchPeople();
            }

            public event EventHandler CanExecuteChanged;
        }
    }
}
