using System;
using System.Collections.Generic;
namespace Core.ElementsOrginizer
{
    public class ElementsOrginizer
    {
        private static ElementsOrginizer _instance = null;
        private static List<FileTypes.IFile> _files = new List<FileTypes.IFile>();
        /// <summary>
        /// Private Constructor.
        /// </summary>
        private ElementsOrginizer()
        {
        }

        /// <summary>
        /// Gets <c>ElementsOrginizer</c> instance.
        /// </summary>
        public static ElementsOrginizer Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ElementsOrginizer();
                }
                return _instance;
            }
        }

        /// <summary>
        /// Adds an IFile to the elements orginizer.
        /// </summary>
        /// <param name="file1"></param>
        public void AddFile(FileTypes.IFile file1)
        {
            if (_files.Contains(file1))
            {
                
            }
        }
    }
}