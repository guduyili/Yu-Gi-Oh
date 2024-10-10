using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightCardManager 
{
    //ս�����ƹ�����
    public static FightCardManager Instance = new FightCardManager();

    public List<string> cardList;//���Ƽ���

    public List<string> usedCardList;//���ƶ�


    //��ʼ��
    public void Init()
    {
        cardList = new List<string>();
        usedCardList = new List<string>();

        //������ʱ����
        List<string> templist = new List<string>();
        //����ҵĿ��ƴ洢����ʱ����
        templist.AddRange(RoleManager.Instance.cardList);

        while (templist.Count > 0)
        {
            //��ʱ�±��
            int tempIndex = Random.Range(0, templist.Count);

            //��ӵ�����
            cardList.Add(templist[tempIndex]);

            //��ʱ����ɾ��
            templist.RemoveAt(tempIndex);
        }
        Debug.Log(cardList.Count);
    }

    //�Ƿ��п�
    public bool HasCard()
    {
        return cardList.Count > 0;
    }

    //�鿨
    public string DrawCard()
    {
        string id = cardList[cardList.Count - 1];

        cardList.RemoveAt(cardList.Count-1);

        return id;
    }
}
