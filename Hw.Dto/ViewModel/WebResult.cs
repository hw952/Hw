using System.IO;
using System.Collections.Generic;
namespace Hw.Dto.ViewModel
{
    public class WebResult
    {
        public WebResultState State { get; set; }
        public string Msg { get; set; }

    }
    public class WebResult<T>
    {
        public WebResultState State { get; set; }
        public string Msg { get; set; }
        public T Data { get; set; }

    }

    public class WebListResult<T>
    {
        public WebResultState State { get; set; }
        public string Msg { get; set; }
        public List<T> Data { get; set; }

        public long Total { get; set; }

        public int Page { get; set; }

        public int Size { get; set; }

    }

    public enum WebResultState

    {
        OK = 200,
        Error = 400,
        ErrorUpdate = 401,
        ErrorSave = 402,
        ErrorDel = 403,

    }

}