using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Mode = Tetris_Helper.Mode;

public class Tetris_ModeController : MonoBehaviour
{
    static Tetris_ModeController instance = null;
    public Button stageMode, infiniteMode;
    private static Mode mode = Mode.stage;
    
    private void Awake()
    {
        if (instance == null) {
        //    Debug.Log("ModeController instance has been assigned");
            instance = this;
            GameObject.DontDestroyOnLoad(gameObject);
        }
    }

    public void StartGame() {
        FindObjectOfType<Tetris_AudioManager>().Play("Start");
        SceneManager.LoadScene(1);
    }

    public Mode GetMode() {
        return mode;
    }

    public void SetMode(int mode) {
        FindObjectOfType<Tetris_AudioManager>().Play("MenuMove");
        Tetris_ModeController.mode = mode == 0 ? Mode.stage : Mode.infinite;
        switch (Tetris_ModeController.mode) {
            case Mode.stage:
                stageMode.interactable = false;
                infiniteMode.interactable = true;
                break;
            case Mode.infinite:
                stageMode.interactable = true;
                infiniteMode.interactable = false;
                break;
        }
    }
}
