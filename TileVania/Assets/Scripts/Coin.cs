using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] AudioClip audioClip;

    private void OnTriggerEnter2D() {
        AudioSource.PlayClipAtPoint(audioClip, Camera.main.transform.position);
        FindObjectOfType<GameSession>().AddToScore(100);
        Destroy(gameObject);
    }

}
