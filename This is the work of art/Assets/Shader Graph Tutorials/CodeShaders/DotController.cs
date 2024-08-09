using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotController : MonoBehaviour
{
    [SerializeField] Material mat;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector4 moveVector = new Vector4(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0, 0);
        mat.SetVector("_Position", mat.GetVector("_Position") + moveVector * 0.01f);
    }
}
