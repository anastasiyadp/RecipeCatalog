using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;
using SQLiteNetExtensions.Extensions;

namespace RecipeCatalog.Models
{
    static class DataInTables
    {
        static List<Recipe> recipes;
        static List<Category> categories;
        static List<Product> products;

        public static void CreateNotesInTables(string dbpath)
        {
            using (SQLiteConnection db = new SQLiteConnection(dbpath))
            {
                if (!db.Table<Recipe>().Any()) {
                    recipes = new List<Recipe> {
                        new Recipe {
                            name = "С фасолью",
                            instruction = "Порезать кубиками копченую курицу, болгарский перец и лук. Добавить фасоль. " +
                            "Заправить майонезом и посолить."
                        },
                        new Recipe{
                            name = "Борщ",
                            instruction = "Приготовить бульон. Сварить отдельно квашенную капусту. Порубить кубиками катошку. " +
                            "Картошку положить в кипящий бульон. Когда доварится, добавить капусту. Обжарить лук, морковь, болгарский перец." +
                            "Добавить томатную пасту и ещё немного подержать на огне. Зажарку положить в кастрюлю к остальным ингредиентам. " +
                            "Добавить свежую зелень."
                        },
                        new Recipe{
                            name = "Гороховый суп",
                            instruction = "Предварительно промыть горох и подержать в холодной воде. Сварить копченую курицу или ребра. " +
                            "Когда бульон будет готов, положить в него горох и варить до полуготовности. Добавить зажарку из лука и моркови. " +
                            "В конце добавить зелень."
                        },
                        new Recipe{
                            name = "Оливье",
                            instruction = "Сварить картошку и яйца. Порезать их кубиками. Также порезать колбасу, лук, сол. огурцы. " +
                            "Высыпать зеленый горошек. Заправить майонезом"
                        }
                    };
                    db.InsertAll(recipes);
                }

                if (!db.Table<Category>().Any())
                {
                    categories = new List<Category> {
                        new Category { name = "Первые блюда" },
                        new Category { name = "Вторые блюда" },
                        new Category { name = "Салаты" },
                        new Category { name = "Закуски" },
                        new Category { name = "Соусы" },
                        new Category { name = "Десерты" },
                        new Category { name = "Выпечка" },
                        new Category { name = "Другое" }
                    };
                    db.InsertAll(categories);


                }

                if (!db.Table<Product>().Any())
                {
                    products = new List<Product> {
                        new Product { name = "Мясо", unitMeasure = "г."}, //0
                        new Product { name = "Курица копченая", unitMeasure = "г."}, //1
                        new Product { name = "Колбаса вареная", unitMeasure = "г."}, //2

                        new Product { name = "Морковь", unitMeasure = "шт."}, //3
                        new Product { name = "Картошка", unitMeasure = "шт." }, //4
                        new Product { name = "Болгарский перец", unitMeasure = "шт." }, //5

                        new Product { name = "Лук", unitMeasure = "шт."}, //6
                        new Product { name = "Укроп", unitMeasure = "пучок" }, //7
                        new Product { name = "Огурец соленый", unitMeasure = "шт."}, //8

                        new Product { name = "Капуста квашеная", unitMeasure = "г."}, //9
                        new Product { name = "Горошек зеленый конс.", unitMeasure = "банка" }, //10
                        new Product { name = "Фасоль конс.", unitMeasure = "банка" }, //11

                        new Product { name = "Яйцо", unitMeasure = "шт." }, //12
                        new Product { name = "Майонез", unitMeasure = "г." }, //13
                        new Product { name = "Томатная паста", unitMeasure = "г."}, //14

                        new Product { name = "Горох", unitMeasure = "г."}, //15
                    };
                    db.InsertAll(products);

                    recipes[0].products = new List<Product> { products[1], products[5], products[11], products[13] };
                    recipes[1].products = new List<Product> { products[0], products[3], products[4], products[5], products[6], products[7], products[9], products[14] };
                    recipes[2].products = new List<Product> { products[1], products[3], products[4], products[6], products[7], products[15] };
                    recipes[3].products = new List<Product> { products[2], products[4], products[6], products[8], products[10], products[12], products[13] };

                    foreach (Recipe recipe in recipes)
                    {
                        db.UpdateWithChildren(recipe);
                    }

                    categories[0].recipes = new List<Recipe> { recipes[1], recipes[2] };
                    categories[2].recipes = new List<Recipe> { recipes[0], recipes[3] };

                    foreach (Category category in categories)
                    {
                        db.UpdateWithChildren(category);
                    }
                }
            }
        }
    }
}