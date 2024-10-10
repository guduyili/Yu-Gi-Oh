using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightCardManager 
{
    //战斗卡牌管理器
    public static FightCardManager Instance = new FightCardManager();

    public List<string> cardList;//卡牌集合

    public List<string> usedCardList;//弃牌堆


    //初始化
    public void Init()
    {
        cardList = new List<string>();
        usedCardList = new List<string>();

        //定义临时集合
        List<string> templist = new List<string>();
        //将玩家的卡牌存储到临时集合
        templist.AddRange(RoleManager.Instance.cardList);

        while (templist.Count > 0)
        {
            //临时下标记
            int tempIndex = Random.Range(0, templist.Count);

            //添加到卡堆
            cardList.Add(templist[tempIndex]);

            //临时集合删除
            templist.RemoveAt(tempIndex);
        }
        Debug.Log(cardList.Count);
    }

    //是否有卡
    public bool HasCard()
    {
        return cardList.Count > 0;
    }

    //抽卡
    public string DrawCard()
    {
        string id = cardList[cardList.Count - 1];

        cardList.RemoveAt(cardList.Count-1);

        return id;
    }
}
