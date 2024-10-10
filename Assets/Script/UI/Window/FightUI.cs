using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;


//ս������
public class FightUI : UIBase
{
    private Text cardCountTxt;//��������
    private Text nocardCountTxt;//���ƶ�����
    private Text powerTxt;
    private Text hpTxt;
    private Image hpImg;
    private Text fyTxt;//������ֵ
    private List<CardItem> cardItemList;
    private void Awake()
    {
        cardItemList = new List<CardItem>();
        cardCountTxt = transform.Find("hasCard/icon/Text").GetComponent<Text>();
        nocardCountTxt = transform.Find("noCard/icon/Text").GetComponent<Text>();
        powerTxt = transform.Find("mana/Text").GetComponent<Text>();
        hpTxt = transform.Find("hp/moneyTxt").GetComponent<Text>();
        hpImg = transform.Find("hp/fill").GetComponent<Image>();
        fyTxt = transform.Find("hp/fangyu/Text").GetComponent<Text>();
        transform.Find("turnBtn").GetComponent<Button>().onClick.AddListener(onChangeTurnBtn);
    }

    //��һغϽ��� �л������˻غ�
    private void onChangeTurnBtn()
    {
        //ֻ����һغϲ����л�
        if(FightManager.Instance.fightUnit is Fight_PlayerTurn)
        {
            FightManager.Instance.ChangeType(FightType.Enemy);
        }
    }

    private void Start()
    {
        UpdateHp();
        UpdatePower();
        UpdateDefence();
        UpdateCardCount();
        UpdateUsedCardCount();
    }

    //����Ѫ����ʾ
    public void UpdateHp()
    {
        hpTxt.text = FightManager.Instance.CurHp + "/" + FightManager.Instance.MaxHp;
        hpImg.fillAmount = (float)FightManager.Instance.CurHp / (float)FightManager.Instance.MaxHp; 
    }


    //��������
    public void UpdatePower() {
        powerTxt.text = FightManager.Instance.CurPowerCount + "/" + FightManager.Instance.MaxPowerCount;
    }

    //��������
    public  void UpdateDefence()
    {
        fyTxt.text = FightManager.Instance.DefenseCount.ToString();
    }

    //���¿�������
    public void UpdateCardCount()
    {
        cardCountTxt.text = FightCardManager.Instance.cardList.Count.ToString();
    }

    //�������ƶ�����
     public void UpdateUsedCardCount()
    {
        nocardCountTxt.text = FightCardManager.Instance.usedCardList.Count.ToString();
    }

    //������������
    public void CreateCardItem(int count)
    {
        if(count > FightCardManager.Instance.cardList.Count) {

            count = FightCardManager.Instance.cardList.Count;
        }

        for(int i = 0; i < count; i++)
        {
            GameObject obj = Instantiate(Resources.Load("UI/CardItem"), transform) as GameObject;
            obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(-1000, 700);
            
            //var item = obj.AddComponent<CardItem>();
            


            string cardId = FightCardManager.Instance.DrawCard();
            Dictionary<string, string> data = GameConfigManager.Instance.GetCardByld(cardId);
            CardItem item = obj.AddComponent(System.Type.GetType(data["Script"])) as CardItem;
            item.Init(data);
            cardItemList.Add(item);
        }

    }


    //更新卡牌位置
    public void UpdateCardItemPos()
    {
        float offset = 800.0f / cardItemList.Count;
        Vector2 startPos = new Vector2(-cardItemList.Count / 2.0f * offset + offset * 0.5f, -700);
        for (int i = 0; i < cardItemList.Count; i++)
        {
            cardItemList[i].GetComponent<RectTransform>().DOAnchorPos(startPos, 0.5f);
            //cardItemList[i].GetComponent<RectTransform>().DOMove(new Vector3(4 * (i - 1),0f,0f),0.5f).SetEase(Ease.OutExpo).SetDelay(0.2f*i);
            //cardItemList[i].GetComponent<RectTransform>().DORotate(new Vector3(0f, 0f, 0f), 0.5f).SetEase(Ease.OutExpo).SetDelay(0.3f + 0.2f * i);
            startPos.x = startPos.x + offset;
        }
    }

    

    public void RemoveCard(CardItem item)
    {
        AudioManager.Instance.PlayEffect("Cards/cardShove");//�Ƴ���Ч

        item.enabled = false;//���ÿ����߼�

        //��ӵ����Ƽ���
        FightCardManager.Instance.usedCardList.Add(item.data["Id"]);

        //����ʹ�ú�Ŀ�������
        nocardCountTxt.text = FightCardManager.Instance.usedCardList.Count.ToString();

        //�Ӽ�����ɾ��
        cardItemList.Remove(item);

        //ˢ�¿���λ��
        UpdateCardItemPos();

        //�����Ƶ����ƶ�Ч��
        item.GetComponent<RectTransform>().DOAnchorPos(new Vector2(1000, -700), 0.25f);

        item.transform.DOScale(0, 0.25f);

        Destroy(item.gameObject,1);
    }

    //ɾ�����п���
    public void RemoveAllCards()
    {
        for(int i = cardItemList.Count - 1;i >= 0;i--) {
            RemoveCard(cardItemList[i]);
        }
    }
}
