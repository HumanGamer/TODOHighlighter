﻿using System;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;
using Task = System.Threading.Tasks.Task;

namespace TODOHighlighter
{
	[PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
	[ProvideAutoLoad(VSConstants.UICONTEXT.ShellInitialized_string, PackageAutoLoadFlags.BackgroundLoad)]
	[Guid(Package.PackageGuidString)]
	public sealed class Package : AsyncPackage
	{
		/// <summary>
		/// TODOHighlighter GUID string.
		/// </summary>
		public const string PackageGuidString = "f3bd3bea-d1a9-4140-ad18-adc750d25b06";

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