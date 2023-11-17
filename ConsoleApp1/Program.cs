using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ConsoleApp1
{
    internal class Program
    {
        public static Player player1;
        public static void DisplayGameIntro()
        {
            Console.Clear();

            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
            Console.WriteLine("이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("1. 상태보기");
            Console.WriteLine("2. 인벤토리");
            Console.WriteLine("3. 상점");
            Console.WriteLine("4. 던전 입장");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">>");

            int input = CheckValidInput(1, 4);
            switch (input)
            {
                case 1:
                    Console.WriteLine("상태를 보여주는 메서드를 넣어주세요.");
                    player1.DisplayMyInfo();
                    break;

                case 2:
                    Console.WriteLine("인벤토리를 보여주는 메서드를 넣어주세요.");
                    break;
                case 3:
                    Console.WriteLine("상점을 보여주는 메서드를 넣어주세요.");
                    break;
                case 4:
                    Console.WriteLine("던전을 보여주는 메서드를 넣어주세요.");
                    player1.UseSkill();
                    break;
            }
        }

        public static int CheckValidInput(int min, int max)
        {
            while (true)
            {
                string input = Console.ReadLine();

                bool parseSuccess = int.TryParse(input, out var ret);
                if (parseSuccess)
                {
                    if (ret >= min && ret <= max)
                        return ret;
                }

                Console.WriteLine("잘못된 입력입니다. 다시 입력해주세요");
                Console.Write(">>");
            }
        }

        public static Player ChoiceJob(string nickname)
        {
            Console.WriteLine("직업을 선택해주세요");
            Console.WriteLine("1. 전사");
            Console.WriteLine("2. 마법사");
            Console.WriteLine("3. 도적");
            Console.WriteLine("4. 궁수");
            Console.WriteLine();
            Console.Write(">>");

            Player p1;
            int input = CheckValidInput(1, 4);

            if (input % 4 == 1)
            {
                p1 = new Warrior(nickname);
                return p1;
            }
            else if (input % 4 == 2)
            {
                //p1 = new Mage(nickname);
                p1 = new Warrior(nickname);
                return p1;
            }
            else if (input == 3)
            {
                //p1 = new Thief(nickname);
                p1 = new Warrior(nickname);
                return p1;
            }
            else
            {
                //p1 = new Archer(nickname);
                p1 = new Warrior(nickname);
                return p1;
            }
        }


        public static string SetNickname()
        {
            string nickname;
            do
            {
                Console.WriteLine("닉네임을 입력하세요:");
                nickname = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(nickname))
                {
                    Console.Clear();
                    Console.WriteLine("닉네임이 올바르지 않습니다. 다시 시도하세요.");
                }

            } while (string.IsNullOrWhiteSpace(nickname));

            return nickname;
        }

        static void Main()
        {
            string nickname = SetNickname();
            player1 = ChoiceJob(nickname);
            DisplayGameIntro();
        }

    }
}