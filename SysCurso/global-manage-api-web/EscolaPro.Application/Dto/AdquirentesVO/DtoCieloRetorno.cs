using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Service.Dto.AdquirentesVO
{
    public class DtoCieloRetorno
    {
        public Version Version { get; set; }
        public Content Content { get; set; }
        public int StatusCode { get; set; }
        public string ReasonPhrase { get; set; }
        public List<Header2> Headers { get; set; }
        public List<object> TrailingHeaders { get; set; }
        public RequestMessage RequestMessage { get; set; }
        public bool IsSuccessStatusCode { get; set; }
    }

    public class Version
    {
        public int Major { get; set; }
        public int Minor { get; set; }
        public int Build { get; set; }
        public int Revision { get; set; }
        public int MajorRevision { get; set; }
        public int MinorRevision { get; set; }
    }

    public class Header
    {
        public string Key { get; set; }
        public List<string> Value { get; set; }
    }

    public class Content
    {
        public List<Header> Headers { get; set; }
    }

    public class Header2
    {
        public string Key { get; set; }
        public List<string> Value { get; set; }
    }

    public class Version2
    {
        public int Major { get; set; }
        public int Minor { get; set; }
        public int Build { get; set; }
        public int Revision { get; set; }
        public int MajorRevision { get; set; }
        public int MinorRevision { get; set; }
    }

    public class Header3
    {
        public string Key { get; set; }
        public List<string> Value { get; set; }
    }

    public class Content2
    {
        public List<Header3> Headers { get; set; }
    }

    public class MethodCielo
    {
        public string Method { get; set; }
    }

    public class Header4
    {
        public string Key { get; set; }
        public List<string> Value { get; set; }
    }

    public class Properties
    {
    }

    public class RequestMessage
    {
        public Version2 Version { get; set; }
        public Content2 Content { get; set; }
        public MethodCielo Method { get; set; }
        public string RequestUri { get; set; }
        public List<Header4> Headers { get; set; }
        public Properties Properties { get; set; }
    }
}
