using System.Collections.Generic;
using TWYK.Core.Domain;
using TWYK.Core.Infrastructure.Mapper;
using TWYK.Web.Models;

namespace TWYK.Web.Infrastructure.Mapper
{
    public static class MappingExtensions
    {
        public static TDestination MapTo<TSource, TDestination>(
            this TSource source
        ) {
            return AutoMapperConfiguration.Mapper.Map<TSource, TDestination>(source);
        }

        public static TDestination MapTo<TSource, TDestination>(
            this TSource source,
            TDestination destination
        ) {
            return AutoMapperConfiguration.Mapper.Map(source, destination);
        }

        public static List<TDestination> MapToList<TSource, TDestination>(
            this IList<TSource> source
        ) {
            return AutoMapperConfiguration.Mapper.Map<IList<TSource>, List<TDestination>>(source);
        }

        #region Category

        public static CategoryModel ToModel(
            this Category entity
        ) {
            return entity.MapTo<Category, CategoryModel>();
        }

        public static Category ToEntity(
            this CategoryModel model
        ) {
            return model.MapTo<CategoryModel, Category>();
        }

        public static Category ToEntity(
            this CategoryModel model,
            Category destination
        ) {
            return model.MapTo(destination);
        }

        public static List<CategoryModel> ToModelList(
            this IList<Category> entities
        ) {
            return entities.MapToList<Category, CategoryModel>();
        }

        #endregion

        #region Product

        public static ProductModel ToModel(
            this Product entity
        ) {
            return entity.MapTo<Product, ProductModel>();
        }

        public static Product ToEntity(
            this ProductModel model
        ) {
            return model.MapTo<ProductModel, Product>();
        }

        public static Product ToEntity(
            this ProductModel model,
            Product destination
        ) {
            return model.MapTo(destination);
        }

        public static List<ProductModel> ToModelList(
            this IList<Product> entities
        ) {
            return entities.MapToList<Product, ProductModel>();
        }

        #endregion

        #region Customer

        public static CustomerModel ToModel(
            this Customer entity
        ) {
            return entity.MapTo<Customer, CustomerModel>();
        }

        public static Customer ToEntity(
            this CustomerModel model
        ) {
            return model.MapTo<CustomerModel, Customer>();
        }

        public static Customer ToEntity(
            this CustomerModel model,
            Customer destination
        ) {
            return model.MapTo(destination);
        }

        public static List<CustomerModel> ToModelList(
            this IList<Customer> entities
        ) {
            return entities.MapToList<Customer, CustomerModel>();
        }

        #endregion

        #region ShoppingCartItem

        public static ShoppingCartItemModel ToModel(
            this ShoppingCartItem entity
        ) {
            return entity.MapTo<ShoppingCartItem, ShoppingCartItemModel>();
        }

        public static ShoppingCartItem ToEntity(
            this ShoppingCartItemModel model
        ) {
            return model.MapTo<ShoppingCartItemModel, ShoppingCartItem>();
        }

        public static ShoppingCartItem ToEntity(
            this ShoppingCartItemModel model,
            ShoppingCartItem destination
        ) {
            return model.MapTo(destination);
        }

        public static List<ShoppingCartItemModel> ToModelList(
            this IList<ShoppingCartItem> entities
        ) {
            return entities.MapToList<ShoppingCartItem, ShoppingCartItemModel>();
        }

        #endregion

        #region Topic

        public static TopicModel ToModel(
            this Topic entity
        ) {
            return entity.MapTo<Topic, TopicModel>();
        }

        public static Topic ToEntity(
            this TopicModel model
        ) {
            return model.MapTo<TopicModel, Topic>();
        }

        public static Topic ToEntity(
            this TopicModel model,
            Topic destination
        ) {
            return model.MapTo(destination);
        }

        public static List<TopicModel> ToModelList(
            this IList<Topic> entities
        ) {
            return entities.MapToList<Topic, TopicModel>();
        }

        #endregion

        #region Chapter

        public static ChapterModel ToModel(
            this Chapter entity
        ) {
            return entity.MapTo<Chapter, ChapterModel>();
        }

        public static Chapter ToEntity(
            this ChapterModel model
        ) {
            return model.MapTo<ChapterModel, Chapter>();
        }

        public static Chapter ToEntity(
            this ChapterModel model,
            Chapter destination
        ) {
            return model.MapTo(destination);
        }

        public static List<ChapterModel> ToModelList(
            this IList<Chapter> entities
        ) {
            return entities.MapToList<Chapter, ChapterModel>();
        }

        #endregion

        #region Question

        public static QuestionModel ToModel(
            this Question entity
        ) {
            return entity.MapTo<Question, QuestionModel>();
        }

        public static Question ToEntity(
            this QuestionModel model
        ) {
            return model.MapTo<QuestionModel, Question>();
        }

        public static Question ToEntity(
            this QuestionModel model,
            Question destination
        ) {
            return model.MapTo(destination);
        }

        public static List<QuestionModel> ToModelList(
            this IList<Question> entities
        ) {
            return entities.MapToList<Question, QuestionModel>();
        }

        #endregion

        #region Answer

        public static AnswerModel ToModel(
            this Answer entity
        ) {
            return entity.MapTo<Answer, AnswerModel>();
        }

        public static Answer ToEntity(
            this AnswerModel model
        ) {
            return model.MapTo<AnswerModel, Answer>();
        }

        public static Answer ToEntity(
            this AnswerModel model,
            Answer destination
        ) {
            return model.MapTo(destination);
        }

        public static List<AnswerModel> ToModelList(
            this IList<Answer> entities
        ) {
            return entities.MapToList<Answer, AnswerModel>();
        }

        #endregion

        #region TestResult

        public static TestResultModel ToModel(
            this TestResult entity
        ) {
            return entity.MapTo<TestResult, TestResultModel>();
        }

        public static TestResult ToEntity(
            this TestResultModel model
        ) {
            return model.MapTo<TestResultModel, TestResult>();
        }

        public static TestResult ToEntity(
            this TestResultModel model,
            TestResult destination
        ) {
            return model.MapTo(destination);
        }

        public static List<TestResultModel> ToModelList(
            this IList<TestResult> entities
        ) {
            return entities.MapToList<TestResult, TestResultModel>();
        }

        #endregion
    }
}