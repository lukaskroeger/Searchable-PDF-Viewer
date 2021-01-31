using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.Media.Ocr;
using Windows.UI.Xaml.Media;

namespace Searchable_PDF_Viewer
{
    public class Page
    {
        public Page(ImageSource imageSource, SoftwareBitmap image)
        {
            this.Words = new Collection<OcrWord>();
            this.WordOverlays = new ObservableCollection<OcrWord>();
            this.Image = image;
            this.ImageSource = imageSource;
        }
        public Collection<OcrWord> Words { get; set; }
        public ObservableCollection<OcrWord> WordOverlays { get; set; }
        public ImageSource ImageSource { get; set; }
        public SoftwareBitmap Image { get; set; }

        public bool FindWord(string searchString)
        {
            WordOverlays.Clear();
            foreach(var word in Words)
            {
                if (word.Text.Contains(searchString))
                {
                    WordOverlays.Add(word);
                }
            }
            return WordOverlays.Count > 0; 
        }
    }
}
