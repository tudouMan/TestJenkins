using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using UnityEngine;

    public enum AssetLoadType
    {
        /// <summary>
        /// 测试模式
        /// </summary>
        Test,

        /// <summary>
        /// 打包模式
        /// </summary>
        Build,
    }


public static class AssetLoadModel 
{
    public static string xmlpath = Application.streamingAssetsPath + "/AssetLoadModel.xml";

    public static void SetAssetModelType(AssetLoadType buildType)
    {
        string setStr = buildType == AssetLoadType.Build ? "1" : "0";
        XDocument xDoc = XDocument.Load(xmlpath);
        XElement assetBundleNode = xDoc.Element("AssetModel");
        var loadTypeItems =  assetBundleNode.Elements("Item");
        foreach (var item in loadTypeItems)
        {
            if(item.Attribute("Name").Value== "LoadType")
            {
                item.SetAttributeValue("Value", setStr);
            }
           
        }
       
        xDoc.Save(xmlpath); 
    }




    public static AssetLoadType GetLoadType()
    {
        //XDocument xDoc = XDocument.Load(xmlpath);
        //XElement assetBundleNode = xDoc.Element("AssetModel");
        //XElement loadTypeItem = assetBundleNode.Element("Item");
        //string loadStr = loadTypeItem.Attribute("Value").Value;
        //return loadStr == "1" ? AssetLoadType.Build : AssetLoadType.Test;
        return LoadAssetConfig.Instance.CurLoadType;
    }


    public static bool GetLogEnable()
    {
        XDocument xDoc = XDocument.Load(xmlpath);
        XElement assetBundleNode = xDoc.Element("AssetModel");
        var items = assetBundleNode.Elements("Item");
        foreach (var item in items)
        {
            string type = item.Attribute("Name").Value;
            if (type == "LogEnable")
            {
               return item.Attribute("Value").Value == "1"?true:false;
            }

        }
        return false;
    }

    public static void SetLogEnable(bool isEnable)
    {
        string setStr = isEnable ==true ? "1" : "0";
        XDocument xDoc = XDocument.Load(xmlpath);
        XElement assetBundleNode = xDoc.Element("AssetModel");
        var loadTypeItems = assetBundleNode.Elements("Item");
        foreach (var item in loadTypeItems)
        {
            if (item.Attribute("Name").Value == "LogEnable")
            {
                item.SetAttributeValue("Value", setStr);
            }

        }

        xDoc.Save(xmlpath);
    }


}

