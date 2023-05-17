using System;

namespace Hacathoon_Master.Models
{
    public class HackathonDescriptionViewModel : HackathonViewModel
    {
        public bool IsUserInvolved { get; set; }
        
        public byte[] Image { get; set; }
        
        public string GetFileFromBytes()
        {
            return string.Format("data:image/png;base64,{0}", Convert.ToBase64String(Image));
        }
    }
}