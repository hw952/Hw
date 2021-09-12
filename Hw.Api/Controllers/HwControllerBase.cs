using AutoMapper.Internal;
using Hw.Model;
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

namespace Hw.Api.Controllers
{
    public abstract class HwControllerBase<T, AddDto, UpdateDto, ListDto, SearchDto> : ControllerBase where T : BaseModel
    {
        protected ILogger<ControllerBase> _logger;

        protected IServices.IHwServices<T, AddDto, UpdateDto, ListDto, SearchDto> _services;

        public HwControllerBase(ILogger<ControllerBase> logger, IServices.IHwServices<T, AddDto, UpdateDto, ListDto, SearchDto> services)
        {
            _logger = logger;
            _services = services;
        }


        /// <summary>
        /// 获取 Add Dto的模型
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public virtual WebListResult<AddDtoViewModel> GetAddDto()
        {

            List<AddDtoViewModel> lis = new List<AddDtoViewModel>();
            foreach (var prop in typeof(AddDto).GetProperties())
            {
                AddDtoViewModel model = new AddDtoViewModel();
                model.Filed = prop.Name.ToFirstLower();
                var des = prop.GetCustomAttribute(typeof(DescriptionAttribute)) as DescriptionAttribute;
                model.Lable = des?.Description ?? prop.Name;
                var addType = prop.GetCustomAttribute(typeof(AddDtoAttribute)) as AddDtoAttribute;
                if (addType != null)
                {
                    model.Type = addType.AddDtoType.ToString().ToFirstLower();
                    model.Url = addType.Url;
                }
                var filedOrder = prop.GetCustomAttribute(typeof(FiledOrderAttribute)) as FiledOrderAttribute;
                if (filedOrder != null)
                {
                    model.Order = filedOrder.Order;
                }
                if (prop.PropertyType.FullName == typeof(bool?).FullName || prop.PropertyType.FullName == typeof(bool).FullName)
                {
                    model.Default = default(bool);
                }
                else
                    model.Default = null;
                if (prop.Has<NoFiledAttribute>())
                {
                    continue;
                }

                lis.Add(model);
            }


            return new WebListResult<AddDtoViewModel>() { State = WebResultState.OK, Data = lis.OrderBy(d => d.Order).ToList() };

        }



        /// <summary>
        /// 获取 Add Dto的模型
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public virtual WebListResult<AddDtoViewModel> GetUpdateDto()
        {


            List<AddDtoViewModel> lis = new List<AddDtoViewModel>();

            foreach (var prop in typeof(UpdateDto).GetProperties())
            {

                AddDtoViewModel model = new AddDtoViewModel();
                model.Filed = prop.Name.ToFirstLower();
                var des = prop.GetCustomAttribute(typeof(DescriptionAttribute)) as DescriptionAttribute;
                model.Lable = des?.Description ?? prop.Name;
                var addType = prop.GetCustomAttribute(typeof(AddDtoAttribute)) as AddDtoAttribute;
                if (addType != null)
                {
                    model.Type = addType.AddDtoType.ToString().ToFirstLower();
                    model.Url = addType.Url;
                    model.ValueUrl = addType.VlaueUrl;
                }
                var filedOrder = prop.GetCustomAttribute(typeof(FiledOrderAttribute)) as FiledOrderAttribute;
                if (filedOrder != null)
                {
                    model.Order = filedOrder.Order;
                }
                if (prop.PropertyType.Name == typeof(bool?).Name)
                {
                    model.Default = default(bool);
                }
                else
                    model.Default = "";
                if (prop.Has<NoFiledAttribute>())
                {
                    continue;
                }
                lis.Add(model);
            }


            return new WebListResult<AddDtoViewModel>() { State = WebResultState.OK, Data = lis.OrderBy(d => d.Order).ToList() };

        }





        /// <summary>
        /// 获取 Add Dto的模型
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public virtual WebListResult<ListDtoViewModel> GetListDto()
        {


            List<ListDtoViewModel> lis = new List<ListDtoViewModel>();

            foreach (var prop in typeof(ListDto).GetProperties())
            {

                ListDtoViewModel model = new ListDtoViewModel();

                model.Key = prop.Name.ToFirstLower();
                var des = prop.GetCustomAttribute(typeof(DescriptionAttribute)) as DescriptionAttribute;
                model.Title = des?.Description ?? prop.Name;

                if (prop.Name == "Name")
                    model.Tree = true;
                var filedOrder = prop.GetCustomAttribute(typeof(FiledOrderAttribute)) as FiledOrderAttribute;
                if (filedOrder != null)
                {
                    model.Order = filedOrder.Order;
                }
                if (prop.Name == "Id")
                {
                    model.Visible = false;
                    //continue;
                }
                if (prop.PropertyType.Name == typeof(List<>).Name)
                {
                    model.Visible = false;
                    continue;
                }
                if (prop.Has<NoFiledAttribute>())
                {
                    continue;
                }
                lis.Add(model);
            }
            return new WebListResult<ListDtoViewModel>() { State = WebResultState.OK, Data = lis.OrderBy(d => d.Order).ToList() };

        }



        [HttpPost]
        public virtual async Task<WebResult> Add(AddDto user)
        {

            var res = await _services.Add(user);
            if (res != null)
            {
                return new WebResult() { State = WebResultState.OK };
            }

            return new WebResult() { State = WebResultState.ErrorSave };
        }



        [HttpPost]
        public virtual async Task<WebListResult<ListDto>> Query(int page = 1, int size = 20)
        {
            var temp = await _services.Query(page, size);
            temp.State = WebResultState.OK;
            return temp;
        }


        [HttpPost]
        public virtual async Task<WebListResult<ListDto>> QueryAll()
        {
            var temp = await _services.Query();
            temp.State = WebResultState.OK;
            return temp;
        }

        [HttpPost]
        public virtual async Task<WebResult> Update(UpdateDto user)
        {
           

            var res = await _services.Update(user);
            if (res >= 0)
            {

                return new WebResult() { State = WebResultState.OK };
            }

            return new WebResult() { State = WebResultState.ErrorUpdate };
        }



        [HttpPost]
        public virtual async Task<WebResult> Delete(List<int?> ids)
        {

            var res = await _services.Del(ids);
            if (res > 0)
            {

                return new WebResult() { State = WebResultState.OK };
            }

            return new WebResult() { State = WebResultState.ErrorDel };
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">指定ID</param>
        /// <returns></returns>
        [HttpGet]

        public virtual async Task<WebResult> Delete(int id)
        {

            var res = await _services.Del(id);
            if (res >= 0)
            {
                return new WebResult() { State = WebResultState.OK };
            }

            return new WebResult() { State = WebResultState.Error };
        }


        /// <summary>
        /// 获取指定ID
        /// </summary>
        /// <param name="id">指定ID</param>
        /// <returns></returns>
        [HttpGet]
        public virtual async Task<WebResult<UpdateDto>> GetUpdate(int id)
        {
            var res = await _services.GetUpdate(id);
            if (res != null)
            {
                return new WebResult<UpdateDto>() { State = WebResultState.OK, Data = res };
            }

            return new WebResult<UpdateDto>() { State = WebResultState.Error, Data = res };
        }



        [HttpPost]
        public virtual async Task<WebListResult<ListDto>> Search(string key, int page = 1, int size = 20)
        {
            var temp = await _services.SearchKey(key, page, size);
            temp.State = WebResultState.OK;
            return temp;
        }

    }
}
