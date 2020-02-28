using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionTerrain : MonoBehaviour
{
    public int[] angles = new int[4];
    public bool trigger_changeable = true;
    private AreaEffector2D effector;
    // Start is called before the first frame update
    void Start()
    {
        effector = GetComponent<AreaEffector2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            //TODO:
        }
    }
}
