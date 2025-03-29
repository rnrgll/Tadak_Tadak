using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WaterDrop : MonoBehaviour
{
    private CustomPool<WaterDrop> _pool;

    public void SetPool(CustomPool<WaterDrop> pool)
    {
        _pool = pool;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
       if(collision.gameObject.CompareTag("Stump"))
        {
            _pool.Return(this);
        }
    }
}
