using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace DeveloperTest.Utils
{
    public class NumericTextBoxBehavior : Behavior<TextBox>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.PreviewKeyDown += AssociatedObjectPreviewKeyDown;
        }

        void AssociatedObjectPreviewKeyDown(object sender, KeyEventArgs e)
        {
            bool isAllowed = (e.Key >= Key.D0 && e.Key <= Key.D9) || (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 || e.Key == Key.Back || e.Key == Key.Delete);

            if (!isAllowed)
                e.Handled = true;
        }
    }
}
