using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class LevelCreator : MonoBehaviour {


    [System.Serializable]
    public class Layer {
        public int LayerId;
        public int NodeNumber;
        public float RadiusX;
        public float RadiusY;
        public bool isLayerBuilt = false;
    }


    [System.Serializable]
    public class SubLevel {
        public int SubLevelId;
        public bool isSubLevelBuilt = false;
    }


    [System.Serializable]
    public enum ESelection {
        NODES, NMEBLUE, BRIDGE
    }


    [Header("Prefabs")]
    [SerializeField] GameObject nodePrefab;
    [SerializeField] GameObject nmeBluePrefab;
    [SerializeField] GameObject bridgePrefab;

    [Header("Sub Levels")]
    [SerializeField] SubLevel[] subLevels;

    [Header("Layer")]
    public Layer[] layers;



    [HideInInspector]
    public ESelection selection = ESelection.NODES;
    [HideInInspector]
    public GameObject selectionToErase = null;
    [HideInInspector]
    public List<GameObject> Nodes = new List<GameObject>();
    [HideInInspector]
    public List<GameObject> NmeBlues = new List<GameObject>();
    [HideInInspector]
    public List<GameObject> Bridges = new List<GameObject>();

    public List<GameObject> LayersBuilt = new List<GameObject>();


    [HideInInspector]
    public GameObject spawnPoint;
    [HideInInspector]
    public GameObject spawnPointExtra;

    Vector3 spawnCoordinates;
    int nodesToSpawn;
    GameObject subLevelToBuild;
    GameObject layerToBuild;

    GameObject m_Goal; 
    GameObject levelCreatorGoal {
        get {
            if (m_Goal == null)
                m_Goal = GameObject.Find("GOAL");
            return m_Goal;
        }
    }



    void BuildSubLevel() {
        subLevelToBuild = new GameObject("LEVEL_" + "0");
        subLevelToBuild.transform.SetParent(GameObject.Find("GAMEPLAY").transform);
        subLevelToBuild.transform.localPosition = new Vector3(0, 0, 0);
        subLevelToBuild.transform.localScale = new Vector3(1, 1, 1);


        subLevels[0].isSubLevelBuilt = true;
    }


    void BuildLayer(int j) {
        layerToBuild = new GameObject("Layer_" + j);
        layerToBuild.transform.SetParent(subLevelToBuild.transform);
        layerToBuild.transform.localPosition = new Vector3(0, 0, 0);
        layerToBuild.transform.localScale = new Vector3(1, 1, 1);

        LayersBuilt.Add(layerToBuild);
        layers[j].isLayerBuilt = true;
    }


    void BuildNodes() {

        if (!subLevels[0].isSubLevelBuilt) {
            BuildSubLevel();
        }


        for (int j = 0; j < layers.Length; j++) {

            nodesToSpawn = layers[j].NodeNumber;
      
                BuildLayer(j);

                for (int i = 0; i < nodesToSpawn; i++) {

                    float pointNum = (i * 1.0f) / nodesToSpawn;
                    float angle = pointNum * Mathf.PI * 2;

                    float posX = Mathf.Cos(angle) * layers[j].RadiusX;
                    float posY = Mathf.Sin(angle) * layers[j].RadiusY;

                    spawnCoordinates = new Vector3(posX, posY, 0) + levelCreatorGoal.transform.position;

                    GameObject copy = Instantiate(nodePrefab, spawnCoordinates, Quaternion.identity, layerToBuild.transform);
                    SetupNode(copy, j);
                }
            

        }
    }


    void EraseAllNodes() {
        for (int i = 0; i < Nodes.Count; i++) {
            DestroyImmediate(Nodes[i]);
        }
        Nodes.Clear();

        for (int i = 0; i < LayersBuilt.Count; i++) {
            print("here");
            layers[i].isLayerBuilt = false;
            DestroyImmediate(LayersBuilt[i]);
        }

        LayersBuilt.Clear();
    }



    public void BuildSelected() {
        spawnCoordinates = Vector3.zero;

        switch (selection) {
            case ESelection.NODES:
                BuildNodes();
                break;

            case ESelection.NMEBLUE:
                BuildNmeBlue();
                break;

            case ESelection.BRIDGE:
                BuildBridge();
                break;
        }
    }


    public void EraseAllSelected() {
        switch (selection) {
            case ESelection.NODES:
                EraseAllNodes();
                break;

            case ESelection.NMEBLUE:
                EraseAllNmeBlues();
                break;

            case ESelection.BRIDGE:
                EraseAllBridges();
                break;
        }

    }


    public void EraseAll() {
        EraseAllNodes();
        EraseAllNmeBlues();
        EraseAllBridges();
    }


    public void EraseSelected() {
        if (selectionToErase == null)
            return;

        if (Nodes.Contains(selectionToErase)) {
            DestroyImmediate(selectionToErase);
            Nodes.Remove(selectionToErase);
            return;
        }

        if (NmeBlues.Contains(selectionToErase)) {
            DestroyImmediate(selectionToErase);
            NmeBlues.Remove(selectionToErase);
            return;
        }
        if (Bridges.Contains(selectionToErase)) {
            DestroyImmediate(selectionToErase);
            Bridges.Remove(selectionToErase);
            return;
        }

    }


    void BuildBridge() {
        if (spawnPoint == null || spawnPointExtra == null)
            return;

        spawnCoordinates = Vector3.Lerp(spawnPoint.transform.position, spawnPointExtra.transform.position, 0.5f);

        GameObject copy = Instantiate(bridgePrefab, spawnCoordinates, Quaternion.identity);

        SetupBridge(copy);
    }


    void SetupBridge(GameObject spawned) {
        Bridges.Add(spawned);

        GameObject bridgeExt = spawned.transform.Find("Ext").gameObject;
        GameObject bridgeInt = spawned.transform.Find("Int").gameObject;

        bridgeExt.transform.position = spawnPoint.transform.position;
        bridgeInt.transform.position = spawnPointExtra.transform.position;

        Bridge bridgeExtData = bridgeExt.GetComponent<Bridge>();
        Bridge bridgeIntData = bridgeInt.GetComponent<Bridge>();
        Node nodeExt = spawnPoint.GetComponent<Node>();
        Node nodeInt = spawnPointExtra.GetComponent<Node>();

        bridgeExtData.PosX = bridgeExt.transform.position.x;
        bridgeExtData.PosY = bridgeExt.transform.position.y;
        bridgeExtData.LayerId = nodeExt.NodeLayerId;

        bridgeIntData.PosX = bridgeInt.transform.position.x;
        bridgeIntData.PosY = bridgeInt.transform.position.y;
        bridgeIntData.LayerId = nodeInt.NodeLayerId;
    }


    void EraseAllBridges() {
        for (int i = 0; i < Bridges.Count; i++) {
            DestroyImmediate(Bridges[i]);
        }
        Bridges.Clear();
    }


  


    void SetupNode(GameObject spawned, int layerId) {

        Nodes.Add(spawned);

        Node spawnNode = spawned.GetComponent<Node>();
        spawnNode.NodePosX = spawned.transform.position.x;
        spawnNode.NodePosY = spawned.transform.position.y;
        spawnNode.NodeLayerId = layers[layerId].LayerId;

        spawned.name = "Node_" + layerId + "-" + Nodes.IndexOf(spawned);

    }


    void BuildNmeBlue() {

        if (spawnPoint == null)
            return;

        spawnCoordinates = spawnPoint.transform.position;

        GameObject copy = Instantiate(nmeBluePrefab, spawnCoordinates, Quaternion.identity);

        SetupNmeBlue(copy, spawnPoint);

    }


    void SetupNmeBlue(GameObject spawned, GameObject node) {
        NmeBlues.Add(spawned);

        Nme spawnedNmeBlue = spawned.GetComponent<Nme>();
        Node spawnNode = node.GetComponent<Node>();

        spawnedNmeBlue.NmeStartPosX = spawned.transform.position.x;
        spawnedNmeBlue.NmeStartPosY = spawned.transform.position.y;
        spawnedNmeBlue.NmeStartLayerId = spawnNode.NodeLayerId;

        spawned.name = "NmeBlue" + spawnNode.NodeLayerId + "-" + NmeBlues.IndexOf(spawned);
    }


    void EraseAllNmeBlues() {
        for (int i = 0; i < NmeBlues.Count; i++) {
            DestroyImmediate(NmeBlues[i]);
        }
        NmeBlues.Clear();
    }



    public void CheckCurrentObjects() {
        Node[] foundNodes = FindObjectsOfType<Node>();

        if (foundNodes == null) {
            Nodes.Clear();
            return;
        }
    }



}
