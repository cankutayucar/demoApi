using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Business.Abstract;
using Entities.Dtos;
using Microsoft.AspNetCore.Http;

namespace Business.Concrete
{
    public class FileService : IFileService
    {
        public string FileSave(string filePath, IFormFile file)
        {
            string fileNme = Guid.NewGuid().ToString();
            var ext = fileNme.Substring(fileNme.LastIndexOf('.'));
            ext = ext.ToLower();
            fileNme += ext;
            string path = filePath + fileNme;
            using (var stream = File.Create(path))
            {
                file.CopyTo(stream);
            }
            return fileNme;
        }

        public string FileSaveToFtp(IFormFile file)
        {
            string fileNme = Guid.NewGuid().ToString();
            var ext = fileNme.Substring(fileNme.LastIndexOf('.'));
            ext = ext.ToLower();
            fileNme += ext;
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create("Ftp adresi" + fileNme);
            request.Credentials = new NetworkCredential("kullanıcı adı", "şifre");
            request.Method = WebRequestMethods.Ftp.UploadFile;
            using (Stream ftpstream = request.GetRequestStream())
            {
                file.CopyTo(ftpstream);
            }
            return fileNme;
        }

        public byte[] FileSaveToDatabase(IFormFile file)
        {
            using (var memoryStream = new MemoryStream())
            {
                file.CopyTo(memoryStream);
                var fileBytes = memoryStream.ToArray();
                string files = Convert.ToBase64String(fileBytes);
                return fileBytes;
            }
        }
    }
}
