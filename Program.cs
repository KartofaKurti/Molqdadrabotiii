using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;

namespace TukaSheBachkam
{
    public class Bats
    {
        public static string Name = "Bats";
        public static int Hp = 14;
        public static int Dmg;
        public static int RewardGold = 4;
        public static int RewardMana;

        public static int Dmg_
        {
            get => Dmg;
            set => Dmg = value;
        }
    }

    public class Wolves
    {
        public static string Name = "Wolves";
        public static int Hp = 24;
        public static int Dmg;
        public static int RewardGold = 6;
        public static int RewardMana;

        public static int Dmg_
        {
            get => Dmg;
            set => Dmg = value;
        }
    }

    public class Dragons
    {
        public static string Name = "Dragons";
        public static int Hp = 75;
        public static int Dmg;
        public static double RewardDmgMultiplier = 1.6;
        public static int RewardMana;

        public static int Dmg_
        {
            get => Dmg;
            set => Dmg = value;
        }
    }

    public class Goblins
    {
        public static string Name = "Goblins";
        public static int Hp = 29;
        public static int Dmg;
        public static int RewardGold = 9;
        public static int RewardMana;

        public static int Dmg_
        {
            get => Dmg;
            set => Dmg = value;
        }
    }

    public class RiftHerald
    {
        public static string Name = "RiftHerald";
        public static double Hp = 95;
        public static double Dmg;
        public static double RewardBonusHp = 35;
        public static double RewardMana;

        public static double Dmg_
        {
            get => Dmg;
            set => Dmg = value;
        }
    }

    public class BaronNashor
    {
        public static string Name = "BaronNashor";
        public static double Hp = 160;
        public static double Dmg;
        public static double Dmg_
        {
            get => Dmg;
            set => Dmg = value;
        }
    }

    public class Character
    {
        private string Name { get; set; }
        public double Hp { get; set; }
        public double DMG { get; set; }
        public int Mana { get; set; }
        public string AbilityOne { get; set; }
        public string AbilityTwo { get; set; }
        public int AbilityOneCost { get; set; }
        public int AbilityTwoCost { get; set; }
        public double AbilityOneDmg { get; set; }
        public double AbilityTwoDmg { get; set; }
        public Dictionary<string, int> Inventory { get; set; }

        public Character(double hp, double dMG, int mana,
            string abilityOne, string abilityTwo,
            int abilityOneCost, int abilityTwoCost,
            double abilityOneDmg, double abilityTwoDmg)
        {
            Name = "Kartof";
            Hp = hp;
            DMG = dMG;
            Mana = mana;
            AbilityOne = abilityOne;
            AbilityTwo = abilityTwo;
            AbilityOneCost = abilityOneCost;
            AbilityTwoCost = abilityTwoCost;
            AbilityOneDmg = abilityOneDmg;
            AbilityTwoDmg = abilityTwoDmg;
            Inventory = new Dictionary<string, int>();
        }

        internal class Program
        {
            private const double DefaultHp = 100;
            private const double DefaultMana = 100;
            private static double playerGold = 0;
            private static string[] availableChampions = { "Viego", "Ahri", "Jinx" };
            private static int currentLevel = 1;
            private static Character playerCharacter;

            static void Main(string[] args)
            {
                Console.WriteLine("Welcome \nSelect your character");
                Console.WriteLine("\\Available Champions/ \n:Viego - 1\n:Ahri - 2\n:Jinx - 3");

                var playerInput = ChampSelection();
                while (!availableChampions.Contains(playerInput))
                {
                    Console.WriteLine("Choose another champion!!!");
                    playerInput = ChampSelection();
                }

                switch (playerInput)
                {
                    case "Viego":
                        playerCharacter = new Character(DefaultHp, 10, 70, "Blade of the Ruined King", "Spectral Maw", 13,
                            25, 9, 19);
                        Console.WriteLine("Great Choice");
                        break;
                    case "Ahri":
                        playerCharacter = new Character(DefaultHp, 12, 80, "Orb of Deception", "Charm", 28, 12, 20, 10);
                        Console.WriteLine("Great Choice");
                        break;
                    case "Jinx":
                        playerCharacter = new Character(DefaultHp, 8, 60, "Switchero!", "Zap!", 25, 14, 6, 10);
                        Console.WriteLine("Great Choice");
                        break;
                }

                while (currentLevel != null && playerCharacter.Hp > 0)
                {
                    int baronDeaths = 0;
                    if (BaronIsDeteated((baronDeaths)))
                    {
                        Console.WriteLine("Congrats homie!");
                        Environment.Exit(0);
                    }
                    if (CharacterDied())
                    {
                        Console.WriteLine("Game Over");
                        break;
                    }
                    Console.WriteLine($"Welcome to level {currentLevel}.");
                    Console.WriteLine($"You are facing {GetEnemyName(currentLevel)}!");

                    
                    double enemyHp = GetEnemyHp(currentLevel);
                    while (enemyHp > 0 && playerCharacter.Hp > 0)
                    {
                        if (currentLevel == 5)
                        {
                            MiniBossFight();
                        }
                        if (currentLevel == 6)
                        {
                            BossFight(baronDeaths);
                        }

                        var playerMove = PlayerMove().ToLower();
                        switch (playerMove)
                        {
                            case "q":
                                Console.WriteLine($"You used {playerCharacter.AbilityOne} - {playerCharacter.AbilityOneCost}");
                                Console.WriteLine($"You dealt {playerCharacter.AbilityOneDmg}");
                                enemyHp -= playerCharacter.AbilityOneDmg;
                                break;
                            case "w":
                                Console.WriteLine($"You used {playerCharacter.AbilityTwo}");
                                Console.WriteLine($"You dealt {playerCharacter.AbilityTwoDmg}");
                                enemyHp -= playerCharacter.AbilityTwoDmg;
                                break;
                            case "shop":
                                OpenShop();
                                continue;
                            case "inventory":
                                OpenInventory();
                                continue;
                            default:
                                Console.WriteLine("Invalid move. Try again.");
                                continue;
                        }

                        if (enemyHp <= 0)
                        {
                            Console.WriteLine("The enemy is defeated");
                            playerGold += GetEnemyRewardGold(currentLevel);
                            RewardMana(GetEnemyName(currentLevel));
                            playerCharacter.Mana += GetEnemyRewardMana(currentLevel);
                            PassedLevel(GetEnemyRewardGold(currentLevel), GetEnemyRewardMana(currentLevel),
                                playerCharacter.Hp);
                            if (GetEnemyName(currentLevel) == "Dragons")
                            {
                                Console.WriteLine("Choose the ability you want to upgrade!");
                                var abilityToUpgrade = PlayerMove().ToLower();
                                if (abilityToUpgrade == "q")
                                {
                                    playerCharacter.AbilityOneDmg *= Dragons.RewardDmgMultiplier;
                                }
                                else if (abilityToUpgrade == "w")
                                {
                                    playerCharacter.AbilityTwoDmg *= Dragons.RewardDmgMultiplier;
                                }
                            }
                        }
                        else
                        {
                            MonsterDamage(GetEnemyName(currentLevel));
                            playerCharacter.Hp -= GetEnemyDamage(currentLevel);
                            if (!CharacterDied())
                                Console.WriteLine($"You survived a hit for {GetEnemyDamage(currentLevel)}");
                            else
                                Console.WriteLine("You died");
                            break;
                        }
                    }
                    currentLevel++;
                }

                Console.WriteLine("Game Over");
            }

            private static bool BaronIsDeteated(int o)
            {
                var end = 0;
                return end < o;
            }

            private static int? BossFight(int baronDeaths)
            {
                Console.WriteLine("Welcome to the BaronPit!");
                var enemyHp = GetEnemyHp(currentLevel);
                while (playerCharacter.Hp > 0 || enemyHp > 0)
                {
                    int bossStage = 1;
                    var playerMove = PlayerMove().ToLower();
                    switch (playerMove)
                    {
                        case "q":
                            Console.WriteLine(
                                $"You used {playerCharacter.AbilityOne} - {playerCharacter.AbilityOneCost}");
                            Console.WriteLine($"You dealt {playerCharacter.AbilityOneDmg}");
                            enemyHp -= playerCharacter.AbilityOneDmg;
                            break;
                        case "w":
                            Console.WriteLine($"You used {playerCharacter.AbilityTwo}");
                            Console.WriteLine($"You dealt {playerCharacter.AbilityTwoDmg}");
                            enemyHp -= playerCharacter.AbilityTwoDmg;
                            break;
                        case "shop":
                            OpenShop();
                            continue;
                        case "inventory":
                            OpenInventory();
                            continue;
                        default:
                            Console.WriteLine("Invalid move. Try again.");
                            continue;
                    }

                    if (enemyHp > 0)
                    {
                        if (bossStage == 1)
                        {
                            Console.WriteLine("Baron spit on you and reduced your damage!! \n Your damage is 10% lower!!");
                            playerCharacter.AbilityOneDmg -= playerCharacter.AbilityTwoDmg * 0.10f;
                            playerCharacter.AbilityTwoDmg -= playerCharacter.AbilityTwoDmg * 0.10f;
                            bossStage++;
                        }
                        playerCharacter.Hp -= GetEnemyDamage(currentLevel);
                        if (!CharacterDied())
                        {
                            Console.WriteLine($"You survived a hit for {GetEnemyDamage(currentLevel)}");
                        }
                        else
                        {
                            Console.WriteLine("You died");
                            return null;
                        }
                        break;
                    }
                    else
                    {
                        Console.WriteLine("You have defeated BaronNashor! \n Congratulations on passing the last level!!");
                        return baronDeaths = 1;
                    }
                }
                return null;
                
            }
        

            private static float? CritChance(string getEnemyName)
            {
                Random rand = new Random();
                int chance = 0;
                chance = rand.Next(1, 11);
                if (chance < 2)
                {
                   return 1.5 as float?;
                }
                else
                {
                    return null;
                }
            }

            private static void OpenInventory()
            {
                if (playerCharacter.Inventory.Keys.Count != 0)
                {
                    foreach (var key in playerCharacter.Inventory)
                    {
                        Console.WriteLine($"{key.Key} => {key.Value}");
                    }
                }
                else
                {
                    Console.WriteLine("You have no items!");
                }

                while (true)
                {
                    var input = PlayerMove();
                    // "HealthPot", "ManaPot", "LargeHealthPot", "LargeManaPot"
                    switch (input)
                    {
                        case "HealthPot":
                            if (playerCharacter.Inventory["HealthPot"] > 0)
                            {
                                playerCharacter.Inventory["HealthPot"]--;
                                playerCharacter.Hp += 10;
                            }
                            else
                            {
                                Console.WriteLine("No Health Potions left in inventory!");
                            }
                            break;
                        case "ManaPot":
                            if (playerCharacter.Inventory["ManaPot"] > 0)
                            {
                                playerCharacter.Inventory["ManaPot"]--;
                                playerCharacter.Mana += 11;
                            }
                            else
                            {
                                Console.WriteLine("No Mana Potions left in inventory.");
                            }

                            break;
                        case "LargeHealthPot":
                            if (playerCharacter.Inventory["LargeHealthPot"] > 0)
                            {
                                playerCharacter.Inventory["LargeHealthPot"]--;
                                playerCharacter.Hp += 25;
                            }
                            else
                            {
                                Console.WriteLine("No Large Health Potions left in inventory.");
                            }

                            break;
                        case "LargeManaPot":
                            if (playerCharacter.Inventory["LargeManaPot"] > 0)
                            {
                                playerCharacter.Inventory["LargeManaPot"]--;
                                playerCharacter.Mana += 26;
                            }
                            else
                            {
                                Console.WriteLine("No Large Mana Potions left in inventory.");
                            }

                            break;
                        case "exit":
                            break;
                        default:
                            Console.WriteLine("Choose an item from your inventory");
                            ;
                            continue;
                    }
                }
            }

            private static void MiniBossFight()
            {
                Console.WriteLine("You've encountered a RiftHerald, do you want to fight it [y/n], or open shop!");
                var miniBossAction = PlayerMove().ToLower();
                if (miniBossAction == "shop")
                {
                    OpenShop();
                    return;
                }

                if (miniBossAction == "y")
                {
                    double enemyHp = RiftHerald.Hp;
                    while (enemyHp > 0)
                    {
                        var playerMove = PlayerMove().ToLower();
                        switch (playerMove)
                        {
                            case "q":
                                Console.WriteLine($"You used {playerCharacter.AbilityOne} - {playerCharacter.AbilityOneCost}");
                                Console.WriteLine($"You dealt {playerCharacter.AbilityOneDmg}");
                                enemyHp -= playerCharacter.AbilityOneDmg;
                                break;
                            case "w":
                                Console.WriteLine($"You used {playerCharacter.AbilityTwo}");
                                Console.WriteLine($"You dealt {playerCharacter.AbilityTwoDmg}");
                                enemyHp -= playerCharacter.AbilityTwoDmg;
                                break;
                            default:
                                Console.WriteLine("Invalid move. Try again.");
                                continue;
                        }

                        if (enemyHp <= 0)
                        {
                            Console.WriteLine("The Herald is defeated");
                            playerCharacter.Hp += RiftHerald.RewardBonusHp;
                            PassedLevel(GetEnemyRewardGold(currentLevel), GetEnemyRewardMana(currentLevel),
                                playerCharacter.Hp);
                        }
                        else
                        {
                            MonsterDamage(GetEnemyName(currentLevel));
                            playerCharacter.Hp -= GetEnemyDamage(currentLevel);
                            if (!CharacterDied())
                                Console.WriteLine($"You survived a hit for {GetEnemyDamage(currentLevel)}");
                            else
                                Console.WriteLine("You died!");
                            break;
                        }
                    }
                }
            }

            private static string GetEnemyName(int level)
            {
                switch (level)
                {
                    case 1:
                        return Bats.Name;
                    case 2:
                        return Wolves.Name;
                    case 3:
                        return Goblins.Name;
                    case 4:
                        return Dragons.Name;
                    case 5:
                        return RiftHerald.Name;
                    case 6:
                        return BaronNashor.Name;
                    default:
                        return string.Empty;
                }
            }

            private static double GetEnemyHp(int level)
            {
                switch (level)
                {
                    case 1:
                        return Bats.Hp;
                    case 2:
                        return Wolves.Hp;
                    case 3:
                        return Goblins.Hp;
                    case 4:
                        return Dragons.Hp;
                    case 5:
                        return RiftHerald.Hp;
                    case 6:
                        return BaronNashor.Hp;
                    default:
                        return 0;
                }
            }

            private static double GetEnemyDamage(int level)
            {
                switch (level)
                {
                    case 1:
                        return Bats.Dmg_;
                    case 2:
                        return Wolves.Dmg_;
                    case 3:
                        return Goblins.Dmg_;
                    case 4:
                        return Dragons.Dmg_;
                    case 5:
                        return RiftHerald.Dmg_;
                    case 6:
                        return BaronNashor.Dmg_;
                    default:
                        return 0;
                }
            }

            private static double GetEnemyRewardGold(int level)
            {
                switch (level)
                {
                    case 1:
                        return Bats.RewardGold;
                    case 2:
                        return Wolves.RewardGold;
                    case 3:
                        return Goblins.RewardGold;
                    case 4:
                        return Dragons.RewardDmgMultiplier;
                    default:
                        return 0;
                }
            }

            private static int GetEnemyRewardMana(int level)
            {
                switch (level)
                {
                    case 1:
                        return Bats.RewardMana;
                    case 2:
                        return Wolves.RewardMana;
                    case 3:
                        return Goblins.RewardMana;
                    case 4:
                        return Dragons.RewardMana;
                    default:
                        return 0;
                }
            }

            private static void PassedLevel(double goldReward, int manaReward, double hpLeft)
            {
                Console.WriteLine($"Gold: {goldReward}");
                Console.WriteLine($"Mana: {manaReward}");
                Console.WriteLine($"HpLeft: {hpLeft}");
            }

            private static bool CharacterDied()
            {
                return playerCharacter.Hp <= 0;
            }

            private static void RewardMana(string typeOfEnemy)
            {
                Random rand = new Random();
                int rewardMana = 0;

                switch (typeOfEnemy)
                {
                    case "Bats":
                        rewardMana = rand.Next(14, 22);
                        Bats.RewardMana = rewardMana;
                        break;
                    case "Wolves":
                        rewardMana = rand.Next(18, 30);
                        Wolves.RewardMana = rewardMana;
                        break;
                    case "Dragons":
                        rewardMana = rand.Next(25, 40);
                        Dragons.RewardMana = rewardMana;
                        break;
                    case "Goblins":
                        rewardMana = rand.Next(10, 18);
                        Goblins.RewardMana = rewardMana;
                        break;
                }

                Console.WriteLine($"You received {rewardMana} mana as a reward!");
            }

            private static void OpenShop()
            {
                Console.WriteLine("Welcome to the shop!");
                Console.WriteLine($"Your current gold: {playerGold}");
                Console.WriteLine("Available items:");
                Console.WriteLine("1. HealthPot - 5 gold");
                Console.WriteLine("2. LargeHealthPot - 11 gold");
                Console.WriteLine("3. ManaPot - 6 gold");
                Console.WriteLine("4. LargeManaPot - 9 gold");
                Console.WriteLine("Enter the item name or exit!");

                string input;
                string[] shopItems = new[] { "HealthPot", "ManaPot", "LargeHealthPot", "LargeManaPot" };
                while (shopItems.Contains(input = Console.ReadLine()))
                {
                    int cost;
                    int regen;
                    switch (input)
                    {
                        case "HealthPot":
                            cost = 5;
                            if (playerGold >= cost)
                            {
                                if (DoesPlayerAlreadyHasItem(input))
                                {
                                    playerGold -= cost;
                                    playerCharacter.Inventory["HealthPot"]++;
                                }
                                else
                                {
                                    playerGold -= cost;
                                    playerCharacter.Inventory.Add("HealthPot", 1);
                                    Console.WriteLine("You received a HpPot!");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Not enough gold to purchase the item!");
                            }
                            break;
                        case "ManaPot":
                            cost = 6;
                            if (playerGold >= cost)
                            {
                                if (DoesPlayerAlreadyHasItem(input))
                                {
                                    playerGold -= cost;
                                    playerCharacter.Inventory["ManaPot"]++;
                                    Console.WriteLine("You received a LargeManaPot!");
                                }
                                else
                                {
                                    playerGold -= cost;
                                    playerCharacter.Inventory.Add("ManaPot", 1);
                                    Console.WriteLine("You received a ManaPot!");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Not enough gold to purchase the item!");
                            }
                            break;
                        case "LargeHealthPot":
                            cost = 11;
                            if (DoesPlayerAlreadyHasItem(input))
                            {
                                playerGold -= cost;
                                playerCharacter.Inventory["LargeHealthPot"]++;
                            }
                            else
                            {
                                playerGold -= cost;
                                playerCharacter.Inventory.Add("LargeHealthPot", 1);
                                Console.WriteLine("You received a LargeHpPot!");
                            }
                            break;
                        case "LargeManaPot":
                            cost = 9;
                            if (DoesPlayerAlreadyHasItem(input))
                            {
                                playerGold -= cost;
                                playerCharacter.Inventory["LargeManaPot"]++;
                            }
                            else
                            {
                                playerGold -= cost;
                                playerCharacter.Inventory.Add("LargeManaPot", 1);
                                Console.WriteLine("You received a LargeManaPot!");
                            }
                            break;
                        case "exit":
                            break;
                        default:
                            Console.WriteLine("Select a valid item!");
                            continue;
                    }
                }
                Console.WriteLine("Select a valid item!");

            }

            private static bool DoesPlayerAlreadyHasItem(string item)
            {
                return playerCharacter.Inventory.ContainsKey(item);
            }

            private static string PlayerMove()
            {
                return Console.ReadLine().ToLower();
            }

            private static string ChampSelection()
            {
                return Console.ReadLine();
            }

            private static void MonsterDamage(string typeOfEnemy)
            {
                Random rand = new Random();
                switch (typeOfEnemy)
                {
                    case "Bats":
                        Bats.Dmg_ = rand.Next(5, 11);
                        break;
                    case "Wolves":
                        Wolves.Dmg_ = rand.Next(9, 16);
                        break;
                    case "Dragons":
                        Dragons.Dmg_ = rand.Next(14, 24);
                        break;
                    case "Goblins":
                        Goblins.Dmg_ = rand.Next(6, 10);
                        break;
                    case "RiftHerald":
                        RiftHerald.Dmg_ = rand.Next(17, 29);
                        break;
                    case "BaronNashor":
                        BaronNashor.Dmg_ = rand.Next(19, 24);
                        break;

                }
            }
        }
    }
}