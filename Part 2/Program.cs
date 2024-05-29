using System;
using System.Collections.Generic;
using System.Linq;

namespace RecipeApplication
{
    class Program
    {
        // Delegate for warning about total calories
        public delegate void CalorieWarning(int totalCalories);

        static List<Recipe> recipes = new List<Recipe>(); // Moved recipes list here as a static variable

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("1. Add a new recipe");
                Console.WriteLine("2. Display all of the recipe's");
                Console.WriteLine("3. Exit the application");
                string choice = Console.ReadLine();

                Console.WriteLine(); // Empty line after user input

                switch (choice)
                {
                    case "1":
                        Recipe recipe = EnterRecipe();
                        recipes.Add(recipe);
                        break;
                    case "2":
                        DisplayAllRecipes();
                        break;
                    case "3":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }

                // Prompt the user to press Enter to continue
                Console.WriteLine("Press Enter to continue.");
                Console.ReadLine();
                Console.Clear(); // Optional: Clear the console before showing the menu again
            }
        }

        static Recipe EnterRecipe()
        {
            Recipe recipe = new Recipe();

            Console.WriteLine("Enter the recipe's name:");
            recipe.Name = Console.ReadLine();

            Console.WriteLine("Enter the number of ingredients:");
            int numIngredients;
            while (!int.TryParse(Console.ReadLine(), out numIngredients) || numIngredients <= 0)
            {
                Console.WriteLine("Invalid input. Please enter a number > 0 for the amount of ingredients.");
            }

            recipe.Ingredients = new Ingredient[numIngredients];
            for (int i = 0; i < numIngredients; i++)
            {
                Console.WriteLine($"Enter ingredient #{i + 1}'s name:");
                string name = Console.ReadLine();

                double quantity;
                while (true)
                {
                    Console.WriteLine($"Enter the quantity for - {name}:");
                    if (!double.TryParse(Console.ReadLine(), out quantity) || quantity <= 0)
                    {
                        Console.WriteLine("Invalid input. Please enter a number > 0 for the quantity.");
                    }
                    else
                    {
                        break;
                    }
                }

                Console.WriteLine($"Enter the unit of measurement for - {name}:");
                string unit = Console.ReadLine();

                Console.WriteLine($"Enter the amount of calories for - {name}:");
                int calories;
                while (!int.TryParse(Console.ReadLine(), out calories) || calories < 0)
                {
                    Console.WriteLine("Invalid input. Please enter a number > 0 for the amount of calories.");
                }

                Console.WriteLine($"Enter the name of the food group for - {name}:");
                string foodGroup = Console.ReadLine();

                recipe.Ingredients[i] = new Ingredient { Name = name, Quantity = quantity, Unit = unit, Calories = calories, FoodGroup = foodGroup, OriginalQuantity = quantity };
            }

            // Calculate total calories
            int totalCalories = recipe.Ingredients.Sum(i => i.Calories);
            Console.WriteLine($"Total calories: {totalCalories}");

            // Show warning message if total calories exceed 300
            CalorieWarning calorieWarning = DisplayCalorieWarning;
            calorieWarning(totalCalories);

            Console.WriteLine($"Enter the number of steps for recipe - {recipe.Name}:");
            int numSteps;
            while (!int.TryParse(Console.ReadLine(), out numSteps) || numSteps <= 0)
            {
                Console.WriteLine("Invalid input. Please enter a number > 0 for the number of steps.");
            }

            recipe.Steps = new string[numSteps];
            for (int i = 0; i < numSteps; i++)
            {
                Console.WriteLine($"Enter step #{i + 1}'s description:");
                recipe.Steps[i] = Console.ReadLine();
            }

            // Prompt the user to scale quantities
            Console.WriteLine("Do you want to scale the quantities? ( yes / no )");
            string scaleChoice = Console.ReadLine();
            if (scaleChoice.ToLower() == "yes")
            {
                ScaleQuantities(recipe);
            }

            return recipe;
        }

        static void DisplayAllRecipes()
        {
            if (recipes.Count == 0)
            {
                Console.WriteLine("No recipes are available.");
                return;
            }

            // Sort recipes alphabetically by name
            recipes.Sort((r1, r2) => string.Compare(r1.Name, r2.Name));

            Console.WriteLine("Recipes:");
            foreach (var recipe in recipes)
            {
                Console.WriteLine(recipe.Name);
            }

            // Prompt the user to enter a recipe name to display details
            Console.WriteLine("Enter the name of the recipe to display details:");
            string recipeName = Console.ReadLine();
            Recipe selectedRecipe = recipes.FirstOrDefault(r => r.Name == recipeName);
            if (selectedRecipe != null)
            {
                DisplayRecipe(selectedRecipe);
            }
            else
            {
                Console.WriteLine("No matching recipe was found.");
            }

            // Prompt the user to press Enter to continue
            Console.WriteLine("Press Enter to continue.");
            Console.ReadLine();
            Console.Clear();
        }

        static void ScaleQuantities(Recipe recipe)
        {
            Console.WriteLine("Enter the scaling factor:");
            if (double.TryParse(Console.ReadLine(), out double factor))
            {
                foreach (var ingredient in recipe.Ingredients)
                {
                    ingredient.OriginalQuantity = ingredient.Quantity;
                    ingredient.Quantity *= factor;
                }
                Console.WriteLine("Quantities are scaled successfully.");

                Console.WriteLine("Do you want to reset the quantities to their original values? ( yes / no )");
                string resetChoice = Console.ReadLine();
                if (resetChoice.ToLower() == "yes")
                {
                    ResetQuantities(recipe);
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Quantities are not scaled.");
            }
        }

        static void ResetQuantities(Recipe recipe)
        {
            foreach (var ingredient in recipe.Ingredients)
            {
                ingredient.Quantity = ingredient.OriginalQuantity; // Restore original quantity
            }

            Console.WriteLine("Quantities are successfully reset.");
        }

        static void DisplayRecipe(Recipe recipe)
        {
            Console.WriteLine($"Recipe: {recipe.Name}");
            Console.WriteLine("Ingredients:");
            foreach (var ingredient in recipe.Ingredients)
            {
                Console.WriteLine($"{ingredient.Quantity} {ingredient.Unit} of {ingredient.Name}, {ingredient.Calories} calories, Food group: {ingredient.FoodGroup}");
            }

            int totalCalories = recipe.Ingredients.Sum(i => i.Calories);
            Console.WriteLine($"Total calories: {totalCalories}");

            // Show warning message if total calories exceed 300
            CalorieWarning calorieWarning = DisplayCalorieWarning;
            calorieWarning(totalCalories);

            Console.WriteLine("Steps:");
            foreach (var step in recipe.Steps)
            {
                Console.WriteLine(step);
            }
        }

        static void DisplayCalorieWarning(int totalCalories)
        {
            if (totalCalories > 300)
            {
                Console.WriteLine("Warning: The total calories exceed an amount of 300!");
            }
        }
    }

    public class Recipe
    {
        public string Name { get; set; }
        public Ingredient[] Ingredients { get; set; }
        public string[] Steps { get; set; }
    }

    public class Ingredient
    {
        public string Name { get; set; }
        public double Quantity { get; set; }
        public string Unit { get; set; }
        public int Calories { get; set; }
        public string FoodGroup { get; set; }
        public double OriginalQuantity { get; set; }
    }
}
