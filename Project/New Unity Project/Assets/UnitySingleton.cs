using UnityEngine;
using System.Collections.Generic;
using System;
using System.Collections;

public class UnitySingleton<T> : MonoBehaviour
        where T : Component
{
    protected static T _instance;
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType(typeof(T)) as T;
                if (_instance == null)
                {
                    GameObject obj = new GameObject(typeof(T).Name);
                    _instance = (T)obj.AddComponent(typeof(T));
                }
            }
            return _instance;
        }

        set
        {
            _instance = value;
        }

    }
    public static T GetInstance()
    {
        if (_instance == null)
        {
            _instance = FindObjectOfType(typeof(T)) as T;
            if (_instance == null)
            {
                GameObject obj = new GameObject(typeof(T).Name);
                 obj.hideFlags = HideFlags.DontSave;
                _instance = (T)obj.AddComponent(typeof(T));
            }
        }
        return _instance;
    }
    public static bool GetInstanceIsNull
    {
        get
        {
            return _instance == null;
        }
    }

    public virtual void Awake()
    {
        Init();
        DontDestroyOnLoad(this.gameObject);
        if (_instance == null)
        {
            _instance = this as T;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public virtual void Init()
    {

    }

   
    private void OnDestroy()
    {
        Release();
        _instance = null;
    }
   
    /// <summary>
    /// 单例释放
    /// </summary>
    public virtual void Release()
    {
     
    }


}



public class Singleton<T> where T : class, new()
{
    //
    // Static Fields
    //
    protected static T m_Instance;
    //
    // Static Properties
    //
    public static T Instance
    {
        get
        {
            if (m_Instance == null)
            {
                m_Instance = new T();
            }
            return m_Instance;
        }
    }

    public static T GetInstance()
    {
        return Instance;
    }

    public static bool GetInstanceIsNull
    {
        get
        {
            return m_Instance == null;
        }
    }
}

