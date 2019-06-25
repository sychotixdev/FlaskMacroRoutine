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
    public class FlaskMacroRoutineSettings : BaseTreeSettings
    {
        public FlaskSettings[] FlaskSettings { get; set; } = new FlaskSettings[5]
        {
            new FlaskSettings(false, new HotkeyNode(Keys.D1)),
            new FlaskSettings(false, new HotkeyNode(Keys.D2)),
            new FlaskSettings(false, new HotkeyNode(Keys.D3)),
            new FlaskSettings(false, new HotkeyNode(Keys.D4)),
            new FlaskSettings(false, new HotkeyNode(Keys.D5))
        };

        public MacroSettings[] MacroSettings { get; set; } = new MacroSettings[5]
        {
            new MacroSettings(false, new HotkeyNode(Keys.D1)),
            new MacroSettings(false, new HotkeyNode(Keys.D2)),
            new MacroSettings(false, new HotkeyNode(Keys.D3)),
            new MacroSettings(false, new HotkeyNode(Keys.D4)),
            new MacroSettings(false, new HotkeyNode(Keys.D5))
        };
        public RangeNode<int> TicksPerSecond { get; set; } = new RangeNode<int>(10, 1, 30);

    }
}
