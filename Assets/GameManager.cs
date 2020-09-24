using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{


    #region variables
    public static GameManager instance;


    public List<GameObject> _collectables;
    public static List<GameObject> ElementsToCollect
    {
        set { GameManager.instance._collectables = value; }
        get { return GameManager.instance._collectables; }
    }

    public List<Liquidateur> _liquidateurs;
    public static List<Liquidateur> Liquidateurs
    {
        set { GameManager.instance._liquidateurs = value; }
        get { return GameManager.instance._liquidateurs; }
    }



    public List<Liquidateur> _requireAssistance;
    public static List<Liquidateur> RequireAssistance
    {
        set { GameManager.instance._requireAssistance = value; }
        get { return GameManager.instance._requireAssistance; }
    }
    #endregion

    [Header("Position")]
    public Transform _stock;
    public static Transform Stock
    {
        set { GameManager.instance._stock = value; }
        get { return GameManager.instance._stock; }
    }


    private void Awake()
    {
        if (instance == null)
            instance = this;
    }




    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
        }
    }

    public static void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    public static void IneedAMedic(Liquidateur liquidateur)
    {

        if(GameManager.RequireAssistance.Contains(liquidateur) == false)
            GameManager.RequireAssistance.Add(liquidateur);
    }
    public static void IdontNeedAMedic(Liquidateur liquidateur)
    {
        if (GameManager.RequireAssistance.Contains(liquidateur) == true)
            GameManager.RequireAssistance.Remove(liquidateur);
    }
}
