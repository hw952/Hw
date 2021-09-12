namespace Hw.Dto.ViewModel
{
   public class ListDtoViewModel
    {

     
       
        public string Title { get; set; }      
        public string Key { get; set; }
        public int Width { get; set; }
        public string Fixed { get; set; }

        public bool Visible { get; set; } = true;

        public bool Tree { get; set; }

        public int Order { get; set; } = int.MaxValue;
    }
}