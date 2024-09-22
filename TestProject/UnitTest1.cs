using programing;
namespace TestProject
{
    public class Tests
    {
        public Character character;


        [SetUp]
        public void Setup()
        {
            character = new("sad");
        }

        [Test]
        public void weapon_add_stats()
        {

            one_handed_weapon weapon = Character.CreateWeaponone(1, "asdf", 10);
      
            character.AddToInventory(weapon);


            character.EquipItem(0);
            int i = character.get_addition_dam();
          


            Assert.AreEqual(i,weapon.getdam());


        }
        [Test]
        public void defence_tes()
        {

            Shield shield = Character.CreateShield(1, "asdf", 10);
            Armor armor = Character.CreateArmor(1, "asdf", 10);

            character.AddToInventory(shield);
            character.AddToInventory(armor);

            character.EquipItem(0);
            character.EquipItem(0);
            int a = character.get_addition_def();

            int totaldef = armor.getdef() + shield.getdef();


            Assert.AreEqual(a, totaldef);


        }
        [Test]
        public void test_use()
        {
            Game_Item item = new("dsaf", 5);
            Apple apple = new("asdsdf",1);
            character.AddToInventory(item);
            character.AddToInventory(apple);
            int i=character.use_item(0);
            int a = character.use_item(1);

            Assert.AreEqual(0, i);
            Assert.AreEqual(0, a);
            character.CurrentHp = character.CurrentHp-3;
            a = character.use_item(1);
            Assert.AreEqual(1, a);

        }
        [Test]

        public void test_inventory()
        {
            Shield shield = Character.CreateShield(1, "asdf", 3);
            Shield shield2 = Character.CreateShield(1, "asdf", 80);
             int a=character.AddToInventory(shield);
            int i = character.AddToInventory(shield2);

            Assert.AreEqual(1, a);
            Assert.AreEqual(0, i);
           a= character.RemoveItem(0);
           i= character.RemoveItem(0);

            Assert.AreEqual(1, a);
            Assert.AreEqual(0, i);

        



        }
        [Test]
        public void test_inventory_equip()
        {
            Shield shield = Character.CreateShield(1, "asdf", 3);
            Shield shield2 = Character.CreateShield(1, "asdf", 3);
            one_handed_weapon one_w = Character.CreateWeaponone(1, "asdf", 3);
            two_handed_weapon two_w = Character.CreateWeapontwo(1, "asdf", 3);
       

             character.AddToInventory(shield);
            character.AddToInventory(shield2);


            int a = character.EquipItem(0);
            int i = character.EquipItem(0);

            Assert.AreEqual(1, a);
            Assert.AreEqual(1, i);

            character.UnequipWeapon();
            character.RemoveItem(0);
            character.RemoveItem(0);

            character.AddToInventory(one_w);
            character.AddToInventory(two_w);
             a = character.EquipItem(0);
             i = character.EquipItem(0);
            Assert.AreEqual(1, a);
            Assert.AreEqual(0, i);
            character.UnequipWeapon();
            a = character.EquipItem(0);
            i = character.EquipItem(0);
            Assert.AreEqual(1, a);
            Assert.AreEqual(0, i);

        }

        [Test]
        public void test_stats()
        {
            Game_Item item = new("dsaf", 5);
            Apple apple = new("asdsdf", 1);
            character.AddToInventory(item);
            character.AddToInventory(apple);
            int i = character.ShowItemStats(0);
            int a = character.ShowItemStats(2);

            Assert.AreEqual(1, i);
            Assert.AreEqual(0, a);
            

        }
        [Test]
        public void casual_things_test()
        {
            int a = character.CharacterStats();

            Assert.That(1, Is.EqualTo(a));
             a = character.ViewInventory();
            Assert.AreEqual(1, a);


        }



        [Test]
        public void unequip_weapon()
        {

            one_handed_weapon one_w = Character.CreateWeaponone(1, "asdf", 3);
            two_handed_weapon two_w = Character.CreateWeapontwo(1, "asdf", 3);

            character.AddToInventory(one_w);
            character.AddToInventory(two_w);
            character.EquipItem(0);
            int a = character.UnequipWeapon();
            int i = character.UnequipWeapon();

            Assert.AreEqual(0, i);
            Assert.AreEqual(1, a);

            character.EquipItem(0);
            a = character.UnequipWeapon();

            i = character.UnequipWeapon();

            Assert.AreEqual(1, a);
            Assert.AreEqual(0, i);
           








        }

        [Test]
        public void unequip_armor()
        {
            Armor armor = Character.CreateArmor(1, "asdf", 3);
            Armor armor2 = Character.CreateArmor(3, "hjk", 2);


            character.AddToInventory(armor);
            character.AddToInventory(armor2);
            character.EquipItem(0);

            int a = character.UnequipArmor();
            int i = character.UnequipArmor();

            Assert.AreEqual(0, i);
            Assert.AreEqual(1, a);

            character.EquipItem(0);

             a = character.UnequipArmor();
             i = character.UnequipArmor();

            Assert.AreEqual(0, i);
            Assert.AreEqual(1, a);
        }



        [Test]
        public void money()
        { 
            character.GetMoneyInfo();
            character.AddCoins(10, 0, 0);

          int  a = character.RemoveCoins(10,0,0);
           int i = character.RemoveCoins(0,1,0);
            Assert.AreEqual(0, i);
            Assert.AreEqual(1, a);

        }


        [Test]
        public void create_invalid_items()
        {
            


            Character.CreateItem("adfs" ,- 5);
            Character.CreateArmor(-5, "afds", -5);
            Character.CreateWeaponone(-5, "afds", -5);
            Character.CreateWeapontwo(-5, "afds", -5);
            Character.CreateShield(-5, "afds", -5);
            Assert.Pass();




        }







    }
}