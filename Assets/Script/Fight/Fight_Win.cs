using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//ʤ��
public class Fight_Win : FightUnit
{
    public override void Init()
    {
        FightManager.Instance.StopAllCoroutines();

        UIManager.Instance.ShowUI<WinUI>("SelectCardUI");
    }
}
