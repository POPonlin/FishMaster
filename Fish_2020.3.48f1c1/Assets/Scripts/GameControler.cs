using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameControler : MonoBehaviour
{
    private static GameControler instance;
    public static GameControler Instance
    {
        get
        {
            return instance;
        }
    }
    public Toggle bgmToggle;
    public Toggle audioToggle;

    public Text scoresText;

    public Image Setting;

    public Sprite[] bgs;
    public GameObject seaWave;
    public Image bg;

    public ParticleSystem gunChangePar;
    public ParticleSystem LevelupPar;
    public Image levelupTip;

    public Text lvText;
    public Text lvName;
    public Text goldText;
    public Text bigCountDownText;
    public Text sCountDownText;
    public Button goldButton;
    public const float bCountDown = 60f;
    public const float sCountDown = 15f;
    public Slider expSlider;

    public Text costText;
    public GameObject[] gunPanels;
    public GameObject[] bullet1;
    public GameObject[] bullet2;
    public GameObject[] bullet3;
    public GameObject[] bullet4;
    public int Lv
    {
        get { return lv; }
        set
        {
            if (lv < 99)
            {
                lv = value;
            }
            else
            {
                return;
            }
        }

    }

    public Transform bulletPanel;
    public float Exp
    {
        get
        {
            return exp;
        }
        set
        {
            if (exp < 0)
            {
                exp = 0;
            }
            else
            {
                exp = value;
            }
        }
    }
    public float OwnGold
    {
        get
        {
            return ownGold;
        }
        set
        {
            ownGold = value;
            if (ownGold > 999999f)
            {
                ownGold = 999999f;
            }
        }
    }

    public float Scores
    {
        get
        {
            return scores;
        }
        set
        {            
            if (scores == 999999999f)
            {
                return;
            }
            scores = (scores + value) <= 999999999f ? value : 999999999f;
        }
    }

    private int[] damage = { 10, 20, 30, 40, 50, 60, 70, 80, 90, 100, 200, 300, 400, 500, 600, 700, 800, 900, 1000, 2000 };
    private int costIndex = 0;
    private GameObject[] currBullet;
    private float bTimer = 0f;
    private float sTimer = 0f;
    private string[] lvString = { "青铜", "白银", "黄金", "铂金", "钻石", "黑曜", "大师", "宗师", "超凡", "化境" };

    private float exp = 0f;
    private float ownGold = 100000f;
    private int lv = 0;
    private Color color;
    private int bgIndex = 0;
    private float scores = 0;
    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        currBullet = bullet1;
        lvText.text = "0";
        lvName.text = "青铜";
        expSlider.value = Exp;
        color = goldText.color;

        if (!DonDes.Instance.openChallenge)
        {
            sTimer = PlayerPrefs.GetFloat("sTimer", sCountDown);
            bTimer = PlayerPrefs.GetFloat("bTimer", bCountDown);
            Scores = PlayerPrefs.GetFloat("Scores", 0f);
            Exp = PlayerPrefs.GetFloat("Exp", 0);
            OwnGold = PlayerPrefs.GetFloat("OwnGold", 100000f);
            Lv = PlayerPrefs.GetInt("Lv", 0);
        }

        int bgm = PlayerPrefs.GetInt("bgm", 1);
        int audio = PlayerPrefs.GetInt("audio", 1);
        AudioManger.Instance.isOpen = bgm == 1 ? true : false;
        AudioManger.Instance.isOpen1 = audio == 1 ? true : false;
        bgmToggle.isOn = AudioManger.Instance.isOpen;
        audioToggle.isOn = AudioManger.Instance.isOpen1;
        AudioManger.Instance.DoBGMusic();
    }


    void Update()
    {
        Fire();
        ChangeCostValue();
        UIUpdate();
    }

    void UIUpdate()
    {

        bTimer -= Time.deltaTime;
        sTimer -= Time.deltaTime;

        if (bTimer <= 0f && bigCountDownText.IsActive() == true)
        {
            bigCountDownText.gameObject.SetActive(false);
            goldButton.gameObject.SetActive(true);
        }

        if (sTimer <= 0f)
        {
            OwnGold += 200f;
            sTimer = sCountDown;
        }

        while (Exp > 1000 + 200 * (Lv + 1) && Lv < 99)
        {
            Lv++;
            Exp -= 1000 + 200 * Lv;
            ParticleSystem p = Instantiate(LevelupPar);
            p.gameObject.AddComponent<Ef_Destory>().destoryWaitTime = 1f;
            levelupTip.gameObject.SetActive(true);
            StartCoroutine(levelupTip.gameObject.AddComponent<Ef_HideSelf>().HideSelf(0.5f));
            levelupTip.transform.Find("LevelUpText").GetComponent<Text>().text = Lv.ToString();
            AudioManger.Instance.PlayMusic(AudioManger.Instance.levelUp);
        }

        if (bgIndex != lv / 20)
        {
            bgIndex = lv / 20;
            if (bgIndex > 3)
            {
                return;
            }

            GameObject tmp = Instantiate(seaWave);
            tmp.transform.position = new Vector3(transform.position.x + 9f, transform.position.y, -3f);
            tmp.AddComponent<Ef_MoveTo>().targetPos = new Vector3(transform.position.x - 9f, transform.position.y, -3f);
            tmp.GetComponent<Ef_MoveTo>().speed = 10f; 
            tmp.AddComponent<Ef_Destory>().destoryWaitTime = 2f;
            bg.sprite = bgs[bgIndex];
            AudioManger.Instance.ChangeBGMusic(bgIndex);
            AudioManger.Instance.DoBGMusic();
        }

        scoresText.text = Scores.ToString();
        lvText.text = Lv.ToString();
        lvName.text = lvString[Lv / 10];
        expSlider.value = Exp / (1000 + 200 * (Lv + 1));
        bigCountDownText.text = Mathf.CeilToInt(bTimer) + "s";
        sCountDownText.text = Mathf.CeilToInt(sTimer) / 10 + " " + Mathf.CeilToInt(sTimer) % 10;
        goldText.text = '$' + OwnGold.ToString();
    }

    public void CloseSetting()
    {
        Setting.gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

    public  void OpenSetting()
    {
        Setting.gameObject.SetActive(true);
        Time.timeScale = 0f;
    }

    public void BackToStart()
    {
        if (!DonDes.Instance.openChallenge)
        {
            PlayerPrefs.SetFloat("sTimer", sTimer);
            PlayerPrefs.SetFloat("bTimer", bTimer);
            PlayerPrefs.SetFloat("Scores", Scores);
            PlayerPrefs.SetFloat("Exp", Exp);
            PlayerPrefs.SetFloat("OwnGold", OwnGold);
            PlayerPrefs.SetInt("Lv", Lv);
            if (PlayerPrefs.HasKey("MaxScores"))
            {
                float tmp = PlayerPrefs.GetFloat("MaxScores");
                if (Scores > tmp)
                {
                    PlayerPrefs.SetFloat("MaxScores", Scores);
                }
            }
            else
            {
                PlayerPrefs.SetFloat("MaxScores", Scores);
            }
        }
        else
        {
            PlayerPrefs.SetInt("Bei", Challenge.Instance.trophies);
        }

        int bgm = AudioManger.Instance.isOpen == true ? 1 : 0;
        int audio = AudioManger.Instance.isOpen1 == true ? 1 : 0;
        PlayerPrefs.SetInt("bgm", bgm);
        PlayerPrefs.SetInt("audio", audio);
        PlayerPrefs.Save();

        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void GoldButtonDown()
    {
        goldButton.gameObject.SetActive(false);
        bigCountDownText.gameObject.SetActive(true);
        bTimer = bCountDown;
        OwnGold += 5000f;
        AudioManger.Instance.PlayMusic(AudioManger.Instance.ag);
    }

    void ChangeCostValue()
    {
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            SubCost();
        }
        else if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            AddCost();
        }
    }

    public void AddCost()
    {
        ParticleSystem p = Instantiate(gunChangePar);
        p.gameObject.AddComponent<Ef_Destory>().destoryWaitTime = 1f;
        gunPanels[costIndex / 4].SetActive(false);
        costIndex = (costIndex + 1) % damage.Length;
        gunPanels[costIndex / 4].SetActive(true);
        costText.text = '$' + damage[costIndex].ToString();
        AudioManger.Instance.PlayMusic(AudioManger.Instance.changeGun);
    }

    public void SubCost()
    {
        ParticleSystem p = Instantiate(gunChangePar);
        p.gameObject.AddComponent<Ef_Destory>().destoryWaitTime = 1f;
        gunPanels[costIndex / 4].SetActive(false);
        costIndex = costIndex == 0 ? damage.Length - 1 : --costIndex;
        gunPanels[costIndex / 4].SetActive(true);
        costText.text = '$' + damage[costIndex].ToString();
        AudioManger.Instance.PlayMusic(AudioManger.Instance.changeGun);
    }

    void Fire()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && EventSystem.current.IsPointerOverGameObject() == false)
        {
            if (OwnGold - damage[costIndex] > 0)
            {
                switch (costIndex / 4)
                {
                    case 0: currBullet = bullet1; break;
                    case 1: currBullet = bullet2; break;
                    case 2: currBullet = bullet3; break;
                    case 3: currBullet = bullet4; break;
                    case 4: currBullet = bullet4; break;
                }
                OwnGold -= damage[costIndex];                
                GameObject bullet = Instantiate(currBullet[Lv / 10]);
                AudioManger.Instance.PlayMusic(AudioManger.Instance.fire);
                bullet.transform.SetParent(bulletPanel, false);
                bullet.transform.position = gunPanels[costIndex / 4].transform.Find("FirePos").position;
                bullet.transform.rotation = gunPanels[costIndex / 4].transform.Find("FirePos").rotation;

                bullet.GetComponent<BulletAtter>().damage = damage[costIndex];
                bullet.AddComponent<Ef_AutoMove>().dir = Vector3.up;
                bullet.GetComponent<Ef_AutoMove>().speed = bullet.GetComponent<BulletAtter>().speed;
            }
            else
            {
                StartCoroutine("ChangeTextColor");
                AudioManger.Instance.PlayMusic(AudioManger.Instance.fireNull);
            }
        }
    }

    IEnumerator ChangeTextColor()
    {
        goldText.color = color;
        goldText.color = Color.red;
        yield return new WaitForSeconds(0.8f);
        goldText.color = color;
    }
}
