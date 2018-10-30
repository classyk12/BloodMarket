using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodTrace.Models
{
    public class BloodUser
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Location { get; set; }
        public string BloodGroup { get; set; }
        public string ImagePath { get; set; }
        public string FullLogoPath
        {
            get
            {
                if (string.IsNullOrEmpty(ImagePath))
                    return string.Empty;

                return string.Format("http://bloodmarket.azurewebsites.net/{0}", ImagePath.Substring(1));
            }
        }
        public string Country { get; set; }
        public int Date { get; set; }
        public object ImageArray { get; set; }
    }
}
