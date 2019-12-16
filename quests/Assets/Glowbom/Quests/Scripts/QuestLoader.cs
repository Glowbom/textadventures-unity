﻿using UnityEngine;
using System.IO;

/*
 * Created on Sun Jul 28 2019
 *
 * Copyright (c) 2019 Glowbom.
 */
public class QuestLoader
{
    public Logic logic = null;
    public Buttons buttonsLogic = null;
    public string name = "TemplateQuest";
    public string language = "";

    public void initialize() {
        if (logic == null) {
            load();
        }
    }

    public void loadButtonsLogic()
    {
        var textAsset = Resources.Load("Data/Buttons" + language) as TextAsset;
        buttonsLogic = JsonUtility.FromJson<Buttons>(textAsset.text);
    }

    public void load() {
        var textAsset = Resources.Load ("Data/" + name + language) as TextAsset;
        logic = JsonUtility.FromJson<Logic> (textAsset.text);
        loadButtonsLogic();
    }

    public void save() {
        try
        {
            using (StreamWriter sw = new StreamWriter("Assets/Glowbom/Quests/Resources/Data/" + name + language + ".txt", false))
            {
                sw.Write(JsonUtility.ToJson(logic));
            }

            using (StreamWriter sw = new StreamWriter("Assets/Glowbom/Quests/Resources/Data/Buttons" + language + ".txt", false))
            {
                sw.Write(JsonUtility.ToJson(buttonsLogic));
            }
        }
        catch (IOException e)
        {
            Debug.Log(e.Message);
        }
        finally
        {
#if UNITY_EDITOR
            UnityEditor.AssetDatabase.Refresh();
#endif
        }
    }
}
