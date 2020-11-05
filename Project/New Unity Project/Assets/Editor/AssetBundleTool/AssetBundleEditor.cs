
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class AssetBundleEditor : Editor
{
    private static string _dirName = "";

    [MenuItem("Tools/AssetBundle/Pack")]
    public static void OpenAssetBundleWindow()
    {
        AssetBundleWindow window = EditorWindow.GetWindow<AssetBundleWindow>();
        window.Show();
    }

    [MenuItem("Tools/AssetBundle/测试模式")]
    public static void SetTest()
    {
        AssetLoadModel.SetAssetModelType(AssetLoadType.Test);
    }


    [MenuItem("Tools/AssetBundle/打包模式")]
    public static void SetBuild()
    {
        AssetLoadModel.SetAssetModelType(AssetLoadType.Build);
    }



	
}

public class SelectMenu
{
	[MenuItem("Tools/LogEnable")]
	public static void SetLogEnable()
	{
		bool isSelect = GetIsSelect();
		if (isSelect)
			AssetLoadModel.SetLogEnable(false);
		else
			AssetLoadModel.SetLogEnable(true);
		OnSelectionChanged();
	}


	private const string Mark_Select = "Tools/LogEnable";

	static SelectMenu()
	{
		Selection.selectionChanged = OnSelectionChanged;
	}

	public static void OnSelectionChanged()
	{
		Menu.SetChecked(Mark_Select, GetIsSelect());
	}


	static bool GetIsSelect()
	{
		return AssetLoadModel.GetLogEnable();
	}

	
}