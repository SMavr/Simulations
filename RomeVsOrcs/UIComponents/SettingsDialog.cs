using Myra.Graphics2D.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RomeVsOrcs.UIComponents;
public class SettingsDialog
{
    public void Load(Desktop desktop)
    {
        Dialog dialog = new Dialog
        {
            Title = "Enter Your Name"
        };

        var stackPanel = new HorizontalStackPanel
        {
            Spacing = 8
        };

        var label1 = new Label
        {
            Text = "Name:"
        };
        stackPanel.Widgets.Add(label1);

        var textBox1 = new TextBox();
        StackPanel.SetProportionType(textBox1, ProportionType.Fill);
        stackPanel.Widgets.Add(textBox1);

        dialog.Content = stackPanel;

        dialog.Closed += (s, a) => {
            if (!dialog.Result)
            {
                // Dialog was either closed or "Cancel" clicked
                return;
            }

            // "Ok" was clicked or Enter key pressed
            // ...
        };

        dialog.ShowModal(desktop);
    }
}
