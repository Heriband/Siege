using UnityEngine;


public class CastleController : ConstructionController
{

    public static CastleController instance;
    protected override void Start()
    {
        base.Start();
        instance = this;
    }

}
