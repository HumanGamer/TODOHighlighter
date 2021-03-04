using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.Threading;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using TODOHighlighter.Base;
using TODOHighlighter.Highlight;

namespace TODOHighlighter.Options
{
	public interface IOptions : INotifyPropertyChanged
	{
		bool CaseSensitive { get; }
		bool RequireColon { get; }
		bool AllowDocComments { get; }
		string[] Prefixes { get; }
	}

	public class OptionPageGrid : DialogPage, IOptions
	{
		public event PropertyChangedEventHandler PropertyChanged;

		private bool _caseSensitive = false;
		private bool _requireColon = true;
		private bool _allowDocComments = false;
		private string[] _prefixes = new string[] { "TODO", "TEMP", "TMP", "FIXME" };

		[Category("Behaviour")]
		[DisplayName("Case Sensitive")]
		[Description("Do prefixes have to be case sensitive?")]
		public bool CaseSensitive
		{
			get => _caseSensitive;
			set
			{
				_caseSensitive = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CaseSensitive)));
			}
		}

		[Category("Behaviour")]
		[DisplayName("Require Colon")]
		[Description("Is a colon \":\" required after the prefix?")]
		public bool RequireColon
		{
			get => _requireColon;
			set
			{
				_requireColon = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(RequireColon)));
			}
		}

		[Category("Behaviour")]
		[DisplayName("Allow in Doc Comments")]
		[Description("Should special comments be highlighted in Doc Comments?")]
		public bool AllowDocComments
		{
			get => _allowDocComments;
			set
			{
				_allowDocComments = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AllowDocComments)));
			}
		}

		[Category("Behaviour")]
		[DisplayName("Prefixes")]
		[Description("The list of prefixes to look for")]
		public string[] Prefixes
		{
			get => _prefixes;
			set
			{
				_prefixes = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Prefixes)));
			}
		}
	}
}
