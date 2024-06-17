using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.PlayerSettings;

public class Ball : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject goalFX;


    [Header("Attributes")]
    [SerializeField] private float maxPower = 10f;
    [SerializeField] private float power = 2f;
    [SerializeField] private float maxGoalSpeed = 4f;

    private bool isDragging;
    private bool inHole;

    private void Update()
    {
        PlayerInput();
    }

    private bool isReady()
    {
        return rb.velocity.magnitude <= 0.2f;
    }

    private void PlayerInput()
    {
        if (Input.GetKeyDown(KeyCode.R)) RestartGame();
        if (!isReady()) return;

        Vector2 inputPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float distance = Vector2.Distance(transform.position, inputPos);

        if (Input.GetMouseButtonDown(0) && distance <= 0.5f) DragStart();
        if (Input.GetMouseButton(0) && isDragging) DragChange();
        if (Input.GetMouseButtonUp(0) && isDragging) DragRelease(inputPos);
        //if (Input.GetKeyDown(KeyCode.A) && isDragging) DragRelease2(inputPos);
        if (Input.GetKeyDown(KeyCode.A)) DragRelease2(inputPos);
        if (Input.GetKeyDown(KeyCode.B)) RandomShot();


    }
    private void DragStart()
    {
        isDragging = true;
    }

    private void DragChange()
    {

    }

    private void DragRelease(Vector2 pos)
    {
        float distance = Vector2.Distance((Vector2)transform.position, pos);
        isDragging = false;

        if (distance < 0.5f)
        {
            return;
        }
        Vector2 dir = (Vector2)transform.position - pos;

        rb.velocity = Vector2.ClampMagnitude(dir * power, maxPower);
    }

    private void DragRelease2(Vector2 pos)
    {
        float distance = Vector2.Distance((Vector2)transform.position, pos);
        isDragging = false;

        if (distance < 0.5f)
        {
            return;
        }
        Vector2 dir = (Vector2)transform.position - pos;

        rb.velocity = Vector2.ClampMagnitude(dir * 4f, 20f);
    }


    private void RandomShot()
    {

        Vector2 dir = new Vector2(Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, 10.0f));


        rb.velocity = Vector2.ClampMagnitude(dir * Random.Range(1f, 20f), 20f);
    }

    private void CheckWinState()
    {
        if (inHole) return;

        if (rb.velocity.magnitude <= maxGoalSpeed){
            inHole = true;
            rb.velocity = Vector2.zero;
            gameObject.SetActive(false);
            GameObject fx = Instantiate(goalFX, transform.position, Quaternion.identity);
            Destroy(fx, 2f);

            //LevelComplete
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Goal") CheckWinState();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Goal") CheckWinState();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // loads current scene
    }
}