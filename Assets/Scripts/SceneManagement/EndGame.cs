using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    [SerializeField] private AudioSource LoadSenceSound;
    [SerializeField] private AudioSource Sound;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Sound.Stop();
            LoadSenceSound.Play();
            Invoke("LoadScence", 6f);
        }
    }
    void LoadScence()
    {
        SceneManager.LoadScene(5);
    }

}
