using Sdl.FileTypeSupport.Framework.Core.Utilities.BilingualApi;
using Sdl.FileTypeSupport.Framework.IntegrationApi;
using Sdl.ProjectAutomation.AutomaticTasks;
using Sdl.ProjectAutomation.Core;

namespace PlaceholderCreatorRefImpl
{
    [AutomaticTask("PlaceholderCreator",
        "Placeholder Creator",
        "Reference implementation for creating placeholders",
        GeneratedFileType = AutomaticTaskFileType.BilingualTarget)]
    [AutomaticTaskSupportedFileType(AutomaticTaskFileType.BilingualTarget)]
    [RequiresSettings(typeof(MyCustomBatchTaskSettings), typeof(MyCustomBatchTaskSettingsPage))]
    public class MyCustomBatchTask : AbstractFileContentProcessingAutomaticTask
    {
        private MyCustomBatchTaskSettings settings;

        public override bool OnFileComplete(ProjectFile projectFile, IMultiFileConverter multiFileConverter)
        {
            return true;
        }

        protected override void ConfigureConverter(ProjectFile projectFile, IMultiFileConverter multiFileConverter)
        {
            var phCreator = new PlaceholderCreator(settings);
            var handler = new BilingualContentHandlerAdapter(phCreator);

            multiFileConverter.AddBilingualProcessor(handler);
        }

        protected override void OnInitializeTask()
        {
            settings = GetSetting<MyCustomBatchTaskSettings>();
        }
    }
}