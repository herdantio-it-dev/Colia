using Colia;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.Serialization;
using System.Security.Cryptography.X509Certificates;
using static Colia.Unit;

Characters tb = new Characters("Trailblazer", 980, 20, 8);
Characters dh = new Characters("DanHeng", 714, 25, 3);
Characters m7 = new Characters("March7th", 857, 14, 6, true, 180);
Boss cocolia = new Boss("Cocolia", 1307, 42, 10);
Random random = new Random();

tb.MaxHp = 980;
dh.MaxHp = 714;
m7.MaxHp = 857;
cocolia.MaxHp = 1307;

List<Unit> team = new List<Unit> { tb, dh, m7 };
List<Unit> boss = new List<Unit> { cocolia };

Console.WriteLine("--------------Battle Start--------------");
while (!cocolia.isDead && !(tb.isDead && dh.isDead && m7.isDead))
{
    Console.WriteLine($"{cocolia.Name}      HP: {cocolia.Hp}/{cocolia.MaxHp}");
    Console.WriteLine("----------------------------------------");
    Console.WriteLine($"{tb.Name}  HP: {tb.Hp}/{tb.MaxHp}");
    Console.WriteLine($"{dh.Name}      Hp: {dh.Hp}/{dh.MaxHp}");
    Console.WriteLine($"{m7.Name}     Hp: {m7.Hp}/{m7.MaxHp}");
    Console.WriteLine("");
    Console.WriteLine("");


    if (!cocolia.isDead)
    {
        Console.WriteLine("Cocolia Turn!");
        Console.WriteLine("----------------------------------------");
        int attack = random.Next(0, 2);
        var alive = team.FindAll(mc => !mc.isDead);

        if (attack == 0)
        {
            Unit target = alive[random.Next(alive.Count)];
            cocolia.Attack(target);
            Console.WriteLine("----------------------------------------");
        }
        else
        {
            cocolia.UseUltimate(team, boss);
            Console.WriteLine("----------------------------------------");
        }
        GameStatus(tb, dh, m7, cocolia);

    }

    if (!tb.isDead && !cocolia.isDead)
    {
        PlayerTurn(tb, team, boss);
        GameStatus(tb, dh, m7, cocolia);
    }

    if (!dh.isDead && !cocolia.isDead)
    {
        PlayerTurn(dh, team, boss);
        GameStatus(tb, dh, m7, cocolia);
    }

    if (!m7.isDead && !cocolia.isDead)
    {
        PlayerTurn(m7, team, boss);
        GameStatus(tb, dh, m7, cocolia);
    }

    static void PlayerTurn(Characters character, List<Unit> team, List<Unit> boss)
    {
        Console.WriteLine($"{character.Name} Turn");
        Console.WriteLine("1.Basic Attack");
        Console.WriteLine("2.Ultimate");
        Console.WriteLine("----------------------------------------");
        string option = Console.ReadLine();

        if (option == "1")
        {
            character.Attack(boss[0]);
        }
        else if (option == "2")
        {
            character.UseUltimate(team, boss);
        }
    }

    static void GameStatus(Characters tb, Characters dh, Characters m7, Boss cocolia)
    {
        if (cocolia.isDead)
        {
            Console.WriteLine("VICTORY");
        }

        if (tb.isDead && dh.isDead && m7.isDead)
        {
            Console.WriteLine("DEFEATED");
        }
    }

}