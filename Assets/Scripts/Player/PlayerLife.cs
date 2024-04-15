using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    [SerializeField] public int _life = 100;
    public void HurtPlayer(int damages)
    {
        //LoseHP
        //FeedBacks
        _life -= damages;
        if (_life <= 0)
        {
            //You Lost
            //Return to Menu
        }
    }
}
