using System;
using AutoMapper;
using TWYK.Core.Domain;
using TWYK.Core.Infrastructure.Mapper;
using TWYK.Web.Models;

namespace TWYK.Web.Infrastructure.Mapper
{
    /// <summary>
    /// AutoMapper configuration for admin area models
    /// </summary>
    public class AtsMapperConfiguration : IMapperConfiguration
    {
        #region Implementation of IMapperConfiguration

        public Action<IMapperConfigurationExpression> GetConfiguration() {
            Action<IMapperConfigurationExpression> action = cfg => {

                cfg.CreateMap<Category, CategoryModel>();
                    //.ForMember(dest => dest.FullDescription, mo => mo.MapFrom(string), mo => mo.Ignore());
                    // Instead we use auto generated default value
                   // .ForMember(dest => dest.FullDescription, opt => opt.MapFrom(src => src.Code + "-" + src.Name));
                cfg.CreateMap<CategoryModel, Category>();

                cfg.CreateMap<Product, ProductModel>();
                cfg.CreateMap<ProductModel, Product>();

                cfg.CreateMap<Customer, CustomerModel>();
                cfg.CreateMap<CustomerModel, Customer>();

                cfg.CreateMap<ShoppingCartItem, ShoppingCartItemModel>();
                cfg.CreateMap<ShoppingCartItemModel, ShoppingCartItem>();


                cfg.CreateMap<Topic, TopicModel>();
                cfg.CreateMap<TopicModel, Topic>();

                cfg.CreateMap<Chapter, ChapterModel>();
                cfg.CreateMap<ChapterModel, Chapter>();

                cfg.CreateMap<Question, QuestionModel>();
                cfg.CreateMap<QuestionModel, Question>();

                cfg.CreateMap<Answer, AnswerModel>();
                cfg.CreateMap<AnswerModel, Answer>();

                cfg.CreateMap<TestResult, TestResultModel>();
                cfg.CreateMap<TestResultModel, TestResult>();
            };

            return action;
        }

        public int Order => 0;

        #endregion
    }
}