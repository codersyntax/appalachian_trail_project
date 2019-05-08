namespace GameLogic
{
    class Program
    {
        static void Main(string[] args)
        {
            GameManager gameManager = new GameManager();
            gameManager.Initialize();
            gameManager.StartSetup();
            gameManager.StartGameLoop();
        }
    }
}
