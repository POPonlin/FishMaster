using UnityEngine;
using UnityEngine.UI;

public class Challenge : MonoBehaviour
{
    private static Challenge instance;
    public static Challenge Instance { get { return instance; } }

    public Image taskImage;
    public Image trophiesImage;
    public Image endImage;

    public Text upText;
    public Text timerText;
    public Text trophiesText;
    public Text taskText;
    private float timer = 0f;
    private int currentTask = 1;
    public int trophies = 0;
    private bool challengeComplete = false;
    // Update is called once per frame

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        taskImage!.gameObject.SetActive(DonDes.Instance.openChallenge);
        trophiesImage!.gameObject.SetActive(DonDes.Instance.openChallenge);
        if (DonDes.Instance.openChallenge)
        {
            upText!.text = "挑战模式";
        }
        else
        {
            upText!.text = "无尽模式";
        }
    }

    void Update()
    {
        if (!DonDes.Instance.openChallenge)
        {
            return;
        }

        if (!challengeComplete)
        {
            timer += Time.deltaTime;
            timerText.text = ((int)timer).ToString();

            if (currentTask == 4)
            {
                if (timer < 120)
                {
                    if (GameControler.Instance.Scores >= 10000)
                    {
                        trophies++;
                        GameControler.Instance.Scores -= 10000;
                    }
                }
                else
                {
                    challengeComplete = true;
                    Time.timeScale = 0f;
                    endImage.gameObject.SetActive(true);
                }
            }
            else
            {
                if (timer >= GetTargetTime(currentTask))
                {
                    if (GameControler.Instance.Scores >= GetTargetScore(currentTask))
                    {                        
                        trophies++;
                        trophiesText.text = "x " + trophies.ToString();
                    }
                    GameControler.Instance.Scores = 0;
                    currentTask++;
                    timer = 0f;
                    if (currentTask < 4)
                    {
                        taskText.text = "在" + GetTargetTime(currentTask) + "s内达成" + GetTargetScore(currentTask) + "分";
                    }
                    else if (currentTask == 4)
                    {
                        taskText.text = "在120s内获取足够多奖杯";
                    }
                }
            }
        }
    }

    int GetTargetScore(int task)
    {
        return task * 10000;
    }

    int GetTargetTime(int task)
    {
        switch (task)
        {
            case 1:
                return 20;
            case 2:
                return 40;
            case 3:
                return 60;
            default:
                return 0;
        }
    }
}
