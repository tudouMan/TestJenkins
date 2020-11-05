
/**
*┌──────────────────────────────────────────────────────────────┐
*│　描   述：                                                    
*│　作   者：LIU QIANG                                              
*│　版   本：1.0                                                 
*│　创建时间：2019/12/2 12:20:43                             
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间: Assets.Scripts.UtilityTool.MethodExtension                                   
*│　类   名：ListExtention                                      
*└──────────────────────────────────────────────────────────────┘
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

public static class ListExtention
{
    public static T GetRandomValue<T>(this List<T>list)where T : class
    {
        var index= new Random().Next(list.Count);
        return list[index];
    }

    //public static Dictionary<string,T> ListToDic<T>(this List<T> list) where T : class
    //{
    //    Dictionary<string, T> dic = new Dictionary<string, T>();
    //    foreach (var item in list)
    //    {
    //       // item
    //    }
    //    return dic;
    //}

}

