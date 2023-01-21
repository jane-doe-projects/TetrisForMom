using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnControl : MonoBehaviour
{
    public GameObject[] blocks;
    private int blockRangeSize;
    public Transform blockParent;

    // Start is called before the first frame update
    void Start()
    {
        blockRangeSize = blocks.Length;
        SpawnRandomBlock();
    }

    public void SpawnRandomBlock()
    {
        int rndIndx = Random.Range(0, blockRangeSize);
        GameObject newBlock = Instantiate(blocks[rndIndx], transform.position, Quaternion.identity, blockParent);
    }

    public void ResetGrid()
    {
        for (int i = 0; i < Block.gridW; i++)
        {
            for (int j = 0; j < Block.gridH; j++)
            {
                Block.grid[i, j] = null;
            }
        }
    }

    public void ClearBlocks()
    {
        foreach (Transform child in blockParent)
        {
            Destroy(child.gameObject);
        }
    }
}
