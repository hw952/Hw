



using System.Threading.Tasks;
using Hw.Dto.ViewModel;

using Hw.Dto.Permission;
using Hw.Model.Permission;

namespace Hw.IServices.Permission
{
    /// <summary>
    /// 角色管理
    /// </summary>
    public interface IRoleServices : IHwServices<Role, RoleAddDto, RoleUpdateDto, RoleListDto,RoleSearchDto>
    {
       


    }

   
}


            