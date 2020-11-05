using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TempTest : MonoBehaviour
{
    public Image m_Image;

    private void OnGUI()
    {
        if(GUI.Button(new Rect(100, 100, 100, 100), "get"))
        {
            var bundle = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/AssetBundles/Windows/downloadasset/art/2d/icon/achievement/a011.assetbundle");
            m_Image.sprite = bundle.LoadAsset<Sprite>("a011");
        }
    }

}
