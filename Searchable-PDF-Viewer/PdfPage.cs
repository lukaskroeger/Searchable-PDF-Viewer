using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.UI.Xaml.Media;

namespace Searchable_PDF_Viewer
{
    public class PdfPage
    {
        public PdfPage(ImageSource imageSource, SoftwareBitmap image)
        {
            this.Words = new ObservableCollection<Word>();
            this.Image = image;
            this.ImageSource = imageSource;
        }
        public ObservableCollection<Word> Words { get; set; }
        public ImageSource ImageSource { get; set; }
        public SoftwareBitmap Image { get; set; }
    }
}
