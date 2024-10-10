using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//游戏入口脚本
public class GameApp : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //初始化配置表
        GameConfigManager.Instance.Init();

        //初始化音频管理器
        AudioManager.Instance.Init();


        //初始化用户信息
        RoleManager.Instance.Init();
        
        //显示loginUI 创建脚本的名字记得和预制体的名字一样
        //UIManager.Instance.ShowUI<LoginUI>("LoginUI");

        //播放BGM
        AudioManager.Instance.PlayBGM("bgm1");

        ////test
        //string name = GameConfigManager.Instance.GetCardByld("1001")["Name"];
        //print(name);



        FightManager.Instance.ChangeType(FightType.Init);
    }

   
}
