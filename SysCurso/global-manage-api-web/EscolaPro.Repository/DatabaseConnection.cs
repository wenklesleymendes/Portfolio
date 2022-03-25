using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EscolaPro.Repository
{
    public class DatabaseConnection
    {
        public static IConfiguration ConnectionConfiguration
        {
            //get
            //{
            //    //var path = $"{Directory.GetParent(Directory.GetCurrentDirectory())}";
            //    var path = @"C:\Projetos\EscolaPro\CodigoFonte\EscolaPro\api\Escola\EscolaPro.API";
            //    IConfigurationRoot Configuration = new ConfigurationBuilder()
            //        .SetBasePath(path)
            //        .AddJsonFile("appsettings.json")
            //        .Build();
            //    return Configuration;
            //}
            get
            {
                IConfigurationRoot Configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();
                return Configuration;
            }
        }
    }
}