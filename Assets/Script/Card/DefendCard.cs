using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


//������
public class DefendCard : CardItem
{
    public override void OnEndDrag(PointerEventData eventData)
    {

        if (TryUse() == true)
        {
            //ʹ��Ч��
            int val = int.Parse(data["Arg0"]);

            //����ʹ�ú������ ��ÿ�ſ�ʹ�õ�������ܲ�һ��)
            AudioManager.Instance.PlayEffect("Effect/healspell");


            //���ӷ�����
            FightManager.Instance.DefenseCount += val;


            //ˢ�·����ı�
            UIManager.Instance.GetUI<FightUI>("FightUI").UpdateDefence();

            Vector3 pos = Camera.main.transform.position;
            pos.y = 0;
            PlaEffect(pos);
        }
        else
        {
            base.OnEndDrag(eventData);
        }
    }
}
