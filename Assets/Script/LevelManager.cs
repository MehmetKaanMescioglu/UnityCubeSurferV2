using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class LevelManager : MonoBehaviour
{
    public float level = 0;
    public int point = 0;
    public int levelPoint = 0;
    //private TextMeshProUGUI levelTxt;
    
    private void Awake()
    {
        
        DontDestroyOnLoad(gameObject);
        Singleton();
        //levelTxt = GameObject.Find("levelTxt").GetComponent<TextMeshProUGUI>();
        //float chapter = LevelManager.Instance.Getlevel();
        //levelTxt.text = level.ToString();

    }

    private void Start()
    {

        if (level == 1)
            level = 1;
        else 
            level += 1;
        //chapter++;


    }

    private void Update()
    {

        
    }

    #region Singleton

    public static LevelManager Instance;

    public void Singleton()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }

        Instance = this;
    }
    #endregion

    IEnumerator DebugLogIEnumerator()
    {
        var yieldReturn = new WaitForSeconds(1f);

        while(true)
        {
            yield return yieldReturn;
            Debug.Log("DebugLogIEnumerator");
        }
    }

    public void Setlevel(int index)   
    {
        levelPoint = 0;
        level += index;
    }

    public float Getlevel()
    {
        return level;
    }

    public void RePoint()
    {
        point -= levelPoint;
        levelPoint = 0;
    }

    public void SetPoint()
    {
        point += 10;
        levelPoint += 10;
    }

    public int GetPoint()
    {
        return point;
    }

    public int GetLocPoint()
    {
        return levelPoint;
    }



}
