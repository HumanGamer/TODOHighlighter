using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Media;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using TODOHighlighter.Options;
using Task = System.Threading.Tasks.Task;

namespace TODOHighlighter.Base
{
	[PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
	[ProvideAutoLoad(VSConstants.UICONTEXT.ShellInitialized_string, PackageAutoLoadFlags.BackgroundLoad)]
	[Guid(GUID)]
	[ProvideOptionPage(typeof(OptionPageGrid), "TODO Highlighter", "General", 0, 0, true)]
	public sealed class TodoHighlighterPackage : AsyncPackage
	{
		public const string GUID = "f3bd3bea-d1a9-4140-ad18-adc750d25b06";

		protected override async Task InitializeAsync(CancellationToken token, IProgress<ServiceProgressData> progress)
		{
			Project.Package = this;

			await base.InitializeAsync(token, progress);
			await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(token);
		}

		[Export(typeof(IOptions))]
		public OptionPageGrid Options
		{
			get
			{
				if (_options is null)
				{
					_options = Load();
					if (_options is null) _options = new OptionPageGrid();
				}
				return _options;
			}
		}

		public static OptionPageGrid Load()
		{
			var joinableTaskFactory = ThreadHelper.JoinableTaskFactory;
			return joinableTaskFactory.Run(LoadAsync);
		}

		public async static System.Threading.Tasks.Task<OptionPageGrid> LoadAsync()
		{
			await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
			lock (_syncRoot)
			{
				var shell = (IVsShell)GetGlobalService(typeof(SVsShell));
				var guid = new Guid(GUID);
				if (ErrorHandler.Failed(shell.IsPackageLoaded(ref guid, out IVsPackage package)))
				{
					if (ErrorHandler.Failed(shell.LoadPackage(ref guid, out package))) return null;
				}
				var myPack = package as TodoHighlighterPackage;
				return (OptionPageGrid)myPack.GetDialogPage(typeof(OptionPageGrid));
			}
		}

		private OptionPageGrid _options;
		private static readonly object _syncRoot = new object();
	}

	internal static class Project
	{
		internal static Package Package;
	}
}
