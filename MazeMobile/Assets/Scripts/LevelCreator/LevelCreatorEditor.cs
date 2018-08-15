using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;


[CustomEditor(typeof(LevelCreator)), CanEditMultipleObjects]
public class LevelCreatorEditor : Editor {

    int m_Width = 130;
    int m_Height = 20;
    float m_Spacing = 10;

    LevelCreator levelCreator;


    private void OnEnable() {
        levelCreator = (LevelCreator)target;
    }


    public override void OnInspectorGUI() {
        DrawDefaultInspector();

    }



}
#endif

