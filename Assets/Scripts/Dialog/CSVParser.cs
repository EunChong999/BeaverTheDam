using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class CSVParser : MonoBehaviour
{
    public Dialogue[] Parse(string _Data)
    {
        List<Dialogue> dialogueList = new List<Dialogue>();
        print(_Data);
        string[] data = _Data.Split(new char[] { '\n' });
        for (int i = 0; i < data.Length;)
        {
            string[] row = data[i].Split(new char[] { ',' });

            Dialogue dialogue = new Dialogue();

            dialogue.name = row[1];

            string[] pos = row[4].Split('`');

            dialogue.pos = new Vector2(float.Parse(pos[0]),float.Parse(pos[1]));
            dialogue.CharID = row[3];

            List<string> contextList = new List<string>();

            do
            {
                contextList.Add(row[2]);
                if (++i < data.Length)
                {
                    row = data[i].Split(new char[] { ',' });
                }else
                {
                    break;
                }
            } while (row[0].ToString() == "");

            dialogue.contexts = contextList.ToArray();
            

            dialogueList.Add(dialogue);
        }
        return dialogueList.ToArray();
    }
}
