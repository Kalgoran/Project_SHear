using UnityEngine;

public class ApplicationManager : MonoBehaviour
{
    public int defaultTargetFrameRate = -1;
    [SerializeField]
    private int minimalTargetFrameRate = 30;

    private int _targetFrameRate = -1;
    public int targetFrameRate
    {
        set
        {
            _targetFrameRate = value;
            if (_targetFrameRate > minimalTargetFrameRate)
                Application.targetFrameRate = value;
            else if (_targetFrameRate > 0)
                Application.targetFrameRate = minimalTargetFrameRate;
            else
                Application.targetFrameRate = -1;
        }
    }


    private void Awake()
    {
        targetFrameRate = defaultTargetFrameRate;
    }
}