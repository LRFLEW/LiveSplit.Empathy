using System;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;
using LiveSplit.UI;

namespace LiveSplit.Empathy
{
    public partial class Settings : UserControl
    {
        public bool AutoStart { get; private set; } = true;
        public bool AutoEnd { get; private set; } = true;
        public bool AutoSplit { get; private set; } = true;
        public bool AutoMiddle { get; private set; } = true;
        public bool AutoReset { get; private set; } = true;

        public Settings()
        {
            InitializeComponent();
        }

        private void AutoStartBox_CheckedChanged(object sender, EventArgs e)
        {
            AutoStart = AutoStartBox.Checked;
        }

        private void AutoEndBox_CheckedChanged(object sender, EventArgs e)
        {
            AutoEnd = AutoEndBox.Checked;
        }

        private void AutoSplitBox_CheckedChanged(object sender, EventArgs e)
        {
            AutoMiddle = AutoMidBox.Checked;
        }

        private void AutoMidBox_CheckedChanged(object sender, EventArgs e)
        {
            AutoMiddle = AutoMidBox.Checked;
        }

        private void AutoResetBox_CheckedChanged(object sender, EventArgs e)
        {
            AutoReset = AutoResetBox.Checked;
        }

        public XmlNode GetSettings(XmlDocument document)
        {
            XmlElement settingsNode = document.CreateElement("Settings");

            settingsNode.AppendChild(SettingsHelper.ToElement(document, "Version", Assembly.GetExecutingAssembly().GetName().Version.ToString(3)));
            settingsNode.AppendChild(SettingsHelper.ToElement(document, "AutoStart", AutoStart));
            settingsNode.AppendChild(SettingsHelper.ToElement(document, "AutoEnd", AutoEnd));
            settingsNode.AppendChild(SettingsHelper.ToElement(document, "AutoSplit", AutoSplit));
            settingsNode.AppendChild(SettingsHelper.ToElement(document, "AutoMiddle", AutoMiddle));
            settingsNode.AppendChild(SettingsHelper.ToElement(document, "AutoReset", AutoReset));

            return settingsNode;
        }

        public void SetSettings(XmlNode settings)
        {
            var element = (XmlElement)settings;

            AutoStart = SettingsHelper.ParseBool(settings["AutoStart"], true);
            AutoEnd = SettingsHelper.ParseBool(settings["AutoEnd"], true);
            AutoSplit = SettingsHelper.ParseBool(settings["AutoSplit"], true);
            AutoMiddle = SettingsHelper.ParseBool(settings["AutoMiddle"], true);
            AutoReset = SettingsHelper.ParseBool(settings["AutoReset"], true);

            AutoStartBox.Checked = AutoStart;
            AutoEndBox.Checked = AutoEnd;
            AutoSplitBox.Checked = AutoSplit;
            AutoMidBox.Checked = AutoMiddle;
            AutoResetBox.Checked = AutoReset;
        }
    }
}
