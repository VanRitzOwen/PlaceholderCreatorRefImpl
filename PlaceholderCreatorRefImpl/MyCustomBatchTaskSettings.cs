using Sdl.Core.Settings;

namespace PlaceholderCreatorRefImpl
{
    public class MyCustomBatchTaskSettings : SettingsGroup
    {
        public string WordToReplace
        {
            get { return GetSetting<string>(nameof(WordToReplace)); }
            set { GetSetting<string>(nameof(WordToReplace)).Value = value; }
        }

        protected override object GetDefaultValue(string settingId)
        {
            switch (settingId)
            {
                case nameof(WordToReplace):
                    return string.Empty;

                default:
                    break;
            }

            return base.GetDefaultValue(settingId);
        }
    }
}