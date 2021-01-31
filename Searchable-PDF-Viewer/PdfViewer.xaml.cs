using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Searchable_PDF_Viewer
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PdfViewer : Windows.UI.Xaml.Controls.Page
    {
        private PdfViewerViewModel _viewModel;
        public PdfViewer()
        {
            this.InitializeComponent();
            _viewModel = new PdfViewerViewModel();
            this.DataContext = _viewModel;
        }

        private void OpenFileButton_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.OpenPdf();
        }

        private void Search(SearchBox sender, SearchBoxQueryChangedEventArgs args)
        {
            if (_viewModel.FindWord(args.QueryText))
            {
                _viewModel.FoundPages?.MoveNext();
                (PdfContainer.ContainerFromItem(_viewModel.FoundPages?.Current) as FrameworkElement)?.StartBringIntoView();
            }
        }
        
        private void SearchNext(object sender, SearchBoxQuerySubmittedEventArgs e)
        {
            _viewModel.FoundPages?.MoveNext();
            (PdfContainer.ContainerFromItem(_viewModel.FoundPages?.Current) as FrameworkElement)?.StartBringIntoView();          
           
        }

    }
}
