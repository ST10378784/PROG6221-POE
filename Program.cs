using System;

namespace recipeApp
{
    // Program class containing Main method
    class Program
    {
        static void Main(string[] args)
        {
            // Instance of Recipe class
            Recipe recipe = new Recipe();

            // Prompting user for recipe details/storing them
            Console.WriteLine("Enter The Recipe's Name: ");
            recipe.Name = Console.ReadLine();
            Console.WriteLine("\n");

            Console.WriteLine("Enter The Number Of Ingredients: ");
            int numIngredients = int.Parse(Console.ReadLine());
            recipe.Ingredients = new Ingredient[numIngredients];

            for (int i = 0; i < numIngredients; i++)
            {
                Console.WriteLine($"Enter Ingredient #{i + 1}'s Name: ");
                string name = Console.ReadLine();

                Console.WriteLine($"Enter The Quantity For - {name}: ");
                double quantity = double.Parse(Console.ReadLine());

                Console.WriteLine($"Enter The Unit Of Measurement For - {name}: ");
                string unit = Console.ReadLine();

                recipe.Ingredients[i] = new Ingredient { Name = name, Quantity = quantity, Unit = unit };
            }
            Console.WriteLine("\n");


            Console.WriteLine("Enter The Number Of Steps: ");
            int numSteps = int.Parse(Console.ReadLine());
            recipe.Steps = new string[numSteps];

            for (int i = 0; i < numSteps; i++)
            {
                Console.WriteLine($"Enter The Description Of Step #{i + 1}: ");
                recipe.Steps[i] = Console.ReadLine();
            }

            Console.WriteLine("\n");

            // Display recipe details
            DisplayRecipe(recipe);

            Console.WriteLine("\n");

            // Prompting user for scaling factor/reset option
            Console.WriteLine("Enter The Scaling Factor / Type 'Reset' To Reset The Quantities: ");
            string input = Console.ReadLine();

            Console.WriteLine("\n");

            if (input == "Reset")
            {
                ResetQuantities(recipe);
                DisplayRecipe(recipe);
            }
            else
            {
                double factor = double.Parse(input);
                ScaleQuantities(recipe, factor);
                DisplayRecipe(recipe);
            }

            Console.WriteLine("\n");

            // Prompting user to clear data/start over
            Console.WriteLine("Press Any Key To Clear The Data And Enter A New Recipe...");
            Console.ReadKey();
            Console.Clear();
            Main(args);
        }

        // Method to display recipe details
        static void DisplayRecipe(Recipe recipe)
        {
            Console.WriteLine($"Recipe: {recipe.Name}");
            Console.WriteLine("Ingredients: ");

            foreach (var ingredient in recipe.Ingredients)
            {
                Console.WriteLine($"{ingredient.Quantity} {ingredient.Unit} Of {ingredient.Name}");
            }

            Console.WriteLine("Steps: ");

            foreach (var step in recipe.Steps)
            {
                Console.WriteLine(step);
            }
        }

        // Method to scale ingredient quantities
        static void ScaleQuantities(Recipe recipe, double factor)
        {
            foreach (var ingredient in recipe.Ingredients)
            {
                ingredient.Quantity *= factor;
            }
        }

        // Method to reset ingredient quantities
        static void ResetQuantities(Recipe recipe)
        {
            // Reset quantities to original/assuming values are stored already
        }
    }

    // Recipe class that represents a recipe
    class Recipe
    {
        public string Name { get; set; }
        public Ingredient[] Ingredients { get; set; }
        public string[] Steps { get; set; }
    }

    // Ingredient class representing an ingredient
    class Ingredient
    {
        public string Name { get; set; }
        public double Quantity { get; set; }
        public string Unit { get; set; }
    }
}
