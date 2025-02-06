using UnityEngine;

public class Enemy : Entity
{
    public Entity target;

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
