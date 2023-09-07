using System.Diagnostics;
using System.Windows;
using System.Windows.Media;

namespace ERPSystem
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Access color resources and modify them as needed
            if (Current.Resources["LeftMenuBackgroundColor"] is Color leftMenuBackgroundColor)
            {
                // Update the resource with the new color value
                Current.Resources["LeftMenuBackgroundColor"] = leftMenuBackgroundColor;
            }

            if (Current.Resources["TextColor"] is Color textColor)
            {
                // Update the resource with the new color value
                Current.Resources["TextColor"] = textColor;
            }

            if (Current.Resources["LineColor"] is Color lineColor)
            {
                // Update the resource with the new color value
                Current.Resources["LineColor"] = lineColor;
            }

            // Rest of your application startup logic
        }
    }
}
