using ConsoleApp1;
using EnumsNamespace;
using System;
using System.Collections.Generic;

namespace ConsoleApp1
{

    public abstract class Monster : Character
    {
        public static List<Monster> GetMonstersOfStage()
        {
            List<Monster> monsters = new List<Monster>();

            // 여기에서 특정 스테이지의 몬스터를 추가
            Slime slime = new Slime();
            monsters.Add(slime);

            Carrot carrot = new Carrot();
            monsters.Add(carrot);

            OrangeMushroom orangeMushroom = new OrangeMushroom();
            monsters.Add(orangeMushroom);


            return monsters;
        }
    }
    //***************************
    //          Stage1           
    //***************************
    public class Slime : Monster
    {
        public Slime()
        {
            Hp = 100;
            MaxHp = 100;
            Damage = 3;
            Defence = 1;
            Exp = 1;
            Gold = 50;
        }

        public override bool IsDie()
        {
            if (Hp == 0)
            {
                return true;
            }

            return false;
        }

        public override void ReceiveDamage(int damage, DamageType damageType)
        {
            if (damageType == DamageType.DT_Skill)
            {
                if (damage <= Defence) damage = 1;
                else damage -= Defence;

                Hp -= damage;

                if (Hp < 0)
                {
                    Hp = 0;
                }
            }
            else if (damageType == DamageType.DT_Normal)
            {
                //10퍼 확률로 회피
            }

        }

        public override void Attack(Character target)
        {
            Random random = new Random();

            int damageErrorRange = Convert.ToInt32(Math.Ceiling(Damage / 10.0f));

            int minDamage = Damage - damageErrorRange;
            int maxDamage = Damage + damageErrorRange;

            int randomDamage = random.Next(minDamage, maxDamage);

            target.ReceiveDamage(randomDamage, DamageType.DT_Normal);
        }
    }

    public class Carrot : Monster
    {
        public Carrot()
        {
            Hp = 10;
            MaxHp = 10;
            Damage = 3;
            Defence = 1;
            Exp = 2;
            Gold = 50;
        }

        public override bool IsDie()
        {
            if (Hp == 0)
            {
                return true;
            }

            return false;
        }

        public override void ReceiveDamage(int damage, DamageType damageType)
        {
            if (damageType == DamageType.DT_Skill)
            {
                if (damage <= Defence) damage = 1;
                else damage -= Defence;

                Hp -= damage;

                if (Hp < 0)
                {
                    Hp = 0;
                }
            }
            else if (damageType == DamageType.DT_Normal)
            {
                //10퍼 확률로 회피
            }

        }

        public override void Attack(Character target)
        {
            Random random = new Random();

            int damageErrorRange = Convert.ToInt32(Math.Ceiling(Damage / 10.0f));

            int minDamage = Damage - damageErrorRange;
            int maxDamage = Damage + damageErrorRange;

            int randomDamage = random.Next(minDamage, maxDamage);

            target.ReceiveDamage(randomDamage, DamageType.DT_Normal);
        }
    }

    public class OrangeMushroom : Monster
    {
        public OrangeMushroom()
        {
            Hp = 50;
            MaxHp = 50;
            Damage = 3;
            Defence = 1;
            Exp = 10;
            Gold = 50;
        }

        public override bool IsDie()
        {
            if (Hp == 0)
            {
                return true;
            }

            return false;
        }

        public override void ReceiveDamage(int damage, DamageType damageType)
        {
            if (damageType == DamageType.DT_Skill)
            {
                if (damage <= Defence) damage = 1;
                else damage -= Defence;

                Hp -= damage;

                if (Hp < 0)
                {
                    Hp = 0;
                }
            }
            else if (damageType == DamageType.DT_Normal)
            {
                //10퍼 확률로 회피
            }

        }

        public override void Attack(Character target)
        {
            Random random = new Random();

            int damageErrorRange = Convert.ToInt32(Math.Ceiling(Damage / 10.0f));

            int minDamage = Damage - damageErrorRange;
            int maxDamage = Damage + damageErrorRange;

            int randomDamage = random.Next(minDamage, maxDamage);

            target.ReceiveDamage(randomDamage, DamageType.DT_Normal);
        }
    }
}