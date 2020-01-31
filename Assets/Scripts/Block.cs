using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    // Serialization
    [SerializeField] AudioClip breakSFX;
    [SerializeField] GameObject breakVFX;
    [SerializeField] Sprite[] hitSprites;

    // cached references
    Level level;

    // state vars
    [SerializeField] int timesHit; //for debug purpuses

    private void Start()
    {
        CountBreakableBlocks();
    }

    private void CountBreakableBlocks()
    {
        level = FindObjectOfType<Level>();
        if (tag == "Breakable")
        {
            level.CountBlocks();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (tag == "Breakable")
        {
            timesHit++;
            int maxHits = hitSprites.Length + 1;
            if (timesHit >= maxHits)
            {
                DestroyBlock();
            }
            else
            {
                ShowNextSprite();
            }
        }     
    }

    private void ShowNextSprite()
    {
        int spriteIndex = timesHit - 1;
        if (hitSprites[spriteIndex] != null)
        {
            GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
        }
        else
        {
            Debug.LogError("Block sprite is missing in array of " + gameObject.name);
        }
    }

    private void DestroyBlock()
    {
        AudioSource.PlayClipAtPoint(breakSFX, Camera.main.transform.position, 1f);
        level.BreakBlocks();
        Destroy(gameObject);
        TriggerSparklesVFX();
        FindObjectOfType<GameStatus>().AddToScore(); 
    }

    private void TriggerSparklesVFX()
    {
        GameObject sparkles = Instantiate(breakVFX, transform.position, transform.rotation);
        Destroy(sparkles, 2f);
    }
}
