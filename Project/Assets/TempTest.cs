using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TempTest : MonoBehaviour
{
    public Image m_Image;

    private void Update()
    {
        m_Image.transform.localScale = new Vector3(1, UnityEngine.Random.Range(0,2), 1);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
