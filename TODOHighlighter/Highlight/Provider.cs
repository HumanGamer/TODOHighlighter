using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TODOHighlighter.Base;
using TODOHighlighter.Options;

namespace TODOHighlighter.Highlight
{
	[Export(typeof(IClassifierProvider))]
	[ContentType("C/C++"), ContentType("CSharp"), ContentType("JavaScript"), ContentType("TypeScript")]
	[TagType(typeof(ClassificationTag))]
	internal sealed class Provider : IClassifierProvider
	{
		private readonly IClassificationTypeRegistryService ClassificationTypeRegistryService;
		private readonly IClassifierAggregatorService ClassifierAggregatorService;
		private static bool _ignoreRequest;

		private readonly IOptions _options;

		[ImportingConstructor]
		public Provider(IClassificationTypeRegistryService classificationTypeRegistry, IClassifierAggregatorService classifierAggregator, IOptions options)
		{
			ClassificationTypeRegistryService = classificationTypeRegistry;
			ClassifierAggregatorService = classifierAggregator;
			_options = options;
		}


		public IClassifier GetClassifier(ITextBuffer textBuffer)
		{
			if (_ignoreRequest) return null;

			try
			{
				_ignoreRequest = true;

				return textBuffer.Properties.GetOrCreateSingletonProperty
				(
					() => new Classifier
					(
						ClassificationTypeRegistryService,
						ClassifierAggregatorService.GetClassifier(textBuffer),
						_options
					)
				);
			}

			finally
			{
				_ignoreRequest = false;
			}
		}
	}
}
