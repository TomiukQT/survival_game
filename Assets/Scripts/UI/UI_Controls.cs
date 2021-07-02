using System;
using System.Collections.Generic;
using UnityEngine;



public class UI_Controls : MonoBehaviour
{

     [SerializeField] private List<ControlPair> _showables;

     private void Awake()
     {
         
     }

     private void Start()
    {
        HideAllToggables();
    }

    private void Update()
    {
        GetInputForToggables();
    }

    private void HideAllToggables()
    {
        foreach (var pair in _showables)
            pair.Showable.GetComponent<IShowable>().SetActive(false);
    }
    
    private void GetInputForToggables()
    {
        foreach (var pair in _showables)
            if(Input.GetKeyDown(pair.KeyCode))
                pair.Showable.GetComponent<IShowable>().Toggle();
        
    }

    [System.Serializable]
    struct ControlPair
    {
        public KeyCode KeyCode;
        public GameObject Showable;
    }
    
}
