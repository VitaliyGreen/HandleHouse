using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Threading;
using Caliburn.Micro;
using HandleHouse.Data;
using HandleHouse.Data.Models;
using HandleHouse.UI.Views;
using Exception = System.Exception;
using String = System.String;



namespace HandleHouse.UI.ViewModels
{
    class SettlementsViewModel : INotifyPropertyChanged
    {
        private readonly HouseContext _currentContext;

        private BindableCollection<Settlement> _settlements;
        public BindableCollection<Settlement> Settlements
        {
            get => _settlements;
            set
            {
                _settlements = value;
                NotifyOfPropertyChange();
            }
        }

        private Settlement NewSettlement { get; set; }

        private Settlement _selectedSettlement = new Settlement();
        public Settlement SelectedSettlement
        {
            get => _selectedSettlement;
            set
            {
                _selectedSettlement = value;
                NotifyOfPropertyChange();
            }
        }

        private string _newSettlementName;
        private TypeHelper.SettlementType _newSettlementType;
        private string _newSettlementRegion;
        private string _newSettlementDistrict;
        
        public string NewSettlementName
        {
            get { return this._newSettlementName; }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    this._newSettlementName = value;
                }
            }
        }
        public string NewSettlementRegion
        {
            get { return _newSettlementRegion; }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                    _newSettlementRegion = value;
            }
        }
        public string NewSettlementDistrict
        {
            get { return _newSettlementDistrict; }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                    _newSettlementDistrict = value;
            }
        }
        public TypeHelper.SettlementType NewSettlementType
        {
            get { return _newSettlementType; }
            set
            {
                if(!String.IsNullOrEmpty(value.ToString()))
                _newSettlementType = value;
            }
        }

        public DeleteSettlementClass DeletingSettlement { get; set; }

        public SettlementsViewModel(HouseContext db)
        {
            _currentContext = db;
            Settlements = new BindableCollection<Settlement>(DataAccess.GetSettlements(_currentContext));
            SaveNewSettlementClass = new SaveSettlementClass(this);
            DeletingSettlement = new DeleteSettlementClass(this);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public virtual void NotifyOfPropertyChange(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            if ((SelectedSettlement != null) && !String.IsNullOrWhiteSpace(SelectedSettlement.Name))
            {
                _currentContext.Entry(SelectedSettlement).State = EntityState.Modified;
                _currentContext.SaveChanges();

            }
        }

        public void SaveSettlement()
        {
            NewSettlement = new Settlement(NewSettlementType, NewSettlementName, NewSettlementRegion, NewSettlementDistrict);
            _currentContext.Settlements.Add(NewSettlement);
            _currentContext.SaveChanges();
            Settlements.Insert(Settlements.Count, NewSettlement);
            NotifyOfPropertyChange();
            
        }

        public void DeleteSettlement()
        {
            try
            {
                _currentContext.Entry(SelectedSettlement).State = EntityState.Deleted;
                Settlements.Remove(SelectedSettlement);
                _currentContext.SaveChanges();
                NotifyOfPropertyChange();
            }
            catch (Exception e)
            {
                return;
            }
        }

        public SaveSettlementClass SaveNewSettlementClass { get; set; }

        internal class DeleteSettlementClass : ICommand
        {
            private SettlementsViewModel viewModel;

            public DeleteSettlementClass(SettlementsViewModel viewModel)
            {
                this.viewModel = viewModel;
                this.viewModel.PropertyChanged += (s, e) =>
                {
                    if (this.CanExecuteChanged != null)
                    {
                        this.CanExecuteChanged(this, new EventArgs()); 
                    }
                };
            }
            public bool CanExecute(object parameter)
            {
                return viewModel.SelectedSettlement != null;
            }
            
            public void Execute(object parameter)
            {
                this.viewModel.DeleteSettlement();
            }

            public event EventHandler CanExecuteChanged;
        }
        internal class SaveSettlementClass : ICommand
        {
            private SettlementsViewModel viewModel;

            public SaveSettlementClass(SettlementsViewModel viewModel)
            {
                this.viewModel = viewModel;
                this.viewModel.PropertyChanged += (s, e) =>
                {
                    if (this.CanExecuteChanged != null)
                    {
                        this.CanExecuteChanged(this, new EventArgs());
                    }
                };
            }
            public bool CanExecute(object parameter)
            {
                return true;
            }

            public void Execute(object parameter)
            {
                this.viewModel.SaveSettlement();
            }

            public event EventHandler CanExecuteChanged;
        }

    }
}
