using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapNavigationPin : Image
{
    private PolygonCollider2D polygon;
    // Start is called before the first frame update
    protected override void Start()
    {
        polygon = GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override bool IsRaycastLocationValid(Vector2 screenPoint, Camera eventCamera)
    {
        return polygon.OverlapPoint(eventCamera.ScreenToWorldPoint(screenPoint));
    }
}
