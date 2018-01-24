using PoeHUD.Hud.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreeRoutine.DefaultBehaviors.Actions;
using TreeRoutine.DefaultBehaviors.Helpers;
using TreeSharp;

namespace TreeRoutine.Routine.FlaskMacroRoutine
{
    public class FlaskMacroRoutine : BaseTreeRoutinePlugin<FlaskMacroRoutineSettings, BaseTreeCache>
    {
        private KeyboardHelper KeyboardHelper { get; set; } = null;

        public override void Initialise()
        {
            base.Initialise();

            PluginName = "FlaskMacroRoutine";
            KeyboardHelper = new KeyboardHelper(GameController);

            Tree = createTree();
        }

        private Composite createTree()
        {
            return new Decorator(x => TreeHelper.canTick(),
                    new PrioritySelector(
                    CreateMacroHotkeyComposite(0),
                    CreateMacroHotkeyComposite(1),
                    CreateMacroHotkeyComposite(2),
                    CreateMacroHotkeyComposite(3),
                    CreateMacroHotkeyComposite(4)
                ));
        }

        private Composite CreateMacroHotkeyComposite(int index)
        {
            MacroSettings macroSettings = Settings.MacroSettings[index];
            return new Decorator(x => macroSettings.Enable && macroSettings.Hotkey != null && macroSettings.Hotkey.PressedOnce(),
                new Sequence(
                    new DecoratorContinue(x => macroSettings.UseFlask1 && Settings.FlaskSettings[0].Enable, new UseHotkeyAction(KeyboardHelper, x => Settings.FlaskSettings[0].Hotkey)),
                    new DecoratorContinue(x => macroSettings.UseFlask2 && Settings.FlaskSettings[1].Enable, new UseHotkeyAction(KeyboardHelper, x => Settings.FlaskSettings[1].Hotkey)),
                    new DecoratorContinue(x => macroSettings.UseFlask3 && Settings.FlaskSettings[2].Enable, new UseHotkeyAction(KeyboardHelper, x => Settings.FlaskSettings[2].Hotkey)),
                    new DecoratorContinue(x => macroSettings.UseFlask4 && Settings.FlaskSettings[3].Enable, new UseHotkeyAction(KeyboardHelper, x => Settings.FlaskSettings[3].Hotkey)),
                    new DecoratorContinue(x => macroSettings.UseFlask5 && Settings.FlaskSettings[4].Enable, new UseHotkeyAction(KeyboardHelper, x => Settings.FlaskSettings[4].Hotkey))
                    ));
        }

        public override void InitialiseMenu(MenuItem mainMenu)
        {

            var rootMenu = MenuPlugin.AddChild(mainMenu, PluginName, Settings.Enable);

            var flaskParent = MenuPlugin.AddChild(rootMenu, "Flask Settings ");

            for (int i = 0; i < 5; i++)
            {
                var parent = MenuPlugin.AddChild(flaskParent, "Flask " + (i + 1) + " Settings",
                    Settings.FlaskSettings[i].Enable);
                parent.TooltipText = "Enables the macro";

                var tmpNode = MenuPlugin.AddChild(parent, "Hotkey", Settings.FlaskSettings[i].Hotkey);
                tmpNode.TooltipText = "Path of Exile key for flask in this slot";
            }

            var macroParent = MenuPlugin.AddChild(rootMenu, "Macro Settings ");

            for (int i = 0; i < 5; i++)
            {
                var parent = MenuPlugin.AddChild(macroParent, "Macro " + (i + 1),
                    Settings.MacroSettings[i].Enable);
                macroParent.TooltipText = "Enables the macro";

                var tmpNode = MenuPlugin.AddChild(parent, "Macro hotkey", Settings.MacroSettings[i].Hotkey);
                tmpNode.TooltipText = "Hotkey for using the flask";

                tmpNode = MenuPlugin.AddChild(parent, "Flask 1 Enable", Settings.MacroSettings[i].UseFlask1);
                tmpNode.TooltipText = "Enables using Flask 1 for this macro";

                tmpNode = MenuPlugin.AddChild(parent, "Flask 2 Enable", Settings.MacroSettings[i].UseFlask2);
                tmpNode.TooltipText = "Enables using Flask 2 for this macro";

                tmpNode = MenuPlugin.AddChild(parent, "Flask 3 Enable", Settings.MacroSettings[i].UseFlask3);
                tmpNode.TooltipText = "Enables using Flask 3 for this macro";

                tmpNode = MenuPlugin.AddChild(parent, "Flask 4 Enable", Settings.MacroSettings[i].UseFlask4);
                tmpNode.TooltipText = "Enables using Flask 4 for this macro";

                tmpNode = MenuPlugin.AddChild(parent, "Flask 5 Enable", Settings.MacroSettings[i].UseFlask5);
                tmpNode.TooltipText = "Enables using Flask 5 for this macro";
            }

            var item = MenuPlugin.AddChild(rootMenu, "Tick Rate", Settings.TickRate);
            item.TooltipText = "Milliseconds between every tick of plugin.";

            item = MenuPlugin.AddChild(rootMenu, "Strict Tick Rate", Settings.StrictTickRate);
            item.TooltipText = "Enable to force a strict tick rate. This will ensure the ticks are at a constant timing, but may cause ticks to overlap as the previous tick may not have finished. Enable only if you have a reason to.";

            item = MenuPlugin.AddChild(rootMenu, "Debug", Settings.Debug);
            item.TooltipText = "Enables debug logging to help debug flask issues.";

        }
    }
}
