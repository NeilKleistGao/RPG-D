using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionalMessageBox : MonoBehaviour
{
    public Color selected_color = Color.yellow;
    public float speed = 1.0f;
    public KeyCode next_key, left_key, right_key;
    public AudioClip effect;

    private const int options_count = 4;

    private Text[] options = new Text[options_count];
    private bool[] avaliable = new bool[options_count];
    private Text message = null, speaker = null;
    private Image sp = null;
    // Start is called before the first frame update

    Text getAndClear(string name) 
    {
        Text temp = transform.Find(name).GetComponent<Text>();
        temp.text = "";
        return temp;
    }

    void Start()
    {
        message = getAndClear("Message");
        speaker = getAndClear("Name");
        sp = transform.Find("SP").GetComponent<Image>();
        sp.sprite = null;

        for (int i = 0; i < options_count; i++)
        {
            string name = "Option" + i.ToString();
            options[i] = getAndClear(name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(next_key))
        {
            //TODO:
        }
        else if (Input.GetKeyUp(left_key))
        {
            //TODO:
        }
        else if (Input.GetKeyUp(right_key))
        {
            //TODO:
        }
    }
}
