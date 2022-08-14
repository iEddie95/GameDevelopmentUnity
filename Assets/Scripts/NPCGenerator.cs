using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCGenerator : MonoBehaviour
{
    public GameObject npc1;

    public int numOfNpcs;
    public int xPos_start, xPos_end;
    public float xPos;
    public float yPos;
    public float zPos;

    public int teamCount = 0;
    public GameObject[] Npcs;

    // Start is called before the first frame update
    void Awake()
    {
        Npcs = new GameObject[numOfNpcs];
        StartCoroutine(NPCGenerate());
    }

    IEnumerator NPCGenerate()
    {
        while (teamCount < numOfNpcs)
        {
            xPos = Random.Range(xPos_start, xPos_end);
            Npcs[teamCount] = Instantiate(npc1, new Vector3(xPos, 0, zPos), Quaternion.identity);
            Npcs[teamCount].SetActive(true);
            teamCount++;
        }
        yield return new WaitForSeconds(0.001f);
    }

    public void HandleInputData(string value)
    {
        numOfNpcs = int.Parse(value);
    }
}

