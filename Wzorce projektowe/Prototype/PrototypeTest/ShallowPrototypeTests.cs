using Microsoft.VisualStudio.TestTools.UnitTesting;
using Prototype.ShallowPrototype;

namespace PrototypeTest
{
    [TestClass]
    public class PrototypeTestsTaskOne
    {
        string name = "Kowalski";
        int income = 10000;
        Restaurant restaurant;
        Restaurant clonedRestaurant;

        [TestInitialize]
        public void Initialize()
        {
            restaurant = new Restaurant(new Kitchen(new Chef(new Assistant() { Name = name, Income = income })));
            clonedRestaurant = restaurant.Clone() as Restaurant;
        }

        [TestMethod]
        public void RestaurantsAreNotEqual()
        {
            Assert.AreNotEqual(restaurant, clonedRestaurant);
        }

        [TestMethod]
        public void KitchensOfBothRestaurantsAreEqual()
        {
            var kitchen = restaurant.Kitchen;
            var clonedKitchen = clonedRestaurant.Kitchen; //"clonedKitchen"
            Assert.AreEqual(kitchen, clonedKitchen);
        }

        [TestMethod]
        public void ChefsOfKitchensOfBothRestaurantsAreEqual()
        {
            var chef = restaurant.Kitchen.Chef;
            var clonedChef = clonedRestaurant.Kitchen.Chef; //"clonedChef"
            Assert.AreEqual(chef, clonedChef);
        }

        [TestMethod]
        public void AssistantsOfChefsOfKitchensOfBothRestaurantsAreEqual()
        {
            var assistant = restaurant.Kitchen.Chef.Assistant;
            var clonedAssistant = clonedRestaurant.Kitchen.Chef.Assistant; //"clonedAssistant"
            Assert.AreEqual(assistant, clonedAssistant);
        }

        [TestMethod]
        public void AssistantsOfChefsOfKitchensOfBothRestaurantsHaveEqualFields()
        {
            var assistant = restaurant.Kitchen.Chef.Assistant;
            var clonedAssistant = clonedRestaurant.Kitchen.Chef.Assistant;

            Assert.AreEqual(assistant.Name, clonedAssistant.Name);
            Assert.AreEqual(assistant.Income, clonedAssistant.Income);
        }

        [TestCleanup]
        public void Cleanup()
        {
            restaurant = null;
            clonedRestaurant = null;
        }
    }
}
