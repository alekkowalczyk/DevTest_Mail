using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace DeveloperTest.Utils
{
    public static class WebBrowserAttachedProperties
    {
        public static readonly DependencyProperty BindableContentProperty =
            DependencyProperty.RegisterAttached("BindableContent", typeof(string), typeof(WebBrowserAttachedProperties), new UIPropertyMetadata(null, BindableContentPropertyChanged));

        public static string GetBindableContent(DependencyObject obj)
        {
            return (string)obj.GetValue(BindableContentProperty);
        }

        public static void SetBindableContent(DependencyObject obj, string value)
        {
            obj.SetValue(BindableContentProperty, value);
        }

        public static void BindableContentPropertyChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            WebBrowser browser = o as WebBrowser;
            if (browser != null)
            {
                if (e.NewValue != null && !string.IsNullOrWhiteSpace(e.NewValue.ToString()))
                    browser.NavigateToString(e.NewValue as string);
                else
                    browser.Navigate("about:blank");
            }
        }


    }
}
