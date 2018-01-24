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
        public FlaskMacroRoutineSettings()
        {
            FlaskSettings[0] = new FlaskSettings(false, Keys.D1);
            FlaskSettings[1] = new FlaskSettings(false, Keys.D2);
            FlaskSettings[2] = new FlaskSettings(false, Keys.D3);
            FlaskSettings[3] = new FlaskSettings(false, Keys.D4);
            FlaskSettings[4] = new FlaskSettings(false, Keys.D5);

            MacroSettings[0] = new MacroSettings(false, Keys.D1);
            MacroSettings[1] = new MacroSettings(false, Keys.D1);
            MacroSettings[2] = new MacroSettings(false, Keys.D1);
            MacroSettings[3] = new MacroSettings(false, Keys.D1);
            MacroSettings[4] = new MacroSettings(false, Keys.D1);


        }

        public FlaskSettings[] FlaskSettings { get; set; } = new FlaskSettings[5];
        public MacroSettings[] MacroSettings { get; set; } = new MacroSettings[5];
    }
}
