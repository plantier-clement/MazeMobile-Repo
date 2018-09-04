using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(LevelCreator))]
public class LCSubLevel : MonoBehaviour {

    private LevelCreator m_LevelCreator;
    public LevelCreator LevelCreator {
        get {
            if (m_LevelCreator == null)
                m_LevelCreator = GetComponent<LevelCreator>();
            return m_LevelCreator;
        }
    }

    [SerializeField] GameObject parent;
    public bool lockSubLevelsCreation;
  


    GameObject subLevelToAdd;
    SubLevel subLevelCmpt;
    public List<GameObject> SubLevels = new List<GameObject>();
    [HideInInspector] public GameObject SubLevelToErase;

    public void AddSubLevel() {
        subLevelToAdd = new GameObject("SubLevel_" + SubLevels.Count);
        subLevelCmpt = subLevelToAdd.AddComponent<SubLevel>();

        SubLevels.Add(subLevelToAdd);
        subLevelToAdd.transform.SetParent(parent.transform);
        subLevelToAdd.transform.localPosition = new Vector3(0, 0, 0);
        subLevelToAdd.transform.localScale = new Vector3(1, 1, 1);

        BuildStructure();
    }


    public void RemoveSubLevel() {

        if (SubLevels.Count == 0 || !SubLevels.Contains(SubLevelToErase))
            return;

        GameObject subLevelToRemove = SubLevelToErase;

        DestroyImmediate(subLevelToRemove);
        SubLevels.Remove(subLevelToRemove);

        RenameSubLevels();
    }


    void BuildStructure() {

        GameObject bridgesParent = new GameObject("Bridges");
        bridgesParent.transform.SetParent(subLevelToAdd.transform);
        bridgesParent.transform.localPosition = new Vector3(0, 0, 0);
        bridgesParent.transform.localScale = new Vector3(1, 1, 1);
        subLevelCmpt.BridgesParent = bridgesParent;


        GameObject gpeParent = new GameObject("GPE");
        gpeParent.transform.SetParent(subLevelToAdd.transform);
        gpeParent.transform.localPosition = new Vector3(0, 0, 0);
        gpeParent.transform.localScale = new Vector3(1, 1, 1);
        subLevelCmpt.GpeParent = gpeParent;


        GameObject goal = Instantiate(LevelCreator.GoalPrefab, Vector3.zero, Quaternion.identity, subLevelToAdd.transform);
        goal.transform.localPosition = new Vector3(0, 0, 0);

        GameObject start = Instantiate(LevelCreator.StartPrefab, Vector3.zero, Quaternion.identity, subLevelToAdd.transform);
        start.transform.localPosition = new Vector3(0, 0, 0);
    }


    void RenameSubLevels() {

        if (SubLevels.Count == 0)
            return;

        for (int i = 0; i < SubLevels.Count; i++) {
            SubLevels[i].name = "SubLevel_" + i;
        }

    }
}