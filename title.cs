using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class title : MonoBehaviour
{
    public Image background;
    public Image titlebox;
    public int swidth;
    public int sheight;
    // Start is called before the first frame update
    void Start()
    {
        swidth = Screen.width;
        sheight = Screen.height;
        background.rectTransform.transform.position = new Vector3(swidth/2, sheight/2);
        background.rectTransform.sizeDelta = new Vector2(swidth, sheight);
        titlebox.rectTransform.transform.position = new Vector3(swidth / 2, sheight / 2);
        titlebox.rectTransform.sizeDelta = new Vector2(swidth/1.3f, sheight/1.3f);
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startgame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void quitgame()
    {
        Application.Quit();
    }
}
