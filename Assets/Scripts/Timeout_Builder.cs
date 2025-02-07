using System;
using System.Timers;
using UnityEngine;

public class Timeout_Builder
{
    private Action callback;
    private float seconds;
    private Timer timer;

    public void Build()
    {
        if (callback == null)
        {
            throw new InvalidOperationException("Callback must be set.");
        }

        if (seconds <= 0)
        {
            throw new InvalidOperationException("Delay time must be greater than 0.");
        }

        timer = new Timer(1000);
        timer.Elapsed += delegate
        {
            callback?.Invoke();
            Debug.Log("Tick");
        };
        timer.AutoReset = false;
        timer.Start();
    }

    public Timeout_Builder SetCallback(Action callback)
    {
        this.callback = callback;
        return this;
    }

    public Timeout_Builder SetDelay(float seconds)
    {
        this.seconds = seconds;
        return this;
    }

    public void StopTimeout()
    {
        timer?.Stop();
        timer?.Dispose();
    }

    private void OnTimeout(object sender, ElapsedEventArgs e)
    {
        callback?.Invoke();
        timer.Stop();
        timer.Dispose();
    }
}
