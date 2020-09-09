using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class uicon : MonoBehaviour
{

    public Text rocks;
    public Image health;
    public Image bar;
    public Image topbox;
    public playerCon p;
    public int swidth;
    public int sheight;
    public Image rightArrow;
    public Image leftArrow;
    public Image middle;
    public int maxwidth;
    public bool o = false;
    public bool c = false;
    public GameObject textbar;
    public string[] testlist;
    public int currenttext = 0;
    public Text showntext;
    public GameObject wall;
    public Image bossheath;
    public Image bossbar;
    public Text t;
    public boss b;
    public KeyCode[] keylist;
    
    // Start is called before the first frame update
    void Start()
    {
       
        swidth = Screen.width;
        sheight = Screen.height;
        maxwidth = sheight / 2;
        rocks.rectTransform.transform.position = new Vector3(swidth / 11, sheight / 1.15f, 0);
        rocks.fontSize = swidth / 25;
        health.rectTransform.transform.position = new Vector3(swidth / 10.5f, sheight / 1.05f, 0);
        bar.rectTransform.transform.position = new Vector3(swidth / 10.5f, sheight / 1.05f, 0);
        health.rectTransform.sizeDelta = new Vector2(swidth / 6, swidth / 50);
        bar.rectTransform.sizeDelta = new Vector2(swidth / 6, swidth / 50);
        topbox.rectTransform.sizeDelta = new Vector2(swidth/5,swidth/10);
        topbox.rectTransform.transform.position = new Vector3(swidth / 10.3f, sheight - swidth / 21, 0);
        rightArrow.rectTransform.sizeDelta = new Vector2(swidth/12,swidth/10);
        rightArrow.rectTransform.transform.position = new Vector3(swidth/2 + swidth / 10, sheight/6,0);
        middle.rectTransform.sizeDelta = new Vector2(swidth / 7.2f, swidth / 10);
        middle.rectTransform.transform.position = new Vector3(swidth / 2, sheight / 6, 0);
        leftArrow.rectTransform.sizeDelta = new Vector2(swidth / 12, swidth / 10);
        leftArrow.rectTransform.transform.position = new Vector3(swidth / 2 - swidth / 10, sheight / 6, 0);
        bossheath.rectTransform.sizeDelta = new Vector2(swidth / 3, swidth / 25);
        bossbar.rectTransform.sizeDelta = new Vector2(swidth / 3, swidth / 25);
        bossheath.rectTransform.transform.position = new Vector3(swidth / 2, sheight - swidth / 21, 0);
        bossbar.rectTransform.transform.position = new Vector3(swidth / 2, sheight - swidth / 21, 0);
        t.rectTransform.sizeDelta = new Vector2(swidth / 4f, swidth / 10);
        t.rectTransform.transform.position = new Vector3(swidth / 2, sheight / 6 - sheight/20, 0);
        t.fontSize = sheight / 20;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(keylist[currenttext]))
        {
            next();
        }
        if (o)
        {
            open();
        }
        if (c)
        {
            close();
        }
        
        bossheath.rectTransform.sizeDelta = new Vector2(b.health / (500.0f / (swidth / 3)), swidth / 25);
        rocks.text = "rocks: " + p.rocks.Count;
        health.rectTransform.localScale = new Vector3(p.health / 200f, 1, 1);
    }

    public void next()
    {
        if (currenttext >= testlist.Length - 1)
        {
            c = true;
            o = false;
        }
        else
        {
            currenttext++;
            showntext.text = testlist[currenttext];
        }
    }

    public void open()
    {
       
        textbar.SetActive(true);
        if (rightArrow.rectTransform.transform.position.x - GetComponent<RectTransform>().transform.position.x < maxwidth)
        {
            rightArrow.rectTransform.transform.position += Vector3.right*3;
            leftArrow.rectTransform.transform.position += Vector3.left*3;
            middle.rectTransform.sizeDelta += new Vector2(6, 0);
        }
        else
        {
             showntext.text = testlist[currenttext];
            o = false;
        }

       
    }

    public void close()
    {
        currenttext = 0;
        showntext.text = "";
        if (rightArrow.rectTransform.transform.position.x - GetComponent<RectTransform>().transform.position.x > swidth / 12)
        {
            rightArrow.rectTransform.transform.position -= Vector3.right * 3;
            leftArrow.rectTransform.transform.position -= Vector3.left * 3;
            middle.rectTransform.sizeDelta -= new Vector2(6, 0);
        }
        else
        {
            textbar.SetActive(false);
            c = false;
        }
        breakwall();

    }

    public void breakwall()
    {
        foreach (Rigidbody r in wall.GetComponentsInChildren<Rigidbody>())
        {
            r.isKinematic = false;
        }
    }
}
