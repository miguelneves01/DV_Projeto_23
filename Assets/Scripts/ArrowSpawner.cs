using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSpawner : MonoBehaviour
{
    [SerializeField] private Transform launchPos;
    [SerializeField] private GameObject arrowPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void FireArrow(){
        Instantiate(arrowPrefab, launchPos.position,arrowPrefab.transform.rotation);
    }
}
