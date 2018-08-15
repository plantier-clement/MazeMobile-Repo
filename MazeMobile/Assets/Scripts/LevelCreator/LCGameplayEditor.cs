using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(LCGameplay)), CanEditMultipleObjects]
public class LCGameplayEditor : Editor {

    int m_Width = 130;
    int m_Height = 20;
    float m_Spacing = 10;

    LCGameplay m_LCGameplay;

    private void OnEnable() {
        m_LCGameplay = (LCGameplay)target;
    }


    public override void OnInspectorGUI() {

        DrawDefaultInspector();

        GUILayout.Space(m_Spacing);

        m_LCGameplay.Selection = (LCGameplay.ESelection)EditorGUILayout.EnumPopup("Object to spawn", m_LCGameplay.Selection);
        GUILayout.Space(m_Spacing);


        // ADD

        if (m_LCGameplay.Selection == LCGameplay.ESelection.BRIDGE) {
          
            GUILayout.BeginHorizontal();

            EditorGUILayout.LabelField("BRIDGE", EditorStyles.boldLabel, GUILayout.Width(m_Width));
            m_LCGameplay.LockBridge = EditorGUILayout.ToggleLeft("Lock Bridge", m_LCGameplay.LockBridge, GUILayout.Width(100));
            GUILayout.EndHorizontal();
            GUILayout.Space(m_Spacing);

            GUILayout.BeginHorizontal();

            GUILayout.BeginVertical();
            m_LCGameplay.Parent = (GameObject)EditorGUILayout.ObjectField("Parent", m_LCGameplay.Parent, typeof(GameObject), true);
            m_LCGameplay.SpawningNode = (GameObject)EditorGUILayout.ObjectField("Spawning Node (Ext)", m_LCGameplay.SpawningNode, typeof(GameObject), true);
            m_LCGameplay.ExtraSpawningNode = (GameObject)EditorGUILayout.ObjectField("Spawning Node (Int)", m_LCGameplay.ExtraSpawningNode, typeof(GameObject), true);
            GUILayout.EndVertical();

            if (GUILayout.Button("Add Selection", GUILayout.Width(m_Width - 20), GUILayout.Height(m_Height)))
                m_LCGameplay.AddSelection();
            GUILayout.EndHorizontal();
       

        } else if (m_LCGameplay.Selection == LCGameplay.ESelection.NMEBLUE) {
            GUILayout.BeginHorizontal();

            EditorGUILayout.LabelField("NME BLUE", EditorStyles.boldLabel, GUILayout.Width(m_Width));
            m_LCGameplay.LockNmeBlue = EditorGUILayout.ToggleLeft("Lock Nme Blue", m_LCGameplay.LockNmeBlue, GUILayout.Width(100));
            GUILayout.EndHorizontal();
            GUILayout.Space(m_Spacing);

            GUILayout.BeginHorizontal();
            GUILayout.BeginVertical();
            m_LCGameplay.Parent = (GameObject)EditorGUILayout.ObjectField("Parent", m_LCGameplay.Parent, typeof(GameObject), true);
            m_LCGameplay.SpawningNode = (GameObject)EditorGUILayout.ObjectField("Spawning Node (Ext)", m_LCGameplay.SpawningNode, typeof(GameObject), true);
            GUILayout.EndVertical();

            if (GUILayout.Button("Add Selection", GUILayout.Width(m_Width - 20), GUILayout.Height(m_Height)))
                m_LCGameplay.AddSelection();
            GUILayout.EndHorizontal();

        }

        GUILayout.Space(m_Spacing);

        // ERASE
        GUILayout.BeginHorizontal();

        m_LCGameplay.SelectionToErase = (GameObject)EditorGUILayout.ObjectField("Selection to Erase", m_LCGameplay.SelectionToErase, typeof(GameObject), true);

        if (GUILayout.Button("Erase Selection", GUILayout.Width(m_Width - 20), GUILayout.Height(m_Height)))
            m_LCGameplay.EraseSelection();
        GUILayout.EndHorizontal();

        GUILayout.Space(m_Spacing);

        // ERASE ALL INSTANCES
        if (m_LCGameplay.LockBridge && m_LCGameplay.Selection == LCGameplay.ESelection.BRIDGE) {
            GUILayout.TextArea("SELECTION LOCKED");
        }
        else if (m_LCGameplay.LockNmeBlue && m_LCGameplay.Selection == LCGameplay.ESelection.NMEBLUE) {
            GUILayout.TextArea("SELECTION LOCKED");
        }
        else {
            if (GUILayout.Button("Erase All Instances", GUILayout.Width(m_Width), GUILayout.Height(m_Height)))
                m_LCGameplay.EraseAllInstancesOfSelection();
        }
    }
}
#endif
