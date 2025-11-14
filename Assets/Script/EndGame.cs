using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    [SerializeField] private GameObject win;
    [SerializeField] private GameObject loss;

    public void WinFunction()
    {
        win.SetActive(true);
        StartCoroutine(TimeForStart());
    }

    public void LossFunction()
    {
        loss.SetActive(true);
        StartCoroutine(TimeForStart());
    }

    private IEnumerator TimeForStart()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("Start");
    }
}
