using UnityEngine;
using System.IO;

namespace UBear
{
/// <summary>
/// Interface for objects that can be saved and loaded. 'FileName' must be provided. 
/// 'Save'd/'Load'ed object defaults to 'this'. 'SaveObject' and 'LoadObject' can be used to specify a different object to save/load.
/// Consider changing 'FilePath' and/or 'FileExtension'.
/// </summary>
public abstract class SavableObject : ScriptableObject
{
  public virtual string FilePath() => Application.persistentDataPath;
  public virtual string FileName() => name;
  public virtual string FileExtension() => ".sav";
  public virtual string SaveFilePath() => string.Concat(FilePath(), FileName(), FileExtension());
  public virtual void Save() => Save(this);

  public virtual void Save(object data)
  {
    Directory.CreateDirectory(FilePath());
    string saveData = JsonUtility.ToJson(data);

    // BinaryFormatter bf = new BinaryFormatter();
    // bf.Serialize(file, saveData);
    ///////////////////////////////////////////
    File.WriteAllText(SaveFilePath(), saveData);
  }

  public virtual void Load(object data)
  {
    if (File.Exists(SaveFilePath()))
    {
      // BinaryFormatter bf = new BinaryFormatter();
      // FileStream file = File.Open(SaveFilePath, FileMode.Open);
      // JsonUtility.FromJsonOverwrite((string)bf.Deserialize(file), this);
      // file.Close();
      //////////////////////////////////////////
      string saveData = File.ReadAllText(SaveFilePath());
      JsonUtility.FromJsonOverwrite(saveData, data);
    }
  }

}
}
