using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameboardSectionGenerator gameboardSectionGenerator;

    // Start is called before the first frame update
    void Start()
    {
        gameboardSectionGenerator.GenerateBlankBoard(18, 18, BoardSectionType.HOME);
    }

  
}
