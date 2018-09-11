using AlphaPuzzle.State;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ParentalLock : MonoBehaviour
{

    public TextMeshProUGUI Problem;
    public InputField UserSolution;
    private int min = 3;
    private int max = 10;
    private int multiplier = 2;
    public int solution;

    public GameObject OptionsPanel, ParentTestPanel;

    void Start()
    {
        GenerateProblem();
    }

    void OnEnable()
    {
        GameState.DragEnabled = false;
        GenerateProblem();
    }    

    private void GenerateProblem()
    {
        int first = (int)UnityEngine.Random.Range(min, max) * multiplier;
        int second = (int)UnityEngine.Random.Range(min, max) * multiplier;
        string problemstring = first + " + " + second + " = ";
        Problem.text = problemstring;
        solution = first + second;
    }

    public void CheckSolution()
    {
        if (UserSolution.text == solution.ToString())
        {
            OptionsPanel.SetActive(true);
            ParentTestPanel.SetActive(false);
            UserSolution.text = "";
        }
        else
        {
            UserSolution.text = "";
            GenerateProblem();
        }
    }
}
