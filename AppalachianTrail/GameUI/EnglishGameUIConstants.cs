namespace GameUI
{
    public class EnglishGameUIConstants : GameUIConstants
    {
        public override string Title => @"
            
  _______ _                                       _            _     _               _______        _ _ 
 |__   __| |              /\                     | |          | |   (_)             |__   __|      (_) |
    | |  | |__   ___     /  \   _ __  _ __   __ _| | __ _  ___| |__  _  __ _ _ __      | |_ __ __ _ _| |
    | |  | '_ \ / _ \   / /\ \ | '_ \| '_ \ / _` | |/ _` |/ __| '_ \| |/ _` | '_ \     | | '__/ _` | | |
    | |  | | | |  __/  / ____ \| |_) | |_) | (_| | | (_| | (__| | | | | (_| | | | |    | | | | (_| | | |
    |_|  |_| |_|\___| /_/    \_\ .__/| .__/ \__,_|_|\__,_|\___|_| |_|_|\__,_|_| |_|    |_|_|  \__,_|_|_|
                               | |   | |                                                                
                               |_|   |_|                                                                
";

        public override string ConsoleTitle => "The Appalachian Trail";
        public override string ShopItems => "1. Water Bottle \t$3 \n" +
                                        "\t2. Sets Of Clothing \t$10 \n" +
                                        "\t3. Tent \t \t$200 \n" +
                                        "\t4. Ounces of Food \t$1 \n" +
                                        "\t5. Sleeping Bag \t$100 \n";

        public override string HelloTraveler => "\nHello traveler";
        public override string Welcome => "And welcome to...";
        public override string Intro => "You're about to embark on a 2,190 mile journey stretching from Georgia to Maine";
        public override string Start => "You've got a long road ahead so let's get started";
        public override string PlayerName => "What's your name? ";
        public override string Occupation => "What's your occupation? [Doctor, Carpenter, Student, Hippie]: ";
        public override string NotVaild => " is not a valid response, please try again...";
        public override string UserStartPoint => "When would you like to begin your trek on the AT? \n\t\t(Please specify full month or # of month ex. March or 3): ";
        public override string NotVaildStartDate => " is not a valid response, please try again...";
        public override string Date => "\n\tDate: ";
        public override string Weather => "\tWeather: ";
        public override string Health => "\tHealth: ";
        public override string Food => "\tFood: ";
        public override string Landmark => "\tNext landmark: ";
        public override string Miles => "\tMiles traveled: ";
        public override string ContinueTrail => "Enter 1 to continue on the trail";
        public override string Rest => "Enter 2 to rest";
        public override string ChangePace => "Enter 3 to change pace";
        public override string ChangeFoodRatio => "Enter 4 to change food rations";
        public override string Choice => "Your choice: ";
        public override string PurchaseSupplies => "Enter 1 to purchase some supplies";
        public override string ContinueTrailhead => "Enter 3 to continue to the next trailhead";
        public override string SpeakTownsfolk => "Enter 4 to speak with the townsfolk";
        public override string EndGame => "Congrats traveller! You made it to Rangeley, ME! Would you like to record your high score? ";
        public override string RecordHighScore => " Would you like to record your high score? ";
        public override string EnterHighScore => "Enter 1 to record your high score";
        public override string Quit => "Enter 2 to quit";
        public override string WelcomeSupplyStore => "Welcome to the supply store. What would you like to purchase?";
        public override string SupplyStoreItems => "We have the following items in stock.";
        public override string Currently => "You currently have ";
        public override string Spend => " to spend.";
        public override string PurchaseQuestion => "Would you like to purchase anything? (Y/N): ";
        public override string CurrentCart => "\n\tYour current shopping cart items";
        public override string CurrentBackpack => "\n\tYour current back pack items";
        public override string SelectItem => "Select an item to purchase [1-5]: ";
        public override string PurchaseSize => "How many would you like to purchase?: ";
        public override string InsufficientFunds => "You don't have enough money for that";
        public override string PaceQuestion => "What pace would you like to hike at?";
        public override string PaceOptions => "[1] Strenuous / [2] Steady / [3] Slow";
        public override string CurrentPace => "Your current pace: ";
        public override string FoodRationQuestion => "How much food rations would you like to consume?";
        public override string FoodRationOption => "[1] Filling / [2] Meager / [3] Bare bones";
        public override string CurrentFoodRation => "Your current food rations: ";
    }
}
