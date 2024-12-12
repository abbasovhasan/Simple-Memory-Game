using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.Win32;

namespace memory_game
{
    public enum CenterMethod { CenterHorizontal, CenterVertical, CenterBoth }

    public class CustomUtils
    {

        public bool SelectComboItem(ComboBox cb, string what)
        {
            for (var k = 0; k < cb.Items.Count; k++)
            {
                if (!cb.Items[k].Equals(what)) continue;
                cb.SelectedIndex = k;
                return true;
            }

            return false;
        }

        public bool SelectComboItem(ToolStripComboBox cb, string what)
        {
            try
            {
                for (var k = 0; k < cb.Items.Count - 1; k++)
                {
                    if (!cb.Items[k].Equals(what)) continue;
                    cb.SelectedIndex = k;
                    return true;
                }

                cb.Text = what;
            }
            catch { }

            return false;
        }

        public void CenterForm(Form frm)
        {
            int width = frm.Size.Width,
                height = frm.Size.Height,
                scrW = Screen.PrimaryScreen.Bounds.Width,
                scrH = Screen.PrimaryScreen.Bounds.Height;

            var x = scrW / 2 - width / 2;
            var y = scrH / 2 - height / 2;

            frm.Location = new Point(x, y);
        }

        public void CenterControl(Form parentForm, Control ctrl, CenterMethod method)
        {
            var x = parentForm.Width / 2 - ctrl.Width / 2;
            var y = parentForm.Height / 2 - ctrl.Height / 2;

            switch (method)
            { 
                case CenterMethod.CenterVertical:
                    ctrl.Location = new Point(ctrl.Location.X, y);
                    break;

                case CenterMethod.CenterHorizontal:
                    ctrl.Location = new Point(x, ctrl.Location.Y);
                    break;

                case CenterMethod.CenterBoth:
                    ctrl.Location = new Point(x, y);
                    break;
                default:
                    throw new ArgumentOutOfRangeException("method", method, null);
            }
        }
    }
    public class Defaults
    {
        // window position
        public int WinPosX = -1;
        public int WinPosY = -1;
        public int WinPosWidth = 600;
        public int WinPosHeight = 400;

        public bool SoundEnabled = true;
    }

    public class AppConfig
    {
        private readonly RegistryKey _registry;
        private readonly bool _registryEnabled;
        public Defaults Defaults;

        /// Constructor
        public AppConfig()
        {
            try
            {
                Defaults = new Defaults();
                var currentUser = Registry.CurrentUser.OpenSubKey("Software", RegistryKeyPermissionCheck.ReadWriteSubTree);
                _registry = currentUser.CreateSubKey(Program.AppRegistryRoot);
                _registryEnabled = true;
            }
            catch
            {
                _registryEnabled = false;
            }
        }

        public bool IsEnabled
        {
            get { return _registryEnabled; }
        }

        public void DeleteKey(string keyName)
        {
            _registry.DeleteSubKey(keyName, false);
        }

        public string GetCFG(string name, string defaultVal)
        {
            if (!_registryEnabled)
                return defaultVal;

            try
            {
                var val = _registry.GetValue(name, defaultVal).ToString();
                return val;
            }
            catch
            {
                return defaultVal;
            }
        }

        public int GetCFG(string name, int defaultVal)
        {
            if (!_registryEnabled)
                return defaultVal;

            try
            {
                var val = _registry.GetValue(name, defaultVal).ToString();
                return Convert.ToInt32(val);
            }
            catch
            {
                return defaultVal;
            }
        }

        public double GetCFG(string name, double defaultVal)
        {
            if (!_registryEnabled)
                return defaultVal;

            try
            {
                var val = _registry.GetValue(name, defaultVal).ToString();
                return Convert.ToDouble(val);
            }
            catch
            {
                return defaultVal;
            }
        }

        public bool GetCFG(string name, bool defaultVal)
        {
            if (!_registryEnabled)
                return defaultVal;

            try
            {
                var val = _registry.GetValue(name, defaultVal).ToString();
                return Convert.ToBoolean(val);
            }
            catch
            {
                return defaultVal;
            }
        }

        public Font GetCFG(string name, Font defaultVal)
        {
            if (!_registryEnabled)
                return defaultVal;

            try
            {
                var fontName = _registry.GetValue(name + "Title", defaultVal.FontFamily.Name).ToString();
                var fontSize = (float) Convert.ToDouble(_registry.GetValue(name + "Size", defaultVal.Size).ToString());
                var fontStyle = _registry.GetValue(name + "Style", defaultVal.Style.ToString()).ToString().ToLower();
                var style = FontStyle.Regular;
                if (fontStyle.Equals("bold"))
                    style = FontStyle.Bold;
                if (fontStyle.Equals("italic"))
                    style = FontStyle.Italic;
                if (fontStyle.Equals("strikeout"))
                    style = FontStyle.Strikeout;
                if (fontStyle.Equals("underline"))
                    style = FontStyle.Underline;

                return new Font(fontName, fontSize, style, GraphicsUnit.Point);
            }
            catch
            {
                return defaultVal;
            }
        }

        public Color GetCFG(string name, Color defaultVal)
        {
            if (!_registryEnabled)
                return defaultVal;

            try
            {
                var val = _registry.GetValue(name, defaultVal).ToString();
                return ColorTranslator.FromHtml(val);
            }
            catch
            {
                return defaultVal;
            }
        }


        // SAVE SETTINGS routines


        public void SaveCFG(string name, string val)
        {
            if (!_registryEnabled)
                throw new Exception(Program.StrRegFailed);

            _registry.SetValue(name, val, RegistryValueKind.String);
        }

        public void SaveCFG(string name, int val)
        {
            if (!_registryEnabled)
                throw new Exception(Program.StrRegFailed);

            _registry.SetValue(name, val.ToString(), RegistryValueKind.String);
        }

        public void SaveCFG(string name, double val)
        {
            if (!_registryEnabled)
                throw new Exception(Program.StrRegFailed);

            _registry.SetValue(name, val.ToString(), RegistryValueKind.String);
        }

        public void SaveCfg(string name, bool val)
        {
            if (!_registryEnabled)
                throw new Exception(Program.StrRegFailed);

            _registry.SetValue(name, val.ToString(), RegistryValueKind.String);
        }

        public void SaveCFG(string name, Font val)
        {
            if (!_registryEnabled)
                throw new Exception(Program.StrRegFailed);

            _registry.SetValue(name + "Title", val.FontFamily.Name, RegistryValueKind.String);
            _registry.SetValue(name + "Size", val.Size.ToString(), RegistryValueKind.String);
            _registry.SetValue(name + "Style", val.Style.ToString(), RegistryValueKind.String);
        }

        public void SaveCFG(string name, Color val)
        {
            if (!_registryEnabled)
                throw new Exception(Program.StrRegFailed);

            _registry.SetValue(name, ColorTranslator.ToHtml(val), RegistryValueKind.String);
        }
    }
    public class NoSystemMenuException : Exception
    {
    }

    public enum ItemFlags
    {
        // The item ...
        MfUnchecked = 0x00000000, // ... is not checked
        MfString = 0x00000000, // ... contains a string as label
        MfDisabled = 0x00000002, // ... is disabled
        MfGrayed = 0x00000001, // ... is grayed
        MfChecked = 0x00000008, // ... is checked
        MfPopup = 0x00000010, // ... Is a popup menu. Pass the
        //     menu handle of the popup
        //     menu into the ID parameter.
        MfBarBreak = 0x00000020, // ... is a bar break
        MfBreak = 0x00000040, // ... is a break
        MfByPosition = 0x00000400, // ... is identified by the position
        MfByCommand = 0x00000000, // ... is identified by its ID
        MfSeparator = 0x00000800 // ... is a seperator (String and
        //     ID parameters are ignored).
    }

    public enum WindowMessages
    {
        WmSysCommand = 0x0112
    }

    public class SystemMenu
    {
        
        [DllImport("USER32", EntryPoint = "GetSystemMenu", SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = true, CallingConvention = CallingConvention.Winapi)]
        private static extern IntPtr apiGetSystemMenu(IntPtr windowHandle, int bReset);

        
        [DllImport("USER32", EntryPoint = "AppendMenuW", SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = true, CallingConvention = CallingConvention.Winapi)]
        private static extern int apiAppendMenu(IntPtr menuHandle, int flags, int newId, String item);

       
        [DllImport("USER32", EntryPoint = "InsertMenuW", SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = true, CallingConvention = CallingConvention.Winapi)]
        private static extern int apiInsertMenu(IntPtr hMenu, int position, int flags, int newId, String item);

        private IntPtr _mSysMenu = IntPtr.Zero; 
        public bool InsertSeparator(int pos)
        {
            return InsertMenu(pos, ItemFlags.MfSeparator | ItemFlags.MfByPosition, 0, "");
        }

       
        public bool InsertMenu(int pos, int id, String item)
        {
            return InsertMenu(pos, ItemFlags.MfByPosition | ItemFlags.MfString, id, item);
        }

        public bool InsertMenu(int pos, ItemFlags flags, int id, String item)
        {
            return apiInsertMenu(_mSysMenu, pos, (Int32) flags, id, item) == 0;
        }

        public bool AppendSeparator()
        {
            return AppendMenu(0, "", ItemFlags.MfSeparator);
        }

       
        public bool AppendMenu(int id, String item)
        {
            return AppendMenu(id, item, ItemFlags.MfString);
        }

        public bool AppendMenu(int id, String item, ItemFlags flags)
        {
            return apiAppendMenu(_mSysMenu, (int) flags, id, item) == 0;
        }
        
        public static SystemMenu FromForm(Form frm)
        {
            var cSysMenu = new SystemMenu();

            cSysMenu._mSysMenu = apiGetSystemMenu(frm.Handle, 0);
            if (cSysMenu._mSysMenu == IntPtr.Zero)
            {
                // Throw an exception on failure
                throw new NoSystemMenuException();
            }

            return cSysMenu;
        }

        // Reset's the window menu to it's default
        public static void ResetSystemMenu(Form frm)
        {
            apiGetSystemMenu(frm.Handle, 1);
        }

        public static bool VerifyItemId(int id)
        {
            return id < 0xF000 && id > 0;
        }
    }


    public class Top10
    {
        private readonly AppConfig _cfg;
        private string[][] _entries;

        public Top10(AppConfig config)
        {
            _cfg = config;
        }
        public int AddEntry(int score, int time, int difficulty)
        {
            _entries = GetEntries();


            return -1;
        }

        public string[][] GetEntries()
        {
            var entries = new string[10][];
            var raw = _cfg.GetCFG("lstTop", "");
            raw = Convert.FromBase64String(raw).ToString();


            var rawEntries = raw.Split(new[] {';'}, 10);
            var row = new string[4];
            for (var k = 0; k < 10; k++)
            {
                try
                {
                    row = rawEntries[k].Split(new[] {'&'}, 4);
                }
                catch
                {
                }

                if (row[0].Equals(string.Empty))
                {
                    row = new[] {"-", "-", "-", "-"};
                }

                entries[k] = row;
            }

            return entries;
        }

        public void Flush()
        {
        }
    }
}