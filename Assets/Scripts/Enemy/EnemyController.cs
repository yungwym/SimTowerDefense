using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    //Basic Enemy Variables 
    [SerializeField] private float enemyMovementSpeed;

    [SerializeField] private int enemyHealth = 75;

    [SerializeField] private int damageAmount = 5;

    //State Machine Variables 
    private EnemyStateMachine enemyStateMachine;
    [SerializeField] private EnemyStateId initialState;

    //NavMesh Agent 
    private NavMeshAgent navMeshAgent;

    //Castle Target Object for Road Pathfind State
    [SerializeField] private GameObject castleTargetObject;

    //List of Resource Target Objects for Open Pathfind State
    [SerializeField] private List<Tile> resourceTargetObjects;

    // Start is called before the first frame update
    void Start()
    {   
        //Get Nav Mesh Agent 
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = enemyMovementSpeed;

        castleTargetObject = GameObject.FindGameObjectWithTag("Homepoint");

        //State Machine Setup
        enemyStateMachine = new EnemyStateMachine(this);
      // enemyStateMachine.RegisterState(new OpenPathfindState());
        enemyStateMachine.RegisterState(new RoadPathfindState());
        enemyStateMachine.ChangeState(initialState);
    }

    // Update is called once per frame
    void Update()
    {
        //State Machine Update Call
        enemyStateMachine.Update();
    }

    public NavMeshAgent GetNavMeshAgent()
    {
        return navMeshAgent;
    }

    public Transform GetCastleTargetTransform()
    {
        return castleTargetObject.transform;
    }

    public void SetResourceTargetObjectsList(List<Tile> resourceList)
    {
        resourceTargetObjects = resourceList;
    }

    public List<Tile> GetResourceTargetObjectsList()
    {
        return resourceTargetObjects;
    }



    public void TakeDamage(int damageAmount)
    {
        enemyHealth -= damageAmount;

        if (enemyHealth <= 0)
        {
            Debug.Log("ENEMY DEATH");
            Destroy(gameObject);
        }
    }

    public int GetDamage()
    {
        return damageAmount;
    }
}
