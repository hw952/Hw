


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
    /// 用户管理
    /// </summary>
    public  class UserServices :HwServices<User, UserAddDto, UserUpdateDto, UserListDto, UserSearchDto> ,IUserServices
    {
     
              protected readonly IUserRoleRepository _userRoleRepository;

        public UserServices(        IUserRoleRepository userRoleRepository,
IMapper mapper, IUserRepository repository, ILogger<UserServices> logger):base(mapper,repository,logger)
        {
                    _userRoleRepository = userRoleRepository;

        }

        public override  Expression<Func<User,  UserListDto>> DtoMap(){

            return d=> new UserListDto(){



            };

        }



        
        

        public override async Task<UserUpdateDto> GetUpdate(int id)
        {
            var temp = await _repository.Where(d => d.Id == id).FirstAsync<UserUpdateDto>();           
                        temp.UserRoles= await _userRoleRepository.Where(f => f.UserId == id).ToListAsync<UserRoleAddDto>();

            return temp;
        }



        



    }

}


            