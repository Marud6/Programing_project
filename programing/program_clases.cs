using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace programing
{
    public class Character
    {
        private string Username { get; set; }
        private int Strength { get; set; }
        private int Agility { get; set; }
        private int Capacity { get; set; }
        private int ItemsWeight { get; set; }
        public int CurrentHp { get; set; }
        private bool IsAlive { get; set; }
        private int Defence { get; set; }
        private int MaxHp { get; set; }
        private List<Item> Inventory { get; set; }
        private Item ItemInHand { get; set; }
        private Item ItemInHandother { get; set; }

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


        public int CharacterStats()
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
            if (item.getType() == "apple")
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

            int yourPowerIndex = CalculatePowerIndex((Defence + get_addition_def()), (Strength + get_addition_dam()));
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
            if (index < 0 || index >= Inventory.Count) { Console.WriteLine("Invalid inventory index."); return 0; }



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
                ItemInHandother = item;
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
                else if (ItemInHandother.getname() == null)
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
            if (ItemInHandother.getType() == "two-weapon")
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
            Name = enemy_name;
            damage = enemy_damage;
            defence = enemy_defence;
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
            Damage = i;
        }
        public void setdef(int i)
        {
            Defense = i;
        }

        public Item()
        {
            Name = null;
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




        public Apple(string item_name, int item_wei)
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

}
