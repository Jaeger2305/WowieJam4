using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    [SerializeField]
    float tickSpeed = 0.2f;

    [SerializeField]
    int quickTickSpeed = 1;
    [SerializeField]
    int normalTickSpeed = 3;
    [SerializeField]
    int slowTickSpeed = 5;

    public UnityEvent quickTick;
    public UnityEvent normalTick;
    public UnityEvent slowTick;

    private float timer = 0f;
    private int tick = 0;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= tickSpeed)
        {
            timer -= tickSpeed;
            tick++;
            if (tick % quickTickSpeed == 0) quickTick.Invoke();
            if (tick % normalTickSpeed == 0) normalTick.Invoke();
            if (tick % slowTickSpeed == 0) slowTick.Invoke();
        }

    }
}
