using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using System;
using System.IO;
using System.Text;
using UnityEngine;

public class AssetBundleWindow : EditorWindow
{
    AssetBundleDAL m_Dal;
    List<AssetBundleEntity> m_List;
    private Dictionary<string, bool> m_Dic;

    private string[] m_ArrTag = { "All", "Scene", "Sprite","Altas","Prefab", "Data","Audio","Materail","Texture","Proceesing","Live2D","Animation","Shader", "Font","None" };
    private int m_TagIndex = 0; //标记索引
    private int m_SelectTagIndex = -1; //选择的标记索引

    private string[] m_ArrBuildTarget = { "Windows", "Android", "Ios","Ps4" };
    private int m_SelectBuildTargetIndex = -1;  //选择的打包平台索引

#if UNITY_STANDALONE_WIN
    private BuildTarget m_Target = BuildTarget.StandaloneWindows;
    private int m_BuildTargetIndex = 0;  //平台索引
#elif UNITY_ANDROID
    private BuildTarget m_Target = BuildTarget.Android;
    private int m_BuildTargetIndex = 1;
#elif UNITY_IPONE
    private BuildTarget m_Target = BuildTarget.iOS;
    private int m_BuildTargetIndex = 2;
#endif

 

   
    public void OnEnable()
    {
        //从xml读取数据
        string xmlpath = Application.dataPath + "/DownLoadAsset/Config/AssetBundleConfig.xml";

        m_Dal = new AssetBundleDAL(xmlpath);

        m_List = m_Dal.GetList();

        m_Dic = new Dictionary<string, bool>();

        for (int i = 0; i < m_List.Count; i++)
        {
            m_Dic[m_List[i].Key] = true;
        }
    }


    private Vector2 pos;
    /// <summary>
    /// 绘制窗口
    /// </summary>
    private void OnGUI()
    {
       
        if (m_List == null)
        {
            return;
        }

        #region 按钮层
        GUILayout.BeginHorizontal("box");

        m_SelectTagIndex = EditorGUILayout.Popup(m_TagIndex, m_ArrTag, GUILayout.Width(100));
        if (m_SelectTagIndex != m_TagIndex)
        {
            m_TagIndex = m_SelectTagIndex;
            EditorApplication.delayCall = OnSelectTagCallBack;
        }

        m_SelectBuildTargetIndex = EditorGUILayout.Popup(m_BuildTargetIndex, m_ArrBuildTarget, GUILayout.Width(100));
        if (m_SelectBuildTargetIndex != m_BuildTargetIndex)
        {
            m_BuildTargetIndex = m_SelectBuildTargetIndex;
            EditorApplication.delayCall = OnSelectTargetCallBack;
        }
        //if (GUILayout.Button("清理设置", GUILayout.Width(200)))
        //{
        //    EditorApplication.delayCall = OnClearAssetBundlePathCallBack;
        //}

        if (GUILayout.Button("保存设置", GUILayout.Width(200)))
        {
            EditorApplication.delayCall = OnSaveAssetBundleCallBack;
        }

        if (GUILayout.Button("打AssetBundle包", GUILayout.Width(200)))
        {
            EditorApplication.delayCall = OnAssetBundleCallBack;
        }

        if (GUILayout.Button("清空AssetBundle包", GUILayout.Width(200)))
        {
            EditorApplication.delayCall = OnClearAssetBundleCallBack;
        }

        if (GUILayout.Button("生成版本文件", GUILayout.Width(200)))
        {
            EditorApplication.delayCall = OnCreatVerstionTextCallBack;
        }
        if (GUILayout.Button("CopyAsset", GUILayout.Width(200)))
        {
            EditorApplication.delayCall = OnCopyAssetCallBack;
        }
        GUILayout.EndHorizontal();
        #endregion

        GUILayout.BeginHorizontal("box");

        GUILayout.Label("包名");
        GUILayout.Label("标记", GUILayout.Width(100));
        GUILayout.Label("文件夹", GUILayout.Width(200));
        GUILayout.Label("初始资源", GUILayout.Width(200));

        GUILayout.EndHorizontal();

        GUILayout.BeginVertical();

        pos = EditorGUILayout.BeginScrollView(pos);
        for (int i = 0; i < m_List.Count; i++)
        {
            AssetBundleEntity entity = m_List[i];

            GUILayout.BeginHorizontal();

            m_Dic[entity.Key] = GUILayout.Toggle(m_Dic[entity.Key], "", GUILayout.Width(20));
            GUILayout.Label(entity.Name);
            GUILayout.Label(entity.Tag, GUILayout.Width(100));
            GUILayout.Label(entity.IsFolder.ToString(), GUILayout.Width(200));
            GUILayout.Label(entity.IsFirstData.ToString(), GUILayout.Width(200));


            GUILayout.EndHorizontal();

            foreach (string path in entity.PathList)
            {
                GUILayout.BeginHorizontal("box");
                GUILayout.Space(40);
                GUILayout.Label(path);
                GUILayout.EndHorizontal();
            }
        }
        EditorGUILayout.EndScrollView();

        GUILayout.EndVertical();
    }



    private void OnClearAssetBundlePathCallBack()
    {
        //需要打包的对象
        List<AssetBundleEntity> lst = new List<AssetBundleEntity>();

        foreach (AssetBundleEntity entity in m_List)
        {
            if (m_Dic[entity.Key])
            {
                entity.IsChecked = true;
                lst.Add(entity);
            }
            else
            {
                entity.IsChecked = false;
                lst.Add(entity);
            }
        }

        //循环设置信息
        for (int i = 0; i < lst.Count; i++)
        {
            AssetBundleEntity entity = lst[i];
            if (entity.IsFolder)
            {
                //文件夹，遍历文件夹
                string[] folderArr = new string[entity.PathList.Count];
                for (int j = 0; j < entity.PathList.Count; j++)
                {
                    folderArr[j] = Application.dataPath + "/" + entity.PathList[j];
                }
                SaveFolderSeeting(folderArr, true);
            }
            else
            {
                //文件
                string[] folderArr = new string[entity.PathList.Count];
                for (int j = 0; j < entity.PathList.Count; j++)
                {
                    folderArr[j] = Application.dataPath + "/" + entity.PathList[j];
                    SaveFileSetting(folderArr[j], true);
                }

            }

        }
    }
    /// <summary>
    /// 保存 设置
    /// </summary>
    public void OnSaveAssetBundleCallBack()
    {
        //需要打包的对象
        List<AssetBundleEntity> lst = new List<AssetBundleEntity>();

        foreach (AssetBundleEntity entity in m_List)
        {
            if (m_Dic[entity.Key])
            {
                entity.IsChecked = true;
                lst.Add(entity);
            }
            else
            {
                entity.IsChecked = false;
                lst.Add(entity);
            }
        }

        //循环设置信息
        for (int i = 0; i < lst.Count; i++)
        {
            AssetBundleEntity entity = lst[i];
            if (entity.IsFolder)
            {
                //文件夹，遍历文件夹
                string[] folderArr = new string[entity.PathList.Count];
                for (int j = 0; j < entity.PathList.Count; j++)
                {
                    folderArr[j] = Application.dataPath + "/" + entity.PathList[j];
                }
                SaveFolderSeeting(folderArr, !entity.IsChecked);
            }
            else
            {
                //文件
                string[] folderArr = new string[entity.PathList.Count];
                for (int j = 0; j < entity.PathList.Count; j++)
                {
                    folderArr[j] = Application.dataPath + "/" + entity.PathList[j];
                    SaveFileSetting(folderArr[j], !entity.IsChecked);
                }

            }

        }


        Debug.Log("资源标签设置完成，进行资源Build");

       

    }

    public  void AutoBuild(Action callBack)
    {
        //打标签  
        OnSaveAssetBundleCallBack();
        //清理
        OnClearAssetBundleCallBack();
        //打包
        OnAssetBundleCallBack();
        //迁移文件
        OnCopyAssetCallBack();

        callBack?.Invoke();
    }



    private void SaveFolderSeeting(string[] floderArray, bool isSetNull)
    {
        foreach (var floderPath in floderArray)
        {
            string[] arrFile = Directory.GetFiles(floderPath);  //文件夹下文件
            //文件设置
            foreach (var filePath in arrFile)
            {
                //进行设置
                SaveFileSetting(filePath, isSetNull);
            }

            //文件夹下子文件夹
            string[] arrFolder = Directory.GetDirectories(floderPath);
            SaveFolderSeeting(arrFolder, isSetNull);
        }
    }

    private void SaveFileSetting(string filePath, bool isSetNull)
    {
        
        FileInfo file = new FileInfo(filePath);
        if (!file.Extension.Equals(".meta", StringComparison.CurrentCultureIgnoreCase))
        {
            int index = filePath.IndexOf("Assets/", StringComparison.CurrentCultureIgnoreCase);

            string newPath = filePath.Substring(index);  //路径
            string fileName = string.Empty;
            if (file.Extension.Equals(""))
                fileName = newPath.Replace("Assets/", ""); //文件名
            else
                fileName = newPath.Replace("Assets/", "").Replace(file.Extension, ""); //文件名

            try
            {
                string variant = file.Extension.Equals(".unity", StringComparison.CurrentCultureIgnoreCase) ? "unity" : "assetbundle";//后缀
                AssetImporter import = AssetImporter.GetAtPath(newPath);
                import.SetAssetBundleNameAndVariant(fileName, variant);
                if (isSetNull)
                {
                    import.SetAssetBundleNameAndVariant(null, null);
                }
                import.SaveAndReimport();
            }
            catch
            {
                Debug.Log(string.Format("设置资源AB Tag 出错，请检查文件名:{0}", fileName));
            }
          
        }
    }

    /// <summary>
    /// 清空AssetBundle包
    /// </summary>
    private void OnClearAssetBundleCallBack()
    {
        string path = Application.dataPath + "/../AssetBundles/" + m_ArrBuildTarget[m_BuildTargetIndex];
        if (Directory.Exists(path))
        {
            Directory.Delete(path, true);
        }
        Debug.Log("清空完毕");
    }

    /// <summary>
    ///  打包回调
    /// </summary>
    private void OnAssetBundleCallBack()
    {
        string path = Application.dataPath + "/../AssetBundles/" + m_ArrBuildTarget[m_BuildTargetIndex];
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        BuildPipeline.BuildAssetBundles(path, BuildAssetBundleOptions.None, m_Target);
        Debug.Log("打包完毕");

    }

    //生成版本文件信息
    private void OnCreatVerstionTextCallBack()
    {
        
    }

    //复制资源到StreamingAsset
    private void OnCopyAssetCallBack()
    {
        string folder = Application.streamingAssetsPath + "/AssetBundles/";
        if (Directory.Exists(folder))
            Directory.Delete(folder,true);

        AssetDatabase.Refresh();

        string path = Application.streamingAssetsPath + "/AssetBundles/" + m_ArrBuildTarget[m_BuildTargetIndex];
        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);

      
        string inPath = Application.dataPath + "/../AssetBundles/" + m_ArrBuildTarget[m_BuildTargetIndex];
        CopyFolder(inPath, path);
        AssetDatabase.Refresh();

        Debug.Log("复制资源到StreamingAsset");
    }

    /// <summary>
    /// 选定Target回调
    /// </summary>
    private void OnSelectTargetCallBack()
    {
        switch (m_BuildTargetIndex)
        {
            case 0:
                m_Target = BuildTarget.StandaloneWindows;
                break;
            case 1:
                m_Target = BuildTarget.Android;
                break;
            case 2:
                m_Target = BuildTarget.iOS;
                break;
            case 3:
                m_Target = BuildTarget.PS4;
                break;

        }
    }

    /// <summary>
    /// 选定Tag回调
    /// </summary>
    private void OnSelectTagCallBack()
    {
        switch (m_TagIndex)
        {
            case 0://全选
                foreach (AssetBundleEntity entity in m_List)
                {
                    m_Dic[entity.Key] = true;
                }
                break;
            case 1:
                foreach (AssetBundleEntity entity in m_List)
                {                                                  //忽略大小写
                    m_Dic[entity.Key] = entity.Tag.Equals("Scene", StringComparison.CurrentCultureIgnoreCase);
                }
                break;
            case 2:
                foreach (AssetBundleEntity entity in m_List)
                {                                                  //忽略大小写
                    m_Dic[entity.Key] = entity.Tag.Equals("Sprite", StringComparison.CurrentCultureIgnoreCase);
                }
                break;
            case 3:
                foreach (AssetBundleEntity entity in m_List)
                {                                                  //忽略大小写
                    m_Dic[entity.Key] = entity.Tag.Equals("Altas", StringComparison.CurrentCultureIgnoreCase);
                }
                break;
            case 4:
                foreach (AssetBundleEntity entity in m_List)
                {                                                  //忽略大小写
                    m_Dic[entity.Key] = entity.Tag.Equals("Prefab", StringComparison.CurrentCultureIgnoreCase);
                }
                break;
            case 5:
                foreach (AssetBundleEntity entity in m_List)
                {                                                  //忽略大小写
                    m_Dic[entity.Key] = entity.Tag.Equals("Data", StringComparison.CurrentCultureIgnoreCase);
                }
                break;

            case 6:
                foreach (AssetBundleEntity entity in m_List)
                {                                                  //忽略大小写
                    m_Dic[entity.Key] = entity.Tag.Equals("Audio", StringComparison.CurrentCultureIgnoreCase);
                }
                break;

            case 7:
                foreach (AssetBundleEntity entity in m_List)
                {                                                  //忽略大小写
                    m_Dic[entity.Key] = entity.Tag.Equals("Material", StringComparison.CurrentCultureIgnoreCase);
                }
                break;

            case 8:
                foreach (AssetBundleEntity entity in m_List)
                {                                                  //忽略大小写
                    m_Dic[entity.Key] = entity.Tag.Equals("Texture", StringComparison.CurrentCultureIgnoreCase);
                }
                break;

            case 9:
                foreach (AssetBundleEntity entity in m_List)
                {                                                  //忽略大小写
                    m_Dic[entity.Key] = entity.Tag.Equals("Proceesing", StringComparison.CurrentCultureIgnoreCase);
                }
                break;
                
            case 10:
                foreach (AssetBundleEntity entity in m_List)
                {                                                  //忽略大小写
                    m_Dic[entity.Key] = entity.Tag.Equals("Live2D", StringComparison.CurrentCultureIgnoreCase);
                }
            
                break;
            case 11:
                foreach (AssetBundleEntity entity in m_List)
                {                                                  //忽略大小写
                    m_Dic[entity.Key] = entity.Tag.Equals("Animation", StringComparison.CurrentCultureIgnoreCase);
                }
                break;
            case 12:
                foreach (AssetBundleEntity entity in m_List)
                {                                                  //忽略大小写
                    m_Dic[entity.Key] = entity.Tag.Equals("Shader", StringComparison.CurrentCultureIgnoreCase);
                }
                break;

            case 13:
                foreach (AssetBundleEntity entity in m_List)
                {                                                  //忽略大小写
                    m_Dic[entity.Key] = entity.Tag.Equals("Font", StringComparison.CurrentCultureIgnoreCase);
                }
                break;

            case 14:
                foreach (AssetBundleEntity entity in m_List)
                {                                                  //忽略大小写
                    m_Dic[entity.Key] = false;
                }
                break;
        }
    }



    /// <summary>
    /// 复制文件夹所有文件
    /// </summary>
    /// <param name="sourcePath">源目录</param>
    /// <param name="destPath">目的目录</param>
    public void CopyFolder(string sourcePath, string destPath)
    {
        if (Directory.Exists(sourcePath))
        {
            if (!Directory.Exists(destPath))
            {
                //目标目录不存在则创建
                try
                {
                    Directory.CreateDirectory(destPath);
                }
                catch (Exception ex)
                {
                    throw new Exception("创建目标目录失败：" + ex.Message);
                }
            }
            //获得源文件下所有文件
            List<string> files = new List<string>(Directory.GetFiles(sourcePath));
            files.ForEach(c =>
            {
                string destFile = Path.Combine(new string[] { destPath, Path.GetFileName(c) });
                File.Copy(c, destFile, true);//覆盖模式
            });
            //获得源文件下所有目录文件
            List<string> folders = new List<string>(Directory.GetDirectories(sourcePath));
            folders.ForEach(c =>
            {
                string destDir = Path.Combine(new string[] { destPath, Path.GetFileName(c) });
                //采用递归的方法实现
                CopyFolder(c, destDir);
            });

        }
    }
}
