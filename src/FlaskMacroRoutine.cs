using ImGuiNET;
using PoeHUD.Framework;
using PoeHUD.Framework.Helpers;
using TreeRoutine.DefaultBehaviors.Actions;
using TreeRoutine.DefaultBehaviors.Helpers;
using TreeRoutine.Menu;
using TreeRoutine.TreeSharp;

namespace TreeRoutine.Routine.FlaskMacroRoutine
{
    public class FlaskMacroRoutine : BaseTreeRoutinePlugin<FlaskMacroRoutineSettings, BaseTreeCache>
    {
        private KeyboardHelper KeyboardHelper { get; set; } = null;

        public Composite Tree { get; set; }
        private Coroutine TreeCoroutine { get; set; }

        public override void Initialise()
        {
            base.Initialise();

            PluginName = "FlaskMacroRoutine";
            KeyboardHelper = new KeyboardHelper(GameController);

            Tree = createTree();

            // Add this as a coroutine for this plugin
            TreeCoroutine = (new Coroutine(() => TickTree(Tree)
            , new WaitTime(1000 / Settings.TicksPerSecond), nameof(FlaskMacroRoutine), "FlaskMacroRoutine Tree"))
                .AutoRestart(GameController.CoroutineRunner).Run();

            Settings.TicksPerSecond.OnValueChanged += UpdateCoroutineWaitRender;
        }

        private void UpdateCoroutineWaitRender()
        {
            if (TreeCoroutine != null)
            {
                TreeCoroutine.UpdateCondtion(new WaitTime(1000 / Settings.TicksPerSecond));
            }
        }

        private Composite createTree()
        {
            return new Decorator(x => TreeHelper.CanTick(),
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

        public override void InitializeSettingsMenu()
        {

        }

        public override void DrawSettingsMenu()
        {

            for (int i = 0; i < 5; i++)
            {
                if (ImGui.TreeNode("Flask " + (i + 1) + " Settings"))
                {
                    Settings.FlaskSettings[i].Enable.Value = ImGuiExtension.Checkbox("Enable", Settings.FlaskSettings[i].Enable);
                    ImGuiExtension.ToolTip("Enables the macro");

                    Settings.FlaskSettings[i].Hotkey.Value = ImGuiExtension.HotkeySelector("Flask Hotkey", "Flask Hotkey", Settings.FlaskSettings[i].Hotkey);
                    ImGuiExtension.ToolTip("Path of Exile key for flask in this slot");

                    ImGui.TreePop();
                }    
            }

            if (ImGui.TreeNode("Macro Settings"))
            {
                for (int i = 0; i < 5; i++)
                {
                    if (ImGui.TreeNode("Macro " + (i + 1)))
                    {
                        Settings.MacroSettings[i].Enable.Value = ImGuiExtension.Checkbox("Enable", Settings.MacroSettings[i].Enable);
                        ImGuiExtension.ToolTip("Enables the macro");

                        Settings.MacroSettings[i].UseFlask1.Value = ImGuiExtension.Checkbox("Flask 1 Enable", Settings.MacroSettings[i].UseFlask1);
                        ImGuiExtension.ToolTip("Enables using Flask 1 for this macro");

                        Settings.MacroSettings[i].UseFlask2.Value = ImGuiExtension.Checkbox("Flask 2 Enable", Settings.MacroSettings[i].UseFlask2);
                        ImGuiExtension.ToolTip("Enables using Flask 2 for this macro");

                        Settings.MacroSettings[i].UseFlask3.Value = ImGuiExtension.Checkbox("Flask 3 Enable", Settings.MacroSettings[i].UseFlask3);
                        ImGuiExtension.ToolTip("Enables using Flask 3 for this macro");


                        Settings.MacroSettings[i].UseFlask4.Value = ImGuiExtension.Checkbox("Flask 4 Enable", Settings.MacroSettings[i].UseFlask4);
                        ImGuiExtension.ToolTip("Enables using Flask 4 for this macro");

                        Settings.MacroSettings[i].UseFlask5.Value = ImGuiExtension.Checkbox("Flask 5 Enable", Settings.MacroSettings[i].UseFlask5);
                        ImGuiExtension.ToolTip("Enables using Flask 5 for this macro");

                        ImGui.TreePop();
                    }

                    Settings.MacroSettings[i].Hotkey.Value = ImGuiExtension.HotkeySelector($"Macro Hotkey {i+1}", $"Macro Hotkey {i + 1}", Settings.MacroSettings[i].Hotkey);
                    ImGuiExtension.ToolTip("Hotkey for using the flask");
                }

                ImGui.TreePop();
            }

            Settings.TicksPerSecond.Value = ImGuiExtension.IntSlider("Ticks Per Second", Settings.TicksPerSecond);
            ImGuiExtension.ToolTip("Specifies number of ticks per second");

            Settings.Debug.Value = ImGuiExtension.Checkbox("Debug", Settings.Debug);
            ImGuiExtension.ToolTip("Enables debug logging to help debug flask issues.");
        }
    }
}
