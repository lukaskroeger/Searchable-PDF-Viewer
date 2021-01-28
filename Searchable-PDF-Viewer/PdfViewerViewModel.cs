using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Pdf;
using Windows.Graphics.Imaging;
using Windows.Media.Ocr;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace Searchable_PDF_Viewer
{
    public class PdfViewerViewModel
    {
        public ObservableCollection<PdfPage> PdfPages { get; set; }
        public PdfViewerViewModel()
        {
            PdfPages = new ObservableCollection<PdfPage>();
        }

        public async void OpenLocal()
        {

            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
            picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.HomeGroup;
            picker.FileTypeFilter.Add(".pdf");

            Windows.Storage.StorageFile file = await picker.PickSingleFileAsync();
            PdfDocument doc = await PdfDocument.LoadFromFileAsync(file);

            await LoadPdf(doc);
            await ExtractWords();
        }

        public async Task LoadPdf(PdfDocument pdfDoc)
        {
            PdfPages.Clear();
            for (uint i = 0; i < pdfDoc.PageCount; i++)
            {
                var page = pdfDoc.GetPage(i);

                using (InMemoryRandomAccessStream stream = new InMemoryRandomAccessStream())
                {
                    await page.RenderToStreamAsync(stream);
                    var decoder = await BitmapDecoder.CreateAsync(stream);
                    var image = await decoder.GetSoftwareBitmapAsync(BitmapPixelFormat.Bgra8, BitmapAlphaMode.Premultiplied);

                    var source = new SoftwareBitmapSource();
                    await source.SetBitmapAsync(image);
                    PdfPages.Add(new PdfPage(source, image));
                };
            }
        }

        public async Task ExtractWords()
        {
            foreach (var pdfPage in PdfPages)
            {
                if (pdfPage.Image.PixelWidth > OcrEngine.MaxImageDimension || pdfPage.Image.PixelHeight > OcrEngine.MaxImageDimension)
                {
                    return;
                }
                OcrEngine ocrEngine = OcrEngine.TryCreateFromUserProfileLanguages();
                if (ocrEngine != null)
                {
                    var ocrResult = await ocrEngine.RecognizeAsync(pdfPage.Image);
                    foreach (var line in ocrResult.Lines)
                    {
                        foreach (var word in line.Words)
                        {
                            pdfPage.Words.Add(new Word(word));
                        }
                    }
                }
            }

        }


        public IEnumerator<PdfPage> FoundPages{get; set;}

       public bool FindWord(string searchString)
        {
            if (!searchString.Equals(""))
            {
                var foundPages = new List<PdfPage>();
                foreach (var page in PdfPages)
                {
                    if (page.FindWord(searchString))
                    {
                        foundPages.Add(page);
                    }
                }
                FoundPages = foundPages.GetEnumerator();
                FoundPages.Reset();
                return true;
            }
            else
            {
                FoundPages.Dispose();
                foreach (var page in PdfPages)
                {
                    page.WordOverlays.Clear();
                }
                return false;
            }
        }

    }
}
