using ShortCutter.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using WindowsInput;
using WindowsInput.Native;

namespace ShortCutter
{
    public class DataModel : ObservableModel, IDisposable
    {
        public List<HotkeyModel> HotkeyModels { get; private set; } = new List<HotkeyModel>();
        KeyboardHook keyboardHook;
        KeyboardSimulator keyboardSimulator = new KeyboardSimulator(new InputSimulator());

        public DataModel()
        {
            for (int i = 1; i < 13; i++)
            {
                //create hotkeys for all F-keys
                HotkeyModels.Add(new HotkeyModel("F" + i));
            }
            
            #if DEBUG
            if (DesignerProperties.GetIsInDesignMode(new DependencyObject()))
            {
                return;
            }
            #endif

            //hook keyboard
            keyboardHook = new KeyboardHook();
            keyboardHook.OnKeyPressed = OnKeyPressed;
            keyboardHook.HookKeyboard();
        }

        public void Save()
        {
            foreach (HotkeyModel hotkeyModel in HotkeyModels)
            {
                hotkeyModel.Save();
            }

            Settings.Default.Save();
        }

        public void Dispose()
        {
            keyboardHook.UnHookKeyboard();
        }

        bool OnKeyPressed(KeyPressedArgs args)
        {
            HotkeyModel hotkeyModel = HotkeyModels.FirstOrDefault(x => x.Key == args.KeyPressed);

            if (hotkeyModel == null || string.IsNullOrEmpty(hotkeyModel.Content))
            {
                return false;
            }

            keyboardSimulator.TextEntry(hotkeyModel.Content);
            return true;
        }
    }
}
