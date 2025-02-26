using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerUISprite : MonoBehaviour
{
   private GameObject boss; 
    private Image playerSrite;
    // Start is called before the first frame update
    void Start()
    {
        boss = GameObject.Find("EnemyTest");
    }

    // Update is called once per frame
    void Update()
    {
        if (boss.GetComponent<BossNavigation>().playerCaught)
        {
           
        }
    }
    public void SetImage(Sprite newSprite)

    {

        playerSrite.sprite = newSprite;

    }
}
