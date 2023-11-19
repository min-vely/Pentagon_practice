using ConsoleApp1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Dungeon
    {
        //Program program = new Program();
        public void DisplayDungeon()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("던전입장");
            Console.ResetColor();
            Console.WriteLine("들어갈 던전의 난이도를 선택해 주세요.\n");
            Console.WriteLine("1. 쉬운 던전");
            Console.WriteLine("2. 일반 던전");
            Console.WriteLine("3. 어려운 던전");
            Console.WriteLine("0. 나가기");

            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">>");

            int input = Program.CheckValidInput(0, 3);
            switch (input)
            {
                case 1:
                    EasyDungeon();
                    break;
                case 2:
                    NormalDungeon();
                    break;
                case 3:
                    HardDungeon();
                    break;
                case 0:
                    Program.DisplayGameIntro();
                    break;
            }
        }//던전 입장 화면

        static void EasyDungeon()
        {
            Console.Clear();
        }

        static void NormalDungeon()
        {
            Console.Clear();
        }

        static void HardDungeon()
        {
            
            Console.Clear();
            
        }
        public void Battle(Player player, List<Monster> monster, int stage)
        {
            Console.Clear();
            var random = new Random();
            var takemonster = monster.OrderBy(x => x.Name).Take(random.Next(1, 4));
            //for (int i = 0; i < monster.Count; i++)
            //{
            //    Console.WriteLine((i + 1) + monster.Name + " HP " + monster.Hp);
            //}
            Console.WriteLine("[내 정보]");
            //Console.WriteLine("Lv." + player.Level + " " + player.Name + " (" + player._job + ")");
            Console.WriteLine("Lv." + player.Level + " " + player.Name);
            Console.WriteLine("HP " + player.Hp + "/" + player.MaxHp);
            Console.WriteLine();
            Console.WriteLine("1. 공격");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">>");
            int input = Program.CheckValidInput(1, 1);

            if (input == 1)
            {
                player.UseSkill();
            }
        }
    }
}