using System.Collections.Generic;
using System.Threading.Tasks;
using Hw.Dto.ViewModel;

namespace Hw.IServices
{
    public interface IHwServices<TEntity, AddDto, UpdateDto, ShowDto, SearchDto>
    {
        Task<TEntity> Add(AddDto add);
        Task<int> Update(UpdateDto update);
        Task<int> Del(int id);

        Task<int> Del(List<int?> ids);
        Task<WebListResult<ShowDto>> Query(int page = 1, int size = 20);

        Task<WebListResult<ShowDto>> SearchKey(string key, int page = 1, int size = 20);

        Task<ShowDto> Get(int id);
        Task<UpdateDto> GetUpdate(int id);
    }
}