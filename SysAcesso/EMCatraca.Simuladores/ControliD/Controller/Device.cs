﻿using EMCatraca.Simuladores.ControliD.Model.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace EMCatraca.Simuladores.ControliD.Controller
{
    class Device
    {
        public static string IPAddress;
        public static string ServerIp;
        private string session = null;
        Util config = new Util();

        public Device()
        {
            var ip_terminal = config.ipServer;
            var ip_servidor = config.ipTerminal;
        }

        public Device(string IPTerminal, string IPServer)
        {
            IPAddress = IPTerminal;
            ServerIp = IPServer;
        }

        public string[] CadastrarNoSevidor(out bool success)
        {
            List<string> response = new List<string>();
            response.Add("Cadastrando Servidor no equipamento.\r\n");
            try
            {
                //Verifica se o equipamento já está cadastrado no servidor e cadastra caso seja necessário
                if (ListObjects("{" +
                        "\"object\" : \"devices\"," +
                        "\"where\" : [{" +
                                "\"id\" : -1," +
                                "\"object\" : \"devices\"," +
                                "\"field\" : \"ip\"," +
                                "\"value\" : \"" + ServerIp + "\"" +

                            "}]" +
                        "}").Length == 0)
                {
                    try
                    {
                        sendJson("create_objects", "{" +
                                "\"object\" : \"devices\"," +
                                "\"values\" : [{" +
                                        "\"id\" : -1," +
                                        "\"name\" : \"Servidor\"," +
                                        "\"ip\" : \"" + ServerIp + "\"," +
                                        "\"public_key\" : \"anA=\"" +

                                    "}]" +
                                "}");
                        response.Add("Equipamento Servidor cadastrado com sucesso no Cliente.\r\n");
                    }
                    catch (Exception ex)
                    {
                        response.Add("Erro ao cadastrar Servidor, Device.cs, L65:");
                        response.Add("  - " + ex.Message + "\r\n");

                    }
                }
                else
                {
                    response.Add("Equipamento Servidor já cadastrado no Cliente\r\n");
                }
                ChangeType(true);
                success = true;
            }
            catch (Exception ex)
            {
                success = false;
                response.Add("Erro ao cadastrar Servidor, Device.cs, L79:");
                response.Add("  - " + ex.Message + "\r\n");
                return response.ToArray();
            }

            //Inicia o monitoramento de identificações do equipamento
            try
            {
                // basic wcf web http service
                //WebServiceHost host = new WebServiceHost(typeof(Server), new Uri("http://localhost:8000/")); entender esta linha aqui
                //ServiceEndpoint ep = host.AddServiceEndpoint(typeof(IServer), new WebHttpBinding(), "");
                //ServiceDebugBehavior sdb = host.Description.Behaviors.Find<ServiceDebugBehavior>();
                //sdb.HttpHelpPageEnabled = false;
                //host.Open();
            }
            catch (Exception ex)
            {
                success = false;
                response.Add("Erro ao monitorar identificações:");
                response.Add("  - " + ex.Message + "\r\n");
                return response.ToArray();
            }

            return response.ToArray();
        }

        public string[] ChangeType(bool isOnline)
        {
            List<string> response = new List<string>();
            response.Add("Alterando modo de operação do equipamento\r\n");
            try
            {
                sendJson("set_configuration",
                    "{" +
                        "\"online_client\" : {" +
                                "\"server_id\" : \"-1\"," +
                                "\"extract_template\" : \"0\"" +

                            "}," +
                         "\"general\" : {" +
                         "\"online\" : \"" + (isOnline ? 1 : 0) + "\"" +

                            "}" +
                        "}"
                );
                response.Add("Modo de operação alteredo\r\n");
            }
            catch (Exception ex)
            {
                response.Add("Erro ao alterar Modo de Operação:");
                response.Add("  - " + ex.Message + "\r\n");
            }
            return response.ToArray();
        }

        public Dictionary<string, string>[] ListObjects(string data)
        {
            List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();
            string response = sendJson("load_objects", data);
            int i = response.IndexOf("[") + 1;
            response = response.Substring(i, response.Length - 1 - i).Replace("\"", "");
            string[] listObjects = response.Split('}', '{');
            foreach (string sObj in listObjects)
            {
                if (sObj.Length <= 1)
                    continue;
                Dictionary<string, string> obj = new Dictionary<string, string>();
                string[] objFilds = sObj.Split(',');
                foreach (string field in objFilds)
                {
                    string[] value = field.Split(':');
                    obj.Add(value[0], value[1]);
                }
                list.Add(obj);
            }
            return list.ToArray();
        }

        public string Login()
        {
            if (session == null)
            {
                string response = sendJson("login", "{\"login\":\"admin\",\"password\":\"admin\"}", false);
                session = response.Split('"')[3];
            }
            return session;
        }


        public string sendJson(string uri, string data, bool checkLogin = true)
        {
            if (checkLogin)
            {
                Login();
                uri += ".fcgi?session=" + session;
            }
            else
                uri += ".fcgi";
            ServicePointManager.Expect100Continue = false;
            try
            {
                var request = (HttpWebRequest)WebRequest.Create("http://" + IPAddress + "/" + uri);
                request.ContentType = "application/json";
                request.Method = "POST";

                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    streamWriter.Write(data);
                }

                var response = (HttpWebResponse)request.GetResponse();
                using (var streamReader = new StreamReader(response.GetResponseStream()))
                {
                    string responseData = streamReader.ReadToEnd();
                    Console.WriteLine(responseData);
                    return responseData;
                }
            }
            catch (WebException e)
            {
                using (WebResponse response = e.Response)
                {
                    HttpWebResponse httpResponse = (HttpWebResponse)response;
                    if (httpResponse == null)
                    {
                        throw new Exception("O servidor referente ao IP indicado não pôde ser encontrado");
                    }
                    Console.WriteLine("Error code: {0}", httpResponse.StatusCode);
                    using (Stream responseData = response.GetResponseStream())
                    using (var reader = new StreamReader(responseData))
                    {
                        string text = reader.ReadToEnd();
                        Console.WriteLine(text);
                        throw new Exception(text);
                    }
                }
            }
        }
    }
}
