using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class QuickSand : MonoBehaviour, ITraps
{
    [SerializeField] private GameObject particleSystem;
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            PlayerMovement playersMovement;
            col.TryGetComponent(out playersMovement);

            playersMovement.SlowDownPlayer();

            particleSystem.transform.position = col.transform.position;
            particleSystem.SetActive(true);
            particleSystem.GetComponent<ParticleSystem>().Play();
            
            GameController.instance.ShowGameOver();
        }
    }
}