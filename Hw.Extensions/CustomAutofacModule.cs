
using Autofac;

namespace Hw.Extensions
{
   public class CustomAutofacModule : Module
    {
        public CustomAutofacModule(ContainerBuilder builder)
        {

        }
        /// <summary>
        /// AutoFac注册类
        /// </summary>
        /// <param name="builder"></param>
        protected override void Load(ContainerBuilder builder)
        {

            //builder.RegisterGeneric(typeof(Repository.HwRepository<>)).As(typeof(IRepository.IHwRepository<>)).InstancePerDependency();


            builder.RegisterAssemblyTypes(typeof(Repository.HwRepository<>).Assembly)
             .Where(a => a.Name.EndsWith("Repository"))
             .AsImplementedInterfaces().InstancePerDependency();


            builder.RegisterAssemblyTypes(typeof(Hw.Services.HwServices<,,,,>).Assembly)
             .Where(a => a.Name.EndsWith("Services"))
             .AsImplementedInterfaces().InstancePerDependency();

            //  builder.RegisterAssemblyTypes(typeof(Services.Report.ReportServices).Assembly)
            //  .Where(a => a.Name.EndsWith("Services"))
            //  .AsImplementedInterfaces().InstancePerDependency();


            //builder.RegisterGeneric(typeof(Services.HwServices<>)).As(typeof(IServices.IHwServices<>)).InstancePerDependency();//注册仓储泛型
            //builder.RegisterGeneric(typeof(MyRepositoryBase<，>)).As(typeof(IMyRepository<，>)).InstancePerDependency();//注册仓储泛型 2个以上的泛型参数
            //  builder.RegisterType<myAssembly>().As<ImyAssembly>();   //普通依赖注入
        }

    }
}