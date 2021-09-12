



using System.Threading.Tasks;
using Hw.Dto.ViewModel;

using Hw.Dto.Permission;
using Hw.Model.Permission;

namespace Hw.IServices.Permission
{
    /// <summary>
    /// 用户管理
    /// </summary>
    public interface IUserServices : IHwServices<User, UserAddDto, UserUpdateDto, UserListDto,UserSearchDto>
    {
       


    }

   
}


            