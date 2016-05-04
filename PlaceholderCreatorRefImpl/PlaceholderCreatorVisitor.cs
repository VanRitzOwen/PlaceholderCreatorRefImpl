using Sdl.FileTypeSupport.Framework.BilingualApi;
using Sdl.FileTypeSupport.Framework.Core.Utilities.BilingualApi;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace PlaceholderCreatorRefImpl
{
    internal class PlaceholderCreatorVisitor : IMarkupDataVisitor
    {
        private readonly IDocumentItemFactory itemFactory;
        private readonly List<IText> itextList = new List<IText>();
        private readonly MyCustomBatchTaskSettings settings;
        public PlaceholderCreatorVisitor(MyCustomBatchTaskSettings settings)
        {
            this.settings = settings;

            // The itemfactory is how we create all the IAbstractMarkupData
            this.itemFactory = DefaultDocumentItemFactory.CreateInstance();
        }

        public void VisitSegment(ISegment segment)
        {
            VisitChildren(segment);

            // After you visited and collected all IText, you can loop through
            // and find the text you want to replace
            ProcessITexts();
        }

        public void VisitTagPair(ITagPair tagPair)
        {
            VisitChildren(tagPair);
        }

        public void VisitText(IText text)
        {
            // Just collect all IText for now
            itextList.Add(text);
        }

        private IAbstractMarkupData CreateIText(string chunk)
        {
            var textProps = itemFactory.PropertiesFactory.CreateTextProperties(chunk);
            return itemFactory.CreateText(textProps);
        }

        private IAbstractMarkupData CreatePlaceholder(string chunk)
        {
            var tag = $"<{chunk}/>";
            var phProps = itemFactory.PropertiesFactory.CreatePlaceholderTagProperties(tag);
            phProps.TextEquivalent = chunk;
            phProps.TagContent = tag;

            return itemFactory.CreatePlaceholderTag(phProps);
        }

        private void ProcessITexts()
        {
            foreach (var itext in itextList)
            {
                var rawText = itext.Properties.Text;

                if (rawText.Contains(settings.WordToReplace))
                {
                    // Okay found the text we need to replace
                    // Now split the text up into chunks
                    var chunks = Regex.Split(rawText, $"({settings.WordToReplace})");

                    // We will be adding to the parent, so grab a ref to that
                    var parent = itext.Parent;

                    foreach (var chunk in chunks)
                    {
                        if (chunk == settings.WordToReplace)
                        {
                            parent.Add(CreatePlaceholder(chunk));
                        }
                        else
                        {
                            parent.Add(CreateIText(chunk));
                        }
                    }

                    // Important! Remove the original IText from parent
                    itext.RemoveFromParent();
                }
            }
        }

        private void VisitChildren(IAbstractMarkupDataContainer container)
        {
            foreach (var item in container)
            {
                item.AcceptVisitor(this);
            }
        }

        #region Not Used

        public void VisitCommentMarker(ICommentMarker commentMarker)
        {
        }

        public void VisitLocationMarker(ILocationMarker location)
        {
        }

        public void VisitLockedContent(ILockedContent lockedContent)
        {
        }

        public void VisitOtherMarker(IOtherMarker marker)
        {
        }

        public void VisitPlaceholderTag(IPlaceholderTag tag)
        {
        }

        public void VisitRevisionMarker(IRevisionMarker revisionMarker)
        {
        }

        #endregion Not Used
    }
}