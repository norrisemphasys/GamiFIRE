using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoSingleton<ScoreManager>
{
    private UserManager userManager;

    private PrizeType _currentBonusType = PrizeType.NONE;
    private float _bonusPercentage = 0;

    private bool _hasUser => userManager.currentUser != null;

    public struct TempScore
    {
        public int growthPoint;
        public int innovationPoint;
        public int satsifactionPoint;
        public int coin;
        public int score;
    }

    public TempScore tempScore;

    public void ResetTempScore()
    {
        tempScore.growthPoint = 0;
        tempScore.innovationPoint = 0;
        tempScore.satsifactionPoint = 0;
        tempScore.coin = 0;
        tempScore.score = 0;
    }

    public override void Init()
    {
        base.Init();

        userManager = UserManager.instance;
    }

    public void AddCoin(int coin)
    {
        if (_hasUser)
        {
            if(GameManager.instance.currentScene == SCENE_TYPE.ISLAND_SCENE)
                userManager.currentUser.Coin += GetBonusPointByType(coin);
            else
                userManager.currentUser.Coin += coin;

            if (userManager.currentUser.Coin < 0)
                userManager.currentUser.Coin = 0;
        }

        tempScore.coin += coin;
    }

    public void AddScore(int score)
    {
        if (_hasUser)
             userManager.currentUser.Score += score;
        
        tempScore.score += score;
    }

    public void AddGrowthPoint(int point, bool save = true)
    {
        if(_hasUser && save)
        {
            userManager.currentUser.GrowthPoint += GetBonusPointByType(point);

            if (userManager.currentUser.GrowthPoint < 0)
                userManager.currentUser.GrowthPoint = 0;
        }

        tempScore.growthPoint += point;
    }

    public void AddInnovationPoint(int point, bool save = true)
    {
        if (_hasUser && save)
        {
            userManager.currentUser.InnovationPoint += GetBonusPointByType(point);

            if (userManager.currentUser.InnovationPoint < 0)
                userManager.currentUser.InnovationPoint = 0;
        }

        tempScore.innovationPoint += point;
    }

    public void AddCurrencyPoint(int point, bool save = true)
    {
        if (_hasUser && save)
        {
            userManager.currentUser.CurrencyPoint += GetBonusPointByType(point);

            if (userManager.currentUser.CurrencyPoint < 0)
                userManager.currentUser.CurrencyPoint = 0;
        }

        tempScore.coin += point;
    }

    public void AddSatisfactionPoint(int point, bool save = true)
    {
        if (_hasUser && save)
        {
            userManager.currentUser.SatisfactionPoint += GetBonusPointByType(point);

            if (userManager.currentUser.SatisfactionPoint < 0)
                userManager.currentUser.SatisfactionPoint = 0;
        }

        tempScore.satsifactionPoint += point;
    }

    public void SetBonus(float percentage, PrizeType type)
    {
        _bonusPercentage = percentage;
        _currentBonusType = type;
    }

    public int GetBonusPointByType(int value)
    {
        int newValue = value;
        switch(_currentBonusType)
        {
            case PrizeType.COIN_BOOSTER:
                float percentageValue = value * _bonusPercentage;
                newValue = Mathf.RoundToInt(percentageValue) + value;
                return newValue;
            case PrizeType.GROWTH_POINT:
            case PrizeType.INNOVATION_POINT:
            case PrizeType.SATISFACTION_POINT:
                newValue = value + (int)_bonusPercentage;
                return newValue;
        }

        return newValue;
    }
}
