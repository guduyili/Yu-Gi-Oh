using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Ê§°Ü
public class Fight_Loss : FightUnit
{
    public override void Init()
    {
        FightManager.Instance.StopAllCoroutines();
        
    }

    public override void OnUpdate()
    {
        
    }
}
