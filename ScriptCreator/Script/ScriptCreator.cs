using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ScriptCreator : EditorWindow
{
    public string file_path = "/";
    public DialogList list = new DialogList();
    public int dialog_index = 0, para_index = 0, choice_index = -1;
    public string[] dialog_pop = new string[1] {"#0"}, para_pop = new string[1] {"#0"}, choice_pop = null;

    [MenuItem("RPG-D/Edit Script _e")]
    public static void addNewScript()
    {
        ScriptCreator.CreateInstance<ScriptCreator>().Show();
    }

    string ColorToString(Color color)
    {
        int r = Mathf.RoundToInt(color.r * 255.0f),
            g = Mathf.RoundToInt(color.g * 255.0f),
            b = Mathf.RoundToInt(color.b * 255.0f);
        return string.Format("#{0:X2}{1:X2}{2:X2}", r, g, b);
    }

    Color StringToColor(string str)
    {
        if (str == "")
        {
            return Color.black;
        }

        int r = byte.Parse(str.Substring(1, 2), System.Globalization.NumberStyles.HexNumber),
            g = byte.Parse(str.Substring(3, 2), System.Globalization.NumberStyles.HexNumber),
            b = byte.Parse(str.Substring(5, 2), System.Globalization.NumberStyles.HexNumber);
        return new Color(r / 255.0f, g / 255.0f, b / 255.0f);
    }

    string IntArrayToString(int[] arr)
    {
        string str = arr[0].ToString();
        for (int i = 1; i < list.contents[dialog_index].default_next.Length; i++)
        {
            str += "," + arr[i].ToString();
        }

        return str;
    }

    int[] StringToIntArray(string str)
    {
        string[] indexs = str.Split(',');
        int[] arr = new int[indexs.Length];
        for (int i = 0; i < indexs.Length; i++)
        {
            arr[i] = int.Parse(indexs[i]);
        }

        return arr;
    }

    void addItem<T>(ref T[] items, ref string[] pop, ref int index) where T : new()
    {
        if (index == -1)
        {
            items = new T[1] {new T()};
            pop = new string[1] {"#0"};
            index = 0;
            return;
        }

        T[] more = new T[items.Length + 1];
        items.CopyTo(more, 0);
        items = more;
        items[items.Length - 1] = new T();

        string[] inc = new string[pop.Length + 1];
        pop.CopyTo(inc, 0);
        pop = inc;
        pop[pop.Length - 1] = "#" + (pop.Length - 1).ToString();

        index = items.Length - 1;
    }

    void removeItem<T>(ref T[] items, ref string[] pop, ref int index) where T : new()
    {
        if (pop.Length == 1)
        {
            pop = null;
            index = -1;
            items = null;
        }

        pop = new string[pop.Length - 1];
        for (int i = 0; i < pop.Length; i++)
        {
            pop[i] = "#" + i.ToString();
        }

        T[] less = new T[items.Length - 1];
        for (int i = 0, j = 0; i < items.Length; i++)
        {
            if (i != index)
            {
                less[j] = items[i];
                j++;
            }
        }

        items = less;
        index = pop.Length - 1;
    }

    void bindArray(string label, ref int[] arr)
    {
        string temp = IntArrayToString(arr);
        EditorGUILayout.LabelField(label);
        temp = EditorGUILayout.TextField(temp);
        arr = StringToIntArray(temp);
    }

    void bindPopUp(string[] items, ref int index)
    {
        index = EditorGUILayout.Popup(index, items);
    }

    void bindText(string label, ref string text)
    {
        EditorGUILayout.LabelField(label);
        text = EditorGUILayout.TextField(text);
    }

    void bindNumber(string label, ref float number)
    {
        EditorGUILayout.LabelField(label);
        number = EditorGUILayout.FloatField(number);
    }

    void bindColor(string label, ref string color_code)
    {
        EditorGUILayout.LabelField(label);
        color_code = ColorToString(EditorGUILayout.ColorField(StringToColor(color_code)));
    }

    void resetIndex(int top, ref string[] pop, ref int index)
    {
        pop = new string[top];
        for (int i = 0; i < top; i++)
        {
            pop[i] = "#" + i.ToString();
        }
        if (index >= top)
        {
            index = top - 1;
        }
    }

    public void OnGUI()
    {
        EditorGUILayout.LabelField("File Path:");
        file_path = EditorGUILayout.TextField(file_path);
        
        if (GUILayout.Button("open"))
        {
            string real_path = Application.dataPath + file_path;
            if (File.Exists(real_path))
            {
                StreamReader reader = new StreamReader(real_path);
                string content = reader.ReadToEnd();
                reader.Close();

                list = JsonUtility.FromJson<DialogList>(content);

                dialog_pop = new string[list.contents.Length];
                for (int i = 0; i < list.contents.Length; i++)
                {
                    dialog_pop[i] = "#" + i.ToString();
                }

                dialog_index = 0;
            }
            else
            {
                file_path = "/";
                Debug.LogError("can't open " + real_path);
            }
        }

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.BeginVertical();

        if (GUILayout.Button("add new dialog content")) 
        {
            addItem<Content>(ref list.contents, ref dialog_pop, ref dialog_index);
        }

        bindPopUp(dialog_pop, ref dialog_index);
        bindText("Name:", ref list.contents[dialog_index].name);
        bindText("SP Path:", ref list.contents[dialog_index].sp);
        bindText("Effect Path:", ref list.contents[dialog_index].effect);
        bindText("Condition:", ref list.contents[dialog_index].condition);
        bindArray("Default Next:", ref list.contents[dialog_index].default_next);

        if (list.contents.Length > 1)
        {
            if (GUILayout.Button("delete this dialog"))
            {
                removeItem<Content>(ref list.contents, ref dialog_pop, ref dialog_index);
            }
        }

        EditorGUILayout.EndVertical();
        EditorGUILayout.BeginVertical();

        if (GUILayout.Button("add new paragraph"))
        {
            addItem<TextToShow>(ref list.contents[dialog_index].detail, ref para_pop, ref para_index);
        }

        resetIndex(list.contents[dialog_index].detail.Length, ref para_pop, ref para_index);
        bindPopUp(para_pop, ref para_index);
        bindText("Text:", ref list.contents[dialog_index].detail[para_index].text);
        bindNumber("Speed:", ref list.contents[dialog_index].detail[para_index].speed);
        bindColor("Color:", ref list.contents[dialog_index].detail[para_index].color);

        if (list.contents[dialog_index].detail.Length > 1)
        {
            if (GUILayout.Button("delete this paragraph"))
            {
                removeItem<TextToShow>(ref list.contents[dialog_index].detail, ref para_pop, ref para_index);
            }
        }

        EditorGUILayout.EndVertical();
        EditorGUILayout.BeginVertical();

        if (GUILayout.Button("add new choice"))
        {
            addItem<Choice>(ref list.contents[dialog_index].choice, ref choice_pop, ref choice_index);
        }

        if (list.contents[dialog_index].choice == null)
        {
            choice_pop = null;
            choice_index = -1;
        }
        else if (choice_index == -1)
        {
            choice_index = 0;
        }

        if (choice_index > -1)
        {
            resetIndex(list.contents[dialog_index].choice.Length, ref choice_pop, ref choice_index);
            bindPopUp(choice_pop, ref choice_index);
            bindText("Text:", ref list.contents[dialog_index].choice[choice_index].text);
            bindArray("Next:", ref list.contents[dialog_index].choice[choice_index].next);

            if (GUILayout.Button("delete this choice"))
            {
                removeItem<Choice>(ref list.contents[dialog_index].choice, ref choice_pop, ref choice_index);
            }
        }

        EditorGUILayout.EndVertical();
        EditorGUILayout.EndHorizontal();

        if (GUILayout.Button("save"))
        {
            if (file_path.Length < 7 || file_path[0] != '/' || file_path.Substring(file_path.Length - 5, 5) != ".json")
            {
                Debug.LogError("invalid file path!");
            }
            else
            {
                string content = JsonUtility.ToJson(list);
                StreamWriter writer = new StreamWriter(Application.dataPath + file_path);
                writer.Write(content);
                writer.Close();
                this.Close();
            }
        }
    }

    public void OnDestroy()
    {
        file_path = "/";
        list = new DialogList();
        dialog_index = 0; para_index = 0; choice_index = -1;
        dialog_pop = new string[1] { "#0" }; para_pop = new string[1] { "#0" }; choice_pop = null;
    }
}
