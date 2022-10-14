using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class Collisions_UI : MonoBehaviour
{
    public static int shot_success;
    public Image ShotButton_image;
    public GameObject Shooting_Panel;
    public GameObject[] Shot_Buttons;
    [SerializeField] private GameObject Won_Panel;
    [SerializeField] private GameObject Lost_Panel;
    [SerializeField] private Transform Target_Panel;
    [SerializeField] private TextMeshProUGUI shot_text;
    [SerializeField] private Animator KeeperAni;
    [SerializeField] private SoundManager soundManager;
    [SerializeField] private Ball ballscript;
    private bool goal = false;// gol olmasina ragmen won panel ustune lost panel cikiyodu sahayý terk ettigi icin, bunu onlemek icin yapildi

    void Start()
    {
        shot_success = 0;
        shot_text.text = "Shot: " + shot_success.ToString();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")|| collision.gameObject.CompareTag("Player2")|| collision.gameObject.CompareTag("Player3"))
        {
            Unsuccess_Throw();
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Field"))
        {
            if (!goal)
            {
                ballscript.rb_ball.velocity = Vector3.zero;
                Unsuccess_Throw();
            }          
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bonus"))
        {
            other.gameObject.SetActive(false);
            soundManager.BonusVoice();
            KeeperAni.enabled = false;
            Invoke("GoalkeeperPlay", 12.5f);
        }
        if (other.gameObject.CompareTag("Finish") && Shooting.hitted)
        {
            goal = true;
            ballscript.BallStop();
            soundManager.GoalVoice();
            Won_Panel.SetActive(true);
            Won_Panel.transform.DOMove(Target_Panel.position, .25f);
            Invoke("PauseGame", .28f);
        }
    }
    public void Success_Throw()
    {
        shot_success++;
        shot_text.text = "Shot: " + shot_success.ToString();
        soundManager.PassVoice();
        Shooting.accurated_shot = true;
        Shooting.shot_count++;
    }
    public void Unsuccess_Throw()
    {
        soundManager.FailVoice();
        Lost_Panel.SetActive(true);
        Lost_Panel.transform.DOMove(Target_Panel.position, .25f);
        Invoke("PauseGame", .28f);
    }
    public void TryAgain()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }
    public void PauseGame()
    {
        Time.timeScale = 0;
    }
    private void GoalkeeperPlay()
    {
        KeeperAni.enabled = true;
    }
}