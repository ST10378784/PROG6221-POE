using Microsoft.VisualStudio.TestTools.UnitTesting;
using RecipeApplication;

namespace RecipeApplicationTest
{
    [TestClass]
    public class RecipeTests
    {
        [TestMethod]
        public void TestTotalCaloriesCalculation()
        {
            // Arrange
            var recipe = new Recipe
            {
                Ingredients = new Ingredient[]
                {
                    new Ingredient { Name = "Ingredient1", Quantity = 1, Calories = 50 },
                    new Ingredient { Name = "Ingredient2", Quantity = 2, Calories = 70 },
                    new Ingredient { Name = "Ingredient3", Quantity = 3, Calories = 30 }
                }
            };

            // Expected total calories: 150
            int expectedTotalCalories = 150;

            // Act
            int actualTotalCalories = recipe.Ingredients.Sum(i => i.Calories);

            // Assert
            Assert.AreEqual(expectedTotalCalories, actualTotalCalories);
        }
    }
}
