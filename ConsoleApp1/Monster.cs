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
            Hp = 30;
            MaxHp = 30;
            Damage = 3;
            Defence = 1;
            Exp = 10;
            Gold = 50;
            Name = "Slime";
        }

        public override bool IsDie()
        {
            if (Hp == 0)
            {
                return true;
            }

            return false;
        }

        public override bool ReceiveDamage(int damage, DamageType damageType)
        {
            bool isReceiveDamage = true;

            if (damageType == DamageType.DT_Normal)
            {
                isReceiveDamage = _random.Next(1, 11) != 1;
            }

            if (isReceiveDamage) ApplyDamage(damage);

            return isReceiveDamage;
        }

        public override void Attack(Character target)
        {
            //15퍼 확률로 크리티컬...
            int damageErrorRange = Convert.ToInt32(Math.Ceiling(Damage / 10.0f));

            int minDamage = Damage - damageErrorRange;
            int maxDamage = Damage + damageErrorRange;

            int randomDamage = _random.Next(minDamage, maxDamage + 1);

            target.ReceiveDamage(randomDamage, DamageType.DT_Normal);
        }

        private void ApplyDamage(int damage)
        {
            if (damage <= Defence) damage = 1;
            else damage -= Defence;

            Hp -= damage;

            if (Hp < 0)
            {
                Hp = 0;
            }
        }

        private Random _random = new Random();
    }

        public class Carrot : Monster
    {
        public Carrot()
        {
            Hp = 10;
            MaxHp = 10;
            Damage = 3;
            Defence = 1;
            Exp = 9;
            Gold = 50;
            Name = "Carrot";
        }

        public override bool IsDie()
        {
            if (Hp == 0)
            {
                return true;
            }

            return false;
        }

        public override bool ReceiveDamage(int damage, DamageType damageType)
        {
            bool isReceiveDamage = true;

            if (damageType == DamageType.DT_Normal)
            {
                isReceiveDamage = _random.Next(1, 11) != 1;
            }

            if (isReceiveDamage) ApplyDamage(damage);

            return isReceiveDamage;
        }

        public override void Attack(Character target)
        {
            //15퍼 확률로 크리티컬...
            int damageErrorRange = Convert.ToInt32(Math.Ceiling(Damage / 10.0f));

            int minDamage = Damage - damageErrorRange;
            int maxDamage = Damage + damageErrorRange;

            int randomDamage = _random.Next(minDamage, maxDamage + 1);

            target.ReceiveDamage(randomDamage, DamageType.DT_Normal);
        }

        private void ApplyDamage(int damage)
        {
            if (damage <= Defence) damage = 1;
            else damage -= Defence;

            Hp -= damage;

            if (Hp < 0)
            {
                Hp = 0;
            }
        }

        private Random _random = new Random();
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
            Name = "OrangeMushroom";
        }

        public override bool IsDie()
        {
            if (Hp == 0)
            {
                return true;
            }

            return false;
        }

        public override bool ReceiveDamage(int damage, DamageType damageType)
        {
            bool isReceiveDamage = true;

            if (damageType == DamageType.DT_Normal)
            {
                isReceiveDamage = _random.Next(1, 11) != 1;
            }

            if (isReceiveDamage) ApplyDamage(damage);

            return isReceiveDamage;
        }

        public override void Attack(Character target)
        {
            //15퍼 확률로 크리티컬...
            int damageErrorRange = Convert.ToInt32(Math.Ceiling(Damage / 10.0f));

            int minDamage = Damage - damageErrorRange;
            int maxDamage = Damage + damageErrorRange;

            int randomDamage = _random.Next(minDamage, maxDamage + 1);

            target.ReceiveDamage(randomDamage, DamageType.DT_Normal);
        }

        private void ApplyDamage(int damage)
        {
            if (damage <= Defence) damage = 1;
            else damage -= Defence;

            Hp -= damage;

            if (Hp < 0)
            {
                Hp = 0;
            }
        }

        private Random _random = new Random();
    }
}