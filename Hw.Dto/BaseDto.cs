
using System.ComponentModel;
namespace Hw.Dto
{
    public class BaseDto
    {

        [DescriptionAttribute("名称")]
        public string Name { get; set; }

    }
}