using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(LCLayer)), CanEditMultipleObjects]
public class LCLayerEditor : Editor {

    int m_Width = 130;
    int m_Height = 20;
    float m_Spacing = 10;

    LCLayer m_LCLayer;


    private void OnEnable() {
        m_LCLayer = (LCLayer)target;
    }


    public override void OnInspectorGUI() {

        DrawDefaultInspector();

        if (!m_LCLayer.LockLayers) {

            GUILayout.BeginHorizontal();

            if (!m_LCLayer.LevelCreator.LCSubLevel.SubLevels.Contains(m_LCLayer.selection)) {
                GUILayout.TextArea("To add layers, select a Sub-Level as parent");

            }
            else {
                GUILayout.BeginVertical();

                m_LCLayer.NodesOnNextLayer = (int)EditorGUILayout.IntField("Nodes Number", m_LCLayer.NodesOnNextLayer);
             //   m_LCLayer.RadiusXOnNextLayer = (float)EditorGUILayout.FloatField("Layer Radius X", m_LCLayer.RadiusXOnNextLayer);
              //  m_LCLayer.RadiusYOnNextLayer = (float)EditorGUILayout.FloatField("Layer Radius Y", m_LCLayer.RadiusYOnNextLayer);

                GUILayout.EndVertical();

                if (GUILayout.Button("Add Layer", GUILayout.Width(m_Width), GUILayout.Height(m_Height))) {
                    m_LCLayer.AddLayer();
                }
            }

            GUILayout.EndHorizontal();

            GUILayout.Space(m_Spacing);
            GUILayout.BeginVertical();
            if (m_LCLayer.selection != null && m_LCLayer.Layers.Contains(m_LCLayer.selection)) {
                if (GUILayout.Button("Remove selected layer", GUILayout.Width(m_Width + 5), GUILayout.Height(m_Height))) {
                    m_LCLayer.RemoveSelectedLayer();
                }
            }
            else {
                GUILayout.TextArea("To remove a layer, select a layer");
            }

          

            GUILayout.EndVertical();

        }
        else if (m_LCLayer.LockLayers) {
            GUILayout.TextArea("LAYERS LOCKED");
        }
    }


}
#endif
