using Microsoft.VisualStudio.Shell;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace TODOHighlighter.Options
{
	public static class Settings
	{
		public class Behaviour
		{
			public static bool CaseSensitive = false;
			public static bool RequireColon = true;
			public static bool AllowDocComments = true;
			public static List<string> Prefixes = new List<string>(new string[] { "todo", "temp", "tmp", "fixme" });
		}

		public class Style
		{
			public static Color Color = Color.FromRgb(0, 141, 217);
			public static bool Bold = true;
		}

	}

	public class OptionPageGrid : DialogPage
	{
		[Category("Behaviour")]
		[DisplayName("Case Sensitive")]
		[Description("Do prefixes have to be case sensitive?")]
		public bool CaseSensitive
		{
			get => Settings.Behaviour.CaseSensitive;
			set => Settings.Behaviour.CaseSensitive = value;
		}


		[Category("Behaviour")]
		[DisplayName("Require Colon")]
		[Description("Is a colon \":\" required after the prefix?")]
		public bool RequireColon
		{
			get => Settings.Behaviour.RequireColon;
			set => Settings.Behaviour.RequireColon = value;
		}

		[Category("Behaviour")]
		[DisplayName("Allow in Doc Comments")]
		[Description("Should special comments be highlighted in Doc Comments?")]
		public bool AllowDocComments
		{
			get => Settings.Behaviour.AllowDocComments;
			set => Settings.Behaviour.AllowDocComments = value;
		}

		[Category("Behaviour")]
		[DisplayName("Prefixes")]
		[Description("The list of prefixes to look for")]
		public List<string> Prefixes
		{
			get => Settings.Behaviour.Prefixes;
			set => Settings.Behaviour.Prefixes = value;
		}

		[Category("Style")]
		[DisplayName("Prefixes")]
		[Description("The color of highlighted lines")]
		public Color TodoColor
		{
			get => Settings.Style.Color;
			set => Settings.Style.Color = value;
		}

		[Category("Style")]
		[DisplayName("Bold")]
		[Description("Should highlighted lines be bold?")]
		public bool Bold
		{
			get => Settings.Style.Bold;
			set => Settings.Style.Bold = value;
		}
	}
}
