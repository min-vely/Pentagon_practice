using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
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

        // Attack() 메서드에서 10% 오차를 이용해 계산되는 최종 공격 데미지
        public int randomDamage;

        public int _level = 1;
        public int _mp;
        public int _maxMp;
        public int _attack;
        public int _defence;
        private Inventory _inventory = new Inventory();
        private JobType _job;
        private Item[] _equipmentWeaponArray = new Item[2];
        //private Item[] _equipmentArmorArray = new Item[5];

        // 몬스터 리스트
        Monster monster;
        List<Monster> monsters;
        Random random = new Random();
        //Program program = new Program();

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
            set { _maxMp = value; }
        }
        public int AttackDamage
        {
            get { return _attack; }
            set { _attack = value; }
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


        public Player(string name)
        {
            _name = name;
            _mp = 30;
            randomDamage = 0;

            // 생성자에서 초기화
            monsters = Monster.GetMonstersOfStage();
        }

        // 몬스터 불러오기
        //private void InitializeMonsters()
        //{
        //    //monster = new Slime();
        //    //monsters = MonsterManager.GetMonstersOfStage();
        //    monsters = Monster.GetMonstersOfStage();
        //}

        public abstract void DisplayMyInfo();

        //inventory 생성
        // 인벤토리
        public void DisplayInventory()
        {
            Console.Clear();
            Console.WriteLine("인벤토리");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");
            Console.WriteLine("[아이템 목록]");
            // 아이템 리스트
            Console.WriteLine();
            Console.WriteLine("1. 장착 관리");
            Console.WriteLine("0. 나가기\n");
            Console.WriteLine("원하시는 행동을 입력해주세요.\n");

            int input = Program.CheckValidInput(0, 1);
            switch (input)
            {
                case 0:
                    Program.DisplayGameIntro();
                    break;
                case 1:
                    DisplayEquipManage();
                    break;
            }
        }

        //장착 관리
        public void DisplayEquipManage()
        {
            Console.Clear();
            Console.WriteLine("인벤토리 - 장착 관리");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");
            Console.WriteLine("[아이템 목록]");
            // 아이템 리스트
            Console.WriteLine("1. 아이템 장착 or 사용하기");
            Console.WriteLine("0. 나가기\n");
            Console.WriteLine("원하시는 행동을 입력해주세요.\n");

            int input = Program.CheckValidInput(0, 1);
            switch (input)
            {
                case 0:
                    DisplayInventory();
                    break;
                case 1:
                    // 아이템 장착
                    break;
            }
        }


        public void TempBattle()
        {
            Console.Clear();
            Console.WriteLine("Battle!!\n");
            Console.WriteLine("1. 공격");
            Console.WriteLine("2. 스킬\n");

            int input = Program.CheckValidInput(1, 2);
            switch (input)
            {
                case 1:
                    BasicAttack();
                    break;
                case 2:
                    UseSkill();
                    break;
            }
        }

        // 번호로 몬스터를 선택하면 기본 공격(평타)
        public void BasicAttack()
        {
            // 일단 임시로 몬스터 랜덤 호출!!!!!
            int randomMonsterIndex = random.Next(monsters.Count);
            Monster selectedMonster = monsters[randomMonsterIndex];

            // 플레이어가 선택한 몬스터 공격
            int damage = Program.player1.AttackDamage;
            Attack(selectedMonster);
            //selectedMonster.ReceiveDamage(damage, DamageType.DT_Normal);

            Console.Clear();
            Console.WriteLine($"{_name}이(가) {selectedMonster.Name}에게 기본 공격을 사용하여 {Program.player1.randomDamage}의 데미지를 입혔습니다.\n");

            // 몬스터를 죽여 경험치, 골드, 포션 획득
            if (selectedMonster.IsDie())
            {
                int monsterExp = selectedMonster.Exp;
                int monsterGold = selectedMonster.Gold;
                Console.WriteLine($"{selectedMonster.Name}을(를) 죽였습니다!\n획득한 경험치 : {monsterExp}\n획득한 골드 : {monsterGold}\n");
                Program.player1.Gold += monsterGold;
                GainExp(monsterExp);
                GetPosionItems();
            }
            else // 몬스터가 죽지 않으면 경험치, 골드, 포션 미획득
            {
                Console.WriteLine($"하지만 {selectedMonster.Name}은(는) 살아남았네요 . . .\n");
            }
            Console.WriteLine($"현재 경험치 : {Exp}\n");

            Thread.Sleep(5000);
            //전투 화면으로 돌아가기
            Program.DisplayGameIntro();
            //UseSkill();
        }


        // 스킬 사용
        public void UseSkill()
        {
            Console.Clear();
            Console.WriteLine("Battle!!\n");

            // 몬스터 정보

            Console.WriteLine("[내정보]");
            Console.WriteLine($"Lv.{Level} {_name} ({Program.player1._job})");
            Console.WriteLine($"HP {Program.player1.Hp}/{Program.player1.MaxHp}");
            Console.WriteLine($"MP {Program.player1.Mp}/{Program.player1.MaxMp}\n");
            Console.WriteLine($"1. {_fSkillName} - MP {_fSkillMp}");
            Console.WriteLine($"{_fSkillInfo}");
            Console.WriteLine($"2. {_sSkillName} - MP {_sSkillMp}");
            Console.WriteLine($"{_sSkillInfo}");
            Console.WriteLine("0. 취소\n");
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">>");

            int input = Program.CheckValidInput(0, 2);
            // 플레이어의 MP가 선택한 스킬의 소모 MP보다 적은지 확인
            if ((input == 1 && Program.player1.Mp < _fSkillMp) || (input == 2 && Program.player1.Mp < _sSkillMp))
            {
                Console.WriteLine("MP가 부족하여 스킬을 사용할 수 없습니다.");
                Thread.Sleep(3000);
                Program.DisplayGameIntro();
            }
            else
            {
                switch (input)
                {
                    case 0:
                        Program.DisplayGameIntro();
                        break;
                    case 1:
                        FirstSkill();
                        break;
                    case 2:
                        SecondSkill();
                        break;
                }
            }
        }


        public void FirstSkill()
        {
            // 현재 스테이지의 살아있는 몬스터 선택
            //Random random = new Random();
            int randomMonsterIndex = random.Next(monsters.Count);
            Monster selectedMonster = monsters[randomMonsterIndex];

            // 플레이어가 몬스터 공격
            int damage = _fSkillDamage;
            selectedMonster.ReceiveDamage(damage, DamageType.DT_Skill);


            Console.Clear();
            Console.WriteLine($"{_name}이(가) {selectedMonster.Name}에게 {_fSkillName}을(를) 사용하여 {damage}의 데미지를 입혔습니다.\n");

            // 몬스터를 죽여 경험치, 골드, 포션 획득
            if (selectedMonster.IsDie())
            {
                int monsterExp = selectedMonster.Exp;
                int monsterGold = selectedMonster.Gold;
                Console.WriteLine($"{selectedMonster.Name}을(를) 죽였습니다!\n획득한 경험치 : {monsterExp}\n획득한 골드 : {monsterGold}\n");
                Program.player1.Gold += monsterGold;
                GainExp(monsterExp);
                GetPosionItems();
            }
            else // 몬스터가 죽지 않으면 경험치, 골드, 포션 미획득
            {
                Console.WriteLine($"하지만 {selectedMonster.Name}은(는) 살아남았네요 . . .\n");
            }

            Console.WriteLine($"남은 MP : {Program.player1.Mp - _fSkillMp}\n");
            Program.player1.Mp -= _fSkillMp;
            Console.WriteLine($"현재 경험치 : {Exp}\n");

            Thread.Sleep(5000);
            //전투 화면으로 돌아가기
            Program.DisplayGameIntro();
            //UseSkill();
        }

        public void SecondSkill()
        {
            // 현재 스테이지의 살아있는 몬스터 중에서 랜덤하게 두 몬스터 선택
            List<int> availableMonster = Enumerable.Range(0, monsters.Count).ToList();
            //Random random = new Random();

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
            Console.WriteLine($"{_name}이(가) {selectedMonster1.Name}와 {selectedMonster2.Name}에게 {_sSkillName}을(를) 사용하여 각각 {damage1}의 데미지를 입혔습니다.\n");

            // 각 몬스터를 죽여 경험치, 골드, 포션 획득
            if (selectedMonster1.IsDie())
            {
                int monsterExp1 = selectedMonster1.Exp;
                int monsterGold1 = selectedMonster1.Gold;
                Console.WriteLine($"{selectedMonster1.Name}을(를) 죽였습니다!\n획득한 경험치 : {monsterExp1}\n획득한 골드 : {monsterGold1}\n");
                Program.player1.Gold += monsterGold1;
                GainExp(monsterExp1);
                GetPosionItems();
            }
            else
            {
                Console.WriteLine($"하지만 {selectedMonster1.Name}은(는) 살아남았네요 . . .\n");
            }

            if (selectedMonster2.IsDie())
            {
                int monsterExp2 = selectedMonster2.Exp;
                int monsterGold2 = selectedMonster2.Gold;
                Console.WriteLine($"{selectedMonster2.Name}을(를) 죽였습니다!\n획득한 경험치 : {monsterExp2}\n획득한 골드 : {monsterGold2}");
                Program.player1.Gold += monsterGold2;
                GainExp(monsterExp2);
                GetPosionItems();
            }
            else
            {
                Console.WriteLine($"하지만 {selectedMonster2.Name}은(는) 살아남았네요 . . .\n");
            }

            Console.WriteLine($"남은 MP : {Program.player1.Mp - _sSkillMp}\n");
            Program.player1.Mp -= _sSkillMp;
            Thread.Sleep(5000);
            //전투 화면으로 돌아가기
            Program.DisplayGameIntro();
            //UseSkill();
        }


        // 플레이어가 전투를 마치면 몬스터에게서 서로 다른 경험치 획득
        public void GainExp(int monsterExp)
        {
            Exp += monsterExp;

            // 경험치가 다음 레벨업에 필요한 양보다 많을 경우
            while (Exp >= GetRequiredExpForNextLevel())
            {
                int excessExp = Exp - GetRequiredExpForNextLevel();
                LevelUp();
                Exp = excessExp; // 초과된 경험치를 다음 레벨에 사용
            }
        }

        // 레벨업 메서드
        private void LevelUp()
        {
            Level++;
            Exp = 0; // 레벨업 후 경험치 초기화
            AttackDamage += 1; // 기본 공격력 1 증가
            Defence += 1; // 기본 방어력 1 증가

            Console.WriteLine($"{_name}이(가) Lv.{Level}로 레벨업했습니다!");
        }

        // 다음 레벨까지 필요한 경험치
        private int GetRequiredExpForNextLevel()
        {
            switch (Level)
            {
                case 1:
                    return 10;
                case 2:
                    return 35;
                case 3:
                    return 65;
                case 4:
                    return 100;
                default:
                    return 0;
            }
        }

        public void GetPosionItems()
        {
            // 몬스터 사망 시 10% 확률로 포션을 얻음
            if (random.Next(1, 11) == 1)
            {
                int potionType = random.Next(2); // 0은 HpPotion, 1은 MpPotion
                if (potionType == 0)
                {
                    //HpPotionCount++; // 보유 중인 HpPotion 개수 증가
                    Console.WriteLine($"운 좋게 Hp포션을 1개 획득했습니다!");
                    //Console.WriteLine($"보유 중인 Hp포션 개수 : {HpPotionCount}\n");
                }
                else
                {
                    // MpPotionCount++; // 보유 중인 MpPotion 개수 증가
                    Console.WriteLine($"운 좋게 Mp포션을 1개 획득했습니다!");
                    //Console.WriteLine($"보유 중인 Mp포션 개수 : {MpPotionCount}\n");
                }
            }
        }

    }



    public class Warrior : Player
    {
        Program program = new Program();
        Random random = new Random();

        public Warrior(string name)
            : base(name)
        {
            JobType = JobType.JT_Warrior;

            // Warrior의 스킬 설정
            _fSkillName = "전사 스킬 1";
            _sSkillName = "전사 스킬 2";
            _fSkillMp = 10;
            _sSkillMp = 20;
            _fSkillInfo = "전사의 스킬 1입니다.";
            _sSkillInfo = "전사의 스킬 2입니다.";
            _fSkillDamage = _attack * 2;
            _sSkillDamage = _attack * 1.5f;

            AttackDamage = 15;
            Defence = 15;
            Hp = 40;
            MaxHp = 40;
            Mp = 30;
            MaxMp = 30;
        }

        public override void DisplayMyInfo()
        {
            Console.Clear();

            Console.WriteLine("상태보기");
            Console.WriteLine("캐릭터의 정보를 표시합니다.");
            Console.WriteLine();
            Console.WriteLine($"Lv.{Level}");
            Console.WriteLine($"{Name} ( 전사 )");
            int addAttack = AttackDamage - _initialAttack;
            Console.WriteLine($"공격력: {AttackDamage}" + (addAttack != 0 ? $" (+{addAttack})" : ""));
            int addDefence = Defence - _initialDefence;
            Console.WriteLine($"방어력: {Defence}" + (addDefence != 0 ? $" (+{addDefence})" : ""));
            Console.WriteLine($"체력: {Hp}");
            Console.WriteLine($"MP: {Program.player1.Mp}");
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
        public override bool ReceiveDamage(int damage, DamageType damageType)
        {
            bool isReceiveDamage = true;

            // 10% 확률로 몬스터의 기본 공격 회피
            if (damageType == DamageType.DT_Normal)
            {
                isReceiveDamage = random.Next(1, 11) != 1;
            }

            if (isReceiveDamage) ApplyDamage(damage);

            return isReceiveDamage;
        }

        // 데미지 계산
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


        // 공격하는 메서드
        public override void Attack(Character target)
        {
            Random random = new Random();
            int damageErrorRange = Convert.ToInt32(Math.Ceiling(Program.player1.AttackDamage / 10.0f));

            int minDamage = Program.player1.AttackDamage - damageErrorRange;
            int maxDamage = Program.player1.AttackDamage + damageErrorRange;

            randomDamage = random.Next(minDamage, maxDamage);
            
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
        public int _mp = 30;
        private int _maxMp = 30;
        public int _attack = 15;
        public int _defence = 15;


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
    //    private int _mp = 30;
    //    private int _maxMp = 30;
    //    private int _attack = 20;
    //    private int _defence = 5;

    //}
}
