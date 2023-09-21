using System;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using Autofac;
using Autofac.Integration.Mvc;
using Microsoft.Extensions.Logging;
using TWYK.Core;
using TWYK.Core.Data;
using TWYK.Core.Fakes;
using TWYK.Core.Infrastructure;
using TWYK.Core.Infrastructure.DependencyManagement;
using TWYK.Data;
using TWYK.Services.Answers;
using TWYK.Services.Authentication;
using TWYK.Services.Chapters;
using TWYK.Services.Customers;
using TWYK.Services.Installation;
using TWYK.Services.Questions;
using TWYK.Services.Quizzes;
using TWYK.Services.Security;
using TWYK.Services.TestResults;
using TWYK.Services.Topics;

namespace TWYK.Web.Framework
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        #region Implementation of IDependencyRegistrar

        public void Register(ContainerBuilder builder, ITypeFinder typeFinder) {
            builder.RegisterFilterProvider();

            // HTTP context and other related stuff
            builder.Register(c =>
                    //register FakeHttpContext when HttpContext is not available
                    HttpContext.Current != null
                        ? new HttpContextWrapper(HttpContext.Current)
                        : new FakeHttpContext("~/") as HttpContextBase)
                .As<HttpContextBase>()
                .InstancePerLifetimeScope();

            builder.Register(c => c.Resolve<HttpContextBase>().Request)
                .As<HttpRequestBase>()
                .InstancePerLifetimeScope();

            builder.Register(c => c.Resolve<HttpContextBase>().Response)
                .As<HttpResponseBase>()
                .InstancePerLifetimeScope();

            builder.Register(c => c.Resolve<HttpContextBase>().Server)
                .As<HttpServerUtilityBase>()
                .InstancePerLifetimeScope();

            builder.Register(c => c.Resolve<HttpContextBase>().Session)
                .As<HttpSessionStateBase>()
                .InstancePerLifetimeScope();

            // Register Context
            builder.Register((Func<IComponentContext, IDbContext>)(c =>
                    new AtsContext(WebConfigurationManager.ConnectionStrings["AtsContext"].ConnectionString)))
                .InstancePerLifetimeScope();

            //web helper
            builder.RegisterType<WebHelper>().As<IWebHelper>().InstancePerLifetimeScope();

            //Serilog
            var loggerFactory = new LoggerFactory();
            //loggerFactory.AddSerilog(Serilog.Log.Logger);

            // register logger factory and generic logger
            builder.RegisterInstance<ILoggerFactory>(loggerFactory);
            builder.RegisterGeneric(typeof(Logger<>)).As(typeof(ILogger<>)).SingleInstance();

            //controllers
            builder.RegisterControllers(typeFinder.GetAssemblies().ToArray());

            //Repositories
            builder.RegisterGeneric(typeof(EfRepository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();

            builder.RegisterType<DapperRepository>().As<IDapperRepository>()
                .WithParameter("connectionString", ConfigurationManager.ConnectionStrings["AtsContext"].ConnectionString)
                .InstancePerLifetimeScope();

            //work context
            builder.RegisterType<WebWorkContext>().As<IWorkContext>().InstancePerLifetimeScope();

            //Services
            builder.RegisterType<FormsAuthenticationService>().As<IAuthenticationService>().InstancePerLifetimeScope();
            builder.RegisterType<CustomerService>().As<ICustomerService>().InstancePerLifetimeScope();
            //TODO: Make this with Cache Manager
            builder.RegisterType<PermissionService>().As<IPermissionService>().InstancePerLifetimeScope();
            builder.RegisterType<InstallationService>().As<IInstallationService>().InstancePerLifetimeScope();


            builder.RegisterType<AnswerService>().As<IAnswerService>().InstancePerLifetimeScope();
            builder.RegisterType<ChapterService>().As<IChapterService>().InstancePerLifetimeScope();
            builder.RegisterType<QuestionService>().As<IQuestionService>().InstancePerLifetimeScope();
            builder.RegisterType<TopicService>().As<ITopicService>().InstancePerLifetimeScope();
            builder.RegisterType<TestResultService>().As<ITestResultService>().InstancePerLifetimeScope();
            builder.RegisterType<QuizService>().As<IQuizService>().InstancePerLifetimeScope();

        }

        public int Order => 0;

        #endregion
    }
}