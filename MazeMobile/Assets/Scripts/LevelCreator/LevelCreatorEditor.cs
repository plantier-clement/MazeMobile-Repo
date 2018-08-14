using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(LevelCreator))]
public class LevelCreatorEditor : Editor {


    int height = 20;
    int width = 100;
    int spacing = 20;
    LevelCreator levelCreator;

    private void OnEnable() {
        levelCreator = (LevelCreator)target;
    }


    public override void OnInspectorGUI() {
        DrawDefaultInspector();


        GUILayout.Space(spacing);
        EditorGUILayout.LabelField("Selection", EditorStyles.boldLabel);


        GUILayout.BeginHorizontal();
        GUILayout.BeginVertical();

        levelCreator.selection = (LevelCreator.ESelection)EditorGUILayout.EnumPopup("Object to spawn", levelCreator.selection);

        if (levelCreator.selection == LevelCreator.ESelection.NMEBLUE) {
            levelCreator.spawnPoint = (GameObject)EditorGUILayout.ObjectField("Spawn Point", levelCreator.spawnPoint, typeof(GameObject), true);
        }

        if (levelCreator.selection == LevelCreator.ESelection.BRIDGE) {
            levelCreator.spawnPoint = (GameObject)EditorGUILayout.ObjectField("Spawn Point (Ext)", levelCreator.spawnPoint, typeof(GameObject), true);
            levelCreator.spawnPointExtra = (GameObject)EditorGUILayout.ObjectField("Spawn Point (Int)", levelCreator.spawnPointExtra, typeof(GameObject), true);
        }

        GUILayout.EndVertical();

        if (GUILayout.Button("Build", GUILayout.Width(width), GUILayout.Height(height))) {
            levelCreator.BuildSelected();
        }

        GUILayout.EndHorizontal();
        GUILayout.Space(spacing);

        GUILayout.BeginHorizontal();
        GUILayout.BeginVertical();

        levelCreator.selectionToErase = (GameObject)EditorGUILayout.ObjectField("Object to erase", levelCreator.selectionToErase, typeof(GameObject), true);

        GUILayout.EndVertical();
        if (GUILayout.Button("Erase", GUILayout.Width(width), GUILayout.Height(height))) {
            levelCreator.EraseSelected();
        }

        GUILayout.EndHorizontal();
        GUILayout.Space(spacing);

        if (GUILayout.Button("Erase All Instances", GUILayout.Width(width), GUILayout.Height(height))) {
            levelCreator.EraseAllSelected();
        }

        GUILayout.Space(spacing);


        if (GUILayout.Button("Erase All", GUILayout.Width(width), GUILayout.Height(height))) {
            levelCreator.EraseAll();
        }

    }


}

