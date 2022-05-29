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
    [SerializeField] AudioClip rejoicing;
    [SerializeField] AudioClip theOldTowerInn;
    [SerializeField] AudioClip marketDay;

    AudioSource audioSource;
    int oldScene = 0;
    int musicNum = 0;
    void Awake()
    {
        int numAudio = FindObjectsOfType<Audio>().Length;
        if (numAudio > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 0.3f;
        audioSource.Play();

    }


    void Update()
    {
        Scene scene = SceneManager.GetActiveScene();

        if (oldScene != scene.buildIndex)
        {
            audioSource.Stop();
            oldScene = scene.buildIndex;
            if (oldScene == 3)
            {
                audioSource.clip = marketDay;
                audioSource.volume = 0.3f;
                audioSource.Play();
            }
            else
            {
                switch (musicNum)
                {
                    case 0:
                        audioSource.clip = bardsTale;
                        audioSource.volume = 0.2f;
                        break;
                    case 1:
                        audioSource.clip = mainMusic;
                        audioSource.volume = 0.3f;
                        break;
                    case 2:
                        audioSource.clip = kingsFeast;
                        audioSource.volume = 0.2f;
                        break;
                    case 3:
                        audioSource.clip = mainMusic;
                        audioSource.volume = 0.3f;
                        break;
                    case 4:
                        audioSource.clip = minstrelDance;
                        audioSource.volume = 0.2f;
                        break;
                    case 5:
                        audioSource.clip = mainMusic;
                        audioSource.volume = 0.3f;
                        break;
                    case 6:
                        audioSource.clip = rejoicing;
                        audioSource.volume = 0.2f;
                        break;
                    case 7:
                        audioSource.clip = mainMusic;
                        audioSource.volume = 0.3f;
                        break;
                    case 8:
                        audioSource.clip = theOldTowerInn;
                        audioSource.volume = 0.2f;
                        break;
                    case 9:
                        audioSource.clip = mainMusic;
                        audioSource.volume = 0.3f;
                        break;
                    default:
                        audioSource.clip = mainMusic;
                        audioSource.volume = 0.3f;
                        break;
                }
                musicNum++;
                if (musicNum > 9) musicNum = 0;
                audioSource.Play();
            }

        }


    }
}
