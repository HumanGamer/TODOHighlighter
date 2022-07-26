using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;
using System.ComponentModel.Composition;

namespace TODOHighlighter
{
	[Export(typeof(IClassifierProvider)), ContentType("C/C++"), ContentType("CSharp"), ContentType("JavaScript"), ContentType("TypeScript")]
	internal sealed class Provider : IClassifierProvider
	{
		[Import] private readonly IClassificationTypeRegistryService ClassificationTypeRegistryService;

		[Import] private readonly IClassifierAggregatorService ClassifierAggregatorService;

		private static bool _ignoreRequest;

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
						ClassifierAggregatorService.GetClassifier(textBuffer)
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