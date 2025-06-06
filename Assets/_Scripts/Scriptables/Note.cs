using UnityEngine;

[CreateAssetMenu(fileName = "Note", menuName = "Scriptable Objects/Note")]
public class Note : ScriptableObject
{
  [SerializeField]
  [TextArea(3, 10)]
  [Multiline(3)]
  public string _content;
}
