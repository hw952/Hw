


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
    /// 角色菜单管理
    /// </summary>
    public  class RoleMenuServices :HwServices<RoleMenu, RoleMenuAddDto, RoleMenuUpdateDto, RoleMenuListDto, RoleMenuSearchDto> ,IRoleMenuServices
    {
     
        
        public RoleMenuServices(IMapper mapper, IRoleMenuRepository repository, ILogger<RoleMenuServices> logger):base(mapper,repository,logger)
        {
            
        }

        public override  Expression<Func<RoleMenu,  RoleMenuListDto>> DtoMap(){

            return d=> new RoleMenuListDto(){

                RoleName = d.Role.Name,
                MenuName = d.Menu.Name,


            };

        }



        



    }

}


            