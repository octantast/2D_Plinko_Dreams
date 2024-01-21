using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GeneralController : MonoBehaviour
{
    public PlinkoLevel plinko;
    public UI ui;
    public TouchController touches;

    // backs
    public List<GameObject> backgrounds;
    public List<GameObject> dotVariants;

    public int numberOfBalls;
    public int ballstocollect;

    public float timercurrent;
    public TMP_Text timer;
    public List<TMP_Text> numberOfBallstext; // target (down)
    public TMP_Text ballstocollecttext; // total (up)

    public List<GameObject> allballs;

    public bool paused;
  
    // effects

   // public ParticleSystem push;
    //public ParticleSystem hole;
    //public ParticleSystem iceParticles;


   public void Update()
    {
        if (!paused)
        {
            if (timercurrent > 0)
            {
                timercurrent -= Time.deltaTime * ui.timerindex;
                int minutes = Mathf.FloorToInt(timercurrent / 60);
                int seconds = Mathf.FloorToInt(timercurrent % 60);
                timer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            }
            else
            {
                timer.text = "TIME`S UP!";
                   lose();
            }

            if(ui.a2timer > 0)
            {
                ui.a2timer -= Time.deltaTime;
                ui.a2activeskale.fillAmount = 1 - ui.a2timer / ui.a2timerMax;
            }
            else
            {
                ui.a2activeskale.fillAmount = 1;
                ui.timerindex = 1;
                ui.a2active = false;
            }
        }
    }
 
    public void ballDone(GameObject thisball)
    {
        ballstocollect -= 1;
        //hole.transform.position = thisball.gameObject.transform.position;
        //hole.gameObject.SetActive(true);
        //hole.Play();
        foreach (TMP_Text text in numberOfBallstext)
        {
            text.text = ballstocollect.ToString("0");
        }

        win(); // check

        ui.sounds[2].Play();


    }

    public void win()
    {
        if (ballstocollect == 0 && timercurrent > 0)
        {
            foreach (GameObject obj in allballs)
            {
                obj.SetActive(false);
            }
            ui.tipAnimator.gameObject.SetActive(false);
            paused = true;
            Debug.Log("win");

            ui.winScreen.SetActive(true);
            if (ui.chosenLevel > ui.howManyLevelsDone)
            {
                PlayerPrefs.SetInt("howManyLevelsDone", (int)ui.chosenLevel);
            }

            ui.moneys += ui.levelmoneyBonus;
            PlayerPrefs.SetInt("moneys", ui.moneys);
            PlayerPrefs.Save();

        }

    }

    public IEnumerator loseornot()
    {
        yield return new WaitForSeconds(2f);
        if(ballstocollect != 0)
        {
            lose();
        }

    }

    public void lose()
    {
        foreach (GameObject obj in allballs)
        {
            obj.SetActive(false);
        }
        ui.tipAnimator.gameObject.SetActive(false);
           Debug.Log("lose");
        paused = true;
        ui.loseScreen.SetActive(true);

    }

}
