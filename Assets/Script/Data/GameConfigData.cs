using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//��Ϸ���ñ��� ÿ�������Ӧһ��txt���ñ�
public class GameConfigData : MonoBehaviour
{
    private List<Dictionary<string, string>> dataDic;//�洢���ñ��е���������

    public GameConfigData(string str)
    {
        dataDic = new List<Dictionary<string, string>>();

        //�����и�
        string[] lines = str.Split('\n');
        //��һ���Ǵ洢���ݵ�����
        string[] title = lines[0].Trim().Split('\t');//tab�и�
        //�ӵ������±�2��ʼ ��ʼ�������� �ڶ��������ǽ���˵��
         for(int i = 2;i < lines.Length;i++)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();

            string[] temArr = lines[i].Trim().Split("\t");
            
            for(int j = 0;j < temArr.Length;j++)
            {
                dic.Add(title[j], temArr[j]);
            }

            dataDic.Add(dic);
        }
    }

    public List<Dictionary<string,string>>GetLines()
    {
        return dataDic;
    }

    public Dictionary<string,string>GetOneByld(string id) 
    {
     for(int i = 0; i < dataDic.Count;i++)
        {
            Dictionary<string,string> dic = dataDic[i];
            if (dic["Id"] == id)
            {
                return dic;
            }
        }
     return null;
    }
}
