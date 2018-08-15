using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCreator : MonoBehaviour {

    private LCSubLevel m_LCSubLevel;
    public LCSubLevel LCSubLevel {
        get {
            if (m_LCSubLevel == null)
                m_LCSubLevel = GetComponent<LCSubLevel>();
            return m_LCSubLevel;
        }
    }


    private LCLayer m_LCLayer;
    public LCLayer LCLayer {
        get {
            if (m_LCLayer == null)
                m_LCLayer = GetComponent<LCLayer>();
            return m_LCLayer;
        }
    }


    private LCGameplay m_LCGameplay;
    public LCGameplay LCGameplay {
        get {
            if (m_LCGameplay == null)
                m_LCGameplay = GetComponent<LCGameplay>();
            return m_LCGameplay;
        }
    }

    [Header("Prefabs")]
    public GameObject nodePrefab;
    public GameObject nmeBluePrefab;
    public GameObject bridgePrefab;




}
