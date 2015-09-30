using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DeveloperTest.Logic
{
    class ErrorProvider
    {
        public static void DisplayError(string msg)
        {
            MessageBox.Show(msg);
            MailLogic.Instance.CurrentStatus = Enums.MailLogicStatus.Error;
        }
    }
}
