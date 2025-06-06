using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(EnemySpawnList))]
[CanEditMultipleObjects]
public class EnemySpawnListEditor : Editor
{
    EnemySpawning addedEnemySpawn;
    public override void OnInspectorGUI()
    {
        EnemySpawnList myTarget = (EnemySpawnList)target;
        serializedObject.Update();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("EnemySpawns"), true);
        serializedObject.ApplyModifiedProperties();
        addedEnemySpawn = (EnemySpawning)EditorGUILayout.ObjectField("Add Enemy ScriptableObject", addedEnemySpawn, typeof(EnemySpawning), true);
        if(GUILayout.Button("Add Enemy Spawn"))
        {
            if(addedEnemySpawn != null)
            {
                EnemySpawnData enemySpawnData = new EnemySpawnData(addedEnemySpawn);
                myTarget.EnemySpawns.Add(enemySpawnData);
                EditorUtility.SetDirty(target);
            }
        }
        GUILayout.Space(20);
        if(GUILayout.Button("Reobtain Properties from ScriptableObjects"))
        {
            Debug.Log("Todo: add functionality to update field values from corresponding scriptableobjects");
            //Ensure that the enemymanager creates a copy of the scriptableobject and not a reference to it
        }
    }
}
