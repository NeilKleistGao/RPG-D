using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Choice
{
    public string text;
    public int[] next; // special value: next == -1 -> end dialog

    public Choice()
    {
        text = "";
        next = new int[1] {0}; 
    }
}

[Serializable]
public class TextToShow
{
    public string text;
    public float speed; // special value: speed == 0 -> use default speed
    public string color; // special value: color == "" -> use default color

    public TextToShow()
    {
        text = "";
        color = "";
        speed = 0.0f;
    }
}

[Serializable]
public class Content
{
    public string name; // special value: name == "" -> inherit from last
    public string sp; // special value: sp == "" -> inherit from last
    public string effect; // special value: effect == "" -> inherit from last
    public TextToShow[] detail;
    public Choice[] choice;
    public int[] default_next; // special value: default_next == -1 -> end dialog
    public string condition; // special value: condition == "" -> default

    public Content()
    {
        name = "";
        sp = "";
        effect = "";
        detail = new TextToShow[1]{new TextToShow()};
        choice = null;
        default_next = new int[1]{0};
        condition = "";
    }
}

[Serializable]
public class DialogList
{
    public Content[] contents;

    public DialogList()
    {
        contents = new Content[1] {new Content()};
    }
}
