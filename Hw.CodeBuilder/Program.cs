using System.Linq;
using System.Net.Http.Headers;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System;
using System.IO;
using Hw.CodeBuilder.Modle;
using Newtonsoft.Json;
using Hw.Common;
namespace Hw.CodeBuilder
{
    class Program
    {

        static CodeModel model;

        static string pathModle = @"E:\VsProjects\code\Hw\Hw.Model";
        static string pathDto = @"E:\VsProjects\code\Hw\Hw.Dto";

        static string pathIRepository = @"E:\VsProjects\code\Hw\Hw.Repository\Hw.IRepository";
        static string pathRepository = @"E:\VsProjects\code\Hw\Hw.Repository\Hw.Repository";
        static string pathIServices = @"E:\VsProjects\code\Hw\Hw.Service\Hw.IServices";
        static string pathServices = @"E:\VsProjects\code\Hw\Hw.Service\Hw.Services";
        static string pathControllers = @"E:\VsProjects\code\Hw\Hw.Api\Controllers";
        static string pathExtensions = @"E:\VsProjects\code\Hw\Hw.Extensions";

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var jsonTxt = File.ReadAllText(@"E:\VsProjects\code\Hw\Hw.CodeBuilder\d.chnr.json");
            model = JsonConvert.DeserializeObject<CodeModel>(jsonTxt);
            BuliderModel();
            BuliderDto();
            BuilderIRepository();
            BuilderRepository();
            BuilderIServices();
            BuilderServices();
            BuilderControllers();
            BuilderAutoMap();
            Console.ReadLine();
        }



        static string FiledsEach(List<Field> fields, string defKey, bool isBaseModel = false, bool isDto = false, bool isAddDto = false, bool isUpdateDto = false, bool isListDto = false, bool isSearchDto = false)
        {
            StringBuilder sb = new StringBuilder();

            foreach (var filed in fields)
            {

                if (isBaseModel == false && model.profile.@default.entityInitFields.Any(d => d.defKey == filed.defKey))
                {
                    continue;
                }

                if ((filed.domain == "Int" && isDto == false) || ((filed.domain == "Int" && isDto) && (isAddDto || isUpdateDto || isListDto)))
                {

                    if (!isDto || (isDto && isListDto == false))
                    {
                        sb.AppendLine("        /// <summary>");
                        sb.AppendLine($@"        ///{filed.defName}");
                        sb.AppendLine("        /// <summary>");
                        sb.AppendLine($@"        [DescriptionAttribute(""{ filed.defName }"")]");
                        if (filed.primaryKey)
                        {
                            sb.AppendLine($@"         [Column(IsPrimary = true, IsIdentity = true)]");
                        }
                        if (filed.parentId == true && isDto)
                        {
                            sb.AppendLine($@"        [AddDto(AddDtoType = AddDtoType.TreeCommbox, Url = ""/{defKey}/QueryAll"")]");
                        }
                        if (filed.uiHint == "Select" && isDto && filed.parentId != true)
                        {
                            sb.AppendLine($@"        [AddDto(AddDtoType = AddDtoType.EmunCommbox, Url = ""/{filed.defKey.Replace("Id", "")}/QueryAll"")]");
                        }
                        sb.AppendLine($"        public int? {filed.defKey} {{ get; set; }}");
                        sb.AppendLine();
                    }
                    if (filed.parentId == true && (!isDto || (isDto && isListDto == true)))
                    {
                        // /// <summary>
                        // /// 用于菜单的导航属性[Freesql特性]
                        // /// </summary>
                        //  [Navigate(nameof(ParentId))]
                        // public List<Menu> Children { get; set; }   
                        sb.AppendLine("        /// <summary>");
                        sb.AppendLine("        ///用于菜单的导航属性[Freesql特性]");
                        sb.AppendLine("        /// <summary>");
                        if (!isDto)
                            sb.AppendLine($@"        [Navigate(nameof({filed.defKey}))]");

                        sb.AppendLine($"        public List<{ ((!isDto) ? defKey : $"{defKey}ListDto") }> Children {{ get; set; }}");
                        sb.AppendLine();

                    }
                }

                if ((filed.domain == "DefaultString" && isDto == false) || ((filed.domain == "DefaultString" && isDto) && (isAddDto || isUpdateDto || isListDto)))
                {
                    sb.AppendLine("        /// <summary>");
                    sb.AppendLine($@"        ///{filed.defName}");
                    sb.AppendLine("        /// <summary>");
                    sb.AppendLine($@"        [DescriptionAttribute(""{ filed.defName }"")]");
                    if (isDto && filed.uiHint == "SelectIcon")
                        sb.AppendLine($@"        [AddDto(AddDtoType = AddDtoType.SelectIcon)]");
                    sb.AppendLine($"        public string {filed.defKey.Replace("_", "")} {{ get; set; }}");
                    sb.AppendLine();
                }
                if ((filed.domain == "Password" && isDto == false) || ((filed.domain == "Password" && isDto) && (isAddDto || isUpdateDto || isListDto)))
                {
                    sb.AppendLine("        /// <summary>");
                    sb.AppendLine($@"        ///{filed.defName}");
                    sb.AppendLine("        /// <summary>");
                    sb.AppendLine($@"        [DescriptionAttribute(""{ filed.defName }"")]");
                    if (isDto)
                        sb.AppendLine($@"        [AddDto(AddDtoType = AddDtoType.Password)]");
                    sb.AppendLine($"        public string {filed.defKey.Replace("_", "")} {{ get; set; }}");
                    sb.AppendLine();
                }
                if ((filed.domain == "DateTime" && isDto == false) || ((filed.domain == "DateTime" && isDto) && (isAddDto || isUpdateDto || isListDto)))
                {
                    sb.AppendLine("        /// <summary>");
                    sb.AppendLine($@"        ///{filed.defName}");
                    sb.AppendLine("        /// <summary>");
                    sb.AppendLine($@"        [DescriptionAttribute(""{ filed.defName }"")]");
                    if (filed.defKey == "CreatedTime")
                    {
                        sb.AppendLine($@"         [Column(ServerTime = DateTimeKind.Utc, CanUpdate = false)]");
                    }
                    if (filed.defKey == "UpdatedTime")
                    {
                        sb.AppendLine($@"         [Column(ServerTime = DateTimeKind.Utc)]");
                    }
                    sb.AppendLine($"        public DateTime {filed.defKey} {{ get; set; }}");
                    sb.AppendLine();
                }

                if ((filed.domain == "YesNo" && isDto == false) || ((filed.domain == "YesNo" && isDto) && (isAddDto || isUpdateDto || isListDto)))
                {
                    sb.AppendLine("        /// <summary>");
                    sb.AppendLine($@"        ///{filed.defName}");
                    sb.AppendLine("        /// <summary>");
                    sb.AppendLine($@"        [DescriptionAttribute(""{ filed.defName }"")]");
                    sb.AppendLine($"        public bool {filed.defKey}s {{ get; set; }}");
                    sb.AppendLine();
                }

                if ((filed.domain == "Dict" && isDto == false) || ((filed.domain == "Dict" && isDto) && (isAddDto || isUpdateDto || isListDto)))
                {
                    sb.AppendLine("        /// <summary>");
                    sb.AppendLine($@"        ///{filed.defName}");
                    sb.AppendLine("        /// <summary>");
                    if (isDto)
                    {
                        sb.AppendLine($@"        [AddDto(AddDtoType = AddDtoType.EmunCommbox, Url = ""/Common/GetEmunCommbox?enumName={filed.refDict}"")]");

                    }
                    if (isDto && isListDto)
                    {
                        sb.AppendLine($@"        [System.Text.Json.Serialization.JsonConverter(typeof(JsonEnumDescriptionConvert))]");

                    }
                    sb.AppendLine($@"        [DescriptionAttribute(""{ filed.defName }"")]");
                    sb.AppendLine($"        public {filed.refDict} {filed.defKey} {{ get; set; }}");
                    sb.AppendLine();
                }

                if ((filed.domain == "List<int>" && isDto) && (isAddDto || isUpdateDto))
                {


                    sb.AppendLine($"        private List<int?> _{filed.defKey};");
                    sb.AppendLine();
                    sb.AppendLine("        /// <summary>");
                    sb.AppendLine($@"        ///{filed.defName}");
                    sb.AppendLine("        /// <summary>");
                    if (filed.uiHint == "SelectTreeMult")
                    {
                        sb.AppendLine($@"        [AddDto(AddDtoType = AddDtoType.TreeMultipleCommbox, Url = ""/{filed.defKey}/QueryAll"")]");
                    }
                    if (filed.uiHint == "SelectMult")
                    {
                        sb.AppendLine($@"        [AddDto(AddDtoType = AddDtoType.EmunMultipleCommbox, Url = ""/{filed.defKey}/QueryAll"")]");
                    }
                    sb.AppendLine($@"        [DescriptionAttribute(""{ filed.defName }"")]");
                    sb.AppendLine($"        public List<int?> {filed.defKey} {{ get {{ return _{filed.defKey}; }} set {{ _{filed.defKey} = value; _{(defKey + filed.defKey).ToPlural()} = value?.Select(d => new {defKey + filed.defKey }AddDto() {{ {filed.defKey}Id = d }}).ToList(); }} }}");
                    sb.AppendLine();

                    sb.AppendLine($"        private List<{defKey + filed.defKey }AddDto> _{(defKey + filed.defKey).ToPlural()};");
                    sb.AppendLine();
                    sb.AppendLine("        /// <summary>");
                    sb.AppendLine($@"        ///{filed.defName}");
                    sb.AppendLine("        /// <summary>");
                    sb.AppendLine("         [NoFiled]");

                    sb.AppendLine($@"        [DescriptionAttribute(""{ filed.defName }"")]");
                    sb.AppendLine($"        public List<{defKey + filed.defKey }AddDto> {(defKey + filed.defKey).ToPlural()} {{ get {{ return _{(defKey + filed.defKey).ToPlural()}; }} set {{ _{(defKey + filed.defKey).ToPlural()} = value; _{filed.defKey} = value?.Select(d => d.{filed.defKey}Id).ToList(); }} }}");
                    sb.AppendLine();

                }

            };

            if (!isDto)
            {
                var cells = model.diagrams.SelectMany(d => d.canvasData.cells).ToList();
                cells.Where(d => d.originKey == defKey).ToList().ForEach(d =>
                {
                    var ids = cells.Where(j => j?.source?.cell == d.id).Select(f => f.target.cell).ToList();
                    cells.Where(c => ids.Contains(c.id)).ToList().ForEach(f =>
                    {

                        var temp = model.entities.FirstOrDefault(g => g.defKey == f.originKey);
                        sb.AppendLine("        /// <summary>");
                        sb.AppendLine($@"        ///{temp?.defName}");
                        sb.AppendLine("        /// <summary>");
                        sb.AppendLine($@"        [DescriptionAttribute(""{ temp?.defName }"")]");
                        sb.AppendLine($"        public  List<{f.originKey}> {f.originKey.ToPlural()} {{ get; set; }}");
                        sb.AppendLine();


                    });

                });


                cells.Where(d => d.originKey == defKey).ToList().ForEach(d =>
                {
                    var ids = cells.Where(j => j?.target?.cell == d.id).Select(f => f.source.cell).ToList();
                    cells.Where(c => ids.Contains(c.id)).ToList().ForEach(f =>
                    {
                        var temp = model.entities.FirstOrDefault(g => g.defKey == f.originKey);
                        sb.AppendLine("        /// <summary>");
                        sb.AppendLine($@"        ///{temp?.defName}");
                        sb.AppendLine("        /// <summary>");
                        sb.AppendLine($@"        [DescriptionAttribute(""{ temp?.defName }"")]");
                        sb.AppendLine($"        public  {f.originKey} {f.originKey.ToSingular()} {{ get; set; }}");
                        sb.AppendLine();


                    });

                });

            }
            return sb.ToString();
        }


        static string DictsEach(List<Item> items)
        {
            StringBuilder sb = new StringBuilder();

            foreach (var item in items)
            {
                sb.AppendLine("        /// <summary>");
                sb.AppendLine($@"        ///{item.defName}");
                sb.AppendLine("        /// <summary>");
                sb.AppendLine($@"        [DescriptionAttribute(""{ item.defName }"")]");
                sb.AppendLine($"        {item.defKey.Replace("_", "")},");
                sb.AppendLine();
            }
            return sb.ToString();

        }

        /// <summary>
        ///  生产Model
        /// </summary>
        static void BuliderModel()
        {

            #region 枚举  

            model.dicts.ForEach(d =>
            {


                string html = $@"

using System.Collections.Generic;          
using FreeSql.DataAnnotations;
using System.ComponentModel;
using System;
namespace Hw.Model
{{  
     /// <summary>
    /// {d.defName}
    /// <summary>
    [Description(""{d.defName}"")]
    public enum {d.defKey}
    {{
     
{DictsEach(d.items)}        
    }}

}}  ";

                string path = Path.Join(pathModle);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string file = Path.Join(path, d.defKey + ".cs");
                File.WriteAllText(file, html);

            });

            #endregion

            #region 基类



            {

                string html = $@"

using System.Collections.Generic;          
using FreeSql.DataAnnotations;
using System.ComponentModel;
using System;
namespace Hw.Model
{{
    /// <summary>
    /// 基类
    /// <summary>    
    public class BaseModel
    {{

{FiledsEach(model.profile.@default.entityInitFields, "", true)}
    }}
    

}}
            
            ";
                //System.Console.WriteLine(html);
                string path = Path.Join(pathModle);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string file = Path.Join(path, "BaseModel.cs");
                File.WriteAllText(file, html);

            };

            #endregion

            #region Model


            model.entities.ForEach(d =>
                      {

                          string html = $@"

using System.Collections.Generic;          
using FreeSql.DataAnnotations;
using System.ComponentModel;
using System;
using Hw.Model.{d.comment};
namespace Hw.Model.{d.comment}
{{
     /// <summary>
    /// {d.defName}
    /// <summary>
    [Description(""{d.defName}"")]
    public class {d.defKey} : BaseModel
    {{

{FiledsEach(d.fields, d.defKey)}        
    }}

}}
            
            ";
                          //System.Console.WriteLine(html);
                          string path = Path.Join(pathModle, d.comment);
                          if (!Directory.Exists(path))
                          {
                              Directory.CreateDirectory(path);
                          }
                          string file = Path.Join(path, d.defKey + ".cs");
                          File.WriteAllText(file, html);

                      });

            #endregion

        }



        /// <summary>
        ///  生产Dto
        /// </summary>
        static void BuliderDto()
        {


            #region AddDto

            model.entities.ForEach(d =>
                      {

                          string html = $@"

using System.Collections.Generic;          
using System.ComponentModel;
using System;
using Hw.Model;
using Hw.Dto.{d.comment};
using Hw.Dto.Dto;
using System.Linq;
namespace Hw.Dto.{d.comment}
{{
     /// <summary>
    /// {d.defName}
    /// <summary>
    [Description(""{d.defName}"")]
    public class {d.defKey}AddDto : BaseAddDto
    {{

{FiledsEach(d.fields.Where(f => f.dtoAdd == true).ToList(), d.defKey, isDto: true, isAddDto: true)}        
    }}

}}
            
            ";
                          //System.Console.WriteLine(html);
                          string path = Path.Join(pathDto, d.comment);
                          if (!Directory.Exists(path))
                          {
                              Directory.CreateDirectory(path);
                          }
                          string file = Path.Join(path, d.defKey + "AddDto.cs");
                          File.WriteAllText(file, html);

                      });
            #endregion


            #region UpdateDto

            model.entities.ForEach(d =>
                      {

                          string html = $@"

using System.Collections.Generic;          
using System.ComponentModel;
using System;
using Hw.Model;
using Hw.Dto.{d.comment};
using Hw.Dto.Dto;
using System.Linq;
namespace Hw.Dto.{d.comment}
{{
     /// <summary>
    /// {d.defName}
    /// <summary>
    [Description(""{d.defName}"")]
    public class {d.defKey}UpdateDto : BaseUpdateDto
    {{

{FiledsEach(d.fields.Where(f => f.dtoUpdate == true).ToList(), d.defKey, isDto: true, isUpdateDto: true)}        
    }}

}}
            
            ";
                          //System.Console.WriteLine(html);
                          string path = Path.Join(pathDto, d.comment);
                          if (!Directory.Exists(path))
                          {
                              Directory.CreateDirectory(path);
                          }
                          string file = Path.Join(path, d.defKey + "UpdateDto.cs");
                          File.WriteAllText(file, html);

                      });
            #endregion

            #region ListDto

            model.entities.ForEach(d =>
                      {

                          string html = $@"

using System.Collections.Generic;          
using System.ComponentModel;
using System;
using Hw.Model;
using Hw.Dto.{d.comment};
using Hw.Dto.Dto;
using System.Linq;
namespace Hw.Dto.{d.comment}
{{
     /// <summary>
    /// {d.defName}
    /// <summary>
    [Description(""{d.defName}"")]
    public class {d.defKey}ListDto : BaseListDto
    {{

{FiledsEach(d.fields.Where(f => f.dtoList == true).ToList(), d.defKey, isDto: true, isListDto: true)}        
    }}

}}
            
            ";
                          //System.Console.WriteLine(html);
                          string path = Path.Join(pathDto, d.comment);
                          if (!Directory.Exists(path))
                          {
                              Directory.CreateDirectory(path);
                          }
                          string file = Path.Join(path, d.defKey + "ListDto.cs");
                          File.WriteAllText(file, html);

                      });
            #endregion


            #region SearchDto

            model.entities.ForEach(d =>
                      {

                          string html = $@"

using System.Collections.Generic;          
using System.ComponentModel;
using System;
using Hw.Model;
using Hw.Dto.{d.comment};
using Hw.Dto.Dto;
using System.Linq;
namespace Hw.Dto.{d.comment}
{{
     /// <summary>
    /// {d.defName}
    /// <summary>
    [Description(""{d.defName}"")]
    public class {d.defKey}SearchDto : BaseSearchDto
    {{

{FiledsEach(d.fields.Where(f => f.dtoSearch == true).ToList(), d.defKey, isDto: true, isSearchDto: true)}        
    }}

}}
            
            ";
                          //System.Console.WriteLine(html);
                          string path = Path.Join(pathDto, d.comment);
                          if (!Directory.Exists(path))
                          {
                              Directory.CreateDirectory(path);
                          }
                          string file = Path.Join(path, d.defKey + "SearchDto.cs");
                          File.WriteAllText(file, html);

                      });
            #endregion


        }


        /// <summary>
        /// 生产IRepository
        /// </summary>
        static void BuilderIRepository()
        {


            model.entities.ForEach(d =>
            {


                string html = $@"

using System.Linq.Expressions;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using FreeSql;
using Hw.Model.{d.comment};

namespace Hw.IRepository.{d.comment}
{{
    /// <summary>
    /// {d.defName}
    /// </summary>
    public interface I{d.defKey}Repository : IHwRepository<{d.defKey}>
    {{


    }}

}}

            ";
                //System.Console.WriteLine(html);
                string path = Path.Join(pathIRepository, d.comment);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string file = Path.Join(path, "I" + d.defKey + "Repository.cs");
                File.WriteAllText(file, html);



            });

        }


        /// <summary>
        /// 生产Repository
        /// </summary>
        static void BuilderRepository()
        {


            model.entities.ForEach(d =>
            {


                string html = $@"

using System.Linq.Expressions;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Hw.IRepository;
using FreeSql;
using Hw.Model.{d.comment};
using Hw.IRepository.{d.comment};

namespace Hw.Repository.{d.comment}
{{

    /// <summary>
    /// {d.defName}
    /// </summary>
    public class {d.defKey}Repository : HwRepository<{d.defKey}>,I{d.defKey}Repository
    {{
        IFreeSql _fsql;
        public {d.defKey}Repository(IFreeSql fsql) : base(fsql)
        {{
            _fsql = fsql;
        }}

    }}
}}


            ";
                //System.Console.WriteLine(html);
                string path = Path.Join(pathRepository, d.comment);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string file = Path.Join(path, d.defKey + "Repository.cs");
                File.WriteAllText(file, html);


            });

        }




        /// <summary>
        /// 生产IServicesy
        /// </summary>
        static void BuilderIServices()
        {


            model.entities.ForEach(d =>
            {

                StringBuilder stringBuilderOld = new StringBuilder();

                string path = Path.Join(pathIServices, d.comment);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string file = Path.Join(path, "I" + d.defKey + "Services.cs");
                if (File.Exists(file))
                {
                    bool append = false;
                    foreach (var item in File.ReadLines(file))
                    {

                        if (item.IndexOf("#region") >= 0)
                        {
                            append = true;
                        }
                        if (append)
                        {
                            stringBuilderOld.AppendLine(item);
                        }
                        if (item.IndexOf("#endregion") >= 0)
                        {
                            append = false;
                        }

                    }
                }



                string html = $@"



using System.Threading.Tasks;
using Hw.Dto.ViewModel;

using Hw.Dto.{d.comment};
using Hw.Model.{d.comment};

namespace Hw.IServices.{d.comment}
{{
    /// <summary>
    /// {d.defName}
    /// </summary>
    public interface I{d.defKey}Services : IHwServices<{d.defKey}, {d.defKey}AddDto, {d.defKey}UpdateDto, {d.defKey}ListDto,{d.defKey}SearchDto>
    {{
       
{stringBuilderOld}

    }}

   
}}


            ";


                File.WriteAllText(file, html);



            });

        }




        /// <summary>
        /// 生产Servicesy
        /// </summary>
        static void BuilderServices()
        {


            model.entities.ForEach(d =>
            {



                StringBuilder stringBuilder = new StringBuilder();

                var fields = d.fields.Where(f => f.defKey.Split('_').Length > 1);

                foreach (var item in fields)
                {
                    stringBuilder.AppendLine($"                {item.defKey.Replace("_", "")} = d.{item.defKey.Replace("_", ".")},");
                }

                StringBuilder stringBuilderOld = new StringBuilder();

                string path = Path.Join(pathServices, d.comment);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string file = Path.Join(path, d.defKey + "Services.cs");

                if (File.Exists(file))
                {
                    bool append = false;
                    foreach (var item in File.ReadLines(file))
                    {

                        if (item.IndexOf("#region") >= 0)
                        {
                            append = true;
                        }
                        if (append)
                        {
                            stringBuilderOld.AppendLine(item);
                        }
                        if (item.IndexOf("#endregion") >= 0)
                        {
                            append = false;
                        }

                    }
                }




                //一对多关系批量添加
                StringBuilder sbIRepository = new StringBuilder();
                StringBuilder sbIRepository2 = new StringBuilder();
                StringBuilder sbIRepository3 = new StringBuilder();
                StringBuilder sbIRepository4 = new StringBuilder();
                var lists = d.fields.Where(c => c.domain == "List<int>").ToList();
                lists.ForEach(i =>
                {
                    sbIRepository.AppendLine($"      protected readonly I{d.defKey}{i.defKey}Repository _{d.defKey.ToFirstLower()}{i.defKey}Repository;");
                    sbIRepository2.AppendLine($"        I{d.defKey}{i.defKey}Repository {d.defKey.ToFirstLower()}{i.defKey}Repository,");
                    sbIRepository3.AppendLine($"        _{d.defKey.ToFirstLower()}{i.defKey}Repository = {d.defKey.ToFirstLower()}{i.defKey}Repository;");
                    sbIRepository4.AppendLine($"            temp.{(d.defKey + i.defKey).ToPlural()}= await _{d.defKey.ToFirstLower()}{i.defKey}Repository.Where(f => f.{d.defKey}Id == id).ToListAsync<{d.defKey}{i.defKey}AddDto>();");
                });


                string html = $@"


using System.Threading.Tasks;
using AutoMapper;
using Hw.Repository;
using System;
using System.Linq.Expressions;
using Hw.Dto.ViewModel;
using Microsoft.Extensions.Logging;
using Hw.Model.{d.comment};
using Hw.Dto.{d.comment};
using Hw.IRepository.{d.comment};
using Hw.IServices.{d.comment};
using System.Collections.Generic;

namespace Hw.Services.{d.comment}
{{
    /// <summary>
    /// {d.defName}
    /// </summary>
    public  class {d.defKey}Services :HwServices<{d.defKey}, {d.defKey}AddDto, {d.defKey}UpdateDto, {d.defKey}ListDto, {d.defKey}SearchDto> ,I{d.defKey}Services
    {{
     
        {sbIRepository.ToString()}
        public {d.defKey}Services({sbIRepository2.ToString()}IMapper mapper, I{d.defKey}Repository repository, ILogger<{d.defKey}Services> logger):base(mapper,repository,logger)
        {{
            {sbIRepository3.ToString()}
        }}

        public override  Expression<Func<{d.defKey},  {d.defKey}ListDto>> DtoMap(){{

            return d=> new {d.defKey}ListDto(){{

{stringBuilder.ToString()}

            }};

        }}



        {(lists.Count == 0 ? "" : $@"
        

        public override async Task<{d.defKey}UpdateDto> GetUpdate(int id)
        {{
            var temp = await _repository.Where(d => d.Id == id).FirstAsync<{d.defKey}UpdateDto>();           
            {sbIRepository4.ToString()}
            return temp;
        }}



        ")}

{stringBuilderOld}

    }}

}}


            ";

                File.WriteAllText(file, html);



            });

        }


        /// <summary>
        /// 生产Controllers
        /// </summary>
        static void BuilderControllers()
        {


            model.entities.ForEach(d =>
            {

                StringBuilder stringBuilderOld = new StringBuilder();


                string path = Path.Join(pathControllers, d.comment);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string file = Path.Join(path, d.defKey + "Controller.cs");
                if (File.Exists(file))
                {
                    bool append = false;
                    foreach (var item in File.ReadLines(file))
                    {

                        if (item.IndexOf("#region") >= 0)
                        {
                            append = true;
                        }
                        if (append)
                        {
                            stringBuilderOld.AppendLine(item);
                        }
                        if (item.IndexOf("#endregion") >= 0)
                        {
                            append = false;
                        }

                    }
                }


                string html = $@"

using AutoMapper.Internal;
using Hw.Model.{d.comment};
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
using Hw.Dto.{d.comment};
using Hw.IServices.{d.comment};


namespace Hw.Api.Controllers.{d.comment}
{{
    /// <summary>
    /// {d.defName}
    /// </summary>
    [ApiController]
    [Route(""Api/[controller]/[action]"")]
    public class {d.defKey}Controller : HwControllerBase<{d.defKey}, {d.defKey}AddDto, {d.defKey}UpdateDto, {d.defKey}ListDto, {d.defKey}SearchDto> 
    {{
       
        public {d.defKey}Controller(ILogger<{d.defKey}Controller> logger, I{d.defKey}Services services):base(logger,services)
        {{
         
        }}



{stringBuilderOld}
        
    }}

}}


            ";

                File.WriteAllText(file, html);



            });

        }


        /// <summary>
        /// 生产BuilderAutoMap
        /// </summary>
        static void BuilderAutoMap()
        {

            StringBuilder sb = new StringBuilder();

            StringBuilder stringBuilderOld = new StringBuilder();


            string path = Path.Join(pathExtensions);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string file = Path.Join(path, "CustomAutoMapperConfigs.cs");
            if (File.Exists(file))
            {
                bool append = false;
                foreach (var item in File.ReadLines(file))
                {

                    if (item.IndexOf("#region") >= 0)
                    {
                        append = true;
                    }
                    if (append)
                    {
                        stringBuilderOld.AppendLine(item);
                    }
                    if (item.IndexOf("#endregion") >= 0)
                    {
                        append = false;
                    }

                }
            }


            model.entities.ForEach(f =>
                    {
                        sb.AppendLine($"            CreateMap<{f.defKey}AddDto, {f.defKey}>();");
                        sb.AppendLine($"            CreateMap<{f.defKey}UpdateDto, {f.defKey}>();");
                        sb.AppendLine($"            CreateMap<{f.defKey}, {f.defKey}ListDto>();");
                    }
                );


            string html = $@"

using AutoMapper;
{ string.Join("\r\n", model.entities.Select(f => "using Hw.Dto." + f.comment + ";").Distinct()) }
{ string.Join("\r\n", model.entities.Select(f => "using Hw.Model." + f.comment + ";").Distinct()) }


namespace Hw.Extensions
{{
    /// <summary>
    /// Automapper 自动映射
    /// </summary>
    public class CustomAutoMapperConfigs : Profile
    {{

        public CustomAutoMapperConfigs()
        {{
{sb.ToString()}
{stringBuilderOld}
        }}
    }}

}}


            ";
            //System.Console.WriteLine(html);

            File.WriteAllText(file, html);





        }



    }

}
