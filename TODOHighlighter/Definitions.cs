﻿using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TODOHighlighter
{
	internal static class Definitions
	{
		[Export(typeof(ClassificationTypeDefinition))]
		[Name("Comment.Default")]
		private static readonly ClassificationTypeDefinition
		Definition_Comment_Default;

		[Export(typeof(ClassificationTypeDefinition))]
		[Name("Comment.Todo")]
		private static readonly ClassificationTypeDefinition
		Definition_Comment_Todo;
	}
}
