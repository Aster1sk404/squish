using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotManager : MonoBehaviour
{
    [SerializeField] private float maxHeight = 0;
    public Transform spawn;
    [SerializeField] private Bot curWin;
    [SerializeField] private GameObject playerObj;
    [SerializeField] private int amount = 20;
    [SerializeField] private List<Bot> botList;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < amount; i++)
        {
            var pl = Instantiate(playerObj, spawn);
            botList.Add(pl.GetComponent<Bot>());
        }
        StartCoroutine(EndRound());
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Bot k in botList)
        {
            if (k.transform.position.y > maxHeight)
            {
                maxHeight = k.transform.position.y;
                curWin = k;
            }
        }
    }

    IEnumerator EndRound()
    {
        yield return new WaitForSeconds(10);

        maxHeight = 0;
        foreach (Bot k in botList)
        {
            k.transform.position = spawn.position;
            k.moveA = curWin.moveA;
            k.moveD = curWin.moveD;
            k.moveSpace = curWin.moveSpace;
            k.Round();
        }
        
    }
}
