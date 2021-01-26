using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Ocr;

namespace Searchable_PDF_Viewer
{
    public class Word
    {

        public Word(OcrWord ocrWord)
        {
            Text = ocrWord.Text;
            X = ocrWord.BoundingRect.X;
            Y = ocrWord.BoundingRect.Y;
            Width = ocrWord.BoundingRect.Width;
            Height = ocrWord.BoundingRect.Height;
        }

        public string Text { get; set; }
        private double _x;
        public double X
        {
            get => _x;
            set
            {
                _x = value;
            }
        }

        private double _y;
        public double Y
        {
            get => _y;
            set
            {
                _y = value;
            }
        }

        private double _width;
        public double Width
        {
            get => _width;
            set
            {
                _width = value;
            }
        }

        private double _height;
        public double Height
        {
            get => _height;
            set
            {
                _height = value;
            }
        }
    }
}
