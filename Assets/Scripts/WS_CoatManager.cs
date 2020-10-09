using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WS_CoatManager : MonoBehaviour
{
    static List<Sprite> sprites = new List<Sprite>();
    static List<int> indices = new List<int>();

    static public void Init()
    {
        for (int i = 1; i < 51; i++)
        {
            string s = "CoatOfArms/shield (";
            s += i.ToString();
            s += ")";

            sprites.Add(Resources.Load<Sprite>(s));
            indices.Add(i - 1);
        }
    }

    static public Sprite GetCoat()
    {
        int number = Random.Range(0, indices.Count);
        int index = indices[Random.Range(0, indices.Count)];
        Sprite sprite = sprites[index];

        indices.RemoveAt(number);

        if(indices.Count == 0)
            for (int i = 0; i < 50; i++)
                indices.Add(i);


        return sprite;
    }
}
