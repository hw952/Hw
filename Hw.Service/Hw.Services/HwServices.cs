using System.Diagnostics;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Hw.Repository;
using System;
using System.Linq.Expressions;
using Hw.Dto.ViewModel;
using Microsoft.Extensions.Logging;
using Hw.Model;
using Hw.IRepository;
using FreeSql;
namespace Hw.Services
{
    public abstract class HwServices<TEntity, AddDto, UpdateDto, ShowDto, SerachDto> : Hw.IServices.IHwServices<TEntity, AddDto, UpdateDto, ShowDto, SerachDto> where TEntity : BaseModel, new()
    {

        protected readonly Hw.IRepository.IHwRepository<TEntity> _repository;
        protected readonly ILogger _logger;
        protected readonly IMapper _mapper;
        public HwServices(IMapper mapper, IHwRepository<TEntity> repository, ILogger logger)
        {
            _mapper = mapper;
            _repository = repository;
            _logger = logger;
        }


        //工作单元模式添加，这种是有事务的
        protected T UnitOfWork<T>(Func<IRepositoryUnitOfWork, Boolean, T> func)
        {
            using (var unitOfWork = _repository.Orm.CreateUnitOfWork())
            {
                try
                {
                    var res = func.Invoke(unitOfWork, false);
                    unitOfWork.Commit();
                    return res;
                }
                catch (Exception ex)
                {
                    unitOfWork.Rollback();
                    _logger.LogInformation(ex.Message);
                    return func.Invoke(unitOfWork, true);

                }

            }
        }

        public async Task<TEntity> Add(AddDto add)
        {
            return await UnitOfWork(async (unitOfWork, isRollback) =>
                       {
                           if (isRollback) return default(TEntity);
                           var model = _mapper.Map<TEntity>(add);
                           _repository.UnitOfWork = unitOfWork;
                           var temp = await _repository.InsertAsync(model);
                           foreach (var item in model.GetType().GetProperties())
                           {
                               if (item.PropertyType.Name == "List`1" && item.GetValue(model) != null)
                               {
                                   await _repository.SaveManyAsync(model, item.Name);
                               }
                           }
                           return temp;
                       });
        }
        public async Task<int> Update(UpdateDto update)
        {


            var temp = await

                          UnitOfWork(async (unitOfWork, isRollback) =>
                                    {
                                        if (isRollback) return -1;
                                        var model = _mapper.Map<TEntity>(update);
                                        // _repository.Attach(model);
                                        //var temp = await _repository.UpdateAsync(model);
                                        _repository.UnitOfWork = unitOfWork;
                                        var temp = _repository.Orm.Update<TEntity>()
                                            .SetSourceIgnore(model, col => col == null)
                                            .ExecuteAffrows();

                                        foreach (var item in model.GetType().GetProperties())
                                        {
                                            if (item.PropertyType.Name == "List`1" && item.GetValue(model) != null)
                                            {
                                                await _repository.SaveManyAsync(model, item.Name);
                                            }
                                        }
                                        return temp;

                                    });

            return temp;

        }
        public async Task<int> Del(int id)
        {
            return await UnitOfWork(async (unitOfWork, isRollback) =>
                        {
                            if (isRollback) return -1;
                            _repository.UnitOfWork = unitOfWork;
                            return await _repository.DeleteAsync(id);
                        });

        }
        public async Task<int> Del(List<int?> ids)
        {

            return await UnitOfWork(async (unitOfWork, isRollback) =>
                        {

                            if (isRollback) return -1;
                            _repository.UnitOfWork = unitOfWork;
                            return await _repository.DeleteAsync(d => ids.Contains(d.Id));

                        });
        }


        public virtual async Task<UpdateDto> GetUpdate(int id)
        {
            return await _repository.Where(d => d.Id == id).FirstAsync<UpdateDto>();
        }

        public virtual async Task<ShowDto> Get(int id)
        {
            return await _repository.Where(d => d.Id == id).FirstAsync(DtoMap());
        }


        public abstract Expression<Func<TEntity, ShowDto>> DtoMap();

        public async Task<WebListResult<ShowDto>> Query(int page = 1, int size = 20)
        {
            WebListResult<ShowDto> result = new WebListResult<ShowDto>() { State = WebResultState.OK, Page = page, Size = size };
            result.Data = await _repository.Select.Page(page, size).ToListAsync(DtoMap());
            result.Total = await _repository.Select.CountAsync();
            return result;
        }

        public async Task<WebListResult<ShowDto>> SearchKey(string key, int page = 1, int size = 20)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                return await Query(page, size);
            }

            WebListResult<ShowDto> result = new WebListResult<ShowDto>() { State = WebResultState.OK, Page = page, Size = size };
            result.Data = await _repository.Where(d => d.Name.Contains(key)).Page(page, size).ToListAsync(DtoMap());
            result.Total = await _repository.Select.CountAsync();
            return result;
        }

    }
}