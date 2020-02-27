using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public float alpha = 0.85f;
    // Start is called before the first frame update
    void Start()
    {
        PolygonCollider2D[] colliders = transform.GetComponentsInChildren<PolygonCollider2D>();
        foreach (PolygonCollider2D collider in colliders)
        {
            if (collider.isTrigger)
            {
                Transparent transparent = collider.gameObject.AddComponent<Transparent>();
                transparent.alpha = alpha;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
