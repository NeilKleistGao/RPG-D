using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transparent : MonoBehaviour
{
    public float alpha;
    private SpriteRenderer renderer;
    private string player_name = "Player";
    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        checkAndSetAlpha(collision.gameObject.name, alpha);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        checkAndSetAlpha(collision.gameObject.name, 1);
    }

    void checkAndSetAlpha(string name, float alp)
    {
        if (name == player_name)
        {
            Color temp = renderer.color;
            renderer.color = new Color(temp.r, temp.g, temp.b, alp);
        }
    }
}
