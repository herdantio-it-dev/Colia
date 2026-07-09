using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace Colia
{

    public interface IComabat
    {
        void Damage(int TrueDamage);
        void Attack(Unit target);
        void UseUltimate(List<Unit> team, List<Unit> boss);
    }

    public class Unit : IComabat
    {
        public string Name { get; set; }
        public int Hp { get; set; }
        public int Atk { get; set; }
        public int Def { get; set; }
        public int MaxHp { get; set; }
        public bool isDead { get; set; }

        public Random random = new Random();

        public Unit(string name, int hp, int atk, int def)
        {
            Name = name;
            MaxHp = hp;
            Hp = hp;
            Atk = atk;
            Def = def;
        }

        public void Damage(int TrueDamage)
        {
            int DamageTotal = TrueDamage - Def;
            Hp = Math.Max(0, Hp - DamageTotal);

            Console.WriteLine($"{Name} has taken {DamageTotal} amount of damage, HP: {Hp}/{MaxHp}");
            Console.WriteLine("----------------------------------------");

            if (Hp == 0)
            {
                isDead = true;
                Console.WriteLine($"{Name} is down");
                Console.WriteLine("----------------------------------------");
            }
        }

        public virtual void Attack(Unit target)
        {
            Console.WriteLine($"{Name} attacks {target.Name}");
            Console.WriteLine("----------------------------------------");
            target.Damage(Atk);
        }

        public virtual void UseUltimate(List<Unit> team, List<Unit> boss)
        {

        }

        public class Characters : Unit
        {
            public bool isHealer;
            public int Heal;
            public Characters(string name, int hp, int atk, int def, bool isHealer = false, int Heal = 0)
            : base(name, hp, atk, def)
            {
                this.isHealer = isHealer;
                this.Heal = Heal;
            }

            public override void UseUltimate(List<Unit> team, List<Unit> boss)
            {
                Console.WriteLine($"{Name} used an Ultimate");
                Console.WriteLine("----------------------------------------");
                if (isHealer)
                {
                    Console.WriteLine($"{Name} heals everyone");
                    foreach (var character in team)
                    {
                        if (!character.isDead)
                        {
                            character.Hp = Math.Min(character.MaxHp, character.Hp + Heal);
                            Console.WriteLine($"{character.Name} Healed the team by {Heal}, HP: {character.Hp}/{character.MaxHp}");
                        }

                        else
                        {
                            int rng = random.Next(2, 11);
                            int DamageTotal = Atk + rng;
                            boss[0].Damage(DamageTotal);
                            //Console.WriteLine($"{Name} dealt {rngDamage} amount of damage");
                        }
                    }
                }
            }
        }

        public class Boss : Unit
        {
            public Boss(string Name, int Hp, int Atk, int Def) : base(Name, Hp, Atk, Def)
            {

            }

            public override void UseUltimate(List<Unit> team, List<Unit> boss)
            {
                Console.WriteLine($"Freeze to death, {Name} used Blizzard");
                Console.WriteLine("----------------------------------------");

                var alive = team.FindAll(mc => !mc.isDead);

                foreach (var target in alive)
                {
                    var rng = random.Next(2, 11);
                    var rngDamage = Atk + rng;
                    target.Damage(rngDamage);
                }
            }
        }

    }

}
