using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TODOHighlighter.Highlight
{
	internal static class Definitions
	{
		[Export(typeof(ClassificationTypeDefinition))]
		[Name("Comment.Todo")]
		private static readonly ClassificationTypeDefinition
		Definition_Comment_Todo;
	}
}
