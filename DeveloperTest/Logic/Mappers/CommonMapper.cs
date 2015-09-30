using DeveloperTest.Model;
using DeveloperTest.ViewModels.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeveloperTest.Logic.Mappers
{
    static  class CommonMapper
    {
        public static AddressViewModel ToViewModel(this AddressModel model)
        {
            var adrVm = new AddressViewModel();
            adrVm.Address = model.Address;
            adrVm.Name = model.Name;
            return adrVm;
        }
    }
}
