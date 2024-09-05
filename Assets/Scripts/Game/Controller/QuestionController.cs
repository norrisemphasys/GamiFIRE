using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionController : MonoBehaviour
{
    public Dictionary<JobType, List<QuestionSO>> questionBank = new Dictionary<JobType, List<QuestionSO>>();

    [SerializeField] List<QuestionSO> studentQuestions;
    [SerializeField] List<QuestionSO> professionalQuestions;
    [SerializeField] List<QuestionSO> argriculturistQuestions;
    [SerializeField] List<QuestionSO> businessmanQuestions;

    public void Init()
    {
        questionBank.Add(JobType.STUDENT, studentQuestions);
        questionBank.Add(JobType.PROFESSIONAL, professionalQuestions);
        questionBank.Add(JobType.AGRICULTRIST, argriculturistQuestions);
        questionBank.Add(JobType.BUSINESSMAN, businessmanQuestions);
    }

    public List<QuestionSO> GetQuetionByType(JobType type)
    {
        return questionBank.ContainsKey(type) ? questionBank[type] : null;
    }

    public List<QuestionSO> GetShuffledQuestionByType(JobType type)
    {
        if (questionBank.ContainsKey(type))
        {
            questionBank[type].Shuffle();
            return questionBank[type];
        }
        return null;
    }

    public QuestionSO GetQuestionByIndexAndType(JobType type, int idx)
    {
        return GetQuetionByType(type)[idx];
    }

    public int GetCurrentQuestionTypeCount() 
    {
        return GetQuetionByType(GameManager.instance.IslandType).Count;
    }

}
