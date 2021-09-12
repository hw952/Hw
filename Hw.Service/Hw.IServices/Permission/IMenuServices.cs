



using System.Threading.Tasks;
using Hw.Dto.ViewModel;

using Hw.Dto.Permission;
using Hw.Model.Permission;

namespace Hw.IServices.Permission
{
    /// <summary>
    /// 菜单管理
    /// </summary>
    public interface IMenuServices : IHwServices<Menu, MenuAddDto, MenuUpdateDto, MenuListDto,MenuSearchDto>
    {
       
        #region 默认是自定义标签，标签内的方法下次Builder代码时会自动迁移到新的代码中

        /// <summary>
        /// 获取全部菜单
        /// </summary>
        /// <returns></returns>
        public Task<WebListResult<MenuListDto>> GetAllMenuTree();

        public Task<WebListResult<MenuListDto>> GetAllMenuTreeNoWithMenuAction();

        #endregion


    }

   
}


            