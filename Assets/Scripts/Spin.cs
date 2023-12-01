using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    [SerializeField] private float spinSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         gameObject.transform.rotation = gameObject.transform.rotation * Quaternion.Euler(Time.deltaTime * new Vector3(spinSpeed, spinSpeed*2, spinSpeed*3));
    }
}
