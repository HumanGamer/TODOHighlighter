using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TODOHighlighter
{
	using Color = System.Windows.Media.Color;

	internal static class Default
	{
		internal static class Colors
		{
			internal static readonly Color Comment = Color.FromRgb(87, 166, 74);
			internal static readonly Color Todo = Color.FromRgb(38, 78, 196);
		}
	}
}
