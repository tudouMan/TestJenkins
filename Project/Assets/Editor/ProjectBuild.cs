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
        BuildPackage();
    }

    public static  void BuildPackage()
    {
        Debug.Log("*******************Build Start*******************");

        string localBuildPath = @"E:\PCBuild\BuildTestProject\TestBuild.exe";
        BuildPipeline.BuildPlayer(GetBuildScenes(), localBuildPath, BuildTarget.StandaloneWindows64, BuildOptions.None);
        Debug.Log("*******************Build End*********************");

    }


    static string[] GetBuildScenes()
    {
        return new string[] 
        {

          "Assets/Scenes/SampleScene.unity"
        };
    }


}
