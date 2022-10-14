using UnityEngine;
public class Ball : MonoBehaviour
{
    [SerializeField] private int BallIndex;
    [SerializeField] private Collisions_UI collisions_UI;
    [SerializeField] private GameObject arrow;
    [SerializeField] private LineRenderer line1_2;
    [SerializeField] private LineRenderer line1_3;
    [SerializeField] private LineRenderer line2_3;
    public Rigidbody rb_ball;
    private RaycastHit hit_pick;
    private float destination = 1;
    private Vector3 pos;
    private Vector3 diff;
    private void OnMouseDrag()
    {
        arrow.SetActive(true);
        arrow.transform.position = transform.position;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit_pick))
        {
            arrow.transform.LookAt(new Vector3(hit_pick.point.x, transform.position.y, hit_pick.point.z));
        }
    }
    private void OnMouseDown()
    {
        Ray ray2 = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray2, out hit_pick))
        {
            if (hit_pick.transform.CompareTag("Player"))
            {
                line2_3.gameObject.SetActive(true);
            }
            else if (hit_pick.transform.CompareTag("Player2"))
            {
                line1_3.gameObject.SetActive(true);
            }
            else if (hit_pick.transform.CompareTag("Player3"))
            {
                line1_2.gameObject.SetActive(true);
            }
        }
    }
    private void OnMouseOver()
    {
        arrow.SetActive(false);

    }
    private void OnMouseUp()
    {
        if (arrow.activeSelf)
        {
            Shooting.hitted = false;
            PickShotPower();
            pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 9.3f));
            diff = (transform.position - pos).normalized;
        }
        else
        {
            line1_2.gameObject.SetActive(false);
            line1_3.gameObject.SetActive(false);
            line2_3.gameObject.SetActive(false);
        }
    }
    private void PickShotPower()
    {
        collisions_UI.Shooting_Panel.SetActive(true);
        if (BallIndex==1)//hangi topa ait olan butonun aktif edilmesi ayarlandi.
        {
            collisions_UI.Shot_Buttons[0].SetActive(true);
        }
        if (BallIndex == 2)
        {
            collisions_UI.Shot_Buttons[1].SetActive(true);
        }
        if (BallIndex == 3)
        {
            collisions_UI.Shot_Buttons[2].SetActive(true);
        }
        InvokeRepeating(nameof(ShotBar), .2f, .07f);
    }
    public void BallThrow()
    {
        float shot_power = collisions_UI.ShotButton_image.fillAmount;
        CancelInvoke(nameof(ShotBar));
        collisions_UI.ShotButton_image.fillAmount = 0f;
        arrow.SetActive(false);
        collisions_UI.Shooting_Panel.SetActive(false);
        line1_2.gameObject.SetActive(false);
        line1_3.gameObject.SetActive(false);
        line2_3.gameObject.SetActive(false);
        for (int i = 0; i < collisions_UI.Shot_Buttons.Length; i++)
        {
            collisions_UI.Shot_Buttons[i].SetActive(false);
        }
        rb_ball.velocity = diff * shot_power * 4.5f;
        Shooting.accurated_shot = false;//atis basarili mi degil mi hesaplanmadigini gösterdi, hesaplanmadigi icin hesaplansin denilmis oldu.
        Invoke("BallStop", 1f);//topun hareket suresi
    }
    public void BallStop()
    {
        rb_ball.velocity = Vector3.zero;
        if (!Shooting.accurated_shot)
        {
            Shooting.shot_count++; /*sadece basarisizsa sut sayisini arttir dedik, basarili sutlarda sut sayisi diger bir scriptten arttiriliyor.
            basarili sutlarda shot_success de arttiriliyor, yani atis basarisiz olursa shot_success burada artmadigi icin degerler farkli olacak
            ve gameover tetiklenecek.*/
        }
        transform.GetComponent<BoxCollider>().enabled = false;//raycast cizgisinden gecerken degil top durunca collideri kapadýk,ses ve shot text artiminin tekrar etmemesi icin de hitted kullanildi.
    }
    public void ShotBar()
    {
        collisions_UI.ShotButton_image.fillAmount += 0.05f * destination;
        if (collisions_UI.ShotButton_image.fillAmount == 1f)
        {
            destination = -1;
        }
        if (collisions_UI.ShotButton_image.fillAmount == 0f)
        {
            destination = 1;
        }
    }
}