using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class PinArray
{
    public MapNavigationPin[] pin;
}

[Serializable]
public class MapPiecesArray
{
    public Sprite[] pieces;
}

public class GameMap : MonoBehaviour
{
    public Vector2Int player_position;
    public Vector2 player_detail_position;
    public Sprite player_icon;

    public int level = 1;
    private int current_index, current_level;
    public PinArray[] pins = null;
    public MapPiecesArray[] maps = null;

    public Animator in_animation, out_animation;
    public Sprite back_button_sprite;
    private Button back_button;

    // Start is called before the first frame update
    void Start()
    {
        if ((pins != null && level != pins.Length)
            || (maps != null && maps.Length != level)
            || level < 1)
        {
            Debug.LogError("error on level!");
            return;
        }

        Image map = transform.Find("Map").GetComponent<Image>();
        map.sprite = maps[0].pieces[0];
        back_button = transform.Find("Back").GetComponent<Button>();
        back_button.image.sprite = back_button_sprite;
    }

    void getBack()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        back_button.interactable = (level > 1);
    }
}
