using System.IO;
using Microsoft.AspNetCore.Http;

namespace Hacathoon_Master.Helpers
{
    public interface IFileHelper
    {
        public byte[] ReadIFormFileToByteArray(IFormFile file)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                file.CopyTo(ms);
                return ms.ToArray();
            }
        }
    }
}