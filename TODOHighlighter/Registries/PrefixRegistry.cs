using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TODOHighlighter.Registries
{
	internal static class PrefixRegistry
	{
		private static readonly List<string> _prefixes = new List<string>();

		static PrefixRegistry()
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
			PrefixRegistry.Add("todo", "temp", "tmp", "fixme");
		}
	}
}
