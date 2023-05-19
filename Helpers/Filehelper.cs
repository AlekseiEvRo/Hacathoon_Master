using System.IO;
using Microsoft.AspNetCore.Http;

namespace Hacathoon_Master.Helpers
{
    public class Filehelper : IFileHelper
    {
        public byte[] ReadIFormFileToByteArray(IFormFile file)
        {
            if (file == null)
                return new byte[0];
            using (MemoryStream ms = new MemoryStream())
            {
                file.CopyTo(ms);
                return ms.ToArray();
            }
        }
    }
}