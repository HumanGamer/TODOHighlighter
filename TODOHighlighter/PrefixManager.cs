using System.Collections.Generic;

namespace TODOHighlighter
{
	internal static class PrefixManager
	{
		private static readonly List<string> _prefixes = new List<string>();

		static PrefixManager()
		{
			InitDefaults();
		}

		public static void Add(params string[] prefixes)
		{
			foreach (var prefix in prefixes)
			{
				if (Contains(prefix))
					continue;
				_prefixes.Add(prefix);
			}
		}

		public static void Remove(string prefix)
		{
			if (!Contains(prefix))
				return;
			_prefixes.Remove(prefix);
		}

		public static bool Contains(string prefix)
		{
			return _prefixes.Contains(prefix);
		}

		public static int Count => _prefixes.Count;

		public static string GetPrefix(int index)
		{
			return _prefixes[index];
		}

		private static void InitDefaults()
		{
			Add("todo", "temp", "tmp", "fixme");
		}
	}
}