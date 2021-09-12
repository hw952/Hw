

using AutoMapper.Internal;
using Hw.Model.Permission;
using Hw.Dto.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Hw.Common;
using Hw.Dto.Dto;
using Hw.Dto.Permission;
using Hw.IServices.Permission;


namespace Hw.Api.Controllers.Permission
{
    /// <summary>
    /// 菜单管理
    /// </summary>
    [ApiController]
    [Route("Api/[controller]/[action]")]
    public class MenuController : HwControllerBase<Menu, MenuAddDto, MenuUpdateDto, MenuListDto, MenuSearchDto> 
    {
       
        public MenuController(ILogger<MenuController> logger, IMenuServices services):base(logger,services)
        {
         
        }



        #region 默认是自定义标签，标签内的方法下次Builder代码时会自动迁移到新的代码中
        public override async Task<WebListResult<MenuListDto>> QueryAll()
        {
            return await (_services as IMenuServices).GetAllMenuTree();
        }





        public override async Task<WebListResult<MenuListDto>> Query(int page = 1, int size = 20)
        {
            return await (_services as IMenuServices).GetAllMenuTree();
        }

        [HttpPost]
        public async Task<WebListResult<MenuListDto>> QueryAllNoWithMenuAction()
        {
            return await (_services as IMenuServices).GetAllMenuTreeNoWithMenuAction();
        }

        #endregion

        
    }

}


            