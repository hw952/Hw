


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
    /// 角色管理
    /// </summary>
    public  class RoleServices :HwServices<Role, RoleAddDto, RoleUpdateDto, RoleListDto, RoleSearchDto> ,IRoleServices
    {
     
              protected readonly IRoleMenuRepository _roleMenuRepository;

        public RoleServices(        IRoleMenuRepository roleMenuRepository,
IMapper mapper, IRoleRepository repository, ILogger<RoleServices> logger):base(mapper,repository,logger)
        {
                    _roleMenuRepository = roleMenuRepository;

        }

        public override  Expression<Func<Role,  RoleListDto>> DtoMap(){

            return d=> new RoleListDto(){



            };

        }



        
        

        public override async Task<RoleUpdateDto> GetUpdate(int id)
        {
            var temp = await _repository.Where(d => d.Id == id).FirstAsync<RoleUpdateDto>();           
                        temp.RoleMenus= await _roleMenuRepository.Where(f => f.RoleId == id).ToListAsync<RoleMenuAddDto>();

            return temp;
        }



        



    }

}


            