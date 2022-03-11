using Microsoft.VisualStudio.Language.StandardClassification;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;
using System.ComponentModel.Composition;

namespace TODOHighlighter
{
	[Export(typeof(EditorFormatDefinition))]
	[ClassificationType(ClassificationTypeNames = Consts.ClassificationTypName)]
	[Name(Consts.ClassificationTypName)]
	[BaseDefinition(PredefinedClassificationTypeNames.Comment)]
	[UserVisible(true)]
	[Order(After = PredefinedClassificationTypeNames.Comment)]
	[Order(After = "XML Doc Comment")]
	internal sealed class Format_Comment_Todo : ClassificationFormatDefinition
	{
		public Format_Comment_Todo()
		{
			DisplayName = "Todo Comment";

			BackgroundCustomizable = false;
			ForegroundColor = Consts.Colors.Todo;
			IsBold = true;
		}
	}
}