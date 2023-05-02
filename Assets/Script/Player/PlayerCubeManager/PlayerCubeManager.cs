using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCubeManager : MonoBehaviour
{
    private float stepLength = 0.0670f;
    private float PlayerStepLengthValue = 0.0226f;
    private float groundValue = -0.0702f;
    private float LastGroundValue = 0.207f;

    AudioSource audioSource;


    public List<CubeBehaviour> listOfCubeBehaviour = new List<CubeBehaviour>();
    public Transform cubeDetector;

    private void Awake()
    {
        Singleton();
        
    }
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    #region Singleton

    public static PlayerCubeManager Instance;

    public void Singleton()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }

        Instance = this;
    }
    #endregion


    public void GetCube(CubeBehaviour cubeBehaviour)
    {
        listOfCubeBehaviour.Add(cubeBehaviour);

        cubeBehaviour.isStacked = true;

        cubeBehaviour.transform.parent = transform;

        int indexOfCube = listOfCubeBehaviour.Count - 1;

        // Todo: Reorder All Cubes

        ReorderCubes();

        RelocatePlayer();

    }

    private void RelocatePlayer()
    {
        var playerTransform = PlayerBehaviour.Instance.transform;
        var yValue = stepLength * listOfCubeBehaviour.Count + groundValue;
        var playerTarget = new Vector3(0f, yValue, 0f);

        playerTransform.DOLocalMove(playerTarget, 0.05f);
    }

    private void RelocatePlayerFinal()
    {
        var playerTransform = PlayerBehaviour.Instance.transform;
        var yValue = stepLength * listOfCubeBehaviour.Count + LastGroundValue;
        var playerTarget = new Vector3(0f, yValue, 0f);

        playerTransform.DOLocalMove(playerTarget, 0.05f);

        //var cubeTarget = new Vector3(0f, LastGroundValue, 0f);
        cubeDetector.DOLocalMoveY(0.14f, 0.05f);

        
    }

    public void DropCube(CubeBehaviour cubeBehaviour)
    {
        cubeBehaviour.transform.parent = null;
        cubeBehaviour.isStacked = false;
 
        listOfCubeBehaviour.Remove(cubeBehaviour);

        // Level Fail Trigger

        if (listOfCubeBehaviour.Count < 1)
        {
            Debug.Log("GameOver");

            PlayerBehaviour.Instance.FailAnimation();
            PlayerBehaviour.Instance.StopPlayer();
            
            var playerTransform = PlayerBehaviour.Instance.transform;

           //Vector3 groundPosition = new Vector3(playX, playY, playZ);
            Vector3 groundPosition = new Vector3(0f, -0.0686f, -0.14f);
            playerTransform.DOLocalJump(groundPosition, 0.05f, 1, 0.5f);
            MenuInMain.Instance.ActiveFail();
            return;

        }

        RelocatePlayer();
    }

    public void DropFinalCube(CubeBehaviour cubeBehaviour)
    {
        cubeBehaviour.transform.parent = null;
        cubeBehaviour.isStacked = false;

        listOfCubeBehaviour.Remove(cubeBehaviour);

        // Level Fail Trigger

        if (listOfCubeBehaviour.Count < 1)
        {
            Debug.Log("YouWon!");
            //RelocatePlayer();
            PlayerBehaviour.Instance.StopPlayer();
            PlayerBehaviour.Instance.VictoryAnimation();
            //RelocatePlayer();
            var playerTransform = PlayerBehaviour.Instance.transform;
            var yValue = stepLength * listOfCubeBehaviour.Count + groundValue;
            var playerTarget = new Vector3(0f, 0.1973f, 0.1f);

            playerTransform.DOLocalMove(playerTarget, 0.05f);
            
            MenuInMain.Instance.ActiveWin();
            return;

        }
        RelocatePlayerFinal();
        //RelocatePlayer();
    }

    public void FinishGame(CubeBehaviour cubeBehaviour)
    {
        Debug.Log("Finish! oyun bitti1-");
        PlayerBehaviour.Instance.StopPlayer();
        PlayerBehaviour.Instance.VictoryAnimation();
        
        MenuInMain.Instance.ActiveWin();
        //TransitionScene();
    }

    public void GetPoint(CubeBehaviour cubeBehaviour)
    {
        Debug.Log("Puan Kazandýnýz!");
        LevelManager.Instance.SetPoint();
        audioSource.Play();


    }

    private void ReorderCubes()
    {
        int index = listOfCubeBehaviour.Count - 1;

        foreach(var cube in listOfCubeBehaviour)
        {
            Vector3 target = new Vector3(0f, index * stepLength, 0f);
            cube.transform.DOLocalMove(target, 0.05f);
            index--;  
        }
    }


}
