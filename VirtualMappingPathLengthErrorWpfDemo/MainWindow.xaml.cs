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
        private FileInfo _longFileNameFile;
        private DirectoryInfo _longFileNameFolder;

        public MainWindow()
        {
            InitializeComponent();
            InitializeAsync();
            DataContext = this;
        }

        public FileInfo LongFileNameFile
        {
            get => _longFileNameFile;
            set
            {
                if (Equals(value, _longFileNameFile)) return;
                _longFileNameFile = value;
                OnPropertyChanged();
            }
        }

        public DirectoryInfo LongFileNameFolder
        {
            get => _longFileNameFolder;
            set
            {
                if (Equals(value, _longFileNameFolder)) return;
                _longFileNameFolder = value;
                OnPropertyChanged();
            }
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

            LongFileNameFolder = new DirectoryInfo(Path.Combine(testSiteFolder.FullName,
                "1808-Agua-Blanca-Ranch-Sign-at-the-Manville-Road-Entrance-to-the-Ironwood-Forest-National-Monument"));

            LongFileNameFile = new FileInfo(Path.Combine(LongFileNameFolder.FullName,
                "1808-Agua-Blanca-Ranch-Sign-at-the-Manville-Road-Entrance-to-the-Ironwood-Forest-National-Monument.jpg"));

            await VirtualView.EnsureCoreWebView2Async();

            VirtualView.CoreWebView2.SetVirtualHostNameToFolderMapping("webview2longfile.test", testSiteFolder.FullName,
                CoreWebView2HostResourceAccessKind.Allow);

            VirtualView.Source = new Uri("https://webview2longfile.test/index.html");
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}