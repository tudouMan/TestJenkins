using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using UnityEngine;

public static class MathfExtention
{
    /// <summary>
    ///Int 取绝对值
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public static int IntToAbs(this int data)
    {
        return Mathf.Abs(data);
    }

    /// <summary>
    /// 浮点数绝对值
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public static float FloatToAbs(this float data)
    {
        return Mathf.Abs(data);
    }

    /// <summary>
    /// 返回List中随机一个值
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="_list"></param>
    /// <returns></returns>
    public static T GetListRandomT<T>(this List<T> _list)
    {
        if (_list.Count <= 0) return default(T);
        else
        {
            int randomRange = UnityEngine.Random.Range(0, _list.Count);
            return _list[randomRange];
        }
 
    }

    /// <summary>
    /// 返回List中随机Count个值
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="_list"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    public static List<T>GetRandomCountListByList<T>(this List<T>_list,int count)
    {
        if (count >= _list.Count)
            count = _list.Count;

        List<T> returnList = new List<T>();
        List<int> indexList = new List<int>();
        for (int i = 0; i < _list.Count; i++)
        {
            indexList.Add(i);
        }


        for (int i = 0; i < count; i++)
        {
          int index= indexList.GetListRandomT();
            returnList.Add(_list[index]);
            indexList.Remove(index);

        }
        return returnList;
    
    }

    #region 判断当前值是否在链表中
    /// <summary>
    /// 判断当前值是否在链表中
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="t"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static bool GetValueForList<T>(this List<T> list, T value)
    {
        bool isHave = false;
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].Equals(value))
            {
                isHave = true;
                break;
            }
        }
        return isHave;
    }
    #endregion

    /// <summary>
    /// 返回枚举的title中文
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static string GetDescription(Enum obj)
    {
        string objName = obj.ToString();
        Type t = obj.GetType();
        FieldInfo fi = t.GetField(objName);
        DescriptionAttribute[] arrDesc = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

        return arrDesc[0].Description;
    }

    public static int GetRmoveFloatToInt(this float value)
    {
       int returnNum= (int)value;
        return (int)value;
    }

    public static int GetRmoveDoubleToInt(this double value)
    {
        return (int)value;
    }
}
