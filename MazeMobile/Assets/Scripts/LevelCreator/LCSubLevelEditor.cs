using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(LCSubLevel)), CanEditMultipleObjects]
public class LCSubLevelEditor : Editor {

    int m_Width = 130;
    int m_Height = 20;
    float m_Spacing = 10;

    LCSubLevel m_LCSubLevel;


    private void OnEnable() {
        m_LCSubLevel = (LCSubLevel)target;
    }


    public override void OnInspectorGUI() {

        DrawDefaultInspector();

        if (!m_LCSubLevel.lockSubLevelsCreation) {
            if (GUILayout.Button("Add Sub-Level", GUILayout.Width(m_Width), GUILayout.Height(m_Height))) {
                m_LCSubLevel.AddSubLevel();
            }


            GUILayout.BeginHorizontal();

            m_LCSubLevel.SubLevelToErase = (GameObject)EditorGUILayout.ObjectField("Sub level to Erase", m_LCSubLevel.SubLevelToErase, typeof(GameObject), true);


            if (GUILayout.Button("Remove Selected Sub-Level", GUILayout.Width(m_Width), GUILayout.Height(m_Height))) {
                m_LCSubLevel.RemoveSubLevel();
            }
            GUILayout.EndHorizontal();
        }
        else if (m_LCSubLevel.lockSubLevelsCreation) {
            GUILayout.TextArea("SUB-LEVELS LOCKED");
        }
    }

}
#endif
