using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;


//<summary>
//���˹�����
//</summary>
public class EnemyManager
{
    public static EnemyManager Instance = new EnemyManager();

    private List<Enemy> enemyList;//�洢ս���еĵ���




    //<summary>
    //���ص��˹�����
    //</summary>
    //<param name = "id">�ؿ�Id</param>
    public void LoadRes(string id)
    {

        enemyList = new List<Enemy>();
        //Id	Name	EnemyIds	Pos	
        //10003	3	10001=10002=10003	3,0,1=0,0,1=-3,0,1	
        //��ȡ�ؿ���
        Dictionary<string, string> levelData = GameConfigManager.Instance.GetLevelByld(id);

        //���˵�id��Ϣ
        string[] enemyIds = levelData["EnemyIds"].Split('=');


        string[] enemyPos = levelData["Pos"].Split('=');//����λ����Ϣ


        for (int i = 0; i < enemyIds.Length; i++)
        {
            string enemyId = enemyIds[i];
            string[] posArr = enemyPos[i].Split(',');
            //����λ��
            float x = float.Parse(posArr[0]);
            float y = float.Parse(posArr[1]);
            float z = float.Parse(posArr[2]);


            //���ݵ���id ��õ���������Ϣ
            Dictionary<string, string> enemyData = GameConfigManager.Instance.GetEnemyByld(enemyId);

            GameObject obj = Object.Instantiate(Resources.Load(enemyData["Model"])) as GameObject;//����Դ·�����ض�Ӧ�ĵ���

            Enemy enemy = obj.AddComponent<Enemy>();

            enemy.Init(enemyData);//�洢������Ϣ

            //�洢�ļ���
            enemyList.Add(enemy);

            obj.transform.localPosition = new Vector3(x, y, z);
        }
    }


    //�Ƴ�����
    public void DeleteEnemy(Enemy enemy) {

        enemyList.Remove(enemy);

        //�������Ƿ��ɱ���Թ����ж�

        if(enemyList.Count == 0 )
        {
            FightManager.Instance.ChangeType(FightType.Win);
        }
    }


    //ִ�л��ŵĹ������Ϊ
    public IEnumerator DoAllEnemyAction()
    {
        for(int i = 0;i < enemyList.Count;i++)
        {
            yield return FightManager.Instance.StartCoroutine(enemyList[i].DoAction());
        }    

        //�ж���� �������е��˵���Ϊ
        for(int i = 0; i < enemyList.Count;i++)
        {
            enemyList[i].SetRandomAction();
        }

        //�л��� ��һغ�
        FightManager.Instance.ChangeType(FightType.Player);
    }
      
        

      
     
      

 
}
