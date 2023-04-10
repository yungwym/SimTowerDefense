using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;

public class CastleController : MonoBehaviour
{
    //Castle Singleton
    public static CastleController castleInstance;

    [SerializeField] private int castleHealth = 100;

    [SerializeField] private int resourceWoodAmount = 150;
    [SerializeField] private int resoureStoneAmount = 100;
    [SerializeField] private int resoureCrystalAmount = 0;

    private HUDViewController hudViewController;

    private void Awake()
    {
        if (castleInstance != null)
        {
            return;
        }
        castleInstance = this;
    }

    private void Start()
    {
        hudViewController = GameObject.FindObjectOfType<HUDViewController>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Enemy")
        {
            DecreaseHealth(collision.gameObject.GetComponent<EnemyController>().GetDamage());
            Destroy(collision.gameObject);
        }
    }

    private void DecreaseHealth(int decreaseAmount)
    {
        if (castleHealth > 0)
        {
            castleHealth -= decreaseAmount;
            Debug.Log($"Damage Taken: {decreaseAmount} \n Current Health: {castleHealth}");
        }
        else if (castleHealth <= 0)
        {
            Debug.Log("Game Over");
        }
    }

    public void AddResourceAmount(ResourceType rType, int amount)
    {
        switch (rType) 
        {
            case ResourceType.WOOD:
                resourceWoodAmount += amount;
                break;

            case ResourceType.STONE:
                resoureStoneAmount += amount;
                break;

            case ResourceType.CRYSTAL:
                resoureCrystalAmount += amount;
                break;
        }
        hudViewController.UpdateResourceText(resourceWoodAmount, resoureStoneAmount, resoureCrystalAmount);
    }

    public bool RemoveResourceAmount(ResourceType rType, int amount) 
    { 
        switch(rType) 
        {
            case ResourceType.WOOD:
                return CheckIfTransactionIsValid(resourceWoodAmount, amount);
                
            case ResourceType.STONE:
                return CheckIfTransactionIsValid(resoureStoneAmount, amount);

            case ResourceType.CRYSTAL:
                return CheckIfTransactionIsValid(resoureCrystalAmount, amount);
        }
        return false;
    }

    private bool CheckIfTransactionIsValid(int baseAmount, int decrementAmount)
    {
        if (baseAmount >= 0)
        {
            baseAmount -= decrementAmount;
            return true;
        }
        else
        {
            return false;
        }
    }

}
