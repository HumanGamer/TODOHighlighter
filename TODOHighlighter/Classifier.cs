using Microsoft.VisualStudio.Language.StandardClassification;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TODOHighlighter
{

	internal class Classifier : IClassifier
	{
		public event EventHandler<ClassificationChangedEventArgs> ClassificationChanged;

		private readonly IClassificationType Comment_Todo;

		private bool _isClassificationRunning;
		private readonly IClassifier _classifier;

		internal Classifier(IClassificationTypeRegistryService registry, IClassifier classifier)
		{
			_isClassificationRunning = false;
			_classifier = classifier;

			Comment_Todo = registry.GetClassificationType("Comment.Todo");
		}

		public IList<ClassificationSpan> GetClassificationSpans(SnapshotSpan span)
		{
			if (_isClassificationRunning) return new List<ClassificationSpan>();

			try
			{
				_isClassificationRunning = true;
				return Classify(span);
			}
			finally
			{
				_isClassificationRunning = false;
			}
		}

		private IList<ClassificationSpan> Classify(SnapshotSpan span)
		{
			List<ClassificationSpan> spans = new List<ClassificationSpan>();

			if (span.IsEmpty)
				return spans;

			var text = span.GetText();

			var offset = 0;

			int currentOffset;

		NextComment:
			foreach (Match match in new Regex(@"(?<Star>\*)?" + @"(?<Slashes>(?<!/)(/{2,}))[ \t\v\f]*" + @"(?<Comment>[^\n]*)").Matches(text))
			{
				var starOffset = 0;

				if (match.Groups["Star"].Length > 0)
					goto SkipComment;

				var matchedSpan = new SnapshotSpan(span.Snapshot, new Span(span.Start + offset + starOffset + match.Index, match.Length - starOffset));
				var intersections = _classifier.GetClassificationSpans(matchedSpan);

				foreach (var Intersection in intersections)
				{
					var Classifications = Intersection.ClassificationType.Classification.Split(new[] { " - " }, StringSplitOptions.None);

					// Comment must be classified as either "comment" or "XML Doc Comment".
					if (!Utils.IsClassifiedAs(Classifications, new[] { PredefinedClassificationTypeNames.Comment, "XML Doc Comment" }))
						goto SkipComment;

					// Prevent recursive matching fragment of comment as another comment.
					if (Utils.IsClassifiedAs(Classifications, new[] { "Comment.Default" }))//, "Comment.Triple" }))
						goto SkipComment;
				}

				// Start offset of slashes (without star part: either "*" or "*/").
				int SlashesStart = span.Start + offset + match.Groups["Slashes"].Index;
				if (starOffset == 2) SlashesStart += 1;

				// Slashes length (optionally without first "/" as it's end of multiline comment).
				var SlashesLength = match.Groups["Slashes"].Length;
				if (starOffset == 2) SlashesLength -= 1;

				// If comment is triple slash (begins with "///").
				var IsTripleSlash = SlashesLength == 3;

				if (IsTripleSlash && !Settings.AllowDocComments)
					goto SkipComment;

				var commentText = match.Groups["Comment"].Value;
				int commentStart = span.Start + offset + match.Groups["Comment"].Index;

				var skipInlineMatching = false;

				for (int i = 0; i < PrefixManager.Count; i++)
				{
					var prefix = PrefixManager.GetPrefix(i);

					if (Settings.RequireColon)
						prefix += ':';

					var compareText = commentText;

					if (!Settings.CaseSensitive)
					{
						prefix = prefix.ToLower();
						compareText = compareText.ToLower();
					}

					if (compareText.Trim().StartsWith(prefix.ToLower() + ":"))
					{
						spans.Add(new ClassificationSpan(new SnapshotSpan
						(
							span.Snapshot, new Span
							(
								SlashesStart,
								commentText.Length + SlashesLength

							)
						), Comment_Todo));

						skipInlineMatching = true;
					}
				}

				if (skipInlineMatching)
					goto FinishClassification;

				FinishClassification:
				currentOffset = match.Index + match.Length;
				text = text.Substring(currentOffset);
				offset += currentOffset;
				goto NextComment;

			SkipComment:
				currentOffset =
						match.Groups["Slashes"].Index
					+ match.Groups["Slashes"].Length
				;

				text = text.Substring(currentOffset);
				offset += currentOffset;
				goto NextComment;
			}

			return spans;
		}
	}
}
