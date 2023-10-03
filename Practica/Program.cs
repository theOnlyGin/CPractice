using System;
using System.Collections.Generic;

namespace practica
{
    class Army
    {
        public List<Unit> units;
        int totalHealth;
        int totalDamage;
        int totalDefense;
        public Army(int countUnits, int totalDefense)
        {
            units = new List<Unit>(countUnits);
            this.totalDefense = totalDefense;
        }
        public void info() 
        {
            totalHealth = 0;
            totalDamage = 0;
            for (int i = 0; i < units.Count; i++)
            {
                totalHealth += units[i].health;
                totalDamage += units[i].damage; 
            }
            Console.WriteLine($"\n\tВсего в армии войск--{units.Count}    общее здоровье--{totalHealth}     общий урон--{totalDamage}     общая защита--{totalDefense}");
        }
        public void attack(int damage)
        {
            if (totalDefense > 0)
            {
                int tmp = totalDefense;
                totalDefense -= damage;
                if (totalDefense > 0)
                {
                    Console.WriteLine("\n\tОбщая защита армии выдержала атаку");
                }
                else
                {
                    damage -= tmp;
                    totalDefense = 0;
                    Console.WriteLine("\n\tЗащита армии пала");
                }
            }
            if (totalDefense <= 0)
            {
                for (int i = 0; i < units.Count; i++)
                {
                    int tmp = units[i].health - damage;
                    if (tmp > 0) // юнит выдерживает атаку
                    {
                        units[i].health -= damage;
                        Console.WriteLine($"\tЮнит {units[i].UnitType} выдержал атаку, остаток здоровья--{units[i].health}");
                        break;
                    }
                    else // юнит не выдерживает атаку.
                    {
                        Console.WriteLine($"\tЮнит {units[i].UnitType} погиб");
                        damage -= units[i].health;
                        units.Remove(units[i]);
                        i--;
                    }
                }
            }
        }
    }
    abstract class Unit // Абстрактный класс Юнитов.
    {
        public int health;
        public int damage;
        public int defense;
        public string UnitType;
        public abstract void info();
        public abstract void attack();
    }
    class Mechanism : Unit
    {
        public string typeMech;
        public Ammo ammo;
        public Mechanism(string typeMech, int health, Ammo ammo, string unitType)
        {
            this.UnitType = unitType;
            this.typeMech = typeMech;
            this.health = health;
            this.ammo = ammo;
            this.damage = ammo.damage;
            this.defense = 0;
        }
        public override void info()
        {
            Console.WriteLine($"\n\tЯ - {UnitType}. Здоровье - {health}, защита - {defense}, тип боеприпасов - {ammo.name}, урон - {ammo.damage}");
        }
        public override void attack()
        {
            if (ammo.count > 0)
            {
                ammo.count--;
                Console.WriteLine($"\n\t{UnitType} нанес {ammo.damage} урона. {ammo.count} боеприпасов осталось");
            }
            else
            {
                Console.WriteLine("\n\tНет боеприпасов");
            }
        }
    }
    class Animal : Unit
    {
        public int speed;

        public Animal(string unitType, int health, int speed, int damage)
        {
            this.UnitType = unitType;
            this.health = health;
            this.speed = speed;
            this.damage = damage;
            this.defense = 0;
        }
        public override void info()
        {
            Console.WriteLine($"\n\tЯ - {UnitType}. Здоровье - {health}, защита - {defense}, урон - {damage}, скорость - {speed}");
        }
        public override void attack()
        {
            Console.WriteLine($"\n\t{UnitType} нанес {damage} урона");
        }
    }
    class Soldier : Unit
    {
        public Armor armor;
        public Soldier(string unitType, int health, int damage, Armor armor)
        {
            this.UnitType = unitType;
            this.armor = armor;
            this.health = health;
            this.damage = damage;
            this.defense = armor.defence;
        }
        public override void info()
        {
            Console.WriteLine($"\n\tЯ - {UnitType}. Здоровье - {health}, урон - {damage}, тип доспехов - {armor.name}, защита доспехов - {armor.defence}");
        }
        public override void attack()
        {
            Console.WriteLine($"\n\t{UnitType} нанес {damage} урона");
        }
    }
    class Knight : Unit
    {
        public Soldier soldier;
        public Animal rideAnimal;
        public Knight(string unitType, Soldier soldier, Animal rideAnimal)
        {
            UnitType = unitType;
            this.soldier = soldier;
            this.rideAnimal = rideAnimal;
            health = soldier.health + rideAnimal.health;
            damage = soldier.damage + rideAnimal.damage;
            defense = soldier.defense;
        }
        public override void info()
        {
            Console.WriteLine($"\n\tЯ - {UnitType}. Здоровье - {health}, защита - {defense}, урон - {damage}, eздовое животное - {rideAnimal.UnitType}");
        }
        public override void attack()
        {
            Console.WriteLine($"\n\t{soldier.UnitType} и {rideAnimal.UnitType} нанесли {damage} урона");
        }
    }
    class Ammo
    {
        public string name;
        public int damage;
        public int count;
        public Ammo(string name, int damage, int count)
        {
            this.name = name;
            this.damage = damage;
            this.count = count;
        }
    }
    class Armor
    {
        public int defence;
        public string name;
        public Armor(int defence, string name)
        {
            this.defence = defence;
            this.name = name;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Execute(Initialization());
        }
        static Army Initialization()
        {
            int unitsCount = 8;
            Army army = new Army(unitsCount, 300);
            army.units.Add(new Mechanism("ballista", 745, new Ammo("Astral Sonic Blast", 26, 10), "mechanism"));// в билете нет кол-ва боеприпасов, оно взято случайным 
            army.units.Add(new Soldier("Rogue", 65, 9, new Armor(50, "Steel Armor")));
            army.units.Add(new Animal("Elephant", 104, 11, 50));
            army.units.Add(new Knight("Knight #1", new Soldier("Rogue", 65, 9, new Armor(50, "Steel Armor")), new Animal("Elephant", 104, 11, 50)));
            army.units.Add(new Knight("Knight #2", new Soldier("Rogue", 65, 9, new Armor(50, "Steel Armor")), new Animal("Elephant", 104, 11, 50)));
            army.units.Add(new Knight("Knight #3", new Soldier("Rogue", 65, 9, new Armor(50, "Steel Armor")), new Animal("Elephant", 104, 11, 50)));
            army.units.Add(new Knight("Knight #4", new Soldier("Rogue", 65, 9, new Armor(50, "Steel Armor")), new Animal("Elephant", 104, 11, 50)));
            army.units.Add(new Knight("Knight #5", new Soldier("Rogue", 65, 9, new Armor(50, "Steel Armor")), new Animal("Elephant", 104, 11, 50)));
            return army;
        }
        static void Execute(Army army)
        {
            string separator = "-------------------";
            bool isExecute = true;
            while (isExecute)
            {
                if (army.units.Count == 0) { Console.WriteLine("\n\tАрмия пала!\n"); break; }
                switch (ReadValues("\n" + separator + "\n\tИнформация о целом войске--1    \tПровести атаку над целым войском--2" +
                    "\n\tДействие над определенным юнитом--3     Выйти из программы--4\n\tВаш выбор--"))
                {
                    case 1:
                        army.info();
                        break;
                    case 2:
                        army.attack(ReadValues("\n\tВведите количество наносимого армии урона--"));
                        army.info();
                        break;
                    case 3:
                        for (int i = 0; i < army.units.Count; i++)
                        {
                            Console.Write("\n\t" + (i + 1) + $"--{army.units[i].UnitType}");
                        }
                        int index = ReadValues("\n\n\tНомер юнита--") - 1;
                        switch (ReadValues($"Я - {army.units[index].UnitType}. Могу дать информацию--1 или атаковать--2\n\tВаш выбор--"))
                        {
                            case 1:
                                army.units[index].info();
                                break;
                            case 2:
                                army.units[index].attack();
                                break;
                            default:
                                break;
                        }
                        break;
                    case 4:
                        isExecute = false;
                        break;
                    default:
                        break;
                }
            }
        }
        static int ReadValues(string message)
        {
            Console.Write("\t" + message);
            return Convert.ToInt32(Console.ReadLine());
        }
    }
}
