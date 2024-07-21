using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CounterClick : MonoBehaviour
{
    public Button button;
    public int counter = 0;

    void Awake()
    {
        button.onClick.AddListener(() =>
        {

            if (counter == 2)
            {
                counter = 0;
            }

            counter++;

        });
    }
}
