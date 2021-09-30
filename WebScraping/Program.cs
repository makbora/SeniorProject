using System;
using CsvHelper;
using HtmlAgilityPack;
using System.IO;
using System.Collections.Generic;
using System.Globalization;
using System.Web;
namespace WebScraping
{
    class Program
    {
        //Class for Recipe Table
        public class Recipe
        {
            public int RecId { get; set; }
            public string Title { get; set; }
            public string Type { get; set; }
            public string Link { get; set; }

        }
        //Class for Ingredients Table
        public class Ingredient
        {
            public int IngId { get; set; }
            public string Name { get; set; }
            public double Price { get; set; }
        }
        //Class for RecipeRequirements Table
        public class RecipeReq
        {
            public int ReqId { get; set; }
            public int RecId { get; set; }
            public int IngId { get; set; }
            public string Amount { get; set; }
            public string Measurment { get; set; } 
        }
        //Class for Instructions Table
        public class Instruction
        {
            public int InstrId { get; set; }
            public int RecId { get; set; }
            public int Order { get; set; }
            public string Direction { get; set; }
            
        }

        static void Main(string[] args)
        {
            //Load initial page and create variables for all the values to be stored
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load("https://www.budgetbytes.com/category/recipes/");
            var lastPage = false;
            var recipes = new List<Recipe>();
            var ingredients = new List<Ingredient>();
            var requirements = new List<RecipeReq>();
            var instructions = new List<Instruction>();
            var recid = 1;
            //Store all the names and links of recipes on each page until the last page is reached
            while (!lastPage)
            {
                var RecipeNames = doc.DocumentNode.SelectNodes("//h2[@class='post-title']");
                foreach (var item in RecipeNames)
                {
                    var title = HttpUtility.HtmlDecode(item.InnerText);
                    var RecipeLink = doc.DocumentNode.SelectNodes("//a[@title='" + item.InnerText + "']");
                    recipes.Add(new Recipe { RecId = recid, Title = title, Link = RecipeLink[0].GetAttributeValue("href", null) });
                    recid++;
                }
                var nextPageHtml = doc.DocumentNode.SelectNodes("//a[@class='next page-numbers']");
                if(nextPageHtml==null)
                {
                    lastPage = true;
                }
                else
                {
                    doc = web.Load(nextPageHtml[0].GetAttributeValue("href", null));
                }
                
            }
            //Go through the links of the recipes stored before and store the information about the recipes in their respective variables
            var foremoval = new List<int>();
            var ingid = 1;
            var reqid = 1;
            var instrid = 1;
            foreach (var recipe in recipes)
            {
                doc = web.Load(recipe.Link);
                var ingredient = doc.DocumentNode.SelectNodes("//span[@class='wprm-recipe-ingredient-name']");
                var amountNum = doc.DocumentNode.SelectNodes("//span[@class='wprm-recipe-ingredient-amount']");
                var amountUnit = doc.DocumentNode.SelectNodes("//span[@class='wprm-recipe-ingredient-unit']");
                var inghtmllen = doc.DocumentNode.SelectNodes("//li[@class='wprm-recipe-ingredient']");
                //Mark recipes that don't fit the format to be removed
                if (ingredient == null)
                {
                    foremoval.Add(recipes.IndexOf(recipe));
                }
                else
                {
                    //Check to see if an ingredient has already been added to the list before adding it 
                    var behindUnit = 0;
                    var behindNum = 0;
                    var amount = "";
                    var unit = "";
                    for (var item = 0; item < ingredient.Count; item++)
                    {
                        Random rand = new Random();
                        var id = 0;
                        if (inghtmllen[item].OuterHtml.Contains("\"wprm-recipe-ingredient-unit\"") && inghtmllen[item].OuterHtml.Contains("\"wprm-recipe-ingredient-amount\""))
                        {
                            amount = amountNum[item - behindNum].InnerText;
                            unit = amountUnit[item - behindUnit].InnerText;
                            amount = HttpUtility.HtmlDecode(amount);
                            unit = HttpUtility.HtmlDecode(unit);
                        }
                        else if(inghtmllen[item].OuterHtml.Contains("\"wprm-recipe-ingredient-unit\""))
                        {
                            unit = amountUnit[item-behindUnit].InnerText;
                            unit = HttpUtility.HtmlDecode(unit);
                            amount = "";
                            behindNum++;
                        }
                        else if(inghtmllen[item].OuterHtml.Contains("\"wprm-recipe-ingredient-amount\""))
                        {
                            amount = amountNum[item-behindNum].InnerText;
                            amount = HttpUtility.HtmlDecode(amount);
                            unit = "";
                            behindUnit++;
                        }
                        else
                        {
                            amount = "";
                            unit = "";
                            behindNum++;
                            behindUnit++;
                        }
                        var name = HttpUtility.HtmlDecode(ingredient[item].InnerText).ToLower();
                        name=name.Replace("*","");
                        name=name.Replace(",", "");
                        if(name.Contains("\""))
                        {
                            char val = name[name.IndexOf("\"") - 1];
                            if (Char.IsDigit(val))
                            {
                                name = name.Replace("\"", "inch");
                            }
                            else
                            {
                                name = name.Replace("\"", "");
                            }
                        }
                        name = name.Replace("a ", "");
                        name=name.Replace("divided", "");
                        /*if (name.Contains("for"))
                        {
                            name = name.Substring(0, name.IndexOf("for") + 1);
                        }*/
                        if (name.Contains('('))
                        {
                            name = name.Substring(0, name.IndexOf('('));
                        }
                        name = name.Trim();
                        if (ingredients.Count != 0)
                        {
                            foreach (var ing in ingredients)
                            {
                                if (ing.Name.Equals(name))
                                {
                                    id = ing.IngId;
                                    requirements.Add(new RecipeReq { ReqId = reqid, RecId = recipe.RecId, IngId = id, Amount = amount, Measurment=unit });
                                    reqid++;
                                    break;
                                }
                            }
                            if (id == 0)
                            {
                                ingredients.Add(new Ingredient { IngId = ingid, Name = name, Price = rand.Next(1, 100) * 0.25 });
                                requirements.Add(new RecipeReq { ReqId = reqid, RecId = recipe.RecId, IngId = ingid, Amount = amount, Measurment = unit });
                                ingid++;
                                reqid++;
                            }
                        }
                        else
                        {
                            ingredients.Add(new Ingredient { IngId = ingid, Name = name, Price = rand.Next(1, 100) * 0.25 });
                            requirements.Add(new RecipeReq { ReqId = reqid, RecId = recipe.RecId, IngId = ingid, Amount = amount, Measurment = unit });
                            ingid++;
                            reqid++;
                        }
                    }
                    var instruction = doc.DocumentNode.SelectNodes("//div[@class='wprm-recipe-instruction-text']"); 
                    var order = 0;
                    foreach (var instr in instruction)
                    {
                        var instruct = "";
                        if (instr.OuterHtml.Contains("span"))
                        {
                            instruct = instr.OuterHtml.Substring(instr.OuterHtml.IndexOf(";") + 3, instr.OuterHtml.Length - 85);
                            instruct = HttpUtility.HtmlDecode(instruct);
                        }
                        else
                        {
                            instruct = instr.InnerText;
                            instruct = HttpUtility.HtmlDecode(instruct);
                        }
                        instructions.Add(new Instruction { InstrId = instrid, RecId = recipe.RecId, Order = order, Direction = instruct });
                        order++;
                        instrid++;
                    }
                    var type = doc.DocumentNode.SelectSingleNode("//p[@id='breadcrumbs']");
                    //var text = type.OuterHtml.Substring(type.OuterHtml.IndexOf("Recipes")+8);
                    var text = type.InnerText.Substring(type.InnerText.IndexOf("Recipes") + 9);
                    if(text.Contains("Recipe"))
                    {
                        text=text.Substring(text.IndexOf(" ")+1,text.IndexOf("»")-1);
                        /*text = text.Substring(text.IndexOf("Recipes") - 20);
                        text = text.Substring(text.IndexOf(">") + 1, ((text.IndexOf("<") - 1) - text.IndexOf(">")));*/
                    }
                    else if(text.Contains("Meals"))
                    {
                        text=text.Substring(text.IndexOf(" ") + 1, text.IndexOf("»") - 1);
                        /*text = text.Substring(text.IndexOf("Meals") - 20);
                        text = text.Substring(text.IndexOf(">") + 1, ((text.IndexOf("<") - 1) - text.IndexOf(">")));*/
                    }
                    else
                    {
                        text = "Other";
                        //Console.WriteLine(type.OuterHtml.Substring(type.OuterHtml.IndexOf("Recipes") - 20);
                    }
                    recipe.Type = HttpUtility.HtmlDecode(text);
                }
            }
            /*var ingid = 1;
            var reqid = 1;
            var instrid = 1;
            for (var i = 0; i < 15; i++)
            {
                doc = web.Load(recipes[i].Link);
                var ingredient = doc.DocumentNode.SelectNodes("//span[@class='wprm-recipe-ingredient-name']");
                var amountNum = doc.DocumentNode.SelectNodes("//span[@class='wprm-recipe-ingredient-amount']");
                var amountUnit = doc.DocumentNode.SelectNodes("//span[@class='wprm-recipe-ingredient-unit']");
                var inghtmllen = doc.DocumentNode.SelectNodes("//li[@class='wprm-recipe-ingredient']");
                //Mark recipes that don't fit the format to be removed
                if (ingredient == null)
                {
                    foremoval.Add(recipes.IndexOf(recipes[i]));
                }
                else
                {
                    //Check to see if an ingredient has already been added to the list before adding it 
                    var behind = 0;
                    var amount = ""; 
                    for (var item=0;item<ingredient.Count;item++)
                    {
                        Random rand = new Random();
                        var id = 0;
                        if(inghtmllen[item].OuterHtml.Contains("\"wprm-recipe-ingredient-unit\""))
                        {
                            amount = amountNum[item].InnerText + " " + amountUnit[item-behind].InnerText;
                        }
                        else
                        {
                            amount = amountNum[item].InnerText;
                            behind++;
                        }
                        if (ingredients.Count!=0)
                        {
                            foreach (var ing in ingredients)
                            {
                                if (ing.Name.Equals(ingredient[item].InnerText))
                                {
                                    id = ing.IngId;
                                    requirements.Add(new RecipeReq { ReqId = reqid, RecId = recipes[i].RecId, IngIg = id, Amount = amount });
                                    reqid++;
                                    break;
                                }
                            }
                            if (id == 0)
                            {
                                ingredients.Add(new Ingredient { IngId = ingid, Name = ingredient[item].InnerText, Price = rand.Next(1, 100) * 0.25 });
                                requirements.Add(new RecipeReq { ReqId = reqid, RecId = recipes[i].RecId, IngIg = ingid, Amount = amount });
                                ingid++;
                                reqid++;
                            }
                        }
                        else
                        {
                            ingredients.Add(new Ingredient { IngId = ingid, Name = ingredient[item].InnerText, Price = rand.Next(1, 100) * 0.25 });
                            requirements.Add(new RecipeReq { ReqId = reqid, RecId = recipes[i].RecId, IngIg = ingid, Amount = amount });
                            ingid++;
                            reqid++;
                        }                        
                    }
                    var instruction = doc.DocumentNode.SelectNodes("//span[@style='display: block;']");
                    var order = 0;
                    foreach (var instr in instruction)
                    {
                        
                        instructions.Add(new Instruction { InstrId = instrid, RecId = recipes[i].RecId, Order = order, Direction = instr.InnerText });
                        order++;
                        instrid++;
                    }
                }

            }*/
            //Remove the recipes that did not fit the format
            var removals = 0;
            foreach(var index in foremoval)
            {
                recipes.Remove(recipes[index-removals]);
                removals++;
            }
            //Print the csv's for each data set
            using (var writer = new StreamWriter("C:/Users/micha/Documents/recipes.csv"))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(recipes);
            }
            using (var writer = new StreamWriter("C:/Users/micha/Documents/ingredients.csv"))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(ingredients);
            }
            using (var writer = new StreamWriter("C:/Users/micha/Documents/reciperequirements.csv"))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(requirements);
            }
            using (var writer = new StreamWriter("C:/Users/micha/Documents/instructions.csv"))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(instructions);
            }
        }

    }
}
