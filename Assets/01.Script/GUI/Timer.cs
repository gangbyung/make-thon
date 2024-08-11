using TMPro;
using UnityEngine;
public class Timer : MonoBehaviour
{
    public TextMeshProUGUI TimerText;
    float time = 0;
    void Update()
    {
        time += Time.deltaTime;
        TimerText.text = "" + Mathf.Round(time);
    }
}
