using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class AIReinSpiritScript : MonoBehaviour
{
    public Rig rig;
    public float targetWeight = 1f;
    public GameObject target;
    public GameObject player;
    public GameObject rangeBox;
    bool playerInSight = false;

    private void OnTriggerEnter(Collider other)
    {
        
        if(other.tag == "Player" && !playerInSight)
        {
            Debug.Log("Teste trigger in");
            playerInSight = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && playerInSight)
        {
            Debug.Log("Teste trigger out");
            playerInSight = false;
        }
    }

    private void Awake()
    {
        rig = GetComponent<Rig>();
    }

    private void Update()
    {
        rig.weight = Mathf.Lerp(rig.weight, targetWeight, Time.deltaTime * 5f);

        //targetPosition = new Vector3(target.transform.position.x, target.transform.position.y, target.transform.position.z);

        if(!playerInSight)
        {
            targetWeight = 0f;
        }
        if(playerInSight)
        {
            target.transform.SetParent(player.transform);
            target.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
            targetWeight = 1f;
        }
    }
}
