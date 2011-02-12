using System;
using System.Collections.Generic;
using System.IO;
using Core.FileTypes;
using Core.FileTypes.GPX;

namespace Core.FileManager
{
    public class OpenGPSFilesManager
    {
        private String _currentFolder = String.Empty;
        /// <summary>
        /// Gets starting folder.
        /// </summary>
        /// <returns></returns>
        public string GetStartFolder()
        {
            return Globals.Globals.GPSFileFolder;
        }

        #region Properties.
        /// <summary>
        /// Gets or Sets current folder.
        /// </summary>
        public string CurrentFolder
        {
            get
            {
                return _currentFolder;
            }
            set
            {
                if (ValidateFolder(value))
                {
                    _currentFolder = value;
                }
                else
                {
                    throw new FileNotFoundException(String.Format("Path {0} is not valid.", value));
                }
            }
        }
        #endregion

        private bool ValidateFolder(string value)
        {
            return Directory.Exists(value);
        }

        /// <summary>
        /// Gets current folder file list.
        /// </summary>
        /// <returns></returns>
        public List<FileInfo> GetFileInfoList()
        {
            return GetFileInfoForCurrentFolder("*.*");
        }

        /// <summary>
        /// Gets current folder's file list according to specific filters.
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        public List<FileInfo> GetFileInfoList(List<string> filters)
        {
            List<FileInfo> filesCollection = new List<FileInfo>();
            foreach (string pattern in filters)
            {
                filesCollection.AddRange(GetFileInfoForCurrentFolder(pattern));
            }
            return filesCollection;
        }

        private List<FileInfo> GetFileInfoForCurrentFolder(string searchPattern)
        {
            try
            {
                List<FileInfo> filesCollection = new List<FileInfo>();
                String[] files = Directory.GetFiles(_currentFolder, searchPattern);
                for (int i = 0; i < files.Length; i++)
                {
                    FileInfo file = new FileInfo(files[i]);
                    filesCollection.Add(file);
                }
                return filesCollection;
            }
            catch (Exception exp)
            {
                throw new ApplicationException(String.Format("Cannot process files in path {0}.", _currentFolder), exp);
            }
        }

        /// <summary>
        /// Gets GPS file for specific filename.
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public IFile GetFile(string filename)
        {
            try
            {
                IFile file;
                FileInfo fileInfo = new FileInfo(filename);
                string ext = fileInfo.Extension.ToUpper();

                switch (ext)
                {
                    case ".GPX":
                        file = new GPX11File(filename);
                        if (file.IsFileValid == false)
                        {
                            file = new FileTypes.GPX.GPX10.GPX10File(filename);
                        }
                        break;
                    default:
                        throw new ApplicationException("File extension is not recognized.");
                }
                return file;
            }
            catch (Exception)
            {
                return new InvalidFile();
            }
        }
    }
}
