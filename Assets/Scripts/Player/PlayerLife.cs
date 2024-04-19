using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    [SerializeField] public int _life = 100;
    private SwitchScene _sceneManager;
    
    private void Awake()
    {
        _sceneManager = GetComponent<SwitchScene>();
    }
    public void HurtPlayer(int damages)
    {
        //LoseHP
        //FeedBacks
        _life -= damages;
        if (_life <= 0)
        {
            _sceneManager.SceneToSwitchTo("MainMenu");
        }
    }
}
