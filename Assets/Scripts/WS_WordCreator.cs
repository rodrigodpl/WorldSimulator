using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WS_WordCreator : MonoBehaviour
{
    enum SyllableForm { NONE, CONSONANT_VOWEL, CONSONANT_VOWEL_CONSONANT, VOWEL_CONSONANT};

    static char[] likelyVowels = null;
    static char[] unlikelyVowels = null;
    static char[] likelyConsonants = null;
    static char[] unlikelyConsonants = null;

    public static void Init()
    {
        likelyVowels = new char[] { 'a', 'e', 'o' };
        unlikelyVowels = new char[] { 'i', 'o', 'y' };

        likelyConsonants = new char[] { 'b', 'c', 'd', 'f', 'g', 'l', 'm', 'n', 'p', 's', 't' };
        unlikelyConsonants = new char[] { 'h', 'j', 'k', 'q', 'r', 'v', 'x', 'z' };
    }

    public static string Create()
    {
        int syllableNum = Random.Range(2, 5);

        string word = "";

        for(int i = 0; i < syllableNum; i++)
        {
            SyllableForm form = SyllableForm.NONE;
            float selector = Random.Range(0.0f, 1.0f);

            if      (selector < 0.5f)   form = SyllableForm.CONSONANT_VOWEL;
            else if (selector < 0.8f)   form = SyllableForm.CONSONANT_VOWEL_CONSONANT;
            else                        form = SyllableForm.VOWEL_CONSONANT;

            switch(form)
            {
                case SyllableForm.CONSONANT_VOWEL:
                    word += getConsonant();
                    word += getVowel();

                    break;

                case SyllableForm.CONSONANT_VOWEL_CONSONANT:
                    word += getConsonant();
                    word += getVowel();
                    word += getConsonant();

                    break;

                case SyllableForm.VOWEL_CONSONANT:
                    word += getVowel();
                    word += getConsonant();

                    break;
            }
        }

        return word;
    }

    public static char getConsonant()
    {
        float sel = Random.Range(0.0f, 1.0f);

        if(sel < 0.8f)  return likelyConsonants[Random.Range(0, likelyConsonants.Length)];
        else            return unlikelyConsonants[Random.Range(0, unlikelyConsonants.Length)];
    }

    public static char getVowel()
    {
        float sel = Random.Range(0.0f, 1.0f);

        if (sel < 0.7f) return likelyVowels[Random.Range(0, likelyVowels.Length)];
        else            return unlikelyVowels[Random.Range(0, unlikelyVowels.Length)];
    }
}
