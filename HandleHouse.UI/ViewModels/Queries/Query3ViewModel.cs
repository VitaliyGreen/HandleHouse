using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
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
    class Query3ViewModel : INotifyPropertyChanged
    {
        private readonly HouseContext _currentContext;

        private BindableCollection<Work> _works;
        public BindableCollection<Work> Works
        {
            get => _works;
            set
            {
                _works = value;
                NotifyOfPropertyChanged();
            }
        }


        private int _workDuration;
        public int WorkDuration
        {
            get => _workDuration;
            set
            {
                int num = -1;
                if (Int32.TryParse(value.ToString(), out num) && value >= 0)
                {
                    _workDuration = value;
                    NotifyOfPropertyChanged();
                }
                else
                    MessageBox.Show("Incorrect work duration input");
            }
        }
        private int _cost;
        public int Cost
        {
            get => _cost;
            set
            {
                int num = -1;
                if (Int32.TryParse(value.ToString(), out num) && value >= 0)
                {
                    _cost = value;
                    NotifyOfPropertyChanged();
                }
                else
                    MessageBox.Show("Incorrect cost input");
            }
        }

        public Searching WorkSearching { get; set; }

        public Query3ViewModel(HouseContext db)
        {
            _currentContext = db;
            WorkSearching = new Searching(this);
        }

        private void SearchWork()
        {
            Works = new BindableCollection<Work>(_currentContext.Works
                .Where(w => DbFunctions.DiffDays(w.EndDate, w.StartDate) < WorkDuration && w.Cost > Cost).ToList());
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
            private Query3ViewModel model;

            public Searching(Query3ViewModel model)
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
                return model.WorkDuration > 0 && model.Cost > 0;
                //return true;
            }

            public void Execute(object parameter)
            {
                model.SearchWork();
            }

            public event EventHandler CanExecuteChanged;
        }
    }
}
