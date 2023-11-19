

using System;


namespace FirstApp
{

    abstract class Delivery // абстрактный класс
    {
        public string Address;
        public int DeliveryStatus=0;
        public virtual void CancelDelivery()
        {
            string Answer;
            Console.WriteLine("Are you sure? (Y/N)");
            Answer=Console.ReadLine();
            if (Answer == "Y" || Answer=="YES")
            {
                DeliveryStatus = 1;
            }
        }       
    }

    class HomeDelivery : Delivery // наследование
    {
        public bool isCourierAssigned;
        public CourierSKA Courier; // композиция
        public HomeDelivery()
        {
            Courier = new CourierSKA();
        }


    }

    class PickPointDelivery : Delivery
    {
        /* ... */
    }

    class ShopDelivery : Delivery
    {
        /* ... */
    }

    class ProductSKA // Мой класс 1
    {
        public enum PossibleArticuls
        {
            ART1,
            ART2,
            ART3,
            ART4
        }
        private PossibleArticuls articul; // Инкапсуляция
        public PossibleArticuls Articul 
        {
            get { return articul; }
            set { articul = value; }
        }
       // public ProductSKA(PossibleArticuls art)
        //{
          //  this.Articul = art;
        //}
        //public string ProductName;
    }
    class CourierSKA // мой класс 2
    {
        public string FIO;
    }
    class ProductOrderSKA:ProductSKA // мой класс 3
    {
        public int ProductQuantity;
        public double ProductWeight;

    }
    class ShopSKA // мой класс 4
    {
        public string ShopAddress;
        public string ShopName;
    }

    class Order<TDelivery> where TDelivery : Delivery //  обобщение
    {
        public static int MaxCountOrders = 5; // Статичный элемент
        public static int MaxCountProducts = 6;
        public ProductOrderSKA[] product;
        public Test[] test;
        public TDelivery Delivery;

        public int Number;

        public string Description;

        public void DisplayAddress()
        {
            Console.WriteLine(Delivery.Address);
        }
    }
    class Test
    {
        int testval;
    }

    class Program
    {
        static bool checkAnswer()
        {
            string Answer;
            Answer = Console.ReadLine();
            if (Answer.ToUpper() == "YES" || Answer.ToUpper() == "Y")
                return true;
            return false;
        }
        static void Main(string[] args)
        {
            int i = 0, j = 0, a=0, b=0;
            string EnteredArticul;
            //HomeDelivery delivery = new HomeDelivery();

            Order<Delivery>[] order = new Order<Delivery>[Order<Delivery>.MaxCountOrders];

            while (true)
            {
                Console.WriteLine("Хотите создать новый заказ? (Y/N)");
                if (checkAnswer() && i< Order<Delivery>.MaxCountOrders)
                {
                    order[i] = new Order<Delivery>();

                    order[i].Number = i+1;
                    order[i].Description = "мой заказ N " + i.ToString();
                    order[i].product = new ProductOrderSKA[Order<Delivery>.MaxCountProducts];
                    //order[i].test = new Test[5];
                    while (true)
                    {
                        Console.WriteLine("Хотите добавить продукт в заказ N{0} (Y/N)",i+1);
                        if (checkAnswer() && j < Order<Delivery>.MaxCountProducts)
                        {
                            order[i].product[j] = new ProductOrderSKA();
                            //order[i].test[0] = new Test();
                            Console.WriteLine("Введите Артикул продукта");
                            EnteredArticul = Console.ReadLine();
                            if (Enum.IsDefined(typeof(ProductSKA.PossibleArticuls), EnteredArticul))
                            {

                                order[i].product[j].Articul = (ProductSKA.PossibleArticuls)Enum.Parse(typeof(ProductSKA.PossibleArticuls), EnteredArticul);
                                //order[i].product[j].Articul = ProductSKA.PossibleArticuls.ART1;
                                Console.WriteLine("Введите количество продукта в заказе");
                                order[i].product[j].ProductQuantity = int.Parse(Console.ReadLine());
                                j++;
                            }
                            else
                            {
                                Console.WriteLine("Такого артикула нет");
                            }
                        }
                        else
                            break;
                     
                    }
                    Console.WriteLine("Заказ {0} сформирован",i+1);
 
                }
                else
                {
                    if (i == Order<Delivery>.MaxCountOrders-1)
                    {
                        Console.WriteLine("Превышено максимальное число заказов {0}", Order<Delivery>.MaxCountOrders);
                    }
                    else
                        Console.WriteLine("До свидания тогда!");
                    for (a = 0; a < i; a++)
                    {
                        for (b = 0; b < j; b++)
                        {
                            Console.WriteLine("Заказ {0} Product {1} Quantity {2}", order[a].Number, order[a].product[b].Articul, order[a].product[b].ProductQuantity);
                        }
                    }
                    return;
                }
                i++;
            }
            Console.ReadKey();
        }
    }
}