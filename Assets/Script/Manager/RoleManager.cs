using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleManager : MonoBehaviour
{
    public static RoleManager Instance = new RoleManager();

    public List<string> cardList;//存储卡牌的id

    public  void Init()
    {
        cardList = new List<string>();
        //四张攻击卡 四张防御卡 两张效果卡
        cardList.Add("1000");
        cardList.Add("1000");
        cardList.Add("1000");
        cardList.Add("1000");

        cardList.Add("1001");
        cardList.Add("1001");
        cardList.Add("1001");
        cardList.Add("1001");

        cardList.Add("1002");
        cardList.Add("1002");
    }
}
