using UnityEngine;

public class StageManager : MonoBehaviour
{
    public int dotsConnected;
    private float timePassed;

    private bool canTimePass = true;

    private void Start()
    {
        Time.timeScale = 100.0f;
    }

    private void Update()
    {
        //if (canTimePass)
        //{
        //    timePassed += Time.deltaTime;

        //    print(DisplayTime(timePassed));
        //}
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
            timer = minutes.ToString("00") + ":" + seconds.ToString("00");
        }
        else
        {
            timer = hours.ToString() + ":" + minutes.ToString("00") + ":" + seconds.ToString("00");
        }

        return timer;
    }
}
