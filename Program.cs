// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");
using Spectre.Console;
using System.Text.Json;
using System.Text.Json.Serialization;
namespace OrderingPizza
{
    class Program
    {
        static void Main(string[] args)
        {
            Menu menu = ReadMenuFromJSON();
            Order order = MakeOrder(menu);
            WriteOrderIntoJSON(order);
            ReviewOrder(order);
        }
        static Menu ReadMenuFromJSON(){
            string fileName = "availableInMenu.json";
            string jsonString = File.ReadAllText(fileName);
            Menu menu = JsonSerializer.Deserialize<Menu>(jsonString);
            return menu;
        }
        static Order MakeOrder(Menu menu){
            string name = AnsiConsole.Ask<string>("[green]Please enter your name[/]");
            string address = AnsiConsole.Ask<string>("[green]Please enter your address[/]");
            string[] availablePizzaTypes = menu.PizzaTypes;
            string[] availablePizzaSizes = menu.PizzaSizes;
            string[] availablePizzaToppings = menu.PizzaToppings;
            List<Pizza> pizzalist = new List<Pizza>();
            bool orderAgain = true;
            while(orderAgain){
                string pizzaType = PizzaChoices(availablePizzaTypes);
                string pizzaSize = PizzaSizeChoices(availablePizzaSizes);
                string[] toppings = ToppingsChoices(availablePizzaToppings);
                Pizza pizza = new Pizza(pizzaType, pizzaSize, toppings);
                pizzalist.Add(pizza);
                orderAgain = ContinueOrReview();
            }
            Order order = new Order(name, address, pizzalist);
            return order;
        }
        static string PizzaChoices(string[] pizzaTypes)
        {

            var pizzaType = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[green]Which pizza do you want to order?[/]")
                        .PageSize(10)
                        .MoreChoicesText("[grey](Move up and down to reveal more pizza types)[/]")
                        .AddChoices(pizzaTypes));        
            return pizzaType;
        }
        static string PizzaSizeChoices(string[] pizzaSizes)
        {
            var pizzaSize = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[green]Which size of pizza do you want?[/]")
                        .PageSize(10)
                        .MoreChoicesText("[grey](Move up and down to reveal more sizes)[/]")
                        .AddChoices(pizzaSizes)); 
            return pizzaSize;
        }
        static string[] ToppingsChoices(string[] pizzaToppings)
        {
            var toppings = AnsiConsole.Prompt(
                    new MultiSelectionPrompt<string>()
                        .Title("[green]Please choose the toppings that you want to add[/]")
                        .NotRequired() // Not required to have a favorite fruit
                        .PageSize(10)
                        .MoreChoicesText("[grey](Move up and down to reveal more toppings)[/]")
                        .InstructionsText(
                            "[grey](Press [blue]<space>[/] to toggle a topping, " + 
                            "[green]<enter>[/] to accept)[/]")
                        .AddChoices(pizzaToppings));
            string[] toppingsArr = new string[toppings.Count];
            int i = 0;
            foreach (string topping in toppings) 
            {
                toppingsArr[i]=topping;
                i++;
            }
            return toppingsArr;
        }
        static bool ContinueOrReview()
        {
           var answer = AnsiConsole.Prompt(
                    new TextPrompt<string>("[green]Do you want to order any thing else? [/]")
                        .InvalidChoiceMessage("[red]That's not a valid answer, please type yes or no[/]")
                        .DefaultValue("yes")
                        .AddChoice("yes")
                        .AddChoice("no"));
            bool orderAgain = false;
            if(string.Equals(answer,"yes"))
                orderAgain = true;
            return orderAgain;
        }
        static void WriteOrderIntoJSON(Order order){
            string fileName = "madeOrders.json";
            var options = new JsonSerializerOptions { WriteIndented = true };
            List<Order> orders = readAllPreviousOrders();
            orders.Add(order);
            string jsonString = JsonSerializer.Serialize(orders, options);
            // File.WriteAllText(fileName, jsonString);
            File.WriteAllText(fileName, jsonString);
        }
        static List<Order> readAllPreviousOrders(){
            string fileName = "madeOrders.json";
            string jsonString = File.ReadAllText(fileName);
            List<Order> orders;
            if(!jsonString.Equals(""))
                orders = JsonSerializer.Deserialize<List<Order>>(jsonString);
            else
                orders = new List<Order>();
            return orders;
        }
        static void ReviewOrder(Order order){
            AnsiConsole.WriteLine(order.ToString());
        }
    }
}