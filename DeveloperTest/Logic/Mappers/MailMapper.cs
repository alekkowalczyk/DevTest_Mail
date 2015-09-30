using DeveloperTest.Model;
using DeveloperTest.ViewModels.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeveloperTest.Logic.Mappers
{
    static class MailMapper
    {
        public static MailEnvelopeViewModel ToViewModel(this EnvelopeModel model)
        {
            var vm = new MailEnvelopeViewModel();
            vm.Date = model.Date;
            vm.From = model.From.ToViewModel();
            vm.Subject = model.Subject;
            vm.Recipients = new System.Collections.ObjectModel.ObservableCollection<AddressViewModel>();
            vm.UID = model.UID;

            foreach(var rec in model.Recipients)
            {
                vm.Recipients.Add(rec.ToViewModel());
            }
            return vm;
        }

        public static MailBodyViewModel ToViewModel(this BodyModel model)
        {
            var vm = new MailBodyViewModel();
            vm.Content = model.Content;
            vm.Type = model.Type;
            return vm;
        }
    }
}
