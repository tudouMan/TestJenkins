
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class AssetBundleEntity 
{
    public string Key;
    public string Name;
    public string Tag;
    public bool IsFolder;
    public bool IsFirstData;
    public bool IsChecked; 
    public List<string> PathList = new List<string>();


}