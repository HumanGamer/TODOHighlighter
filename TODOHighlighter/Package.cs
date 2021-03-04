using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Media;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;
using TODOHighlighter.Options;
using Task = System.Threading.Tasks.Task;

namespace TODOHighlighter
{
	[PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
	[ProvideAutoLoad(VSConstants.UICONTEXT.ShellInitialized_string, PackageAutoLoadFlags.BackgroundLoad)]
	[Guid("f3bd3bea-d1a9-4140-ad18-adc750d25b06")]
	[ProvideOptionPage(typeof(OptionPageGrid), "TODO Highlighter", "General", 0, 0, true)]
	public sealed class Package : AsyncPackage
	{
		protected override async Task InitializeAsync(CancellationToken token, IProgress<ServiceProgressData> progress)
		{
			Project.Package = this;

			await base.InitializeAsync(token, progress);
			await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(token);
		}
	}

	internal static class Project
	{
		internal static Package Package;
	}
}
