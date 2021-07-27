using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using Microsoft.Web.WebView2.Core;
using VirtualMappingPathLengthErrorWpfDemo.Annotations;

namespace VirtualMappingPathLengthErrorWpfDemo
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : INotifyPropertyChanged
    {
        public MainWindow()
        {
            InitializeComponent();
            InitializeAsync();
            DataContext = this;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private async void InitializeAsync()
        {
            //This is a quick hack to find the test directory - easily broken, this is just
            //for testing and not meant to demonstrate anything.
            //
            //Note that the test directory is not included in the project because
            //Visual Studio does not seem to currently support long file paths in the
            //solution explorer GUI?
            //
            var directoryParts = AppContext.BaseDirectory.Split("\\", StringSplitOptions.RemoveEmptyEntries);
            var testSiteFolder = new DirectoryInfo(
                @$"{string.Join("\\", directoryParts[..Array.IndexOf(directoryParts, "VirtualMappingPathLengthErrorWpfDemo")])}\VirtualHostNameToFolderMappingPathLengthTestForLongFilePathLength-LongFolderNameToHelpEnsureLongPathLength");

            await VirtualView.EnsureCoreWebView2Async();

            VirtualView.CoreWebView2.SetVirtualHostNameToFolderMapping("webview2.test", testSiteFolder.FullName,
                CoreWebView2HostResourceAccessKind.Allow);

            VirtualView.Source = new Uri("https://webview2.test/index.html");

            await VirtualViewLongIndex.EnsureCoreWebView2Async();

            VirtualViewLongIndex.CoreWebView2.SetVirtualHostNameToFolderMapping("webview2longindex.test", testSiteFolder.FullName,
                CoreWebView2HostResourceAccessKind.Allow);

            VirtualViewLongIndex.Source = new Uri("https://webview2longindex.test/index-long-long-long-long-long-long-long-long-long-long-long-long-long-long-long-long-long-long-long-long-long-long-long-long-long-long-long-long-long-long-long-long-long-long-long-long-long-long-long-long-long-long-long-long-long-long-long-long-long.html");
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}