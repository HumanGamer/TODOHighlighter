using Microsoft.VisualStudio.Language.StandardClassification;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
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
		public static Format_Comment_Todo instance;

		public Format_Comment_Todo()
		{
			instance = this;

			DisplayName = "Todo Comment";

			BackgroundCustomizable = false;
			ForegroundColor = DefaultStyle.Color;
			IsBold = DefaultStyle.Bold;
			IsItalic = DefaultStyle.Italic;
		}
	}

	internal static class Definitions
	{
		[Export(typeof(ClassificationTypeDefinition))]
		[Name("Comment.Todo")]
		private static readonly ClassificationTypeDefinition Definition_Comment_Todo;
	}

	internal class DefaultStyle
	{
		public static Color Color = Color.FromRgb(0, 141, 217);
		public static bool Bold = true;
		public static bool Italic = false;
	}
}
