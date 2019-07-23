using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Samsonite.Mall.Test.Infrastructure {
    public class CustomHttpPostedFileBase: HttpPostedFileBase {
        private Stream stream;
        private string contentType;
        private string fileName;
        public CustomHttpPostedFileBase(string contentType, string fileName) {
            this.stream = CreateStream();
            this.contentType = contentType;
            this.fileName = fileName;
        }
        public CustomHttpPostedFileBase(int width, int height, string contentType, string fileName) {
            this.stream = CreateStream();
            this.contentType = contentType;
            this.fileName = fileName;
        }

        public override int ContentLength {
            get { return (int)stream.Length; }
        }

        public override string ContentType {
            get { return contentType; }
        }

        public override string FileName {
            get { return fileName; }
        }

        public override Stream InputStream {
            get { return stream; }
        }

        public override void SaveAs(string filename) {
            using (var file = File.Open(filename, FileMode.CreateNew))
                stream.CopyTo(file);
        }

        private MemoryStream CreateStream() {
            var stream = new MemoryStream();
            return stream;
        }
        private MemoryStream CreateStream(int width, int height) {
            var stream = new MemoryStream();
            var bitmap = new Bitmap(width, height);
            bitmap.Save(stream, ImageFormat.Jpeg);
            return stream;
        }
        public string S3FileUpload(Stream stream, string directory, string fileName, bool useNewFuid) {
            return string.Format("{0}/{1}", directory, fileName);
        }
    }
}
