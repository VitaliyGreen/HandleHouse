﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Input;
using Caliburn.Micro;
using HandleHouse.Data;
using HandleHouse.Data.Models;
using HandleHouse.UI.Annotations;

namespace HandleHouse.UI.ViewModels.Queries
{
    class Query5ViewModel : INotifyPropertyChanged
    {
        private readonly HouseContext _currentContext;

        public BindableCollection<string> Districts
        {
            get
            {
                return new BindableCollection<string>(_currentContext.Settlements.Select(s => s.District).ToList()
                    .Distinct());
            }
        }

        private TypeHelper.WorkType _selectedSelectedWorkType;

        public TypeHelper.WorkType SelectedWorkType
        {
            get => _selectedSelectedWorkType;
            set
            {
                _selectedSelectedWorkType = value;
                NotifyOfPropertyChanged();
                NotifyOfPropertyChanged(nameof(ViewHeader));
            }
        }

        private string _selectedDistrict;

        public string SelectedDistrict
        {
            get => _selectedDistrict;
            set
            {
                _selectedDistrict = value;
                NotifyOfPropertyChanged();
                NotifyOfPropertyChanged(nameof(ViewHeader));
            }
        }

        public string ViewHeader
        {
            get
            {
                if (!String.IsNullOrWhiteSpace(SelectedDistrict))
                    return $"Searching for owners in whose house was conducted {SelectedWorkType} type work and live in the {SelectedDistrict} distinct";
                return "";
            }
        }

    private BindableCollection<Owner> _owners;
        public BindableCollection<Owner> Owners
        {
            get => _owners;
            set
            {
                _owners = value;
                NotifyOfPropertyChanged();
            }
        }

        public Searching OwnersSearching { get; set; }

        public Query5ViewModel(HouseContext db)
        {
            _currentContext = db;
            OwnersSearching = new Searching(this);
        }

        private void SearchOwners()
        {
            List<House> selectedHouses = new List<House>(_currentContext.Works.Include("House.Settlement")
                .Where(w => w.WorkType == SelectedWorkType && w.House.Settlement.District == SelectedDistrict)
                .Select(w => w.House));
            List<Owner> res = new List<Owner>();
            if (selectedHouses.Count > 0 && selectedHouses != null)
            {
                foreach (House house in selectedHouses)
                {
                    res.Add(DataAccess.GetOwnerOfHouse(house, _currentContext));
                }
            }

            Owners = new BindableCollection<Owner>(res.Distinct());
            NotifyOfPropertyChanged(nameof(Owners));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        [NotifyPropertyChangedInvocator]
        protected virtual void NotifyOfPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        internal class Searching : ICommand
        {
            private Query5ViewModel model;

            public Searching(Query5ViewModel model)
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
                return model.SelectedDistrict != null && model.SelectedWorkType != null;
            }

            public void Execute(object parameter)
            {
                try
                {
                    model.SearchOwners();
                }
                catch (Exception e)
                {
                    return;
                }
            }

            public event EventHandler CanExecuteChanged;
        }
    }
}
