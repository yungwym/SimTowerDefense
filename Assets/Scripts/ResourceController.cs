using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ResourceType
{
    WOOD, 
    STONE,
    CRYSTAL
}

public class ResourceController : MonoBehaviour
{

    [SerializeField] private ResourceType resourceType;

    [SerializeField] private int currentResourceAmount = 0;

    private int resourceIncrementAmount = 5;
    private int resourceIncrementRate = 10;

    private int resourceAmountMax = 250;
    private int resourceHarvestAmount = 150;

    private bool isConnectedToRoad = false;


    // Start is called before the first frame update
    void Start()
    {
       // StartCoroutine(GenerateResource());
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private IEnumerator GenerateResource()
    {
        /*if (currentResourceAmount < resourceAmountMax)
        {
            currentResourceAmount += resourceIncrementAmount;
        }*/

        CastleController.castleInstance.AddResourceAmount(resourceType, resourceIncrementAmount);

        yield return new WaitForSeconds(resourceIncrementRate);
        StartCoroutine(GenerateResource());
    }

    public void SetIsConnectedToRoad(bool isConcected)
    {
        isConnectedToRoad = isConcected;

        StartCoroutine(GenerateResource());
    }
}
