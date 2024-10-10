using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;


//UI管理器
public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    private Transform canvasTf;//画布的变换组件

    private List<UIBase> uilist;//存储加载过的界面的集合

    //private LoginUI loginUI;

    private void Awake()
    {
        Instance = this;
        //找世界中的画布
        canvasTf = GameObject.Find("Canvas").transform;
        //初始化集合
        uilist = new List<UIBase>();

        
    }


    public UIBase ShowUI<T>(string uiName)where T : UIBase
    {
        UIBase ui = Find(uiName);
        if(ui == null)
        {
            //集合中没有 需要从Resources/UI文件夹中加载
            GameObject obj = Instantiate(Resources.Load("UI/" + uiName), canvasTf) as GameObject;


            //改名字
            obj.name = uiName;

            //添加需要的脚本
            ui = obj.AddComponent<T>();

            //添加到集合进行存储
            uilist.Add(ui);
        }
        else
        {
            ui.Show();
        }
        return ui;
    }

    //隐藏
    public void HideUi(string uiName)
    {
        UIBase ui = Find(uiName);
        if(ui != null)
        {
            ui.Hide();
        }

    }

    //关闭所有界面
    public void CloseAllUI()
    {
        for(int i = 0;i <uilist.Count - 1;i++)
        {
            Destroy(uilist[i].gameObject);
        }
        //loginUI.Init();
        uilist.Clear();
    }
    //关闭某个界面
    public void CloseUI(string uiName)
    {
        UIBase ui = Find(uiName);
        if (ui != null)
        {
            uilist.Remove(ui);
            Destroy(ui.gameObject);
        }
    }

    //从集合中找到名字对应的界面脚本
    public UIBase Find(string uiName)
    {
        for (int i = 0; i < uilist.Count; i++)
        {
            if (uilist[i].name == uiName)
            {
                return uilist[i];
            }
        }

        return null;

    }


    //获取某个界面的脚本
    public T GetUI<T>(string uiName) where T : UIBase
    {
        UIBase uI = Find(uiName);
        if ((uI != null) && (uI is T))
        {
            return uI.GetComponent<T>();
        }
        return null;
    }



    //创建敌人头部的行动的图标物体
    public GameObject CreateActionIcon()
    {
        GameObject obj = Instantiate(Resources.Load("UI/actionIcon"), canvasTf) as GameObject;
        obj.transform.SetAsFirstSibling();//设置在父级的第一位
        return obj;
    }

    public GameObject CreateHpItem()
    {
        GameObject obj = Instantiate(Resources.Load("UI/HpItem"), canvasTf) as GameObject;
        obj.transform.SetAsFirstSibling();//设置在父级的第一位
        return obj;
    }


    //提示界面
    public void ShowTip(string msg,Color color,System.Action callback = null)
    {
        GameObject obj = Instantiate(Resources.Load("UI/Tips"), canvasTf) as GameObject;
        Text text = obj.transform.Find("bg/Text").GetComponent<Text>();
        text.color = color;
        text.text = msg;
        Tween scale1 = obj.transform.Find("bg").DOScale(1, 0.2f);
        Tween scale2 = obj.transform.Find("bg").DOScale(0, 0.2f);


        Sequence seq = DOTween.Sequence();
        seq.Append(scale1);
        seq.AppendInterval(0.5f);
        seq.Append(scale2);
        seq.AppendCallback(delegate ()
        {
            if (callback != null)
            {
                callback();
            }
        });
        MonoBehaviour.Destroy(obj, 1);

    }
}

