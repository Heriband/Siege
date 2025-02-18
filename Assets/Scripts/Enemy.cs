using UnityEngine;

public class Enemy : Entity
{

    public int amountGold = 10;

    protected override void Start()
    {
        base.Start();
        target = CastleController.instance;
    }


    protected override void Update()
    {
        if(target == null){
            target = CastleController.instance;
        }
        base.MoveRoutine(target);
    }

    protected override void Die()
    {
        
        CastleController.instance.earnGold(amountGold);
        base.Die();
    }

}
