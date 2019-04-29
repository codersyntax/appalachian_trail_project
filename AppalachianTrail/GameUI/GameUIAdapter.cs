using System;
using System.Threading;

namespace GameUI
{
    public class GameUIAdapter
    {
        public void Initialize()
        {
            SetGameTitle();
            Intro();
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
            DisplayToUser("You're about to embark on a 2,190 mile journey stretching from Maine to Georgia");
            Delay();
            DisplayToUser("You've got a long road ahead so let's get started");
            Delay();
        }

        public string GetName()
        {
            DisplayToUser("What's your name? ");
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
