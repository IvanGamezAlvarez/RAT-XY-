using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnTriggeredObjs : MonoBehaviour
{
    public GameObject ObjectCollider;
    public Vector2 ScaleObjColl;

    private void OnTriggerEnter2D(Collider2D collision)
    {
       if (collision.CompareTag("ActivableObj"))
        {
            ObjectCollider = collision.transform.gameObject;
            ScaleObjColl = collision.transform.localScale;
        }
    }

}
