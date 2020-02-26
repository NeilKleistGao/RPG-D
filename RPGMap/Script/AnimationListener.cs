using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationListener : MonoBehaviour
{
    private GameMap parent;
    // Start is called before the first frame update
    void Start()
    {
        parent = transform.parent.GetComponent<GameMap>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void end()
    {
        parent.endCallback();
    }
}
