


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
    /// 菜单管理
    /// </summary>
    public  class MenuServices :HwServices<Menu, MenuAddDto, MenuUpdateDto, MenuListDto, MenuSearchDto> ,IMenuServices
    {
     
        
        public MenuServices(IMapper mapper, IMenuRepository repository, ILogger<MenuServices> logger):base(mapper,repository,logger)
        {
            
        }

        public override  Expression<Func<Menu,  MenuListDto>> DtoMap(){

            return d=> new MenuListDto(){



            };

        }



        

        #region 默认是自定义标签，标签内的方法下次Builder代码时会自动迁移到新的代码中
        public async Task<WebListResult<MenuListDto>> GetAllMenuTree()
        {
            WebListResult<MenuListDto> wpr = new WebListResult<MenuListDto>();
            var temp = await _repository.Select.ToTreeListAsync();
            wpr.Data = _mapper.Map<List<MenuListDto>>(temp);
            wpr.State = WebResultState.OK;
            return wpr;
        }

        public async Task<WebListResult<MenuListDto>> GetAllMenuTreeNoWithMenuAction()
        {
            WebListResult<MenuListDto> wpr = new WebListResult<MenuListDto>();
            var temp = await _repository.Where(d => d.ActionType == Hw.Model.MenuType.Menu).ToTreeListAsync();
            wpr.Data = _mapper.Map<List<MenuListDto>>(temp);
            wpr.State = WebResultState.OK;
            return wpr;
        }
        #endregion


    }

}


            