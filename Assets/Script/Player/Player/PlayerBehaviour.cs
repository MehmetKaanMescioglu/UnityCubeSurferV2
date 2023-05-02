using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField]
    private Animator animatorOfPlayer;

    public PlayerMoverRunner playerMoverRunner;

    

    private void Awake()
    {
        Singleton();
    }

    #region Singleton

    public static PlayerBehaviour Instance;

    public void Singleton()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }

        Instance = this;
    }
    #endregion

    public void VictoryAnimation()
    {
        animatorOfPlayer.SetTrigger("Victory");
    }

    public void FailAnimation()
    {
        
        animatorOfPlayer.SetTrigger("Fail");
    }

    public void StopPlayer()
    {
        playerMoverRunner.Velocity = 0;
        
    }





}
