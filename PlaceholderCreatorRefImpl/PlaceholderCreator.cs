using Sdl.FileTypeSupport.Framework.BilingualApi;

namespace PlaceholderCreatorRefImpl
{
    internal class PlaceholderCreator : IBilingualContentHandler
    {
        private MyCustomBatchTaskSettings settings;

        public PlaceholderCreator(MyCustomBatchTaskSettings settings)
        {
            this.settings = settings;
        }

        public void ProcessParagraphUnit(IParagraphUnit paragraphUnit)
        {
            if (paragraphUnit.IsStructure) { return; }

            foreach (var segPair in paragraphUnit.SegmentPairs)
            {
                var source = segPair.Source;

                source.AcceptVisitor(new PlaceholderCreatorVisitor(settings));
            }
        }

        #region Not Used

        public void Complete()
        {
        }

        public void FileComplete()
        {
        }

        public void Initialize(IDocumentProperties documentInfo)
        {
        }

        public void SetFileProperties(IFileProperties fileInfo)
        {
        }

        #endregion Not Used
    }
}