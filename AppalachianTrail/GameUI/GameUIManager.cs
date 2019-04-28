using System;
using System.Threading;

namespace GameUI
{
    public class GameUIManager
    {
        public GameUIManager()
        {
            StartGame();
        }

        public void StartGame()
        {
            SetGameTitle();
            Intro();
            GetName();
        }

        private void SetGameTitle()
        {
            Console.Title = GameUIConstants.ConsoleTitle;
        }

        private void Intro()
        {
            DisplayToUser("Hello traveler");
            Delay();
            DisplayToUser("And welcome to...");
            Delay();
            DisplayToUser(GameUIConstants.Title);
            Delay();
            Console.WriteLine("You're about to embark on a 2,190 mile journey stretching from Maine to Georgia");
            Delay();
            Console.WriteLine("You've got a long road ahead so let's get started");
            Delay();
        }

        private string GetName()
        {
            Console.Write("What's your name? ");
            return Console.ReadLine();
        }

        private void DisplayToUser(string message)
        {
            Console.WriteLine(message);
        }

        private void Delay(int time = 2000)
        {
            Thread.Sleep(time);
        }
    }
}
