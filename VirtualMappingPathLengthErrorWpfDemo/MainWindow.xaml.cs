using Microsoft.Web.WebView2.Core;
using System;
using System.IO;
using System.Linq;
using System.Windows;

namespace VirtualMappingPathLengthErrorWpfDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            InitializeAsync();
        }

        private async void InitializeAsync()
        {
            //This is a quick hack to find the test directory - easily broken, this is just
            //for testing and not meant to demonstrate anything.
            //
            //Note that the test directory is not included in the project because
            //Visual Studio does not seem to currently support long file paths in the
            //solution explorer GUI?
            //
            var testSiteFolder = @$"{string.Join("\\", AppContext.BaseDirectory.Split("\\").TakeWhile(x => x != "VirtualHostNameToFolderMappingPathLengthTest"))}\VirtualHostNameToFolderMappingPathLengthTest\VirtualHostNameToFolderMappingPathLengthTestForLongFilePathLength-LongFolderNameToHelpEnsureLongPathLength";

            await VirtualView.EnsureCoreWebView2Async();

            VirtualView.CoreWebView2.SetVirtualHostNameToFolderMapping(
                "longfilepathtest.com",
                testSiteFolder,
                CoreWebView2HostResourceAccessKind.Allow);

            VirtualView.Source = new Uri("https://longfilepathtest.com/index.html");
        }
    }
}
