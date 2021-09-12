
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Hw.Dto.ViewModel;
namespace Hw.Api.Controllers
{
    [ApiController]
    [Route("Api/[controller]/[action]")]
    public class CommonController : Controller
    {




        /// <summary>
        /// 获取 Add Dto的模型
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public virtual  WebListResult<EnumCommboxViewModel> GetEmunCommbox(string enumName)
        {

            List<EnumCommboxViewModel> lis = new List<EnumCommboxViewModel>();
            var temp = typeof(Hw.Model.MenuType).Assembly.GetTypes().FirstOrDefault(d => d.IsEnum && d.Name.ToLower() == enumName.ToLower());
            if (temp == null)
            {
                return new WebListResult<EnumCommboxViewModel>() { State = WebResultState.Error };
            }
            foreach (FieldInfo field in temp.GetFields(BindingFlags.Static | BindingFlags.Public))
            {
                EnumCommboxViewModel model = new EnumCommboxViewModel();
                model.Id = (int)field.GetValue(null);
                var des = field.GetCustomAttribute(typeof(DescriptionAttribute)) as DescriptionAttribute;
                model.Name = des?.Description ?? field.Name;
                lis.Add(model);
            }
            return new WebListResult<EnumCommboxViewModel>() { State = WebResultState.OK, Data = lis };

        }

    }
}
