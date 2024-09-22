
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Security.Cryptography;
    using static System.Net.Mime.MediaTypeNames;

    namespace programing
    {
      



   

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
                                int copper_add = Get_user_int_input(0,1000);
                                Console.WriteLine("How many silver coins you want to add");
                                int silver_add = Get_user_int_input(0, 1000);
                                Console.WriteLine("How many gold coins you want to add");
                                int gold_add = Get_user_int_input(0,1000);
                                yourCharacter.AddCoins(copper_add, silver_add, gold_add);
                                continue_anything();
                                
                                break;
                            case 2:
                                Console.Clear();
                                Console.WriteLine("How many copper coins you want to remove");
                                int copper_rem = Get_user_int_input(0, 1000);
                                Console.WriteLine("How many silver coins you want to remove");
                                int silver_rem = Get_user_int_input(0,1000);
                                Console.WriteLine("How many gold coins you want to remove");
                                int gold_rem = Get_user_int_input(0, 1000);
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
                                int weapon_damage = Get_user_int_input(0, 1000);
                                Console.WriteLine("weapon name");
                                string weapon_name = Get_user_input();
                                Console.WriteLine("weapon weight");
                                int weapon_weight = Get_user_int_input(0, 1000);
                                one_handed_weapon weapon_created = Character.CreateWeaponone(weapon_damage, weapon_name, weapon_weight);
                                yourCharacter.AddToInventory(weapon_created);
                                continue_anything();

                                break;
                            case 2:
                                Console.Clear();
                                Console.WriteLine("armor defence");
                                int armor_defence = Get_user_int_input(0, 1000);
                                Console.WriteLine("armor name");
                                string armor_name = Get_user_input();
                                Console.WriteLine("armor weight");
                                int armor_weight = Get_user_int_input(0,1000);
                                Armor armor_created = Character.CreateArmor(armor_defence, armor_name, armor_weight);
                                yourCharacter.AddToInventory(armor_created);
                                continue_anything();

                                break;
                            case 3:
                                Console.Clear();
                                Console.WriteLine("armor defence");
                                int shield_defence = Get_user_int_input(0, 1000);
                                Console.WriteLine("armor name");
                                string shield_name = Get_user_input();
                                Console.WriteLine("armor weight");
                                int shield_weight = Get_user_int_input(0, 1000);
                                Shield shield_created = Character.CreateShield(shield_defence, shield_name, shield_weight);
                                yourCharacter.AddToInventory(shield_created);
                                continue_anything();

                                break;
                            case 4:
                                Console.Clear();

                                Console.WriteLine("armor name");
                                string item_name = Get_user_input();
                                Console.WriteLine("armor weight");
                                int item_weight = Get_user_int_input(0, 1000);
                                Game_Item item_created = Character.CreateItem(item_name, item_weight);
                                yourCharacter.AddToInventory(item_created);
                                continue_anything();

                                break;


                            case 5:
                                Console.Clear();
                                Console.WriteLine("weapon damage");
                                int weapon_damage2 = Get_user_int_input(0, 1000);
                                Console.WriteLine("weapon name");
                                string weapon_name2 = Get_user_input();
                                Console.WriteLine("weapon weight");
                                int weapon_weight2 = Get_user_int_input(0, 1000);
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
                                Enemy_damage = Get_user_int_input(0, 1000);
                                Console.WriteLine("Enemy defence");
                                Enemy_defence = Get_user_int_input(0, 1000);
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
                                int enemyes_count = Get_user_int_input(0, 1000);
                                Queue<Enemy> myQueue = new Queue<Enemy>();
                                for (int i = 0; i < enemyes_count; i++)
                                {
                                    Console.Clear();
                                    Console.WriteLine("Enemy name");
                                    Enemy_name = Get_user_input();

                                    Console.WriteLine("Enemy damage");
                                    Enemy_damage = Get_user_int_input(0,1000);

                                    Console.WriteLine("Enemy defence");
                                    Enemy_defence = Get_user_int_input(0,1000);
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