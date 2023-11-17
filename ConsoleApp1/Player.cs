using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using ConsoleApp1;
using EnumsNamespace;
using static System.Net.Mime.MediaTypeNames;

namespace ConsoleApp1
{
    public abstract class Player : Character
    {
        // 스킬 관련 필드
        public string _fSkillName;
        public string _sSkillName;
        public int _fSkillMp;
        public int _sSkillMp;
        public string _fSkillInfo;
        public string _sSkillInfo;
        public int _fSkillDamage;
        public float _sSkillDamage;

        Monster monster;
        List<Monster> monsters;


        public Player(string name)
        {
            _name = name;

            // 생성자에서 초기화
            InitializeMonsters();
        }

        private void InitializeMonsters()
        {
            //monster = new Slime();
            monsters = Monster.GetMonstersOfStage();
        }

        public abstract void DisplayMyInfo();

        //inventory 생성
        //인벤토리
        public void DisplayInventory()
        {

        }

        //장착 관리
        public void DisplayEquipManage()
        {

        }

        //스킬 사용
        public void UseSkill()
        {
            Console.Clear();
            Console.WriteLine($"내 현재 MP : {Program.player1._mp}");
            Console.WriteLine($"1. {_fSkillName} - MP {_fSkillMp}");
            Console.WriteLine($"{_fSkillInfo}\n");
            Console.WriteLine($"2. {_sSkillName} - MP {_sSkillMp}");
            Console.WriteLine($"{_sSkillInfo}\n");
            Console.WriteLine("사용할 스킬을 고르세요.");

            int input = Program.CheckValidInput(1, 2);
            switch (input)
            {
                case 1:
                    FirstSkill();
                    break;
                case 2:
                    SecondSkill();
                    break;
            }
        }


        public void FirstSkill()
        {
            // 현재 스테이지의 살아있는 몬스터 선택
            Random random = new Random();
            int randomMonsterIndex = random.Next(monsters.Count);
            Monster selectedMonster = monsters[randomMonsterIndex];

            // 플레이어가 몬스터 공격
            int damage = _fSkillDamage;
            selectedMonster.ReceiveDamage(damage, DamageType.DT_Skill);


            Console.Clear();
            Console.WriteLine($"{_name}이(가) {selectedMonster}에게 {_fSkillName}을(를) 사용하여 {damage}의 데미지를 입혔습니다.\n");

            //몬스터를 죽여 경험치 획득
            if (selectedMonster.IsDie())
            {
                int monsterExp = selectedMonster.Exp;
                Console.WriteLine($"{selectedMonster}을(를) 죽였습니다! 획득한 경험치: {monsterExp}\n");
                Console.WriteLine($"남은 MP : {Program.player1.Mp}와 {_sSkillMp}의 차\n");
                Console.WriteLine("");
                Program.player1._mp -= _fSkillMp; //Program.player1._mp가 0으로 출력되는 문제
                GainExp(monsterExp);
            }
            else
            {
                Console.WriteLine($"하지만 {selectedMonster}는 살아남았네요 . . .\n");
            }
            Thread.Sleep(4000);
            Program.DisplayGameIntro();
            //UseSkill();
        }

        public void SecondSkill()
        {
            // 현재 스테이지의 살아있는 몬스터 중에서 랜덤하게 두 몬스터 선택
            List<int> availableMonster = Enumerable.Range(0, monsters.Count).ToList();
            Random random = new Random();

            // 첫 번째 몬스터 선택
            int randomMonsterIndex1 = availableMonster[random.Next(availableMonster.Count)];
            availableMonster.Remove(randomMonsterIndex1); // 중복 방지
            Monster selectedMonster1 = monsters[randomMonsterIndex1];

            // 두 번째 몬스터 선택
            int randomMonsterIndex2 = availableMonster[random.Next(availableMonster.Count)];
            Monster selectedMonster2 = monsters[randomMonsterIndex2];

            // 플레이어가 몬스터들에게 공격
            int damage1 = Convert.ToInt32(_sSkillDamage);
            int damage2 = Convert.ToInt32(_sSkillDamage);

            selectedMonster1.ReceiveDamage(damage1, DamageType.DT_Skill);
            selectedMonster2.ReceiveDamage(damage2, DamageType.DT_Skill);

            Console.Clear();
            Console.WriteLine($"{_name}이(가) {selectedMonster1}와 {selectedMonster2}에게 {_sSkillName}을(를) 사용하여 각각 {damage1}의 데미지를 입혔습니다.\n");

            // 각 몬스터를 죽여 경험치 획득
            if (selectedMonster1.IsDie())
            {
                int monsterExp1 = selectedMonster1.Exp;
                Console.WriteLine($"{selectedMonster1}을(를) 죽였습니다! 획득한 경험치: {monsterExp1}\n");
                Console.WriteLine($"남은 MP : {Program.player1.Mp}와 {_sSkillMp}의 차\n");
                Program.player1._mp -= _sSkillMp;
                GainExp(monsterExp1);
            }
            else
            {
                Console.WriteLine($"하지만 {selectedMonster1}는 살아남았네요 . . .\n");
            }

            if (selectedMonster2.IsDie())
            {
                int monsterExp2 = selectedMonster2.Exp;
                Console.WriteLine($"{selectedMonster2}을(를) 죽였습니다! 획득한 경험치: {monsterExp2}\n");
                Console.WriteLine($"남은 MP : {Program.player1.Mp}와 {_sSkillMp}의 차\n");
                Program.player1._mp -= _sSkillMp;
                GainExp(monsterExp2);
            }
            else
            {
                Console.WriteLine($"하지만 {selectedMonster2}는 살아남았네요 . . .\n");
            }

            Thread.Sleep(5000);
            UseSkill();
        }


        // 플레이어가 전투를 마치면 몬스터에게서 서로 다른 경험치 획득
        public void GainExp(int monsterExp)
        {
            Exp += monsterExp;

            // 레벨업 체크
            while (Exp >= GetRequiredExpForNextLevel())
            {
                LevelUp();
            }
        }

        // 레벨업 메서드
        private void LevelUp()
        {
            Level++;
            Exp = 0; // 레벨업 후 경험치 초기화
            //_attack++; // 기본 공격력 1 증가
            //_defence++; // 기본 방어력 1 증가
            
            Console.WriteLine($"{_name}이(가) Lv.{Level}로 레벨업했습니다!");
        }

        // 다음 레벨까지 필요한 경험치
        private int GetRequiredExpForNextLevel()
        {
            // 경험치 요구량
            if (Level == 1)
            {
                return 10;
            }
            else if (Level == 2)
            {
                return 35;
            }
            else if (Level == 3)
            {
                return 65;
            }
            else if (Level == 4)
            {
                return 100;
            }
            else
            {
                return 0;
            }
        }


        public int Level
        {
            get { return _level; }
            set { _level = value; }
        }
        public int Mp
        {
            get { return _mp; }
            set { _mp = value; }
        }
        public int MaxMp
        {
            get { return _maxMp; }
        }
        public Inventory Inventory
        {
            get { return _inventory; }
            set { _inventory = value; }
        }
        public JobType JobType
        {
            get { return _job; }
            set { _job = value; }
        }
        public Item[] EquipmentWeaponArray
        {
            get { return _equipmentWeaponArray; }
            set { }
        }

        private int _level = 1;
        private int _mp;
        private int _maxMp;
        private Inventory _inventory = new Inventory();
        private JobType _job;
        private Item[] _equipmentWeaponArray = new Item[2];
        private Item[] _equipmentArmorArray = new Item[5];

    }



    public class Warrior : Player
    {
        public Warrior(string name)
            : base(name)
        {
            JobType = JobType.JT_Warrior;

            // Warrior의 스킬 설정
            _fSkillName = "전사 스킬 1";
            _sSkillName = "전사 스킬 2";
            _fSkillMp = 15;
            _sSkillMp = 25;
            _fSkillInfo = "전사의 스킬 1입니다.";
            _sSkillInfo = "전사의 스킬 2입니다.";
            _fSkillDamage = _attack * 2;
            _sSkillDamage = _attack * 1.5f;
        }

        public override void DisplayMyInfo()
        {
            Console.Clear();

            Console.WriteLine("상태보기");
            Console.WriteLine("캐릭터의 정보를 표시합니다.");
            Console.WriteLine();
            Console.WriteLine($"Lv.{Level}");
            Console.WriteLine($"{Name}( 전사 )");
            int addAttack = _attack - _initialAttack;
            Console.WriteLine($"공격력: {_attack}" + (addAttack != 0 ? $" (+{addAttack})" : ""));
            int addDefence = _defence - _initialDefence;
            Console.WriteLine($"방어력: {_defence}" + (addDefence != 0 ? $" (+{addDefence})" : ""));
            Console.WriteLine($"체력: {_hp}");
            Console.WriteLine($"MP: {_mp}");
            Console.WriteLine($"Gold : {Gold} G");
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">>");

            int input = Program.CheckValidInput(0, 0);
            switch (input)
            {
                case 0:
                    Program.DisplayGameIntro();
                    break;
            }
        }

        // 데미지 받는 메서드
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

        // 공격하는 메서드
        public override void Attack(Character target)
        {
            Random random = new Random();
            int damageErrorRange = Convert.ToInt32(Math.Ceiling(Damage / 10.0f));

            int minDamage = _attack - damageErrorRange;
            int maxDamage = _attack + damageErrorRange;

            int randomDamage = random.Next(minDamage, maxDamage);

            target.ReceiveDamage(randomDamage, DamageType.DT_Normal);
        }

        public override bool IsDie()
        {
            if (Hp > 0)
            {
                return false;
            }

            return true;
        }

        private const int _initialAttack = 15;
        private const int _initialDefence = 15;
        public int _hp = 40;
        private int _maxHp = 40;
        public int _mp = 20;
        private int _maxMp = 20;
        public int _attack = 15;
        private int _defence = 15;


    }


    //public class Mage : Player
    //{
    //    public Mage(string name)
    //        : base(name)
    //    {
    //        JobType = JobType.JT_Mage;
    //    }

    //    private const int _initialAttack = 10;
    //    private const int _initialDefence = 5;
    //    private int _hp = 20;
    //    private int _maxHp = 20;
    //    private int _mp = 50;
    //    private int _maxMp = 50;
    //    private int _attack = 10;
    //    private int _defence = 5;

    //}

    //public class Thief : Player
    //{
    //    public Thief(string name)
    //        : base(name)
    //    {
    //        JobType = JobType.JT_Thief;
    //    }

    //    private const int _initialAttack = 25;
    //    private const int _initialDefence = 2;
    //    private int _hp = 30;
    //    private int _maxHp = 30;
    //    private int _mp = 30;
    //    private int _maxMp = 30;
    //    private int _attack = 25;
    //    private int _defence = 2;

    //}

    //public class Archer : Player
    //{
    //    public Archer(string name)
    //        : base(name)
    //    {
    //        JobType = JobType.JT_Archer;
    //    }

    //    private const int _initialAttack = 20;
    //    private const int _initialDefence = 5;
    //    private int _hp = 30;
    //    private int _maxHp = 30;
    //    private int _mp = 20;
    //    private int _maxMp = 20;
    //    private int _attack = 20;
    //    private int _defence = 5;

    //}
}
