using UnityEngine;

namespace UBear
{
public class NoteComponent : MonoBehaviour
{
  // This is a multiline string that can be edited in the inspector
  // It is used to store the text of the note
  // The Multiline attribute allows for multiple lines of text
  // The text will be displayed in the inspector as a text area
  [TextArea(3, 10)]
  [SerializeField]
  [Multiline]
  public string noteText;
}

[CreateAssetMenu(fileName = "NoteAsset", menuName = "UBear/Note Asset", order = 0)]
public class NoteAsset : ScriptableObject
{
  // This is a multiline string that can be edited in the inspector
  // It is used to store the text of the note
  // The Multiline attribute allows for multiple lines of text
  // The text will be displayed in the inspector as a text area
  [TextArea(3, 10)]
  [SerializeField]
  [Multiline]
  public string noteText;
}
}