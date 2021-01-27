using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    Color[] colors = new Color[4];
    Color target;
    int i;
    Text txt;
    public float interval;
    
    void Start()
    {
        txt = GameObject.Find("Text").GetComponent<Text>();
        txt.text = "You Found $" + PlayerPrefs.GetInt("Gold").ToString() + " worth of gold!";
        i = 0;
        colors[0] = Color.red;
        colors[1] = Color.yellow;
        colors[2] = Color.blue;
        colors[3] = Color.green;
        target = colors[i];
    }

    private void Update()
    {
        GameObject.Find("Text").GetComponent<Text>().color = Color.Lerp(txt.color, target, interval * Time.deltaTime);
        if(txt.color == target)
        {
            if(i < 3)
            {
                i++;
            }
            else
            {
                i = 0;
            }
        }
        target = colors[i];
    }

}
