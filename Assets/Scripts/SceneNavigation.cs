using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneNavigation : MonoBehaviour
{
    
    public void LoadStartScene()
    {
        SceneManager.LoadScene("StartScene");
    }


    public void LoadInstructionScene()
    {
        SceneManager.LoadScene("InstructionScene");
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;

        #else
        Application.Quit()

        #endif
    }

}
