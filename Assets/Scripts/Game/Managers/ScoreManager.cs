using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoSingleton<ScoreManager>
{
    private UserManager userManager;

    public override void Init()
    {
        base.Init();

        userManager = UserManager.instance;
    }

    public void AddCoin(int coin)
    {
        userManager.currentUser.Coin += coin;
    }

    public void AddScore(int score)
    {
        userManager.currentUser.Score += score;
    }

    public void AddGrowthPoint(int point)
    {
        userManager.currentUser.GrowthPoint += point;
    }

    public void AddInnovationPoint(int point)
    {
        userManager.currentUser.InnovationPoint += point;
    }

    public void AddCurrencyPoint(int point)
    {
        userManager.currentUser.CurrencyPoint += point;
    }

    public void AddSatisfactionPoint(int point)
    {
        userManager.currentUser.SatisfactionPoint += point;
    }
}
