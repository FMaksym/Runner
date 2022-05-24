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
    private int lineToMove = 1; //����� ����� �� ������� �� ��������� 0-�����, 1-������� � 2- ������ 

    public float lineDistance = 3.6f;//��������� ����� �������. ������, � ������ ��������, ���� ����� ������ ����� ������

    void Start()
    {
        controller = GetComponent<CharacterController>();//��������� ������ �� ����������(CharacterController, CapsuleCollider, Animator) ��� ������ � ���� ����� ���
        capsule = GetComponent<CapsuleCollider>();
        anim = GetComponentInChildren<Animator>();

        Time.timeScale = 1;
        coins = PlayerPrefs.GetInt("Coin");//��������� ������ �������� �����
        coinsText.text = coins.ToString();//������� � ����� ����� �� �����
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
        if (SwipeController.swipeRight)//���������� �������� �� ������ ������ � �����
        {
            if (lineToMove < 2)
            {   //�������� �� ����� ���������� ����� �����: 0 ��� 1 ��� 2
                lineToMove++;
            }
        }

        if (SwipeController.swipeLeft)//����� �����
        {
            if (lineToMove > 0)
            {
                lineToMove--;
            }
        }

        if (SwipeController.swipeUp)//�������� �� ����� ���������� �����
        {
            if (controller.isGrounded)//�������� �� ���������� ��������� �� �����
                Jump();//����� ������ ���� �� �� �����
        }

        if (SwipeController.swipeDown)//����� ����
        {
            StartCoroutine(Slide());
        }

        if (controller.isGrounded && !isSliding)//���� �������� � ������� �������� ���� ������������
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
        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;//������ ������� ����� ������

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

    private void Jump()// �������� ������
    {
        dir.y = jumpForce;
        anim.SetTrigger("Jump");
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)// ������� ��� ��������������(��������� ��� ���������)
    {
        if (hit.gameObject.tag == "obstacle")
        {
            losePanel.SetActive(true);
            int lastScore = int.Parse(scoreScript.scoreText.text.ToString());
            PlayerPrefs.SetInt("lastScore", lastScore);
            Time.timeScale = 0;
        }
    }

    private void OnTriggerEnter(Collider other)// ���� ����� � ����������� ������� ����� ��������������
    {
        if (other.gameObject.tag == "Coin")
        {
            coins++;
            PlayerPrefs.SetInt("Coin", coins);
            coinsText.text = coins.ToString();
            Destroy(other.gameObject);
        }
    }

    private IEnumerator SpeedIncrease()// ���������� �������� ��������� �� 0,2 ������ 5 ������ �� ������������
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
