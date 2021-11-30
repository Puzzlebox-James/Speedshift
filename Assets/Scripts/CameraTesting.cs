using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTesting : MonoBehaviour
{
    [SerializeField] private GameObject playerGameObject;

    // Update is called once per frame
    void Update()
    {
        this.transform.LookAt(playerGameObject.transform);
    }
}
