using EnumsNamespace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public abstract class Character
    {
        //데미지 받는 메서드
        public abstract bool ReceiveDamage(int damage, DamageType damageType);
        //공격 하는 메서드
        public abstract void Attack(Character target);
        //죽었는지 아닌지 판별하는 메서드
        public abstract bool IsDie();


        public int Hp
        {
            get { return _hp; }
            set { _hp = value; }
        }

        public int MaxHp
        {
            get { return _maxHp; }
            set { _maxHp = value; }
        }

        public int Damage
        {
            get { return _damage; }
            set { _damage = value; }
        }

        public int Defence
        {
            get { return _defence; }
            set { _defence = value; }
        }

        public int Gold
        {
            get { return _gold; }
            set { _gold = value; }
        }

        public int Exp
        {
            get { return _exp; }
            set { _exp = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private int _hp;
        private int _maxHp;
        private int _damage;
        private int _defence;
        private int _gold;
        private int _exp;
        public string _name = "";
    }
}
