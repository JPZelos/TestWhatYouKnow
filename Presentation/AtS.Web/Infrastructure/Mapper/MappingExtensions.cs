using System.Collections.Generic;
using TWYK.Core.Domain;
using TWYK.Core.Infrastructure.Mapper;
using TWYK.Web.Models;

namespace TWYK.Web.Infrastructure.Mapper
{
    public static class MappingExtensions
    {
        public static TDestination MapTo<TSource, TDestination>(this TSource source) {
            return AutoMapperConfiguration.Mapper.Map<TSource, TDestination>(source);
        }

        public static TDestination MapTo<TSource, TDestination>(
            this TSource source,
            TDestination destination
        ) {
            return AutoMapperConfiguration.Mapper.Map(source, destination);
        }

        public static List<TDestination> MapToList<TSource, TDestination>(this IList<TSource> source) {
            return AutoMapperConfiguration.Mapper.Map<IList<TSource>, List<TDestination>>(source);
        }

        #region Category

        public static CategoryModel ToModel(this Category entity) {
            return entity.MapTo<Category, CategoryModel>();
        }

        public static Category ToEntity(this CategoryModel model) {
            return model.MapTo<CategoryModel, Category>();
        }

        public static Category ToEntity(
            this CategoryModel model,
            Category destination
        ) {
            return model.MapTo(destination);
        }

        public static List<CategoryModel> ToModelList(this IList<Category> entities) {
            return entities.MapToList<Category, CategoryModel>();
        }

        #endregion

        #region Product

        public static ProductModel ToModel(this Product entity) {
            return entity.MapTo<Product, ProductModel>();
        }

        public static Product ToEntity(this ProductModel model) {
            return model.MapTo<ProductModel, Product>();
        }

        public static Product ToEntity(
            this ProductModel model,
            Product destination
        ) {
            return model.MapTo(destination);
        }

        public static List<ProductModel> ToModelList(this IList<Product> entities) {
            return entities.MapToList<Product, ProductModel>();
        }

        #endregion

        #region Customer

        public static CustomerModel ToModel(this Customer entity) {
            return entity.MapTo<Customer, CustomerModel>();
        }

        public static Customer ToEntity(this CustomerModel model) {
            return model.MapTo<CustomerModel, Customer>();
        }

        public static Customer ToEntity(
            this CustomerModel model,
            Customer destination
        ) {
            return model.MapTo(destination);
        }

        public static List<CustomerModel> ToModelList(this IList<Customer> entities) {
            return entities.MapToList<Customer, CustomerModel>();
        }

        #endregion

        #region ShoppingCartItem

        public static ShoppingCartItemModel ToModel(this ShoppingCartItem entity) {
            return entity.MapTo<ShoppingCartItem, ShoppingCartItemModel>();
        }

        public static ShoppingCartItem ToEntity(this ShoppingCartItemModel model) {
            return model.MapTo<ShoppingCartItemModel, ShoppingCartItem>();
        }

        public static ShoppingCartItem ToEntity(
            this ShoppingCartItemModel model,
            ShoppingCartItem destination
        ) {
            return model.MapTo(destination);
        }

        public static List<ShoppingCartItemModel> ToModelList(this IList<ShoppingCartItem> entities) {
            return entities.MapToList<ShoppingCartItem, ShoppingCartItemModel>();
        }

        #endregion
    }
}