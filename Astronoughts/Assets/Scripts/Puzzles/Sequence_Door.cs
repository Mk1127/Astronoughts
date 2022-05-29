using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequence_Door : MonoBehaviour
{
    [SerializeField] Transform door;
    [SerializeField] List<Sequence_Totem> totemList = new List<Sequence_Totem>();
    [SerializeField] List<string> answer = new List<string>();

    List<string> sequence = new List<string>();

    private void Start()
    {

    }

    public void addToSequence(string name)
    {
        sequence.Add(name);
        for (int i = 0; i < sequence.Count; i++)
        {
            Debug.Log(sequence[i]);
            Debug.Log(sequence.Count);
        }

        if (sequence.Count == 3)
        {
            CheckAnswers();
        }
    }

    private void CheckAnswers()
    {
        int correctCount = 0;
        for (int i = 0; i < answer.Count; i++)
        {
            if (answer[i] == sequence[i])
            {
                correctCount++;
            }
        }

        if (correctCount != 3)
        {
            StartCoroutine(ResetTotems());
            sequence = new List<string>();
        }
        else
        {
            StartCoroutine(LowerDoor());
        }
    }

    IEnumerator ResetTotems()
    {
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < totemList.Count; i++)
        {
            totemList[i].ResetTotem();
        }
        sequence = new List<string>();
    }

    IEnumerator LowerDoor()
    {
        for (float i = 0; i < 3; i += 0.1f)
        {
            door.position -= new Vector3(0, 0.1f, 0);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
