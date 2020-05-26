using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public static LevelGenerator sharedInstance;

    public List<LevelBlock> allTheLevelBlocks = new List<LevelBlock>();

    public Transform levelStartPoint;

    public List<LevelBlock> currentBlocks = new List<LevelBlock>();


    private void Awake()
    {
        sharedInstance = this;
    }


    public void AddLevelBlock()
    {
        int randomIndex = Random.Range(0, allTheLevelBlocks.Count);//random entre x,y puede coger x pero no y

        LevelBlock currentBlock = (LevelBlock)Instantiate(allTheLevelBlocks[randomIndex]); //Hacemos un ccasting a la instancia para que sea (LevelBlock) por defecto las instancias son GameObject
        currentBlock.transform.SetParent(this.transform, false);//hacemos que sea hijo en la jerarquia del LevelGenerator

        Vector3 spawnPosition = Vector3.zero;

        if (currentBlocks.Count == 0)
        {
            spawnPosition = levelStartPoint.position;
        }
        else
        {
            spawnPosition = currentBlocks[currentBlocks.Count - 1].exitPoint.position;
        }


        Vector3 correction = new Vector3(spawnPosition.x - currentBlock.startPoint.position.x, spawnPosition.y - currentBlock.startPoint.position.y, 0);

        currentBlock.transform.position = correction;

        currentBlocks.Add(currentBlock);
    }

    private void Start()
    {
        GenerateInitialBlocks();
    }

    public void RemoveOldestLevelBlock()
    {
        LevelBlock oldestBlock = currentBlocks[0];
        currentBlocks.Remove(oldestBlock);
        Destroy(oldestBlock.gameObject);
    }

    public void RemoveAllTheBlocks()
    {
        while (currentBlocks.Count > 0)
        {
            RemoveOldestLevelBlock();
        }
    }

    public void GenerateInitialBlocks()
    {
        for (int i = 0; i < 2; i++)
        {
            AddLevelBlock();
        }
    }
}
