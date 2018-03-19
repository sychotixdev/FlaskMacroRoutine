using PoeHUD.Hud.Settings;
using PoeHUD.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TreeRoutine.Routine.FlaskMacroRoutine
{
    public class MacroSettings
    {
        public MacroSettings(Boolean enable, Keys hotkey)
        {
            Enable = enable;
            Hotkey = hotkey;
        }

        public ToggleNode Enable { get; set; } = false;
        public HotkeyNode Hotkey { get; set; }

        public ToggleNode UseFlask1 { get; set; } = false;
        public ToggleNode UseFlask2 { get; set; } = false;
        public ToggleNode UseFlask3 { get; set; } = false;
        public ToggleNode UseFlask4 { get; set; } = false;
        public ToggleNode UseFlask5 { get; set; } = false;
    }
}
