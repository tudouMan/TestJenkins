using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;


/// <summary>
/// 游戏配置
/// </summary>
public class LoadAssetConfig : Singleton<LoadAssetConfig>
{
    private  string xmlpath = Application.streamingAssetsPath + "/AssetLoadModel.xml";

    private AssetLoadType m_CurLoadType;
    private int m_DisPathchCountMax;
    private int m_HandleCountMax;
    private Vector2 m_HudOffsetNormalMiddle;
    private Vector2 m_HudOffsetNormalHight;
    private Vector2 m_HudOffsetNormalLow;

    private Vector2 m_HudOffsetBossMiddle;
    private Vector2 m_HudOffsetBossHight;
    private Vector2 m_HudOffsetBossLow;


    public AssetLoadType CurLoadType { get => m_CurLoadType;}
    public int DisPathchCountMax { get => m_DisPathchCountMax; }
    public int HandleCountMax { get => m_HandleCountMax; }
    public Vector2 HudOffsetMiddle { get => m_HudOffsetNormalMiddle; }
    public Vector2 HudOffsetHight { get => m_HudOffsetNormalHight;}
    public Vector2 HudOffsetLow { get => m_HudOffsetNormalLow;}
    public Vector2 HudOffsetBossMiddle { get => m_HudOffsetBossMiddle;  }
    public Vector2 HudOffsetBossHight { get => m_HudOffsetBossHight; }
    public Vector2 HudOffsetBossLow { get => m_HudOffsetBossLow; }

    public LoadAssetConfig()
    {
        LoadConfig();
    }


    private void LoadConfig()
    {
        XDocument xDoc = XDocument.Load(xmlpath);
        XElement assetBundleNode = xDoc.Element("AssetModel");
        var items = assetBundleNode.Elements("Item");
        foreach (var item in items)
        {
            string type = item.Attribute("Name").Value;
            if(type == "LoadType")
            {
                m_CurLoadType = item.Attribute("Value").Value == "1" ? AssetLoadType.Build : AssetLoadType.Test;
            }else if(type == "DisPatchCountMax")
                m_DisPathchCountMax= item.Attribute("Value").Value.ToInt();
            else if (type == "HnadleCardCountMax")
                m_HandleCountMax = item.Attribute("Value").Value.ToInt();
            else if (type == "HudOffsetMiddle")
            {
                m_HudOffsetNormalMiddle = new Vector2(item.Attribute("ValueNormalX").Value.ToInt(), item.Attribute("ValueNormalY").Value.ToInt());
                m_HudOffsetBossMiddle= new Vector2(item.Attribute("ValueBossX").Value.ToInt(), item.Attribute("ValueBossY").Value.ToInt());
            }  
            else if (type == "HudOffsetHight")
            {
                m_HudOffsetNormalHight = new Vector2(item.Attribute("ValueNormalX").Value.ToInt(), item.Attribute("ValueNormalY").Value.ToInt());
                m_HudOffsetBossHight = new Vector2(item.Attribute("ValueBossX").Value.ToInt(), item.Attribute("ValueBossY").Value.ToInt());
            } 
            else if (type == "HudOffsetLow")
            {
                m_HudOffsetNormalLow = new Vector2(item.Attribute("ValueNormalX").Value.ToInt(), item.Attribute("ValueNormalY").Value.ToInt());
                m_HudOffsetBossLow = new Vector2(item.Attribute("ValueBossX").Value.ToInt(), item.Attribute("ValueBossY").Value.ToInt());
            }
                

        }

    }
}
