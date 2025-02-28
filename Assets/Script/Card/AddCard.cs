using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


//无中生有卡（抽卡效果的卡）
public class AddCard : CardItem
{
    public override void OnEndDrag(PointerEventData eventData)
    {
        if(TryUse() == true)
        {
            int val = int.Parse(data["Arg0"]);//抽卡数量

            //是否有卡抽
            if(FightCardManager.Instance.HasCard() == true) { 

             UIManager.Instance.GetUI<FightUI>("FightUI").CreateCardItem(val);

                UIManager.Instance.GetUI<FightUI>("FightUI").UpdateCardItemPos();

                Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,2.5f));

                PlaEffect(pos);
            }

            else
            {
                base.OnEndDrag(eventData);
            }
        }
        else
        {
            base.OnEndDrag(eventData);
        }

    }
}
