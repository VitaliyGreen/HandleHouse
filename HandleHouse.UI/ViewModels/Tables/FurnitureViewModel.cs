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
    class FurnitureViewModel : Screen
    {
        public BindableCollection<Furniture> Furniture { get; set; }

        public FurnitureViewModel()
        {
            Furniture = new BindableCollection<Furniture>(DataAccess.GetFurniture());
        }
    }
}
