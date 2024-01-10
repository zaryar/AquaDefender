using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SwordModelSwapper : MonoBehaviour
{
    [SerializeField] MeshRenderer Sword1;
    [SerializeField] MeshRenderer Sword2;
    [SerializeField] MeshRenderer Sword3;

    Weapons.Swords _currentSword = Weapons.Swords.sword1;
    private bool _isActive = false;

    public void swapModel(Weapons.Swords sword)
    {
        _currentSword= sword;
        if(_isActive)
        {
            disableAll();
            switch (sword)
            {
                case Weapons.Swords.sword1:
                    Sword1.enabled = true;
                    break;
                case Weapons.Swords.sword2:
                    Sword2.enabled = true;
                    break;
                case Weapons.Swords.sword3:
                    Sword3.enabled = true;
                    break;
            }
        }  
    }

    public void enableRenderer()
    {
        _isActive = true;
        swapModel(_currentSword);
    }

    public void disableRenderer()
    {
        disableAll();
        _isActive = false;
    }

    private void Awake()
    {
        if(Sword1 == null ||
           Sword2 == null ||
           Sword3 == null) {
            Debug.LogError("SwordModelSwapper is missing Links in the inspector.");
        }
        disableAll();
    }

    private void disableAll()
    {
        Sword1.enabled = false;
        Sword2.enabled = false;
        Sword3.enabled = false;
    }
}

[CustomEditor(typeof(SwordModelSwapper))]
public class SwordModelSwapperEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        var swapper = (SwordModelSwapper)target;
        if (GUILayout.Button("sword1"))
        {
            swapper.swapModel(Weapons.Swords.sword1);
        }
        else if (GUILayout.Button("sword2"))
        {
            swapper.swapModel(Weapons.Swords.sword2);
        }
        else if (GUILayout.Button("sword3"))
        {
            swapper.swapModel(Weapons.Swords.sword3);
        }
    }
}