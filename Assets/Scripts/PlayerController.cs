using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float gravity;
    [SerializeField] private int coins;
    [SerializeField] private GameObject losePanel;
    [SerializeField] private Text coinsText;
    [SerializeField] private Score scoreScript;

    private CharacterController controller;
    private Vector3 dir;
    private CapsuleCollider capsule;
    private Animator anim;

    private bool isSliding;
    private int maxSpeed = 90;
    private int lineToMove = 1; //номер линии на которой мы находимся 0-левая, 1-средняя и 2- правая 

    public float lineDistance = 3.6f;//растояние между линиями. линией, в данной ситуации, есть центр каждой части дороги

    void Start()
    {
        controller = GetComponent<CharacterController>();//получение ссылок на компоненты(CharacterController, CapsuleCollider, Animator) для работы с ними через код
        capsule = GetComponent<CapsuleCollider>();
        anim = GetComponentInChildren<Animator>();

        Time.timeScale = 1;
        coins = PlayerPrefs.GetInt("Coin");//получение нового значения монет
        coinsText.text = coins.ToString();//перевод и вывод монет на экран
        StartCoroutine(SpeedIncrease());
    }

    private void Update()
    {
        MoveController();
        LineController();
    }

    void FixedUpdate()
    {
        dir.z = speed;
        dir.y += gravity * Time.fixedDeltaTime;
        controller.Move(dir * Time.fixedDeltaTime);
    }

    public void MoveController()
    {
        if (SwipeController.swipeRight)//Прописываю проверку на свайпы вправо и влево
        {
            if (lineToMove < 2)
            {   //проверка на место нахождение среди линий: 0 или 1 или 2
                lineToMove++;
            }
        }

        if (SwipeController.swipeLeft)//свайп влево
        {
            if (lineToMove > 0)
            {
                lineToMove--;
            }
        }

        if (SwipeController.swipeUp)//проверка на свайп вверхсвайп вверх
        {
            if (controller.isGrounded)//проверка на нахождение персонажа на земле
                Jump();//вызов метода если мы на земле
        }

        if (SwipeController.swipeDown)//свайп вниз
        {
            StartCoroutine(Slide());
        }

        if (controller.isGrounded && !isSliding)//если персонаж в воздухе анимация бега прекращается
        {
            anim.SetBool("Run", true);
        }
        else
        {
            anim.SetBool("Run", false);
        }
    }

    public void LineController()
    {
        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;//Расчет позиции после свайпа

        if (lineToMove == 0)
        {
            targetPosition += Vector3.left * lineDistance;
        }
        else if (lineToMove == 2)
        {
            targetPosition += Vector3.right * lineDistance;
        }
        if (transform.position == targetPosition)
        {
            return;
        }
        Vector3 diff = targetPosition - transform.position;
        Vector3 moveDir = diff.normalized * 25 * Time.deltaTime;
        if (moveDir.sqrMagnitude < diff.sqrMagnitude)
        {
            controller.Move(moveDir);
        }
        else
        {
            controller.Move(diff);
        }
    }

    private void Jump()// анимация прижка
    {
        dir.y = jumpForce;
        anim.SetTrigger("Jump");
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)// Условие для ПанелиРестарта(появление при проиграше)
    {
        if (hit.gameObject.tag == "obstacle")
        {
            losePanel.SetActive(true);
            int lastScore = int.Parse(scoreScript.scoreText.text.ToString());
            PlayerPrefs.SetInt("lastScore", lastScore);
            Time.timeScale = 0;
        }
    }

    private void OnTriggerEnter(Collider other)// Сбор монет и уничтожение обьекта после взаимодействия
    {
        if (other.gameObject.tag == "Coin")
        {
            coins++;
            PlayerPrefs.SetInt("Coin", coins);
            coinsText.text = coins.ToString();
            Destroy(other.gameObject);
        }
    }

    private IEnumerator SpeedIncrease()// увеличение скорости персонажа на 0,2 каждые 5 секунд до ограничителя
    {
        yield return new WaitForSeconds(5);
        if (speed < maxSpeed)
        {
            speed += 0.2f;
            StartCoroutine(SpeedIncrease());
        }
    }

    private IEnumerator Slide() 
    {
        capsule.center = new Vector3(0, -0.3f, 0);
        capsule.height = 1.1f;
        isSliding = true;
        anim.SetTrigger("Slide");

        yield return new WaitForSeconds(1);

        capsule.center = new Vector3(0, 0, 0);
        capsule.height = 2;
        isSliding = false;
    }
}
