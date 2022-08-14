using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamBehaviour : MonoBehaviour
{
    public GameObject leader;
    public List<GameObject> list;
    public GameObject[] team;
    public int teamCount;
    public NPCGenerator[] NPCGenerator;
    public bool isEnemy;
    public LayerMask aggroLayerMask;
    public LayerMask npcLayerMask;
    public bool isProtect;

    // Start is called before the first frame update

    void Start()
    {

        for (int i = 0; i < NPCGenerator.Length; i++)
        {
            teamCount += NPCGenerator[i].numOfNpcs;
            list.AddRange(NPCGenerator[i].Npcs);
        }
        team = list.ToArray();

        for (int i = 0; i < team.Length; i++)
        {
            team[i].tag = isEnemy == true ? "Enemy" : "Player";
            team[i].GetComponent<NpcTeam>().aggroLayerMask = aggroLayerMask.value;
            team[i].GetComponent<NpcTeam>().leader = leader;
            team[i].layer = LayerMask.NameToLayer(isProtect == true ? "ProtectTeam" : "AttackTeam");
            team[i].transform.SetParent(gameObject.transform);
        }

        leader.tag = isEnemy == true ? "Enemy" : "Player";
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool isGameOver()
    {
        for (int i = 0; i < team.Length; i++)
        {
            if (team[i].GetComponent<Combat>().IsAlive() || leader.GetComponent<Combat>().IsAlive())
                return false;
        }
        return true;
    }
}
