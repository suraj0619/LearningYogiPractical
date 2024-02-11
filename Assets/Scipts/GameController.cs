using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    public static GameController inst;

    [Header("*** UI Texts ***")]
    public TextMeshProUGUI ScoreTxt;
    public TextMeshProUGUI HealthTxt;
    public TextMeshProUGUI ResultScoreTxt;
    public TextMeshProUGUI ResultTxt;

    [Header("*** Player ***")]
    public GameObject Player;

    [Header("*** Panels ***")]
    public GameObject resultPanel;

    [Header("*** Prefabs ***")]
    public GameObject Asteroids_Large;
    public GameObject Asteroids_Small;

    [HideInInspector] public List<GameObject> asteroidLargeList = new();
    [HideInInspector] public List<GameObject> asteroidssmallList = new();

    // Private Variables
    int _score = 0;
    
    private void Awake()
    {
        inst = this;
    }

    IEnumerator Start()
    {
        //Spawing Asteroids in start
        yield return new WaitForSeconds(5);
        HealthTxt.text = "Health\n25";
        Player.GetComponent<Animator>().enabled = false;
        SpawnAsteroids(Asteroids_Large, 10, 0);
    }

    #region Spawning Asteroids  /// Asteroids spawning with input numbers   
    public void SpawnAsteroids(GameObject obj, int num, float pos)
    {
        GameObject gb;
        for (int i = 0; i < num; i++)
        {
            if (obj.name.Contains("Big"))
            {
                gb = Instantiate(obj, new Vector3(Random.Range(-8f, 8f), Random.Range(-2.0f, 5f), 0), Quaternion.Euler(0, 0, Random.Range(0, 360)));
                asteroidLargeList.Add(gb);
            }
            else
            {
                gb = Instantiate(obj, new Vector3(pos, pos, 0), Quaternion.Euler(0, 0, Random.Range(0, 360)));
                pos += 2f;
                asteroidssmallList.Add(gb);
            }
        }
    }
    #endregion

    #region Update Score - On Collision
    public void UpdateScore(int num)
    {
        _score += num;
        ScoreTxt.text = "Score\n" + _score;
    }
    #endregion

    #region Destroy Enemies
    public void DestroyEnemies(AsteroidsManager inst, Collision2D collision)
    {
        Destroy(collision.gameObject);

        if (inst.asteroidsType == AsteroidsManager.AsteroidsType.large)
        {
            GameController.inst.UpdateScore(10);
            GameController.inst.SpawnAsteroids(GameController.inst.Asteroids_Small, 3, collision.transform.localPosition.x);
            GameController.inst.asteroidLargeList.RemoveAt(GameController.inst.asteroidLargeList.Count-1);
        }
        else
        {
            GameController.inst.UpdateScore(5);
            GameController.inst.asteroidssmallList.RemoveAt(GameController.inst.asteroidssmallList.Count - 1);
        }

        if (GameController.inst.asteroidLargeList.Count<=0 && GameController.inst.asteroidssmallList.Count <= 0)
        {
            ShowResult("won", ScoreTxt.text);
        }
    }
    #endregion

    #region Show Result
    public void ShowResult(string str, string score)
    {
        if (str.Contains("won"))
            ResultTxt.text = "Congratulation! You Won!!!";
        else
            ResultTxt.text = "Better Luck Next Time";

        ResultScoreTxt.text = score;
        resultPanel.SetActive(true);

        // Reset Data
        for(int i = 0; i < asteroidLargeList.Count; i++)
        {
            Destroy(asteroidLargeList[i]);
        }
        for (int i = 0; i < asteroidssmallList.Count; i++)
        {
            Destroy(asteroidssmallList[i]);
        }
        asteroidLargeList.Clear();
        asteroidssmallList.Clear();
    }
    #endregion
}
