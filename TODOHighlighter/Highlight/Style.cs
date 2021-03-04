using Microsoft.VisualStudio.Language.StandardClassification;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TODOHighlighter.Options;

namespace TODOHighlighter.Highlight
{
	[Export(typeof(EditorFormatDefinition))]
	[ClassificationType(ClassificationTypeNames = "Comment.Todo")]
	[Name("Comment.Todo")]
	[BaseDefinition(PredefinedClassificationTypeNames.Comment)]
	[UserVisible(true)]
	[Order(After = PredefinedClassificationTypeNames.Comment)]
	[Order(After = "XML Doc Comment")]
	internal sealed class Format_Comment_Todo
	:
		ClassificationFormatDefinition
	{
		public Format_Comment_Todo()
		{
			DisplayName = "Todo Comment";

			BackgroundCustomizable = false;
			ForegroundColor = Settings.Style.Color;
			IsBold = Settings.Style.Bold;
		}
	}
}
