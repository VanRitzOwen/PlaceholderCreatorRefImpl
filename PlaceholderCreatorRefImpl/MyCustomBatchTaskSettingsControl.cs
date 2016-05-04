using Sdl.Desktop.IntegrationApi;
using System;
using System.Windows.Forms;

namespace PlaceholderCreatorRefImpl
{
    public partial class MyCustomBatchTaskSettingsControl : UserControl, ISettingsAware<MyCustomBatchTaskSettings>
    {
        public MyCustomBatchTaskSettingsControl()
        {
            InitializeComponent();
        }

        public MyCustomBatchTaskSettings Settings { get; set; }

        protected override void OnLoad(EventArgs e)
        {
            SettingsBinder.DataBindSetting<string>(textBox1, "Text", Settings, nameof(Settings.WordToReplace));
        }
    }
}