using UnityEngine;
using System;
public class CastleController : ConstructionController
{

    public static CastleController instance;
    protected override void Start()
    {
        base.Start();
        instance = this;
    }


    public void earnGold(int value)
    {
        statistics.gold += value;
    }

    public void spendGold(int value)
    {
        statistics.gold = Math.Max(statistics.gold - value, 0);
    }
    
    public int getGold()
    {
        return statistics.gold;
    }
}
