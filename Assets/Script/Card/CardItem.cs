using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;


public class CardItem : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IBeginDragHandler,IDragHandler,IEndDragHandler
{
    public Dictionary<string, string> data;//卡牌信息

    public void Init(Dictionary<string,string>data)
    {
        this.data = data;
    }

    private int index;
    //鼠标进入
    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.DOScale(1.5f, 0.25f);
        index = transform.GetSiblingIndex();
        transform.SetAsLastSibling();

        transform.Find("bg").GetComponent<Image>().material.SetColor("_lineColor",Color.yellow);
        transform.Find("bg").GetComponent<Image>().material.SetFloat("_lineWidth", 10);
    }
    


    //鼠标离开
    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOScale(1, 0.25f);
        transform.SetSiblingIndex(index);

        transform.Find("bg").GetComponent<Image>().material.SetColor("_lineColor", Color.black);
        transform.Find("bg").GetComponent<Image>().material.SetFloat("_lineWidth", 1);
    }

    private void Start()
    {
        //        Id Name    Script Type    Des BgIcon  Icon Expend  Arg0 Effects
        //唯一的标识（不能重复）	名称 卡牌添加的脚本 卡牌类型的Id 描述  卡牌的背景图资源路径 图标资源的路径 消耗的费用 属性值 特效

        transform.Find("bg").GetComponent<Image>().sprite = Resources.Load<Sprite>(data["BgIcon"]);
        transform.Find("bg/icon").GetComponent<Image>().sprite = Resources.Load<Sprite>(data["Icon"]);
        transform.Find("bg/msgTxt").GetComponent<Text>().text = data["Des"];
        transform.Find("bg/nameTxt").GetComponent<Text>().text = data["Name"];
        transform.Find("bg/useTxt").GetComponent<Text>().text = data["Expend"];

        transform.Find("bg/Text").GetComponent<Text>().text = GameConfigManager.Instance.GetCardTypeByld(data["Type"])["Name"];


        //设置bg背景的image的外边框材质
        transform.Find("bg").GetComponent<Image>().material = Instantiate(Resources.Load<Material>("Mats/outLine"));
    }


    Vector2 initPos;//拖拽开始时记录卡牌的位置

    //开始拖拽
    public virtual void OnBeginDrag(PointerEventData eventData)
    {
        initPos = transform.GetComponent<RectTransform>().anchoredPosition;

        //播放声音
        AudioManager.Instance.PlayEffect("Card/draw");
    }

    //拖拽中
    public  virtual void OnDrag(PointerEventData eventData)
    {
        Vector2 pos;
        if(RectTransformUtility.ScreenPointToLocalPointInRectangle(
            transform.parent.GetComponent<RectTransform>(),
            eventData.position,
            eventData.pressEventCamera,
            out pos
            ))
        {
            transform.GetComponent<RectTransform>().anchoredPosition = pos;
        }
    }


    //结束拖拽
    public virtual void OnEndDrag(PointerEventData eventData)
    {
       transform.GetComponent<RectTransform>().anchoredPosition = initPos;
        transform.SetSiblingIndex(index);

       
    }


    //尝试使用卡牌
    public virtual bool TryUse()
    {
        //卡牌需要的费用
        int cost = int.Parse(data["Expend"]);

        if(cost > FightManager.Instance.CurPowerCount) {


            //费用不足

            AudioManager.Instance.PlayEffect("Effect/lose");//使用失败者音效

            //提示
            UIManager.Instance.ShowTip("费用不足",Color.red);

            return false;
        }

        else
        {
            //减少费用
            FightManager.Instance.CurPowerCount -= cost;
        
            //刷新费用文本
            UIManager.Instance.GetUI<FightUI>("FightUI").UpdatePower();

            //使用的卡牌删除
            UIManager.Instance.GetUI<FightUI>("FightUI").RemoveCard(this);

            return true;
        }
    }

    //创建卡牌使用后的特效
    public void PlaEffect(Vector3 pos)
    {
        GameObject effectObj = Instantiate(Resources.Load(data["Effects"]))as GameObject;
        effectObj.transform.position = pos;
        Destroy(effectObj,2);
    }

}
