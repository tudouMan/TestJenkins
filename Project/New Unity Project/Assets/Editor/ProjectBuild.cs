using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;

public class ProjectBuild:Editor
{

    [MenuItem("Tools/BuildPackage")]
    public static void BuildEditor()
    {
        BuildAsset();
    }

    public static  void BuildPackage()
    {
        Debug.Log("*******************Build Start*******************");

        string localBuildPath = @"E:\PCBuild\BuildTestProject\TestBuild.exe";
        BuildPipeline.BuildPlayer(GetBuildScenes(), localBuildPath, BuildTarget.StandaloneWindows64, BuildOptions.None);
        Debug.Log("*******************Build End*********************");

        //OpenSteam Push
        Application.OpenURL(@"E:\PCBuild\新建文本文档.bat");
    }

    private static void BuildAsset()
    {
        AssetBundleWindow window = new AssetBundleWindow();
        window.AutoBuild(() => BuildPackage());
    }

    static string[] GetBuildScenes()
    {
        //List<string> pathList = new List<string>();
        //foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
        //{
        //    if (scene.enabled)
        //    {
        //        pathList.Add(scene.path);
        //    }
        //}
        //return pathList.ToArray();
        return new string[] 
        {

          "Assets/Scenes/SampleScene.unity"
        };
    }


}
