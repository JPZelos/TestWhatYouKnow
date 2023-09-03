using System;
using System.Collections.Generic;
using Bogus;
using TWYK.Core.Domain;

namespace TWYK.Services.Installation
{
    public class FakeProductFactory
    {
        public static string[] BaseProductNames = {
            "Best",
            "Hard",
            "Classic",
            "Waterproof",
            "Sport"
        };

        private static string[] BrandNames = {
            "Brooks",
            "Asics",
            "Hoka",
            "New Balance",
            "Nike",
            "Saucony",
            "Altra",
            "Karhu",
            "Mizuno",
            "Diadora",
            "Adidas",
            "Topo Athletic"
        };

        private static string[] SkuNames = {
            "BST-",
            "KSX-",
            "HBV-",
            "MNR-",
            "KUS-"
        };

        private static int[] DiscountRange = {5, 10, 15, 20};

        public static List<Product> GenerateProducts(Category cat, int min, int max) {
            var id = 1;
            
            Random r = new Random();
            int rNum = r.Next(min, max);

            var prdFacker = new Faker<Product>("en")
                .StrictMode(false)
                //.UseSeed(1122)
                .RuleFor(d => d.Id, f => 0)
                .RuleFor(d => d.CategoryId, f => cat.Id)
                .RuleFor(p => p.Name,
                    f => f.PickRandom(BaseProductNames) + " " + cat.Name + " " + f.PickRandom(BrandNames))
                .RuleFor(p => p.Brand, f => f.PickRandom(BrandNames))
                .RuleFor(p => p.Price, f => f.Random.Decimal(80, 250).Round() )
                .RuleFor(p => p.DiscountPerCent, f => f.PickRandom(DiscountRange) )
                .RuleFor(p => p.ShortDescription, f => f.Lorem.Sentences(2))
                .RuleFor(p => p.FullDescription, f => f.Lorem.Paragraphs(3))
                .RuleFor(p => p.Sku, f => f.PickRandom(SkuNames) + f.Random.Int(100,999))
                .RuleFor(p => p.Published, true)
                //.RuleFor(p => p.Picture, f => SeoExtensions.GetSeName($"{cat.Name}-{id++}", true) + ".jpg")
                .RuleFor(p => p.ShowOnHomePage, f => f.Random.Int(0,1) == 1)
                .RuleFor(p => p.Category, f => cat);

            return prdFacker.Generate(rNum);
        }
        

        public static List<Product> GetSomeProducts(List<Category> categories, int min, int max) {
            var products = new List<Product>();
            foreach (var c in categories) {
                var prod =  GenerateProducts(c,min,max);
                c.Products = prod;
                products.AddRange(prod);
            }

            return products;
        }
    }
}