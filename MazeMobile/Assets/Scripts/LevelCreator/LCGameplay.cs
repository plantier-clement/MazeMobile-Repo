using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LevelCreator))]
public class LCGameplay : MonoBehaviour {

    private LevelCreator m_LevelCreator;
    public LevelCreator LevelCreator {
        get {
            if (m_LevelCreator == null)
                m_LevelCreator = GetComponent<LevelCreator>();
            return m_LevelCreator;
        }
    }

    public enum ESelection {
        BRIDGE, NMEBLUE
    }


    [HideInInspector] public bool LockBridge = false;
    [HideInInspector] public bool LockNmeBlue = false;

    [HideInInspector] public ESelection Selection = ESelection.BRIDGE;
    [HideInInspector] public GameObject Parent;
    [HideInInspector] public GameObject SpawningNode;
    [HideInInspector] public GameObject ExtraSpawningNode;
    [HideInInspector] public GameObject SelectionToErase;
    [HideInInspector] public List<GameObject> Bridges = new List<GameObject>();
    [HideInInspector] public List<GameObject> NmeBlues = new List<GameObject>();

    Vector3 spawnCoordinates;


    public void AddSelection() {
        switch (Selection) {
            case ESelection.BRIDGE:
                AddBridge();
                break;

            case ESelection.NMEBLUE:
                AddNmeBlue();
                break;
        }
    }


    public void EraseSelection() {
        switch (Selection) {
            case ESelection.BRIDGE:
                EraseBridge();
                break;

            case ESelection.NMEBLUE:
                EraseNmeBlue();
                break;
        }
    }


    public void EraseAllInstancesOfSelection() {
        switch (Selection) {
            case ESelection.BRIDGE:
                //
                break;

            case ESelection.NMEBLUE:
                //
                break;
        }
    }


    void AddBridge() {

        if (!LevelCreator.LCLayer.Nodes.Contains(SpawningNode) || !LevelCreator.LCLayer.Nodes.Contains(ExtraSpawningNode))
            return;

        spawnCoordinates = Vector3.Lerp(SpawningNode.transform.position, ExtraSpawningNode.transform.position, 0.5f);

        GameObject bridgeToSpawn = Instantiate(LevelCreator.BridgePrefab, spawnCoordinates, Quaternion.identity);
        SetupBridge(bridgeToSpawn);
    }


    void SetupBridge(GameObject spawnedBridge) {
        spawnedBridge.transform.SetParent(Parent.transform);
        Bridges.Add(spawnedBridge);

        GameObject bridgeExt = spawnedBridge.transform.Find("Ext").gameObject;
        GameObject bridgeInt = spawnedBridge.transform.Find("Int").gameObject;

        bridgeExt.transform.position = SpawningNode.transform.position;
        bridgeInt.transform.position = ExtraSpawningNode.transform.position;

        BridgeDetector bridgeExtData = bridgeExt.GetComponent<BridgeDetector>();
        BridgeDetector bridgeIntData = bridgeInt.GetComponent<BridgeDetector>();

        Node nodeExt = SpawningNode.GetComponent<Node>();
        Node nodeInt = ExtraSpawningNode.GetComponent<Node>();

        bridgeExtData.LayerID = nodeExt.NodeLayerId;
        bridgeIntData.LayerID = nodeInt.NodeLayerId;

        spawnedBridge.name = "Bridge_" + nodeExt.NodeLayerId + "-" + nodeInt.NodeLayerId; 
    }


    void AddNmeBlue() {
        if (!LevelCreator.LCLayer.Nodes.Contains(SpawningNode))
            return;

        spawnCoordinates = SpawningNode.transform.position;
        GameObject nmeBlueToSpawn = Instantiate(LevelCreator.NmeBluePrefab, spawnCoordinates, Quaternion.identity);

        SetupNme(nmeBlueToSpawn, SpawningNode);
    }


    void SetupNme(GameObject spawnedNme, GameObject node) {
        spawnedNme.transform.SetParent(Parent.transform);
        NmeBlues.Add(spawnedNme);

        Nme spawnedNmeBlue = spawnedNme.GetComponent<Nme>();
        Node spawnNode = node.GetComponent<Node>();

        spawnedNmeBlue.NmeStartPosX = spawnedNme.transform.position.x;
        spawnedNmeBlue.NmeStartPosY = spawnedNme.transform.position.y;
        spawnedNmeBlue.NmeStartLayerId = spawnNode.NodeLayerId;

        spawnedNme.name = "NmeBlue_" + spawnNode.NodeLayerId + "-" + NmeBlues.IndexOf(spawnedNme);

    }


    void EraseBridge() {
        DestroyImmediate(SelectionToErase);
        Bridges.Remove(SelectionToErase);
    }


    void EraseNmeBlue() {
        DestroyImmediate(SelectionToErase);
        NmeBlues.Remove(SelectionToErase);
    }

}
