using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class StringExtention  {

    /// <summary>
    /// 转化为int
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static int ToInt(this string str)
    {
        int temp = 0;
        int.TryParse(str, out temp);
        return temp;
    }

    /// <summary>
    /// 转化为Long
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static long ToLong(this string str)
    {
        long temp = 0;
        long.TryParse(str, out temp);
        return temp;
    }


    /// <summary>
    /// 转化为float
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static float ToFloat(this string str)
    {
        float temp;
        float.TryParse(str, out temp);
        return temp;
    }

    public static bool IsEmpty(this string str)
    {
        return str == "" || str == string.Empty || str == null;
    }

}
