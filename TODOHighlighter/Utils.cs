using System.Linq;

namespace TODOHighlighter
{
	internal static class Utils
	{
		// Check if $Source classifications contains any classification from $Search.
		internal static bool IsClassifiedAs(string[] source, string[] search)
		{
			return
			(
				source.Length > 0
				&& search.Length > 0
				&& (
					from SourceClassification in source
					from SearchClassification in search
					let SourceEntry = SourceClassification.ToLower()
					let SearchEntry = SearchClassification.ToLower()
					where
					(
						SourceEntry == SearchEntry
						|| SourceEntry.StartsWith(SearchEntry + '.')
					)
					select SourceEntry
				)
				.Any()
			);
		}
	}
}