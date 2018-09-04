using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(LevelCreator))]
public class LCLayer : MonoBehaviour {

    private LevelCreator m_LevelCreator;
    public LevelCreator LevelCreator {
        get {
            if (m_LevelCreator == null)
                m_LevelCreator = GetComponent<LevelCreator>();
            return m_LevelCreator;
        }
    }

    public GameObject selection;
    public bool LockLayers = false;


    [HideInInspector] public List<GameObject> Layers = new List<GameObject>();
    [HideInInspector] public List<GameObject> Nodes = new List<GameObject>();

    [HideInInspector] public int NodesOnNextLayer = 8;
    [HideInInspector] public float RadiusXOnNextLayer = 50f;
    [HideInInspector] public float RadiusYOnNextLayer = 50f;
    GameObject layerToAdd;
    Vector3 spawnCoordinates;
    SubLevel subLevelCmpt;

    public void AddLayer() {

        if (!LevelCreator.LCSubLevel.SubLevels.Contains(selection))
            return;

        layerToAdd = Instantiate(LevelCreator.LayerPrefab, Vector3.zero, Quaternion.identity, selection.transform);
        layerToAdd.name = "Layer_" + Layers.Count;

        Layers.Add(layerToAdd);
        layerToAdd.transform.SetParent(selection.transform);
        layerToAdd.transform.localPosition = new Vector3(0, 0, 0);
        layerToAdd.transform.localScale = new Vector3(1, 1, 1);

        subLevelCmpt = selection.GetComponent<SubLevel>();
        subLevelCmpt.Layers.Add(layerToAdd);

        AddNodes(Layers.Last());
    }

   

    public void RemoveSelectedLayer() {
        if (!Layers.Contains(selection))
            return;

        int index = Layers.FindIndex(x => x.Equals(selection));

        for (int i = Nodes.Count - 1; i >= 0; i--) {

            Node node = Nodes[i].GetComponent<Node>();
            if (node.NodeLayerId == index) {
                DestroyImmediate(Nodes[i]);
                Nodes.Remove(Nodes[i]);
            }
        }

        subLevelCmpt = selection.GetComponentInParent<SubLevel>();
        subLevelCmpt.Layers.Remove(selection);
        DestroyImmediate(selection);
        Layers.Remove(selection);
    }


    void AddNodes(GameObject layer) {

        for (int i = 0; i < NodesOnNextLayer; i++) {

            float pointNum = (i * 1.0f) / NodesOnNextLayer;
            float angle = pointNum * Mathf.PI * 2;

            float posX = Mathf.Cos(angle) * RadiusXOnNextLayer;
            float posY = Mathf.Sin(angle) * RadiusYOnNextLayer;

            spawnCoordinates = new Vector3(posX, posY, 0) + layer.transform.position;

            GameObject copy = Instantiate(LevelCreator.NodePrefab, spawnCoordinates, Quaternion.identity, layer.transform);
            SetupNode(copy, Layers.Count - 1);
        }
    }


    void SetupNode(GameObject spawned, int layerId) {

        Nodes.Add(spawned);

        Node spawnNode = spawned.GetComponent<Node>();
        spawnNode.NodePosX = spawned.transform.position.x;
        spawnNode.NodePosY = spawned.transform.position.y;
        spawnNode.NodeLayerId = layerId;

        spawned.name = "Node_" + layerId + "-" + Nodes.IndexOf(spawned);
    }


    public void RemoveNodes() {
        if (Layers.Count == 0 || Nodes.Count == 0)
            return;

        for (int i = 0; i < Nodes.Count; i++) {
            DestroyImmediate(Nodes[i]);
        }
        Nodes.Clear();
    }
}
