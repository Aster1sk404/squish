using System.Collections;
using UnityEngine;
using UnityEngine.U2D.IK;

public class Bot : MonoBehaviour
{
    [SerializeField] private float maxHeight = 0;
    private CharacterController2D cc;
    public float[] moveD = {1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f};
    public float[] moveA = {1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f};
    public float[] moveSpace = {1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f};
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cc = GetComponent<CharacterController2D>();
        Round();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Round()
    {
        for (int i = 0; i < moveA.Length; i++)
        {
            moveA[i] = Mathf.Clamp(moveA[i] + Random.Range(-0.5f, 0.5f), 0, 3);
        }
        
        for (int i = 0; i < moveSpace.Length; i++)
        {
            moveSpace[i] = Mathf.Clamp(moveSpace[i] + Random.Range(-0.5f, 0.5f), 0, 3);
        }

        for (int i = 0; i < moveD.Length; i++)
        {
           moveD[i] = Mathf.Clamp(moveD[i] + Random.Range(-0.5f, 0.5f), 0, 3);
        }
        StartCoroutine(FMoveA());
        StartCoroutine(FMoveD());
        StartCoroutine(FMoveSpace());
    }
/*
    IEnumerator Movement2()
    {
        for (int i = 0; i < moveA.Length; i += 2)
        {
            cc.aKey = true;
            yield return new WaitForSeconds(i);
            cc.aKey = false;
            yield return new WaitForSeconds(i + 1);
        }

        for (int i = 0; i < moveD.Length; i += 2)
        {
            cc.dKey = true;
            yield return new WaitForSeconds(i);
            cc.dKey = false;
            yield return new WaitForSeconds(i + 1);
        }

        for (int i = 0; i < moveSpace.Length; i++)
        {
            cc.spaceKey = true;
            yield return new WaitForEndOfFrame();
            cc.spaceKey = false;
            yield return new WaitForSeconds(i);
        }
    }*/

    IEnumerator FMoveA()
    {
        for (int i = 0; i < moveA.Length; i+= 2)
        {
            cc.aKey = true;
            yield return new WaitForSeconds(i);
            cc.aKey = false;
            yield return new WaitForSeconds(i + 1);
        }
    }

    IEnumerator FMoveD()
    {
        for (int i = 0; i < moveD.Length; i+= 2)
        {
            cc.dKey = true;
            yield return new WaitForSeconds(i);
            cc.dKey = false;
            yield return new WaitForSeconds(i + 1);
        }
    }

    IEnumerator FMoveSpace()
    {
        for (int i = 0; i < moveSpace.Length; i+= 2)
        {
            cc.spaceKey = true;
            yield return new WaitForSeconds(i);
            cc.spaceKey = false;
            yield return new WaitForSeconds(i + 1);
        }
    }
}
