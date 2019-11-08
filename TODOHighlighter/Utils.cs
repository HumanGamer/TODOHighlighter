using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TODOHighlighter
{
	internal static class Utils
	{
		// Check if $Source classifications contains any classification from $Search.
		internal static bool IsClassifiedAs
		(
			string[] Source,
			string[] Search
		)
		{
			return
			(
					Source.Length > 0
				&& Search.Length > 0
				&& (
						from SourceClassification in Source
						from SearchClassification in Search

						let SourceEntry = SourceClassification.ToLower()
						let SearchEntry = SearchClassification.ToLower()

						where
						(
								SourceEntry == SearchEntry
							|| SourceEntry.StartsWith(SearchEntry + ".")
						)

						select SourceEntry
					)

					.Any()
			);
		}
	}
}
