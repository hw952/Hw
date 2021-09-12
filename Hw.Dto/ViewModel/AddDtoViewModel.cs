namespace Hw.Dto.ViewModel
{
    public class AddDtoViewModel
    {


        public string Lable { get; set; }      
        public string Filed { get; set; }
        public string Type { get; set; } = "text";

        public string[] Data { get; set; } =new string[] { };
        public string Url { get; set; }

        public string ValueUrl { get; set; }

        public int Order { get; set; } = int.MaxValue;

        public object Default { get; set; }

    }
}