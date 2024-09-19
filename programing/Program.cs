
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Security.Cryptography;
    using static System.Net.Mime.MediaTypeNames;

    namespace programing
    {
        public class Character
        {
            private string Username { get; set; }
            private int Strength { get; set; }
            private int Agility { get; set; }
            private int Capacity { get;  set; }
            private int ItemsWeight { get;  set; }
            public int CurrentHp { get;  set; }
            private bool IsAlive { get;  set; }
            private int Defence { get;  set; }
            private int MaxHp { get;  set; }
            private List<Item> Inventory { get;  set; }
            private Item ItemInHand { get;  set; }
            private Item ItemInHandother { get;  set; }

            public Item EquippedArmor { get; private set; }
            public Coins Coins { get; private set; }

            private static readonly RandomNumberGenerator rng = RandomNumberGenerator.Create();

            public Character(string name)
            {
                if (string.IsNullOrWhiteSpace(name))
                {
                    throw new ArgumentException("Name cannot be null or empty", nameof(name));
                }

                Username = name;
                Strength = GetRandomNumber(2, 10);
                Defence = GetRandomNumber(2, 10);
                MaxHp = GetRandomNumber(4, 10);
                Agility = GetRandomNumber(2, 10);
                Capacity = GetRandomNumber(25, 40);
                ItemsWeight = 0;
                Inventory = new List<Item>();
                ItemInHand = new Item();
                ItemInHandother = new Item();
                EquippedArmor = new Item();
                EquippedArmor.setdef(0);
                ItemInHand.setdam(0);
                ItemInHand.setdef(0);
                ItemInHandother.setdam(0);
                ItemInHandother.setdef(0);
                Coins = new Coins();
                CurrentHp = MaxHp;
                IsAlive = true;
            }


            public  int CharacterStats()
            {




                Console.WriteLine("Your stats:");
                Console.WriteLine("Username: " + Username);
                Console.WriteLine("Strength: " + Strength + " (" + get_addition_dam() + ")");
                Console.WriteLine("Defence: " + Defence + " (" + get_addition_def() + ")");
                Console.WriteLine("Max HP: " + MaxHp);
                Console.WriteLine("Current Hp: " + CurrentHp);
                Console.WriteLine("agility: " + Agility);
                Console.WriteLine("capacity: " + Capacity + " / " + ItemsWeight);
                return 1;


            }





            private int GetRandomNumber(int minValue, int maxValue)
            {
                byte[] randomNumber = new byte[1];
                rng.GetBytes(randomNumber);
                double asciiValueOfRandomChar = Convert.ToDouble(randomNumber[0]);
                double multiplier = Math.Max(0, (asciiValueOfRandomChar / 255d) - 0.00000000001d);
                int range = maxValue - minValue + 1;
                double randomValueInRange = Math.Floor(multiplier * range);

                return (int)(minValue + randomValueInRange);
            }



            public int get_addition_def()
            {
                int weapon_add_def = 0;
                
                if (ItemInHand.getdef() != 404)
                {
                    weapon_add_def += ItemInHand.getdef();

                }

                if (ItemInHandother.getdef() != 404)
                {
                    weapon_add_def += ItemInHandother.getdef();

                }
                weapon_add_def += EquippedArmor.getdef();
                return weapon_add_def;


            }
            public int get_addition_dam()
            {
                int weapon_add_damage = 0;

                if (ItemInHand.getdam() != 404)
                {
                    weapon_add_damage += ItemInHand.getdam();

                }
                if (ItemInHandother.getdam() != 404)
                {
                    weapon_add_damage += ItemInHandother.getdam();

                }

                return weapon_add_damage;

            }

            public int use_item(int index)
            {
                Item item = Inventory[index];
                if (item.getType()== "apple")
                {
                    CurrentHp += 1;
                    if (CurrentHp > MaxHp)
                    {
                        Console.WriteLine("already max hp");

                        return 0;

                    }
                    RemoveItem(index);
                    return 1;
                }


                Console.WriteLine("item cannot be used");
                return 0;

            }


            public static two_handed_weapon CreateWeapontwo(int damage, string name, int weight)
            {
                if (damage < 0 || weight < 0 || string.IsNullOrWhiteSpace(name))
                {
                    throw new ArgumentException("Invalid weapon parameters.");
                }
                return new two_handed_weapon(damage, name, weight);
            }
            public static one_handed_weapon CreateWeaponone(int damage, string name, int weight)
            {
                if (damage < 0 || weight < 0 || string.IsNullOrWhiteSpace(name))
                {
                    throw new ArgumentException("Invalid weapon parameters.");
                }
                return new one_handed_weapon(damage, name, weight);
            }

            public static Armor CreateArmor(int defense, string name, int weight)
            {
                if (defense < 0 || weight < 0 || string.IsNullOrWhiteSpace(name))
                {
                    throw new ArgumentException("Invalid armor parameters.");
                }
                return new Armor(defense, name, weight);
            }

            public static Shield CreateShield(int defense, string name, int weight)
            {
                if (defense < 0 || weight < 0 || string.IsNullOrWhiteSpace(name))
                {
                    throw new ArgumentException("Invalid shield parameters.");
                }
                return new Shield(defense, name, weight);
            }

            public static Game_Item CreateItem(string name, int weight)
            {
                if (weight < 0 || string.IsNullOrWhiteSpace(name))
                {
                    throw new ArgumentException("Invalid item parameters.");
                }
                return new Game_Item(name, weight);
            }

            public void GetMoneyInfo() => Coins.MoneyInfo();

            public void AddCoins(int copper, int silver, int gold) => Coins.AddMoney(copper, silver, gold);

            public int RemoveCoins(int copper, int silver, int gold) => Coins.RemoveMoney(copper, silver, gold);

            private int CalculatePowerIndex(int defense, int damage)
            {
                return (damage - GetRandomNumber(1, 4) + defense - GetRandomNumber(1, 4));
            }





            public void Fight(Enemy enemy)
            {
                if (enemy == null) throw new ArgumentNullException(nameof(enemy));

                int yourPowerIndex = CalculatePowerIndex((Defence + get_addition_def()), (Strength +get_addition_dam()));
                int enemyPowerIndex = CalculatePowerIndex(enemy.getdef(), enemy.getdam());

                Console.WriteLine($"Your power level: {yourPowerIndex}");
                Console.WriteLine($"Enemy power level: {enemyPowerIndex}");

                if (enemyPowerIndex <= yourPowerIndex)
                {
                    Console.WriteLine("You won");
                }
                else
                {
                    CurrentHp -= 2;
                    if (CurrentHp < 0)
                    {
                        IsAlive = false;
                        Console.WriteLine("You died");
                    }
                    else
                    {
                        Console.WriteLine("Enemy won, you lose");
                    }
                }
            }

            public void Flee(Enemy enemy)
            {
                if (enemy == null) throw new ArgumentNullException(nameof(enemy));

                int enemyPowerIndex = CalculatePowerIndex(enemy.getdef(), enemy.getdam());
                if (enemyPowerIndex <= (Agility + Strength + 5))
                {
                    Console.WriteLine("You escaped");
                }
                else
                {
                    CurrentHp -= 1;
                    if (CurrentHp < 0)
                    {
                        IsAlive = false;
                        Console.WriteLine("You died");
                    }
                    else
                    {
                        Console.WriteLine("You got injured while trying to escape");
                    }
                }
            }

            public int ShowItemStats(int index)
            {
                if (index < 0 ||   index >= Inventory.Count) { Console.WriteLine("Invalid inventory index."); return 0; }



            Item item = Inventory[index];
           


                Console.WriteLine($"Item Type: {item.getType()}");
                Console.WriteLine("Stats:");
                if (item.getdef() != 404)
                    Console.WriteLine($"Defense: {item.getdef()}");
                if (item.getdam() != 404)
                    Console.WriteLine($"Damage: {item.getdam()}");
                Console.WriteLine($"Name: {item.getname()}");
                Console.WriteLine($"Weight: {item.getweight()}");
            return 1;
            }

            public int AddToInventory(Item item)
            {
                if (item == null) throw new ArgumentNullException(nameof(item));
                if (item.getweight() + ItemsWeight <= Capacity)
                {
                    Inventory.Add(item);
                    ItemsWeight += item.getweight();
                    Console.WriteLine("Item added");
                    return 1;
                }
                else
                {
                    Console.WriteLine("Inventory is full");
                    return 0;
                }
            }

            public int RemoveItem(int index)
            {
            if (index < 0 || index >= Inventory.Count) { Console.WriteLine("Invalid inventory index."); return 0; }

                Capacity -= Inventory[index].getweight();
                Inventory.RemoveAt(index);
                Console.WriteLine("Item deleted");
            return 1;
            }

            public int EquipItem(int index)
            {
            if (index < 0 || index >= Inventory.Count) { Console.WriteLine("Invalid inventory index."); return 0; }

                Item item = Inventory[index];

                if (item.getType() == "two-weapon" && ItemInHand.getname() == null && ItemInHandother.getname() == null)
                {
                    ItemInHand = item;
                    ItemInHandother=item;
                    Inventory.RemoveAt(index);
                    Console.WriteLine("Item equipped");
                    return 1;


                }
                if (item.getType() == "one-weapon" || item.getType() == "shield")
                {
                    if (ItemInHand.getname() == null)
                    {
                        ItemInHand = item;
                        Inventory.RemoveAt(index);
                        Console.WriteLine("Item equipped");
                    return 1;

                }
                else if(ItemInHandother.getname()==null)
                    {
                        ItemInHandother = item;
                        Inventory.RemoveAt(index);
                        Console.WriteLine("Item equipped");
                    return 1;



                }
                else
                    {

                    Console.WriteLine("Slot is already full");
                    return 0;

                }

            }
                else if (item.getType() == "armor")
                {
                    if (EquippedArmor.getname() == null)
                    {
                        EquippedArmor = item;
                        Inventory.RemoveAt(index);
                        Console.WriteLine("Armor equipped");
                    return 1;

                }
                else
                    {
                        Console.WriteLine("Armor slot is full");
                    return 0;

                }
            }
                else
                {
                    Console.WriteLine("Item cannot be equipped");
                return 0;

            }
        }

            public void UnequipWeapon()
            {
                if (ItemInHand.getname() != null)
                {
                    AddToInventory(ItemInHand);
                    ItemInHand = new Item();


                    Console.WriteLine("Weapon unequipped");
                    
                }
                if (ItemInHandother.getType() =="two-weapon")
                {
                    return;
                }
               else if (ItemInHandother.getname() != null)
                {
                    AddToInventory(ItemInHandother);
                    ItemInHandother = new Item();
                    Console.WriteLine("Weapon unequipped");
                }
                else
                {
                    Console.WriteLine("Weapon slot is empty");
                }
            }

            public void UnequipArmor()
            {
                if (EquippedArmor.getname() != null)
                {
                    AddToInventory(EquippedArmor);
                    EquippedArmor = new Item();
                    Console.WriteLine("Armor unequipped");
                }
                else
                {
                    Console.WriteLine("Armor slot is empty");
                }
            }

            public void ViewInventory()
            {
                for (int i = 0; i < Inventory.Count; i++)
                {
                    Console.WriteLine($"{i}. {Inventory[i].getname()} ({Inventory[i].getType()})");
                }
                Console.WriteLine(ItemInHand.getname() == null ? "Nothing is in your hand" : $"Item in hand: {ItemInHand.getname()}  ({ItemInHand.getType()})");
                Console.WriteLine(ItemInHandother.getname() == null ? "Nothing is in your hand" : $"Item in other hand: {ItemInHandother.getname()} ({ItemInHandother.getType()})");
                Console.WriteLine(EquippedArmor.getname() == null ? "No armor equipped" : $"Equipped armor: {EquippedArmor.getname()}");
            }
        }

        // Additional class definitions (Enemy, Item, Weapon, Armor, etc.) remain the same
    





    public class Enemy//více nepřátel que
    {
        protected string Name { get; set; }
        protected int damage { get; set; }
        protected int defence { get; set; }

        public string getname()
        {
            return Name;
        }
        public int getdam()
        {
            return damage;
        }
        public int getdef()
        {
            return defence;
        }

        public Enemy(string enemy_name, int enemy_damage, int enemy_defence)
        {
            Name= enemy_name;
            damage=enemy_damage;
            defence=enemy_defence;
        }
        







    }

    public class Item
    {
        protected string? Name { get; set; }
        protected int ItemWeight { get; set; }
        protected string? Type { get; set; }
        protected int Damage { get; set; }
        protected int Defense { get; set; }
        public string? getname()
        {
            return Name;
        }
        public int getdam()
        {
            return Damage;
        }
        public int getdef()
        {
            return Defense;
        }
        public string? getType()
        {
            return Type;
        }
        public int getweight()
        {
            return ItemWeight;
        }
        public void setdam(int i)
        {
            Damage=i;
        }
        public void setdef(int i)
        {
            Defense=i;
        }

        public Item() {
            Name= null;
            ItemWeight = 404;
            Type = null;
            Damage = 404;
            Defense = 404;


        }



    }
    public class Game_Item : Item
    {

        public Game_Item(string item_name, int item_wei)
        {
            ItemWeight = item_wei;
            Name = item_name;
            Type = "item";

        }



    }



    public class two_handed_weapon : Item
    {
       
        public two_handed_weapon(int item_dmg, string item_name, int item_wei)
        {
            Damage = item_dmg;
            ItemWeight = item_wei;
            Name = item_name;
            Type = "two-weapon";
        }

    }

    public class one_handed_weapon : Item
    {

        public one_handed_weapon(int item_dmg, string item_name, int item_wei)
        {
            Damage = item_dmg;
            ItemWeight = item_wei;
            Name = item_name;
            Type = "one-weapon";
        }

    }



    public class Shield : Item
    {

        

        public Shield(int item_def, string item_name, int item_wei)
        {
            Defense = item_def;
            ItemWeight = item_wei;
            Name = item_name;
            Type = "shield";


        }




    }
    public class Armor : Item
    {

       

        public Armor(int item_def, string item_name, int item_wei)
        {
            Defense = item_def;
            ItemWeight = item_wei;
            Name = item_name;
            Type = "armor";

        }



    }



    public class Apple : Item
    {

        


        public Apple( string item_name, int item_wei)
        {
            ItemWeight = item_wei;
            Name = item_name;
            Type = "apple";

        }



    }


    public class Coins : Item
    {
        private int Copper { get; set; }
        private int Silver { get; set; }
        private int Gold { get; set; }

        public Coins()
        {
            Copper = 0;
            Silver = 0;
            Gold = 0;


        }


        private static int Calc_to_number(int copper_calc, int silver_calc, int gold_calc)
        {
            copper_calc = copper_calc + silver_calc * 10 + gold_calc * 100;



            return copper_calc;

        }

        public int RemoveMoney(int copper_rem, int silver_rem, int gold_rem)
        {
            int your_money = Calc_to_number(Copper, Silver, Gold);
            int to_rem_money = Calc_to_number(copper_rem, silver_rem, gold_rem);
            if (your_money > to_rem_money)
            {
                Console.WriteLine("money removed");
                your_money -= to_rem_money;
                Copper = your_money;
                Silver = 0;
                Gold = 0;
                CalculateMoney();
                return 1;

            }
            else
            {
                

                Console.WriteLine("Not enought money");
                return 0;

            }






        }

        public void MoneyInfo()
        {
            Console.WriteLine("you now have " + Copper + " copper");
            Console.WriteLine("you now have " + Silver + " silver");
            Console.WriteLine("you now have " + Gold + " gold");
        }


        public void CalculateMoney()
        {
            if (Copper >= 10)
            {
                Silver += (Copper - Copper % 10) / 10;
                Copper %= 10;


            }
            if (Silver >= 10)
            {
                Gold += (Silver - Silver % 10) / 10;
                Silver %= 10;


            }

        }
        public void AddMoney(int copper_add, int silver_add, int gold_add)
        {
            Copper += copper_add;
            Silver += silver_add;
            Gold += gold_add;
            Console.WriteLine("Money added");

            CalculateMoney();


        }








    }

    class Program
    {
       


        public static string GenerateName(int len)
        {
            Random r = new();
            string[] consonants = { "b", "c", "d", "f", "g", "h", "j", "k", "l", "m", "l", "n", "p", "q", "r", "s", "sh", "zh", "t", "v", "w", "x" };
            string[] vowels = { "a", "e", "i", "o", "u", "ae", "y" };
            string Name = "";
            Name += consonants[r.Next(consonants.Length)].ToUpper();
            Name += vowels[r.Next(vowels.Length)];
            int b = 2;
            while (b < len)
            {
                Name += consonants[r.Next(consonants.Length)];
                b++;
                Name += vowels[r.Next(vowels.Length)];
                b++;
            }

            return Name;


        }



        public static void Find_item_stage(Character yourcharacter)
        {
            Random random = new();
            Console.Clear();

            int itemtype = random.Next(1, 6);
            Item item = new Item();
            switch (itemtype)
            {
                case 1:
                    one_handed_weapon found_weapon = Character.CreateWeaponone((random.Next(1, 9)), GenerateName(5), (random.Next(2, 5)));
                    item = found_weapon;



                    break;
                case 2:
                    Shield found_shield = Character.CreateShield((random.Next(1, 9)), GenerateName(5), (random.Next(2, 5)));
                    item = found_shield;


                    break;
                case 3:
                    Game_Item found_item = Character.CreateItem(GenerateName(5), (random.Next(2, 5)));
                    item = found_item;


                    break;
                case 4:
                    Armor found_armor = Character.CreateArmor((random.Next(1, 9)), GenerateName(5), (random.Next(2, 5)));
                    item = found_armor;

                    break;
                case 5:
                    two_handed_weapon found_weapon2 = Character.CreateWeapontwo((random.Next(1, 9)), GenerateName(5), (random.Next(2, 5)));
                    item = found_weapon2;


                    break;
                default:
                    break;
            }


            Console.WriteLine("you found a " + item.getType());
            Console.WriteLine("with stats:");
            if (item.getdef()!= 404)
            {
                Console.WriteLine("Defence: " + item.getdef());
            }
            if (item.getdam() != 404)
            {
                Console.WriteLine("Damage: " + item.getdam());
            }
            Console.WriteLine("Name: " + item.getname());

            Console.WriteLine("weight: " + item.getType());



            Console.WriteLine("1 get item");
            Console.WriteLine("2 destroy item");

            int userchoice = Get_user_int_input(1, 2);
            switch (userchoice)
            {
                case 1:
                    yourcharacter.AddToInventory(item);
                    break;
                case 2:
                    Console.WriteLine("item destroyed");

                    break;
                default:
                    Console.WriteLine("invalid input");
                    break;
            }




        }
        public static void Find_enemy_stage(Character yourcharacter)
        {

            Random random = new();

            Console.Clear();
            Enemy enemy = new(GenerateName(5), random.Next(1, 15), random.Next(1, 15));
            Console.WriteLine(enemy.getname() +" Wants to fight you");
            Console.WriteLine("he has "+enemy.getdam()+" damage");
            Console.WriteLine("he has " + enemy.getdef() + " defence");


            Console.WriteLine("1. fight!!");
            Console.WriteLine("2. try to escape");
            int play_action = Get_user_int_input(1, 2);
            switch (play_action)
            {
                case 1:
                    Console.Clear();
                    yourcharacter.Fight(enemy);
                    break;
                case 2:
                    Console.Clear();
                    yourcharacter.Flee(enemy);
                    break;
                default:
                    Console.WriteLine("invalid input");
                    break;
            }



        }

        public static void Find_money_stage(Character yourcharacter)
        {

            Random random = new();
            Console.Clear();
            int coins =random.Next(2, 25);

            Console.WriteLine("you found money "+coins+" copper coins");
            yourcharacter.AddCoins(coins, 0, 0);
            yourcharacter.GetMoneyInfo();


        }
        public static void find_apple_stage(Character yourcharacter)
        {
            Console.Clear();
            Apple aple = new("apple", 1);
            Console.WriteLine("you found apple");
            Console.WriteLine("1 get item");
            Console.WriteLine("2 destroy item");

            int userchoice = Get_user_int_input(1, 2);
            switch (userchoice)
            {
                case 1:
                    yourcharacter.AddToInventory(aple);
                    break;
                case 2:
                    Console.WriteLine("item destroyed");

                    break;
                default:
                    Console.WriteLine("invalid input");
                    break;
            }



        }


        public static void pay_or_fight_stage(Character yourcharacter)
        {
            Random random = new();
            Console.Clear();
            int coins = random.Next(2, 8);
            Enemy enemy = new(GenerateName(8), random.Next(4, 20), random.Next(4, 20));
            Console.WriteLine("pay " + coins + " copper coins or fight");
            Console.WriteLine(enemy.getname() );
            Console.WriteLine("he has " + enemy.getdam() + " damage");
            Console.WriteLine("he has " + enemy.getdef() + " defence");


            Console.WriteLine("1. fight!!");
            Console.WriteLine("2. try to escape");
            Console.WriteLine("3. pay");

            int play_action = Get_user_int_input(1,3);
            switch (play_action)
            {
                case 1:
                    Console.Clear();
                    yourcharacter.Fight(enemy);
                    break;
                case 2:
                    Console.Clear();
                    yourcharacter.Flee(enemy);
                    break;
                case 3:
                    int result = yourcharacter.RemoveCoins(coins, 0, 0);
                    if (result == 1)
                    {
                        Console.WriteLine("you payed and he let you pass");

                    }
                    else
                    {
                        Console.WriteLine("you dont have enough money");
                        Console.WriteLine("1. fight!!");
                        Console.WriteLine("2. try to escape");
                         play_action = Get_user_int_input(1, 2);
                        switch (play_action)
                        {
                            case 1:
                                Console.Clear();
                                yourcharacter.Fight(enemy);
                                break;
                            case 2:
                                Console.Clear();
                                yourcharacter.Flee(enemy);
                                break;
                            default:
                                Console.WriteLine("invalid input");
                                break;
                        }

                    }
                    break;
                default:
                    Console.WriteLine("invalid input");
                    break;
            }


        }



        public static void generate_next_stage(Character yourcharacter)
        {
            Random random = new();
            int random_stage_id = random.Next(0, 6);
            switch (random_stage_id)
            {
                case 1:
                    Find_item_stage(yourcharacter);
                    break;
                case 2:
                    Find_enemy_stage(yourcharacter);
                    continue_anything();
                    break;
                case 3:
                    Find_money_stage(yourcharacter);
                    continue_anything();

                    break;
                case 4:
                    pay_or_fight_stage(yourcharacter);
                    continue_anything();

                    break;
                case 5:
                    find_apple_stage(yourcharacter);
                    continue_anything();

                    break;

                default:
                    // code block
                    break;
            }






        }



        

    public static int Get_user_int_input( int start=404,  int end=404)
        {

            
            try
            {
                int input = int.Parse(Get_user_input());
                if (start != 404)
                {
                    if ((input <= end) || (start >= input))
                    {
                        return input;


                    }
                    else
                    {
                        Console.WriteLine("wrong number");
                        Console.WriteLine("Try again");
                        return Get_user_int_input(start, end);
                    }

                }
                    
               

                    return input;
            }
            catch (Exception e)
            {
                Console.WriteLine("you must give a number");
                Console.WriteLine("Try again");
                return Get_user_int_input();

            }
        }

        public static void continue_anything(){
            Console.WriteLine("press anything to continue");
            Get_user_input();


        }
        public static string Get_user_input()
        {

            string? input = Console.ReadLine();
            if (input != null)
            {

                return input;
            }
            else
            {

                Console.WriteLine("Try again cannot use spaces");
                return Get_user_input();

            }




        }

        public static void open_inventory(Character yourCharacter)
        {
            bool show_inv = true;
            while (show_inv)
            {
                Console.Clear();
                yourCharacter.ViewInventory();
                yourCharacter.GetMoneyInfo();
                Console.WriteLine("1. delete item");
                Console.WriteLine("2. equip item");
                Console.WriteLine("3. item info");
                Console.WriteLine("4. unequip armor");
                Console.WriteLine("5. unequip weapon");
                Console.WriteLine("6. use item");
                Console.WriteLine("7. exit inventory");
                int action_inv = Get_user_int_input(1,7);
                int action_item;
                switch (action_inv)
                {
                    case 1:



                        Console.Clear();

                        Console.WriteLine("write index of item you want to destroy");
                        action_item = Get_user_int_input();

                        try
                        {
                            Console.Clear();
                            yourCharacter.RemoveItem(action_item);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("item not found");
                        }
                        continue_anything();


                        break;
                    case 2:
                        Console.Clear();

                        Console.WriteLine("write index of item you want to equip");
                        action_item = Get_user_int_input();


                        try
                        {
                            Console.Clear();
                            yourCharacter.EquipItem(action_item);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("item not found");

                        }
                        continue_anything();

                        break;


                    case 3:
                        Console.Clear();
                        Console.WriteLine("write index of item you want to see stats");
                        action_item = Get_user_int_input();
                        try
                        {
                            Console.Clear();

                            yourCharacter.ShowItemStats(action_item);
                        }
                        catch (Exception e)
                        {
                            Console.Clear();
                            Console.WriteLine("item not found");
                        }
                       
                        continue_anything();

                        break;
                    case 4:
                        Console.Clear();
                        yourCharacter.UnequipArmor();
                        continue_anything();


                        break;
                    case 5:
                        Console.Clear();
                        yourCharacter.UnequipWeapon();
                        continue_anything();


                        break;
                    case 6:
                        Console.Clear();
                        Console.WriteLine("write index of item you want to use");
                        action_item = Get_user_int_input();
                        try
                        {
                            yourCharacter.use_item(action_item);
                        }
                        catch (Exception e)
                        {
                            Console.Clear();
                            Console.WriteLine("item not found");
                        }
                        continue_anything();


                        break;
                    case 7:
                        Console.Clear();
                        show_inv = false;
                        break;
                    default:
                        break;
                }

            }


        }



        static void Main()
        {
            Console.WriteLine("Enter your characters name:");


            string characterName = Get_user_input();

            Character yourCharacter = new(characterName);




            bool game_on=false;
            int action;
            bool game_is_on = true;
            while (game_is_on)
            {
                Console.Clear();
                Console.WriteLine("Dev Menu");
                Console.WriteLine("1. inventory");
                Console.WriteLine("2. Money");
                Console.WriteLine("3. Stats");
                Console.WriteLine("4. Items");
                Console.WriteLine("5. Enemies");
                Console.WriteLine("6. Start adventure");

                action = Get_user_int_input(1,6);


                Console.Clear();
                switch (action)
                {
                    case 1:

                        Console.WriteLine("Your inventory");

                        open_inventory(yourCharacter);




                        break;
                    case 2:
                        Console.Clear();
                        Console.WriteLine("1. add coins");
                        Console.WriteLine("2. remove coins");
                        Console.WriteLine("3.coins info");
                        int action_money = Get_user_int_input(1,3);
                        switch (action_money)
                        {
                            case 1:
                                Console.Clear();
                                Console.WriteLine("How many copper coins you want to add");
                                int copper_add = Get_user_int_input();
                                Console.WriteLine("How many silver coins you want to add");
                                int silver_add = Get_user_int_input();
                                Console.WriteLine("How many gold coins you want to add");
                                int gold_add = Get_user_int_input();
                                yourCharacter.AddCoins(copper_add, silver_add, gold_add);
                                continue_anything();
                                
                                break;
                            case 2:
                                Console.Clear();
                                Console.WriteLine("How many copper coins you want to remove");
                                int copper_rem = Get_user_int_input();
                                Console.WriteLine("How many silver coins you want to remove");
                                int silver_rem = Get_user_int_input();
                                Console.WriteLine("How many gold coins you want to remove");
                                int gold_rem = Get_user_int_input();
                                yourCharacter.RemoveCoins(copper_rem, silver_rem, gold_rem);
                                continue_anything();


                                break;
                            case 3:
                                Console.Clear();
                                yourCharacter.GetMoneyInfo();

                                break;


                            default:

                                break;
                        }



                        break;

                    case 3:
                        yourCharacter.CharacterStats();
                        continue_anything();
                        break;


                    case 4:
                        Console.Clear();
                        Console.WriteLine("1. create one handed weapon");
                        Console.WriteLine("2. create armor");
                        Console.WriteLine("3. create shield");
                        Console.WriteLine("4. create item");
                        Console.WriteLine("5. create two handed weapon");
                        int new_item_action = Get_user_int_input(1,5);
                        switch (new_item_action)
                        {
                            case 1:
                                Console.Clear();
                                Console.WriteLine("weapon damage");
                                int weapon_damage = Get_user_int_input();
                                Console.WriteLine("weapon name");
                                string weapon_name = Get_user_input();
                                Console.WriteLine("weapon weight");
                                int weapon_weight = Get_user_int_input();
                                one_handed_weapon weapon_created = Character.CreateWeaponone(weapon_damage, weapon_name, weapon_weight);
                                yourCharacter.AddToInventory(weapon_created);
                                continue_anything();

                                break;
                            case 2:
                                Console.Clear();
                                Console.WriteLine("armor defence");
                                int armor_defence = Get_user_int_input();
                                Console.WriteLine("armor name");
                                string armor_name = Get_user_input();
                                Console.WriteLine("armor weight");
                                int armor_weight = Get_user_int_input();
                                Armor armor_created = Character.CreateArmor(armor_defence, armor_name, armor_weight);
                                yourCharacter.AddToInventory(armor_created);
                                continue_anything();

                                break;
                            case 3:
                                Console.Clear();
                                Console.WriteLine("armor defence");
                                int shield_defence = Get_user_int_input();
                                Console.WriteLine("armor name");
                                string shield_name = Get_user_input();
                                Console.WriteLine("armor weight");
                                int shield_weight = Get_user_int_input();
                                Shield shield_created = Character.CreateShield(shield_defence, shield_name, shield_weight);
                                yourCharacter.AddToInventory(shield_created);
                                continue_anything();

                                break;
                            case 4:
                                Console.Clear();

                                Console.WriteLine("armor name");
                                string item_name = Get_user_input();
                                Console.WriteLine("armor weight");
                                int item_weight = Get_user_int_input();
                                Game_Item item_created = Character.CreateItem(item_name, item_weight);
                                yourCharacter.AddToInventory(item_created);
                                continue_anything();

                                break;


                            case 5:
                                Console.Clear();
                                Console.WriteLine("weapon damage");
                                int weapon_damage2 = Get_user_int_input();
                                Console.WriteLine("weapon name");
                                string weapon_name2 = Get_user_input();
                                Console.WriteLine("weapon weight");
                                int weapon_weight2 = Get_user_int_input();
                                two_handed_weapon weapon_created2 = Character.CreateWeapontwo(weapon_damage2, weapon_name2, weapon_weight2);
                                yourCharacter.AddToInventory(weapon_created2);
                                continue_anything();

                                break;


                            default:

                                break;
                        }


                        break;

                    case 5:
                        Console.Clear();
                        Console.WriteLine("1. spawn 1 enemy");
                        Console.WriteLine("2. spawn multiple enemyes");
                        string Enemy_name;
                        int Enemy_damage;
                        int Enemy_defence;
                        int enemy_action = Get_user_int_input(1,2);

                        switch (enemy_action)
                        {
                            case 1:

                                Console.Clear();
                                Console.WriteLine("Enemy name");
                                Enemy_name = Get_user_input();
                                Console.WriteLine("Enemy damage");
                                Enemy_damage = Get_user_int_input();
                                Console.WriteLine("Enemy defence");
                                Enemy_defence = Get_user_int_input();
                                Enemy enemy = new(Enemy_name, Enemy_damage, Enemy_defence);


                                Console.WriteLine("1. fight!!");
                                Console.WriteLine("2. try to escape");
                                int play_action = Get_user_int_input(1,2);
                                switch (play_action)
                                {
                                    case 1:
                                        Console.Clear();
                                        yourCharacter.Fight(enemy);
                                        break;
                                    case 2:
                                        Console.Clear();
                                        yourCharacter.Flee(enemy);
                                        break;
                                    default:
                                        Console.WriteLine("invalid input");
                                        break;
                                }

                                continue_anything();

                                break;
                            case 2:
                                Console.Clear();
                                Console.WriteLine("how many enemyes to fight");
                                int enemyes_count = Get_user_int_input();
                                Queue<Enemy> myQueue = new Queue<Enemy>();
                                for (int i = 0; i < enemyes_count; i++)
                                {
                                    Console.Clear();
                                    Console.WriteLine("Enemy name");
                                    Enemy_name = Get_user_input();

                                    Console.WriteLine("Enemy damage");
                                    Enemy_damage = Get_user_int_input();

                                    Console.WriteLine("Enemy defence");
                                    Enemy_defence = Get_user_int_input();
                                    Enemy enemy_que = new(Enemy_name, Enemy_damage, Enemy_defence);
                                    myQueue.Enqueue(enemy_que);
                                  

                                }

                                while (true)
                                {
                                    try
                                    {
                                        Enemy enemy_to_fight = myQueue.Dequeue();

                                        Console.WriteLine(enemy_to_fight.getname() + " want to fight you");
                                        Console.WriteLine("1. fight!!");
                                        Console.WriteLine("2. try to escape");
                                        int play_action2 = Get_user_int_input(1, 2);
                                        switch (play_action2)
                                        {
                                            case 1:

                                                Console.Clear();
                                                yourCharacter.Fight(enemy_to_fight);
                                                break;
                                            case 2:
                                                Console.Clear();
                                                yourCharacter.Flee(enemy_to_fight);
                                                break;
                                            default:
                                                Console.WriteLine("invalid input");
                                                break;
                                        }
                                    }
                                    catch (Exception e)
                                    {
                                        Console.WriteLine("you killed them all");
                                        continue_anything();
                                        break;
                                    }

                                 



                                }





                                break;
                            default:
                                Console.WriteLine("invalid input");

                                break;
                        }





                        break;
                        case 6:
                        game_on = true;
                        game_is_on = false;
                        
                        break;




                    default:
                        Console.WriteLine("nothing");
                        break;
                }

            }


            //CharacterStats(yourCharacter);
            Console.WriteLine("Every time you go to Next stage");
            Console.WriteLine("There can be anything from enemyes");
            Console.WriteLine("To items...");
            continue_anything();
            while (game_on)
            {
                Console.Clear();
                Console.WriteLine("1. inventory");
                Console.WriteLine("2. stats");
                Console.WriteLine("3. next stage");
                int Game_action = Get_user_int_input(1,3);
                switch (Game_action)
                {
                    case 1:
                        open_inventory(yourCharacter);
                        break;
                    case 2:
                        Console.Clear();
                        yourCharacter.CharacterStats();
                        continue_anything();
                        break;
                    case 3:
                        generate_next_stage(yourCharacter);



                        break;
                    default:
                        Console.WriteLine("invalid input");
                        break;
                }





            }
        }
        

    }
}