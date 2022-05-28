using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Audio : MonoBehaviour
{

    [SerializeField] AudioClip mainMusic;
    [SerializeField] AudioClip bardsTale;
    [SerializeField] AudioClip minstrelDance;
    [SerializeField] AudioClip kingsFeast;

    AudioSource audioSource;
    int oldScene = 0;
    int musicNum = 0;
    void Awake() {
        int numAudio = FindObjectsOfType<Audio>().Length;
        if(numAudio>1)
        {
            Destroy(gameObject);
        }  
        else
        {
            DontDestroyOnLoad(gameObject);
        }  
    }

    private void Start() {
        audioSource = GetComponent<AudioSource>();    
        audioSource.Play();
        
    }

    
    void Update()
    {
        Scene scene = SceneManager.GetActiveScene();  

        if(oldScene != scene.buildIndex)
        {
            audioSource.Stop();
            oldScene = scene.buildIndex;
            switch(musicNum)
            {
                case 0:
                audioSource.clip = bardsTale;
                break;
                case 1:
                audioSource.clip = mainMusic;
                break;
                case 2:
                audioSource.clip = kingsFeast;
                break;
                case 3:
                audioSource.clip = mainMusic;
                break;
                case 4:
                audioSource.clip = minstrelDance;
                break;
                case 5:
                audioSource.clip = mainMusic;
                break;
                default:
                audioSource.clip = mainMusic;
                break;
            }
            musicNum++;
            if(musicNum>5)musicNum = 0;
            audioSource.Play();
        }

        
    }
}
