using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;
using System.ComponentModel.Composition;

namespace TODOHighlighter
{
	internal static class Definitions
	{
		[Export(typeof(ClassificationTypeDefinition))] [Name(Consts.ClassificationTypName)]
		private static readonly ClassificationTypeDefinition Definition_Comment_Todo;
	}
}