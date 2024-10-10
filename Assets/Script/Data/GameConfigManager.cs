using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//整个游戏的配置表的管理器
public class GameConfigManager
{
    public static GameConfigManager Instance = new GameConfigManager();

    private GameConfigData cardData;//卡牌表

    private GameConfigData enemyData;//敌人表

    private GameConfigData levelData;//关卡表


    private GameConfigData cardTypeData;//卡牌类型表


    private TextAsset textAsset;

    //初始化配置文件 （txt文件 存储到内存中）
    public void Init()
    {
        textAsset = Resources.Load<TextAsset>("Data/card");
        cardData = new GameConfigData(textAsset.text);

        textAsset = Resources.Load<TextAsset>("Data/enemy");
        enemyData = new GameConfigData(textAsset.text);

        textAsset = Resources.Load<TextAsset>("Data/level");
        levelData = new GameConfigData(textAsset.text);

        textAsset = Resources.Load<TextAsset>("Data/cardType");
        cardTypeData = new GameConfigData(textAsset.text);
        
    }

    public List<Dictionary<string, string>> GetCardLines()
    {
        return cardData.GetLines();
    }

    public List<Dictionary<string, string>> GetEnemyLines()
    {
        return enemyData.GetLines();
    }

    public List<Dictionary<string, string>> GetLevelLines()
    {
        return levelData.GetLines();
    }

    public Dictionary<string, string> GetCardByld(string id)
    {
        return cardData.GetOneByld(id);
    }

    public Dictionary<string, string> GetEnemyByld(string id)
    {
        return enemyData.GetOneByld(id);
    }

    public Dictionary<string, string> GetLevelByld(string id)
    {
        return levelData.GetOneByld(id);
    }

    public Dictionary<string,string>GetCardTypeByld(string id)
    {
        return cardTypeData.GetOneByld(id);
    }
}

