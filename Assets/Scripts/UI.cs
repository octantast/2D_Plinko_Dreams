using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    private AsyncOperation asyncOperation;

    public TouchController touches;
    public PlinkoLevel plinko;
    public GeneralController general;

    private float volume;
    public List<AudioSource> sounds;

    private bool reloadThis;
    private bool reload;
    private float loadingtimer = 3;

    public GameObject volumeOn;
    public GameObject volumeOff;
    public GameObject loadingScreen;
   // public Image loading;
    public GameObject settingScreen;
    public GameObject winScreen;
    public GameObject loseScreen;

    private float mode; // unique level
    public int howManyLevelsDone; // real number of last level
    private int levelMax; // how many levels total
    public float chosenLevel; // real number of level

    public int levelmoneyBonus;
    public TMP_Text levelmoneys;
    public TMP_Text levelTimer;
    public int moneys;
    public int price1;
    public int price2;
    public TMP_Text price1text;
    public TMP_Text price2text;
    public List<TMP_Text> moneysText;

    // skills
    public float a2timer;
    public float a2timerMax;
    public Image a2activeskale;
    public bool a2active;
    public float timerindex = 1;

    // tips
    public Animator tipAnimator;

    public int tutorial1;
    public int tutorial2;
    public int tutorial3;

    public GameObject lightObject;
    //private bool lighton;
    public void Start()
    {
        Time.timeScale = 1;
        asyncOperation = SceneManager.LoadSceneAsync("Preloader");
        asyncOperation.allowSceneActivation = false;

        moneys = PlayerPrefs.GetInt("moneys");
        mode = PlayerPrefs.GetFloat("mode");
        levelMax = PlayerPrefs.GetInt("levelMax");
        volume = PlayerPrefs.GetFloat("volume");
        chosenLevel = PlayerPrefs.GetFloat("chosenLevel");
        howManyLevelsDone = PlayerPrefs.GetInt("howManyLevelsDone");

        tutorial1 = PlayerPrefs.GetInt("tutorial1");
        tutorial2 = PlayerPrefs.GetInt("tutorial2");
        tutorial3 = PlayerPrefs.GetInt("tutorial3");

        sounds[0].Play();
        if (volume == 1)
        {
            Sound(true);
        }
        else
        {
            Sound(false);
        }

        winScreen.SetActive(false);
        loseScreen.SetActive(false);
        settingScreen.SetActive(false);
        loadingScreen.SetActive(false);

        tipAnimator.enabled = false;
        price1text.text = price1.ToString("0");
        price2text.text = price2.ToString("0");


       
        // levels
        if (mode != 0)
        {
            if (mode == 1)
            {
                general.timercurrent = 60;
                general.numberOfBalls = 3;
                general.ballstocollect = 1;
                general.backgrounds[0].SetActive(true);
                general.dotVariants[0].SetActive(true);
                levelmoneyBonus = 30;
            }
            else if (mode == 2)
            {
                general.timercurrent = 90;
                general.numberOfBalls = 3;
                general.ballstocollect = 1;
                general.backgrounds[0].SetActive(true);
                general.dotVariants[1].SetActive(true);
                levelmoneyBonus = 40;
            }
            else if (mode == 3)
            {
                general.timercurrent = 120;
                general.numberOfBalls = 10;
                general.ballstocollect = 2;
                general.backgrounds[1].SetActive(true);
                general.dotVariants[2].SetActive(true);
                levelmoneyBonus = 50;
            }
            else if (mode == 4)
            {
                general.timercurrent = 120;
                general.numberOfBalls = 10;
                general.ballstocollect = 3;
                general.backgrounds[2].SetActive(true);
                general.dotVariants[3].SetActive(true);
                levelmoneyBonus = 50;

            }
            else if (mode == 5)
            {
                general.timercurrent = 120;
                general.numberOfBalls = 12;
                general.ballstocollect = 3;
                general.backgrounds[3].SetActive(true);
                general.dotVariants[4].SetActive(true);
                levelmoneyBonus = 50;
            }
            else if (mode == 6)
            {
                general.timercurrent = 180;
                general.numberOfBalls = 12;
                general.ballstocollect = 7;
                general.backgrounds[0].SetActive(true);
                general.dotVariants[5].SetActive(true);
                levelmoneyBonus = 100;
            }
            else if (mode == 7)
            {
                general.timercurrent = 30;
                general.numberOfBalls = 5;
                general.ballstocollect = 5;
                general.backgrounds[0].SetActive(true);
                general.dotVariants[6].SetActive(true);
                levelmoneyBonus = 100;
            }
            else if (mode == 8)
            {
                general.timercurrent = 180;
                general.numberOfBalls = 27;
                general.ballstocollect = 10;
                general.backgrounds[1].SetActive(true);
                general.dotVariants[7].SetActive(true);
                levelmoneyBonus = 100;
            }
            else if (mode == 9)
            {
                general.timercurrent = 180;
                general.numberOfBalls = 30;
                general.ballstocollect = 11;
                general.backgrounds[2].SetActive(true);
                general.dotVariants[8].SetActive(true);
                levelmoneyBonus = 100;
            }
            else if (mode == 10)
            {
                general.timercurrent = 180;
                general.numberOfBalls = 30;
                general.ballstocollect = 11;
                general.backgrounds[3].SetActive(true);
                general.dotVariants[9].SetActive(true);
                levelmoneyBonus = 100;
            }

        }

        foreach (TMP_Text text in general.numberOfBallstext)
        {
            text.text = general.ballstocollect.ToString("0");
        }
        general.ballstocollecttext.text = general.numberOfBalls.ToString("0"); 

        levelmoneys.text = "+" + levelmoneyBonus.ToString("0") + "!";

        if (tutorial1 == 0)
        {
            //tutorial1 = 1;
            PlayerPrefs.SetInt("tutorial1", 1);
            PlayerPrefs.Save();
            tipAnimator.enabled = false;
            tipAnimator.Play("Start");
            tipAnimator.enabled = true;
        }
        else if (tutorial1 != 0 && tutorial2 == 0 && moneys >= price1)
        {
            PlayerPrefs.SetInt("tutorial2", 1);
            PlayerPrefs.Save();
            tipAnimator.enabled = false;
            tipAnimator.Play("Bonuses");
            tipAnimator.enabled = true;
        }

    }

    public void Update()
    {
        //if (!lighton)
        //{
        //    lighton = true;
        //    GameObject lightObj = Instantiate(lightObject, transform.position, Quaternion.Euler(60, -30, 0));
        //    lightObj.transform.position = Vector3.zero;
        //}

        
        foreach (TMP_Text text in moneysText)
        {
            text.text = moneys.ToString("0");
        }


        if (loadingScreen.activeSelf == true)
        {
            foreach (AudioSource audio in sounds)
            {
                audio.volume = 0;
            }

            if (loadingtimer > 0)
            {
                loadingtimer -= Time.deltaTime;
            }
            else
            {
                if (!reload)
                {
                    reload = true;
                    if (reloadThis)
                    {
                        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                    }
                    else
                    {
                        asyncOperation.allowSceneActivation = true;
                    }
                }
           }
        }
        if (!loadingScreen.activeSelf)
        {
            foreach (AudioSource audio in sounds)
            {
                audio.volume = volume;
            }
        }
    }

    public void ExitMenu()
    {
        Time.timeScale = 1;
        sounds[1].Play();
        plinko.isChoosingAngle = false;
        general.paused = false;
        asyncOperation.allowSceneActivation = true;
        //loading.fillAmount = 0;
        loadingScreen.SetActive(true);
        //loading.enabled = false;
    }
    public void reloadScene()
    {
        Time.timeScale = 1;
        sounds[1].Play();
        //general.paused = false;
        //loading.fillAmount = 0;
        reloadThis = true;
        loadingScreen.SetActive(true);
    }
    public void Sound(bool volumeBool)
    {
        if (volumeBool)
        {
            volumeOn.SetActive(true);
            volumeOff.SetActive(false);
            volume = 1;
        }
        else
        {
            volume = 0;
            volumeOn.SetActive(false);
            volumeOff.SetActive(true);
        }

        PlayerPrefs.SetFloat("volume", volume);
        PlayerPrefs.Save();
    }

    public void closeIt()
    {
        sounds[1].Play();
        touches.blocked = true;
        plinko.isChoosingAngle = false;
        general.paused = false;
        settingScreen.SetActive(false);
        Time.timeScale = 1;
    }

    public void Settings()
    {
        sounds[1].Play();
        touches.blocked = true;
        plinko.isChoosingAngle = false;
        general.paused = true;
        settingScreen.SetActive(true);
        Time.timeScale = 0;
    }

    public void a1()
    {
        sounds[1].Play();
        plinko.isChoosingAngle = false;
        touches.blocked = true;

        if (moneys >= price1)
        {
            moneys -= price1;
            PlayerPrefs.SetInt("moneys", moneys);
            PlayerPrefs.Save();
            general.numberOfBalls += 3;
            general.ballstocollecttext.text = general.numberOfBalls.ToString("0");
        }
        else
        {
           //tipAnimator.enabled = false;
            tipAnimator.Play("Warning");
            tipAnimator.enabled = true;

        }
    }

    public void a2()
    {
        sounds[1].Play();
        plinko.isChoosingAngle = false;
        touches.blocked = true;

        if (moneys >= price2)
        {
            if (!a2active)
            {
                moneys -= price2;
                PlayerPrefs.SetInt("moneys", moneys);
                PlayerPrefs.Save();
                a2active = true;
                timerindex = 0;
                a2timer = a2timerMax;
            }
        }
        else
        {

             //tipAnimator.enabled = false;
            tipAnimator.Play("Warning");
            tipAnimator.enabled = true;

        }
    }
    public void NextLevel()
    {
        Time.timeScale = 1;
        sounds[1].Play();
        if (chosenLevel <= howManyLevelsDone + 1 && chosenLevel != levelMax)
        {
            chosenLevel += 1;
            mode += 1;
            if (mode > 10)
            {
                mode = 1;
            }


            PlayerPrefs.SetFloat("chosenLevel", chosenLevel);
            PlayerPrefs.SetFloat("mode", mode);
            PlayerPrefs.Save();
            reloadScene();
        }
    }
}
