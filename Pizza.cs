namespace OrderingPizza
{
    public class Pizza
    {
        public string PizzaType { get; set; }
        public string PizzaSize { get; set; }
        public string[] Toppings { get; set; }

        public Pizza(string pizzaType, string pizzaSize, string[] toppings){
            this.PizzaType = pizzaType;
            this.PizzaSize = pizzaSize;
            this.Toppings = toppings;
        }
    }
}