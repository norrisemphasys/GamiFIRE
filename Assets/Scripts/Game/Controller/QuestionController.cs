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

    public int currentQuestionCount = 0;

    public void Init()
    {
        questionBank.Add(JobType.STUDENT, studentQuestions);
        questionBank.Add(JobType.PROFESSIONAL, professionalQuestions);
        questionBank.Add(JobType.AGRICULTRIST, argriculturistQuestions);
        questionBank.Add(JobType.BUSINESSMAN, businessmanQuestions);

#if SAVE_ALL_QUESTIONS_TO_FILE
        WriteQuestionToText();
#endif
    }

    public List<QuestionSO> GetQuetionByType(JobType type)
    {
        return questionBank.ContainsKey(type) ? questionBank[type] : null;
    }

    public List<QuestionSO> GetShuffledQuestionByType(JobType type, bool shuffle = true)
    {
        if (questionBank.ContainsKey(type))
        {
            if(shuffle)
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

    #region SAVE QUESTION TO FILE
    public void WriteQuestionToText()
    {
        List<string> allText = new List<string>();

        for(int i = 0; i < studentQuestions.Count; i++)
        {
            SaveTitle(allText, studentQuestions[i].questionTitle);
            SaveQuestion(allText, studentQuestions[i].question);
            SaveAnswer(allText, studentQuestions[i].answerData);
        }

        for (int i = 0; i < professionalQuestions.Count; i++)
        {
            SaveTitle(allText, professionalQuestions[i].questionTitle);
            SaveQuestion(allText, professionalQuestions[i].question);
            SaveAnswer(allText, professionalQuestions[i].answerData);
        }

        for (int i = 0; i < argriculturistQuestions.Count; i++)
        {
            SaveTitle(allText, argriculturistQuestions[i].questionTitle);
            SaveQuestion(allText, argriculturistQuestions[i].question);
            SaveAnswer(allText, argriculturistQuestions[i].answerData);
        }

        for (int i = 0; i < businessmanQuestions.Count; i++)
        {
            SaveTitle(allText, businessmanQuestions[i].questionTitle);
            SaveQuestion(allText, businessmanQuestions[i].question);
            SaveAnswer(allText, businessmanQuestions[i].answerData);
        }

        TextUtil.WriteToTextArray(allText.ToArray(), "AllDillemaQuestions");
    }

    void SaveTitle(List<string> allText, string text) 
    {
        string id = TextUtil.GetUniqueTextToSpeechFilename(text);
        string file = string.Format("{0},*{1}", id, text);

        if (!allText.Contains(file))
            allText.Add(file);
    }

    void SaveQuestion(List<string> allText, string text)
    {
        string id = TextUtil.GetUniqueTextToSpeechFilename(text);
        string file = string.Format("{0},*{1}", id, text);

        if (!allText.Contains(file))
            allText.Add(file);
    }

    void SaveAnswer(List<string> allText, AnswerData[] data)
    {
        for(int i = 0; i < data.Length; i++)
        {
            string id = TextUtil.GetUniqueTextToSpeechFilename(data[i].answer);
            string file = string.Format("{0},*{1}", id, data[i].answer);

            if (!allText.Contains(file))
                allText.Add(file);
        }    
    }
    #endregion
}
