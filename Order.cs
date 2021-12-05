namespace OrderingPizza
{
    public class Order
    {
        public string UserName { get; set; }
        public string UserAddress { get; set; }
        public List<Pizza> ChosenPizzas { get; set; }

        public Order(string userName, string userAddress, List<Pizza> chosenPizzas){
            this.UserName = userName;
            this.UserAddress = userAddress;
            this.ChosenPizzas = chosenPizzas;
        }

        public string ToString(){
            string order = "Order details: \n"+"User name: "+ UserName+"\n"+ "Address: " + UserAddress+"\n"
            +"number of meals: "+ (ChosenPizzas.Count)+"\n";
            for(int i=0; i<ChosenPizzas.Count; i++ ){
                Pizza p = ChosenPizzas[i];
                order+= (i+1)+"- "+p.PizzaType+" pizza of size "+p.PizzaSize;
                if(p.Toppings.Length>0){
                    order+=" with the following toppings: ";
                    for(int j=0; j<p.Toppings.Length-2; j++){
                        order+= p.Toppings[j]+", ";
                    }
                    order+=p.Toppings[p.Toppings.Length-2]+" and "+ p.Toppings[p.Toppings.Length-1]+"\n";
                }
                else{
                    order+= " with no extra toppings \n";
                }
            }
            return order;
        }

    }
}