


using System.Threading.Tasks;
using AutoMapper;
using Hw.Repository;
using System;
using System.Linq.Expressions;
using Hw.Dto.ViewModel;
using Microsoft.Extensions.Logging;
using Hw.Model.Permission;
using Hw.Dto.Permission;
using Hw.IRepository.Permission;
using Hw.IServices.Permission;
using System.Collections.Generic;

namespace Hw.Services.Permission
{
    /// <summary>
    /// 用户角色管理
    /// </summary>
    public  class UserRoleServices :HwServices<UserRole, UserRoleAddDto, UserRoleUpdateDto, UserRoleListDto, UserRoleSearchDto> ,IUserRoleServices
    {
     
        
        public UserRoleServices(IMapper mapper, IUserRoleRepository repository, ILogger<UserRoleServices> logger):base(mapper,repository,logger)
        {
            
        }

        public override  Expression<Func<UserRole,  UserRoleListDto>> DtoMap(){

            return d=> new UserRoleListDto(){

                UserName = d.User.Name,
                RoleName = d.Role.Name,


            };

        }



        



    }

}


            