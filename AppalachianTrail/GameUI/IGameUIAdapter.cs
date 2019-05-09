using GameStorage.GameValues;
using System;
using System.Collections.Generic;

namespace GameUI
{
    public interface IGameUIAdapter
    {
        void Initialize();
        string GetName();
        Occupation GetOccupation();
        Month GetStartDate();
        BackpackItem PurchaseShoppingItem(out int itemPurchaseCount);
        bool StartShopping(int wallet, Dictionary<BackpackItem, int> hikerBackpack, Dictionary<BackpackItem, int> shoppingCart);
        int DisplayGameWin();
        int DisplayGameLoss(string reasonOfDeath);
        Pace GetPaceChange(Pace currentPace);
        Ration GetRationChange(Ration currentRation);
        int DisplayLocationMenu(string newLocationName);
        int DisplayTrailSegmentProgression(DateTime currentDate, Weather currentWeather, HealthStatus currentHealthStatus, int amountOfFood, int distanceToNextLocation, int totalDistanceTraveled);
        int GetResponseOnWhetherToFightBear();
        void DisplayBearFightResolution();
        void FallOffLedge();
        void DisplayInsufficientFunds();
        void DisplayHighScoreMenu(string[] highScores);
    }
}
