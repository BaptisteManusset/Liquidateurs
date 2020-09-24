using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{


    public static GameManager instance;

    #region COLLECTABLES
    public List<GameObject> _collectables;
    public static List<GameObject> ElementsToCollect
    {
        set { GameManager.instance._collectables = value; }
        get { return GameManager.instance._collectables; }
    }
    #endregion

    #region LIQUIDATEURS
    public List<Moveable> _liquidateurs;
    public static List<Moveable> Liquidateurs
    {
        set { GameManager.instance._liquidateurs = value; }
        get { return GameManager.instance._liquidateurs; }
    }
    #endregion


    #region MEDICS
    public List<Moveable> _medics;
    public static List<Moveable> Medics
    {
        set { GameManager.instance._medics = value; }
        get { return GameManager.instance._medics; }
    }
    #endregion

    #region REQUIRE ASSISTANCE
    public List<GameObject> _requireAssistance;
    public static List<GameObject> RequireAssistance
    {
        set { GameManager.instance._requireAssistance = value; }
        get { return GameManager.instance._requireAssistance; }
    }
    #endregion

    [Header("Position")]public Transform _destination;
    public static Transform Destination
    {
        set { GameManager.instance._destination = value; }
        get { return GameManager.instance._destination; }
    }
    
    public Transform _hopital;
    public static Transform Hopital
    {
        set { GameManager.instance._hopital = value; }
        get { return GameManager.instance._hopital; }
    }

    #region singleton
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    #endregion



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


    public static void IneedAMedic(GameObject liquidateur)
    {

        if (GameManager.RequireAssistance.Contains(liquidateur) == false)
            GameManager.RequireAssistance.Add(liquidateur);
    }
    public static void IdontNeedAMedic(GameObject liquidateur)
    {
        if (GameManager.RequireAssistance.Contains(liquidateur) == true)
            GameManager.RequireAssistance.Remove(liquidateur);
    }
}
