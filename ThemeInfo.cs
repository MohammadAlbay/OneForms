using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace Modetor.Design.UI
{
    public class ThemeInfo
    {
        public static bool FollowSystemTheme = true;
        public static Color DefaultColor = Color.FromArgb(54, 80, 180);
        public static Color DefaultColorLight = Color.FromArgb(60, 90, 180);
        [DllImport("uxtheme.dll", EntryPoint = "#95")]
        public static extern uint GetImmersiveColorFromColorSetEx(uint dwImmersiveColorSet, uint dwImmersiveColorType, bool bIgnoreHighContrast, uint dwHighContrastCacheMode);
        [DllImport("uxtheme.dll", EntryPoint = "#96")]
        public static extern uint GetImmersiveColorTypeFromName(IntPtr pName);
        [DllImport("uxtheme.dll", EntryPoint = "#98")]
        public static extern int GetImmersiveUserColorSetPreference(bool bForceCheckRegistry, bool bSkipCheckOnFail);

        public Color GetThemeColor()
        {
            if (!FollowSystemTheme) return DefaultColor;
            var colorSetEx = GetImmersiveColorFromColorSetEx(
                (uint)GetImmersiveUserColorSetPreference(false, false),
                GetImmersiveColorTypeFromName(Marshal.StringToHGlobalUni("ImmersiveStartSelectionBackground")),
                false, 0);

            var colour = Color.FromArgb((byte)((0xFF000000 & colorSetEx) >> 24), (byte)(0x000000FF & colorSetEx),
                (byte)((0x0000FF00 & colorSetEx) >> 8), (byte)((0x00FF0000 & colorSetEx) >> 16));
            return colour;
        }
        public static Color ThemeColor()
        {
            try
            {
                if (!FollowSystemTheme) return DefaultColor;
                var colorSetEx = GetImmersiveColorFromColorSetEx(
                    (uint)GetImmersiveUserColorSetPreference(false, false),
                    GetImmersiveColorTypeFromName(Marshal.StringToHGlobalUni("ImmersiveStartSelectionBackground")),
                    false, 0);

                var colour = Color.FromArgb((byte)((0xFF000000 & colorSetEx) >> 24), (byte)(0x000000FF & colorSetEx),
                    (byte)((0x0000FF00 & colorSetEx) >> 8), (byte)((0x00FF0000 & colorSetEx) >> 16));
                return colour;
            }
            catch
            {
                return DefaultColor;
            }
        }
    }


}

