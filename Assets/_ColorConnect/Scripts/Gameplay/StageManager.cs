using UnityEngine;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    public int dotsConnected;
    private float timePassed;
    [SerializeField] private Text timer;

    private void Update()
    {
        timePassed += Time.deltaTime;

        timer.text = DisplayTime(timePassed);
    }

    private string DisplayTime(float time)
    {
        int seconds;
        int minutes;
        int hours;

        seconds = Mathf.FloorToInt(time % 60);
        minutes = Mathf.FloorToInt((time % 3600) / 60);
        hours = Mathf.FloorToInt(time / 3600);

        string timer;

        if (hours <= 0)
        {
            timer = minutes.ToString("0") + ":" + seconds.ToString("00");
        }
        else
        {
            timer = hours.ToString() + ":" + minutes.ToString("00") + ":" + seconds.ToString("00");
        }

        return timer;
    }
}
