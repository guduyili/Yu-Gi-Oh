using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;//�༭���������ռ�
using System.IO;//�ļ���
using Excel;//��ȡexcel
using System.Data;



public class MyEditor : MonoBehaviour
{

    [MenuItem("�ҵĹ���/excelת��txt")]
   public static void ExportExcelToTxt()
    {
        //Excel�ļ���·��
        string assetPath = Application.dataPath + "/_Excel";

        //���_Excel�ļ����е�excel�ļ�
        string[] files = Directory.GetFiles(assetPath, "*.xlsx");

        for(int i = 0; i < files.Length; i++) 
        {
            files[i] = files[i].Replace('\\', '/');//��б���滻��Ϊ��б��
           

            //ͨ���ļ�����ȡ�ļ�
            using (FileStream fs = File.Open(files[i],FileMode.Open,FileAccess.Read))
            {
                //�ļ���ת��excel����
                var excelDataReader = ExcelReaderFactory.CreateOpenXmlReader(fs);

                //���excel����
                DataSet dataSet = excelDataReader.AsDataSet();

                //��ȡexecl��һ�ű�
                DataTable table = dataSet.Tables[0];

                //���������� ��ȡ�� �洢�� ��Ӧ��txt�ļ�
                readTableToTxt(files[i], table);
            }

        }



        //ˢ�±༭��
        AssetDatabase.Refresh();
    }

    private static void readTableToTxt(string filePath,DataTable table)
    {
        //��ȡ�ļ�������Ҫ�ļ���׺ ������֮������ͬ��txt�ļ���
        string fileName = Path.GetFileNameWithoutExtension(filePath);


        //txt�ļ��洢��·��
        string path = Application.dataPath + "/Resources/Data/" + fileName + ".txt";

        //�ж�Resources/Data�ļ������Ƿ��Ѿ����ڶ�Ӧ��txt�ļ�������� ��ɾ��
        if(File.Exists(path))
        {
            File.Delete(path);
        }

        //�ļ�������txt�ļ�
        using (FileStream fs = new FileStream(path, FileMode.Create))
        {
            //�ļ���תд���� ����д���ַ���
            using (StreamWriter sw = new StreamWriter(fs))
            {
                //����table
                for (int row = 0; row < table.Rows.Count; row++)
                {
                    DataRow dataRow = table.Rows[row];

                    string str = "";
                    //������
                    for (int col = 0; col < table.Columns.Count; col++)
                    {
                        string val = dataRow[col].ToString();

                        str = str + val + "\t";//ÿһ��tab�ָ�
                    }
                    //д��
                    sw.Write(str);

                    //����������һ�� ����
                    if (row != table.Rows.Count - 1)
                    {
                        sw.WriteLine();
                    }
                }
            }
        }
    }
}
