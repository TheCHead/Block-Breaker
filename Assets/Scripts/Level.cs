using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] int numberOfblocks; // serialized for debugging purposes

    //cached reference
    SceneLoader sceneLoader;

    private void Start()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
    }

    public void CountBlocks()
    {
        numberOfblocks++;
    }

    public void BreakBlocks()
    {
        numberOfblocks--;
        if (numberOfblocks <= 0)
        {
            sceneLoader.LoadNextScene();
        }
    }
}
