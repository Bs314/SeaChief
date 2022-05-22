using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeCamera : MonoBehaviour
{
    
    [SerializeField] float magnitude = 1f;
    [SerializeField] float duration = 1f;


    Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.position;       
    }

   public void CameraShake()
   {
       StartCoroutine(Shake());
   }

   IEnumerator Shake()
   {
       float elapsedTime = 0;
       while(elapsedTime<duration)
       {
           transform.position = initialPosition + (Vector3)Random.insideUnitCircle * magnitude;
           elapsedTime += Time.deltaTime;
           yield return new WaitForEndOfFrame();
       }

       transform.position = initialPosition;
   }



}
