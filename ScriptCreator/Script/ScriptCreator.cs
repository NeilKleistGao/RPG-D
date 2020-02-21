using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ScriptCreator : EditorWindow
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [MenuItem("RPG/Add New Script _n")]
    public static void addNewScript()
    {
        ScriptCreator.CreateInstance<ScriptCreator>().Show();
    }
}
