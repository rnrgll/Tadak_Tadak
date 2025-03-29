using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomPool<T> where T : MonoBehaviour
{
    public Stack<T> pool  = new Stack<T>();
    private T _prefab;
    public int poolSize;
    
    public CustomPool(int size, T prefab)
    {
        poolSize = size;
        _prefab = prefab;
        
        for (int i = 0; i < size; i++)
        {
            T obj = Object.Instantiate(prefab);
            obj.gameObject.SetActive(false);
            pool.Push(obj);
        }
    }
    
    public T Get()
    {
        if (pool.Count > 0)
        {
            T target = pool.Pop();
            target.gameObject.SetActive(true);
            return target;
        }

        else
        {
            Debug.Log("Pool empty");
            return null;
        }
    }

    public void Return(T target)
    {
        if (pool.Count < poolSize)
        {
            target.gameObject.SetActive(false);
            pool.Push(target);
        }

        else
        {
            Debug.Log("Pool full"); // 발생하지 않을걸....? 아마....
        }
    }
}
