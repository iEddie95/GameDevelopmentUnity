using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public GameObject protectTeam;
    public GameObject attackTeam;
    public GameObject player;
    public GameObject enemyLeader;
    public GameObject protectTarget;
    public GameObject attackTraget;
    public bool isProtect;

    public TMPro.TextMeshProUGUI winlostText;
    public GameObject gameOverPanel;

    // Start is called before the first frame update
    void Awake()
    {
        if (isProtect)
        {
            player.layer = LayerMask.NameToLayer("ProtectTeam");
            enemyLeader.layer = LayerMask.NameToLayer("AttackTeam");
            player.transform.SetParent(protectTeam.transform);
            enemyLeader.transform.SetParent(attackTeam.transform);
            enemyLeader.GetComponent<NpcTeam>().leader = attackTraget;
            enemyLeader.transform.position = attackTeam.transform.position;
            player.transform.position = new Vector3(0f, 4f, 60f);
            protectTeam.GetComponent<TeamBehaviour>().isEnemy = false;
            attackTeam.GetComponent<TeamBehaviour>().isEnemy = true;
            protectTeam.GetComponent<TeamBehaviour>().leader = player;
            attackTeam.GetComponent<TeamBehaviour>().leader = enemyLeader;
            enemyLeader.GetComponent<NpcTeam>().aggroLayerMask = LayerMask.GetMask("ProtectTeam");
        }
        else
        { //otherwise
            player.layer = LayerMask.NameToLayer("AttackTeam");
            enemyLeader.layer = LayerMask.NameToLayer("ProtectTeam");
            enemyLeader.transform.position = new Vector3(0f, 4f, 60f);
            player.transform.position =attackTeam.transform.position;
            protectTeam.GetComponent<TeamBehaviour>().isEnemy = true;
            attackTeam.GetComponent<TeamBehaviour>().isEnemy = false;
            enemyLeader.transform.SetParent(protectTeam.transform);
            player.transform.SetParent(attackTeam.transform);
            protectTeam.GetComponent<TeamBehaviour>().leader = enemyLeader;
            attackTeam.GetComponent<TeamBehaviour>().leader = player;
            enemyLeader.GetComponent<NpcTeam>().leader = protectTarget;
            enemyLeader.GetComponent<NpcTeam>().aggroLayerMask = LayerMask.GetMask("AttackTeam");

        }

    }

    // Update is called once per frame
    void Update()
    {
        if (checkGameOver())
        {
            StartCoroutine(ShowMsg());
        }

    }

    public void HandleInputData(int value)
    {
        Debug.Log("value is " + value);
        switch (value)
        {
            case 0:
                isProtect = true;
                break;
            case 1:
                isProtect = false;
                break;
            default:
                break;
        }
    }

    public bool checkGameOver()
    {
        if (protectTeam.GetComponent<TeamBehaviour>().isGameOver())
        {
            string msg = protectTeam.GetComponent<TeamBehaviour>().isEnemy == true ? "VICTORY!" : "DEFEAT";
            winlostText.text = msg;
            return true;
        }
        else if (attackTeam.GetComponent<TeamBehaviour>().isGameOver())
        {
            string msg = attackTeam.GetComponent<TeamBehaviour>().isEnemy == true ? "VICTORY!" : "DEFEAT";
            winlostText.text = msg;
            return true;
        }
        return false;
    }

    IEnumerator ShowMsg()
    {
        yield return new WaitForSeconds(4f);
        gameOverPanel.SetActive(true);
    }

  }
