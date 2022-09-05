    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Business.Abstract
{
    public interface IFileService
    {
        string FileSave(string filePath, IFormFile file);
        string FileSaveToFtp(IFormFile file);
        byte[] FileSaveToDatabase(IFormFile file);
        void FileDeleteToServer(string path);
        void FileDeleteFromFtp(string path);
    }
}
