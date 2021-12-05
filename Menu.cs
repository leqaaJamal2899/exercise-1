using System.Security.Cryptography.X509Certificates;
namespace OrderingPizza
{
    public class Menu
    {
        public string[] PizzaTypes { get; set; }
        public string[] PizzaSizes { get; set; }
        public string[] PizzaToppings { get; set; }
    }
}