using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class MapLayer
{
    public MapNavigationPin[] map_pins;
    public int[] next;
    public string back_anim_name;
    public string[] next_anim_name;
}

public class GameMap : MonoBehaviour
{
    public string player_position;
    public Vector2 player_detail_position;

    private Button back_button;
    private Stack<int> stack = new Stack<int>();
    public MapLayer[] layers;
    private Animator back_animation, front_animation;

    // Start is called before the first frame update
    void Start()
    {
        back_button = transform.Find("Back").GetComponent<Button>();
        back_animation = transform.Find("Background").GetComponent<Animator>();
        front_animation = transform.Find("Map").GetComponent<Animator>();
        stack.Push(0);

        setActive(0, true);
    }

    public void getBack()
    {
        checkAndPlay(layers[stack.Peek()].back_anim_name);
        setActive(stack.Peek(), false);
        stack.Pop();
        Debug.Log("Back");
    }

    public void getDetail(int index)
    {
        Debug.Log(index);
        int real_next = layers[stack.Peek()].next[index];
        Debug.Log(real_next);
        
        setActive(stack.Peek(), false);
        checkAndPlay(layers[stack.Peek()].next_anim_name[index]);
        stack.Push(real_next);
    }

    public void endCallback()
    {
        Debug.Log("finish");
        setActive(stack.Peek(), true);
        back_button.interactable = !(stack.Count == 1);
    }

    void setActive(int index, bool stat)
    {
        foreach (MapNavigationPin pin in layers[index].map_pins)
        {
            GameObject obj = pin.gameObject;
            obj.GetComponent<Button>().interactable = stat;
            Image image = obj.GetComponent<Image>();
            Color temp = image.color;

            if (stat && player_position.Contains(obj.name))
            {
                image.color = new Color(temp.r, temp.g, temp.b, 1);
            }
            else
            {
                image.color = new Color(temp.r, temp.g, temp.b, 0);
            }
        }
    }

    void checkAndPlay(string name)
    {
        if (back_animation != null)
        {
            back_animation.Play(name);
        }
        if (front_animation != null)
        {
            front_animation.Play(name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
