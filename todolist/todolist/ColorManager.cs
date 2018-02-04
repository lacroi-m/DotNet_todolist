using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace todolist
{
    public static class ColorManager
    {
        static String[] ColorName = { "Blue cyan", "Gray", "Pink", "Brown" };
        static Xamarin.Forms.Color[] ColorValue = { Xamarin.Forms.Color.Blue, Xamarin.Forms.Color.Gray, Xamarin.Forms.Color.Pink, Xamarin.Forms.Color.Brown };
        static Xamarin.Forms.Color DefaultColor = Xamarin.Forms.Color.LightSkyBlue;

        public static Xamarin.Forms.Color GetDefaultColor()
        {
            return (DefaultColor);
        }

        public static Xamarin.Forms.Color GetColorFromString(String Name)
        {
            if (Name != null)
            {
                int i = 0;
                while (ColorName[i] != null)
                {
                    if (ColorName[i] == Name)
                        return (ColorValue[i]);
                    i++;
                }
            }
            return (GetDefaultColor());
        }

        public static void FeedColorPicker(Xamarin.Forms.Picker ColorPicker)
        {
            foreach (var current in ColorName)
                ColorPicker.Items.Add(current);
            ColorPicker.SelectedIndex = 0;
        }
    }
}
