using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnButton : MonoBehaviour
{
    [SerializeField] GameObject _objet;
    [SerializeField] Transform _position;


    public void OnClick()
    {
        GameObject obj =  Instantiate(_objet, _position.position, _position.rotation, null);
    }
}
