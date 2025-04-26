using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Readme", menuName = "Readme")]
public class Readme : ScriptableObject
{
    public Texture2D icon;
    public string title;
    public Section[] sections;
    public bool loadedLayout;

    [Serializable]
    public class Section
    {
        [TextArea(3, 10)]
        public string heading, text, linkText, url;
    }
}
