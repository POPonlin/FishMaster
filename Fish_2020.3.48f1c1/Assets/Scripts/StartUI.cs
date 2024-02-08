using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartUI : MonoBehaviour
{
    public GameObject tips;
    public GameObject recode;

    public Text noGameText;
    public Text scoresText;
    public Text beiText;

    public void StartGame()
    {
        PlayerPrefs.DeleteKey("sTimer");
        PlayerPrefs.DeleteKey("bTimer");
        PlayerPrefs.DeleteKey("Scores");
        PlayerPrefs.DeleteKey("Exp");
        PlayerPrefs.DeleteKey("OwnGold");
        PlayerPrefs.DeleteKey("Lv");
        PlayerPrefs.DeleteKey("bgm");
        PlayerPrefs.DeleteKey("audio");

        DonDes.Instance.openChallenge = false;
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public void LoadGame()
    {
        if (PlayerPrefs.HasKey("MaxScores") == false)
        {
            noGameText.gameObject.SetActive(true);
            StartCoroutine(noGameText.GetComponent<Ef_HideSelf>().HideSelf(0.3f));
            return;
        }
        DonDes.Instance.openChallenge = false;
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public void ChallengeMode()
    {
        DonDes.Instance.openChallenge = true;
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void OpenTips()
    {
        tips.SetActive(true);
    }

    public void CloseTips()
    {
        tips.SetActive(false);
    }

    public void OpenRecode()
    {
        recode.SetActive(true);
        scoresText.text = PlayerPrefs.GetFloat("MaxScores", 0).ToString();
        beiText.text = PlayerPrefs.GetInt("Bei", 0).ToString();
    }

    public void CloseRecode()
    {
        recode.SetActive(false);
    }

    public void DeleteAllData()
    {
        PlayerPrefs.DeleteAll();
    }
}
