using UnityEngine;

public class Enemy : Entity
{
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

}
