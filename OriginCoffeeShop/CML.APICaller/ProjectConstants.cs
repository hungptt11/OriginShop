using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CML.APICaller
{
    public class ProjectConstants
    {
        public static string APIURL
        {
            get
            {
                return ConfigurationManager.AppSettings["APIURL"];
            }
        }
    }
}
