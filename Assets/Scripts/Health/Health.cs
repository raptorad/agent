using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

class Health:MonoBehaviour
{
    public int hp = 10;
    public float dyingTime = 0;
    public UnityEvent dieEvent;
    // Use this for initialization
    void Start()
    {
        if (dieEvent == null)
            dieEvent = new UnityEvent();
    }
    void OnCollisionEnter(Collision collision)
    {
        Domage d = collision.gameObject.GetComponent<Domage>();
        if(d)
        {
            hp -= d.domage;
            if(hp<=0)
            {
                Die();
            }
        }
    }
    void Die()
    {
        dieEvent.Invoke();
        Destroy(gameObject, dyingTime+0.01f);
    }
}
