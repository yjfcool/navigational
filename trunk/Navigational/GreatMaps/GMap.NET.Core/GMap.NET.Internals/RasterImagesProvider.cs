using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace GMap.NET.Internals
{
    public class RasterImagesProvider
    {
        private string _path = string.Empty;
        private string _fomattedPath = string.Empty;

        /// <summary>
        /// Gets raster images location.
        /// </summary>
        public string ImagesPath
        {
            get
            {
                return _path;
            }
        }

        public RasterImagesProvider(string pathToImages)
        {
            if (Directory.Exists(pathToImages))
            {
                _path = Path.GetFullPath(pathToImages);
                _fomattedPath = Path.Combine(_path, @"{0}\{1}\{2}.png");
            }
            else
            {
                throw new DirectoryNotFoundException("Cannot initiate raster maps.");
            }
        }

        public PureImage GetRasterImage(Point position, int zoom)
        {
            int x = position.X;
            int y = position.Y;
            
            string file = string.Format(_fomattedPath,zoom,y,x);
            
            if (File.Exists(file))
            {
                Stream stream = new StreamReader(file).BaseStream;
                PureImage img = GMaps.Instance.ImageProxy.FromStream(stream);
                stream.Close();
                return img;
            }
            return null;
        }
    }
}
