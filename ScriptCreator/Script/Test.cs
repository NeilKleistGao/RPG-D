using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string path = Application.dataPath + "/temp.json";
        if (File.Exists(path))
        {
            Debug.Log("file found.");
            StreamReader reader = new StreamReader(path);
            string content = reader.ReadToEnd();
            Debug.Log(content);

            DialogList list = new DialogList();
            list = JsonUtility.FromJson<DialogList>(content);

            foreach (Content c in list.contents)
            {
                Debug.Log("Charactor Name:" + c.name);
                Debug.Log("SP Path:" + c.sp);
                Debug.Log("Effect Path:" + c.effect);

                foreach (TextToShow t in c.detail)
                {
                    Debug.Log("\"" + t.text + "\"" + " in" + t.speed.ToString());
                }

                foreach (Choice ch in c.choice)
                {
                    Debug.Log("Select " + ch.text + ": ");
                    foreach (int i in ch.next)
                    {
                        Debug.Log("To: " + i.ToString());
                    }
                    Debug.Log("---");
                }

                Debug.Log("Default:");
                foreach (int i in c.default_next)
                {
                    Debug.Log("To: " + i.ToString());
                }
                Debug.Log("---");

                Debug.Log("Condition:" + c.condition);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
