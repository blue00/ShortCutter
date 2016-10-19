using ShortCutter.Properties;
using System;
using System.Windows.Input;

namespace ShortCutter
{
    public class HotkeyModel
    {
        public HotkeyModel(string name)
        {
            Name = name;
            Key = (Key)Enum.Parse(typeof(Key), name);

            try
            {
                Content = (Settings.Default["Key_" + Name] as string) ?? "";
            }
            catch (Exception)
            {
                //
            }
        }

        public void Save()
        {
            Settings.Default["Key_" + Name] = Content;
        }

        public Key Key { get; private set; }
        public string Name { get; private set; }
        public string Content { get; set; } = "";
    }
}
