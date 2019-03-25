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
    class WorksViewModel : Screen
    {
        public BindableCollection<Work> Works { get; set; }

        public WorksViewModel()
        {
            Works = new BindableCollection<Work>(DataAccess.GetWorks());
        }
    }
}
