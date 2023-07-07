using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool: MonoBehaviour
{
    private GameObject m_Prefab;
    private Transform m_Parent;
    private List<GameObject> pool;

    public void InitializePool(GameObject prefab, Transform parent ,int initialPoolSze)
    {
        m_Prefab = prefab;
        m_Parent = parent;
        pool = new List<GameObject>();

        for (int i = 0; i < initialPoolSze; i++)
        {
            GameObject obj = Instantiate(m_Prefab, m_Parent);
            obj.SetActive(false);
            pool.Add(obj);
        }
    }
    
    public GameObject GetObject()
    {
        GameObject obj = pool.Find(o => !o.activeSelf);
        
        if (obj != null)
        {
            obj.SetActive(true);
            return obj;
        }
        else
        {
            GameObject newObj = Instantiate(m_Prefab, m_Parent);
            newObj.SetActive(true);
            pool.Add(newObj);
            return newObj;
        }
    }
    
    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
    }

    public void RefreshPool()
    {
        foreach (var obj in pool)
        {
            obj.SetActive(false);
        }
    }
}