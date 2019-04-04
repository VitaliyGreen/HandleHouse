using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using HandleHouse.Data;
using HandleHouse.Data.Models;

namespace HandleHouse.UI.ViewModels
{
    class OwnersViewModel : Screen
    {
        public BindableCollection<Owner> Owners { get; set; }

        public OwnersViewModel()
        {
            Owners = new BindableCollection<Owner>(DataAccess.GetOwners());
        }
    }
}
