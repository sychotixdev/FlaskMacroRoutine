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
    public class FlaskSettings
    {
        public FlaskSettings()
        {

        }

        public FlaskSettings(Boolean enable, Keys hotkey)
        {
            Enable = enable;
            Hotkey = hotkey;
        }

        public ToggleNode Enable { get; set; } = new ToggleNode();
        public HotkeyNode Hotkey { get; set; } = new HotkeyNode();
    }
}
