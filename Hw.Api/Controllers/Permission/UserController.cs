

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
    /// 用户管理
    /// </summary>
    [ApiController]
    [Route("Api/[controller]/[action]")]
    public class UserController : HwControllerBase<User, UserAddDto, UserUpdateDto, UserListDto, UserSearchDto> 
    {
       
        public UserController(ILogger<UserController> logger, IUserServices services):base(logger,services)
        {
         
        }




        
    }

}


            