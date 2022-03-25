using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Service.Dto.EmailVO
{
    public class EmailSettings
    {
        public String PrimaryDomain { get; set; }
        public int PrimaryPort { get; set; }
        public bool UseSSL { get; set; }
        public String UsernameEmail { get; set; }
        public String UsernamePassword { get; set; }
        public String FromEmail { get; set; }
        public String ToEmail { get; set; }
        public String CcEmail { get; set; }

        public String PrimaryDomainNacional { get; set; }
        public int PrimaryPortNacional { get; set; }
        public bool UseSSLNacional { get; set; }
        public String UsernameEmailNacional { get; set; }
        public String UsernamePasswordNacional { get; set; }
        public String FromEmailNacional { get; set; }
        public String ToEmailNacional { get; set; }
        public String CcEmailNacional { get; set; }
    }
}
