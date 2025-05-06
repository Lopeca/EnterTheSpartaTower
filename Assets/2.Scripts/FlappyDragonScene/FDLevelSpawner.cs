using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FDLevelSpawner : MonoBehaviour
{
    public List<FDLevelSample> levelPrefabs;
    public Transform objBox;

    private Coroutine spawnCoroutine;
    
    public void SpawnLevels()
    {
        if (levelPrefabs == null || levelPrefabs.Count == 0) return;

        spawnCoroutine = StartCoroutine(SpawnLevelCycle());
    }

    IEnumerator SpawnLevelCycle()
    {
        while (FDGameManager.Instance.gameState == FDGameState.Play)
        {
            FDLevelSample chosenLevel = levelPrefabs[Random.Range(0, levelPrefabs.Count)];
            Instantiate(chosenLevel, transform.position + new Vector3(0, 0, 10), Quaternion.identity, objBox);
            yield return new WaitForSeconds(chosenLevel.duration);
        }
    }

    public void ClearObjs()
    {
        foreach(Transform child in objBox)
        {
            Destroy(child.gameObject);
        }
        if (spawnCoroutine != null) StopCoroutine(spawnCoroutine);
    }
}
