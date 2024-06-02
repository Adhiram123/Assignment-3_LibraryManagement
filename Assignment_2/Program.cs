using System;

namespace Assingment
{
    public class Item
    {
        int _id;
        string _name;
        double _price;
        double _quantity;
        public Item(int id, string name, double price, double quantity)
        {
            _id = id;
            _name = name;
            _price = price;
            _quantity = quantity;
        }
        public Item() { }

        public int id
        {
            get { return _id; }
            set { _id = value; }
        }
        public string name
        {
            get { return _name; }
            set { _name = value; }
        }
        public double price
        {
            get { return _price; }
            set { _price = value; }

        }
        public double quantity
        {
            get { return _quantity; }
            set { _quantity = value; }
        }
    }
    public class ItemOperations
    {
        static List<Item> itemList = new List<Item>();
        static Item iems;
        static void Main()
        {
            bool exit = false;
            while (exit == false)
            {
                Console.WriteLine("--------------------------------------------");
                Console.WriteLine("Select the Option To Performe Operations :");
                Console.WriteLine("--------------------------------------------");
                Console.WriteLine("1.Adding new Item Into List");
                Console.WriteLine("2.Displaying all Items ");
                Console.WriteLine("3.Finding an item bY Id");
                Console.WriteLine("4.Updating an Items Information");
                Console.WriteLine("5.Delteing an item");
                Console.WriteLine("6 Exit!");
                Console.WriteLine("---------------------------------------------");
                try
                {
                    int choic = Convert.ToInt32(Console.ReadLine());
                    switch (choic)
                    {
                        case 1:
                            itemm();
                            Console.WriteLine("new Item is Added!\n");
                            break;
                        case 2:
                            Console.WriteLine("Total available Items "); Console.WriteLine();
                            foreach (Item item in itemList)
                            {
                                Console.WriteLine("[item id is :{" + item.id + "} Name of an Item is:{" + item.name + "} Price of an Item is:{" + item.price + "} Quantity of an Item is:{" + item.quantity + "}]");

                            }
                            break;
                        case 3:
                            Console.WriteLine();
                            Console.WriteLine("Enter the id to find an Item");
                            int ItemIdToFind = FindItemById(Convert.ToInt32(Console.ReadLine()));
                            if (ItemIdToFind != -1)
                            {
                                foreach (Item item in itemList)
                                {
                                    if (ItemIdToFind == item.id)
                                    {
                                        Console.WriteLine("Item Id      :" + item.id);
                                        Console.WriteLine("Item Name    :" + item.name);
                                        Console.WriteLine("Item price   :" + item.price);
                                        Console.WriteLine("Item Quantity:" + item.quantity);
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine("Id NOT FOUND!");
                            }
                            break;
                        case 4:
                            Update();
                            break;
                        case 5:
                            Console.WriteLine("Enter Item Id to delete:");
                            int deleteId = int.Parse(Console.ReadLine());
                            Item deleteItem = itemList.Find(i8 => i8.id == deleteId);
                            if (deleteItem != null)
                            {
                                itemList.Remove(deleteItem);
                                Console.WriteLine("Item deleted Succesfully");
                            }
                            else
                            {
                                Console.WriteLine("You have Entered Invalid Id ");
                            }
                            break;
                        case 6:
                            exit = true;
                            break;
                        default:
                            Console.WriteLine("Invalid Operation is selected"); break;
                    }
                }
                catch (FormatException e)
                {
                    Console.WriteLine(e.Message + "\nSo that current operation is failed");
                }
            }
        }
        static void itemm()
        {
            Item ii = new Item();
            Console.WriteLine("Enter the Id name of the Item");
            ii.id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter the name of the Item");
            ii.name = Console.ReadLine();

            Console.WriteLine("Enter the price of the item");
            ii.price = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("Enter the Quantity of the item");
            ii.quantity = Convert.ToDouble(Console.ReadLine());

            itemList.Add(new Item(ii.id, ii.name, ii.price, ii.quantity));

        }
        static int FindItemById(int i)
        {
            int idd = i;

            Item id1 = itemList.Find(Item => Item.id == idd);
            if (id1 != null)
            {

                return idd;
            }
            else
            {
                return -1;
            }

        }
        static void Update()
        {
            Console.WriteLine("Enter  Item ID ");
            int userEnterdId = Convert.ToInt32(Console.ReadLine());
            Item UpdateItem = itemList.Find(Item => Item.id == userEnterdId);
            if (UpdateItem != null)
            {
                bool exit = true;
                while (exit)
                {
                    Console.WriteLine("1.to Udpate name 2.to Update price 3.to Update Quantity 4. to Eixt!");
                    int Opt = Convert.ToInt32(Console.ReadLine());
                    switch (Opt)
                    {
                        case 1:
                            Console.WriteLine("Enter the name..?");
                            UpdateItem.name = Console.ReadLine();
                            Console.WriteLine("Updated!");
                            break;
                        case 2:
                            Console.WriteLine("Enter the price..?");
                            UpdateItem.price = Convert.ToDouble(Console.ReadLine());
                            Console.WriteLine("Updated!");
                            break;
                        case 3:
                            Console.WriteLine("Enter the quantity..?");
                            UpdateItem.quantity = Convert.ToDouble(Console.ReadLine());
                            Console.WriteLine("Updated!");
                            break;
                        case 4:
                            exit = false;
                            break;
                        default:
                            Console.WriteLine("Please Enter Correct option!!!");
                            break;
                    }

                }

            }
            else
            {
                Console.WriteLine("Id is not found So you Can't Update");
            }
        }

    }
}
