using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Questionnaires.Domain.Models;
using Questionnaires.Infrastructure.Repository;
using Questionnaire = Questionnaires.DataSeeding.Models.Questionnaire;

namespace Questionnaires.DataSeeding
{
    public static class DbSeeder
    {
        public static void Seed(QuestionnaireContext dbContext)
        {
            var questionnaire = ReadQuestionnaireData();

            AddQuestionnaire(dbContext, questionnaire);
            AddSubjects(questionnaire, dbContext);
            AddQuestions(questionnaire, dbContext);
            AddAnswers(questionnaire, dbContext);

            dbContext.SaveChanges();
        }

        private static void AddAnswers(Questionnaire questionnaire, QuestionnaireContext dbContext)
        {
            var answers = questionnaire.QuestionnaireItems
                .SelectMany(x => x.QuestionnaireItems)
                .SelectMany(x => x.QuestionnaireItems)
                .Where(x => x.AnswerId.HasValue)
                .Select(x => new Answer
                {
                    AnswerId = x.AnswerId.Value,
                    QuestionId = x.QuestionId,
                    OrderNumber = x.OrderNumber,
                    AnswerType = x.AnswerType,
                    Texts = ToTranslations(x.Texts)
                })
                .ToArray();

            dbContext.Answers.AddRange(answers);
        }

        private static List<Translation> ToTranslations(Dictionary<string, string> texts)
        {
            return texts.Select(t => new Translation(t.Key, t.Value)).ToList();
        }

        private static void AddQuestions(Questionnaire questionnaire, QuestionnaireContext dbContext)
        {
            var questions = questionnaire.QuestionnaireItems
                .SelectMany(x => x.QuestionnaireItems)
                .Select(x => new Question
                {
                    SubjectId = x.SubjectId,
                    QuestionId = x.QuestionId,
                    OrderNumber = x.OrderNumber,
                    QuestionnaireId = questionnaire.QuestionnaireId,
                    AnswerCategoryType = x.AnswerCategoryType,
                    Texts = ToTranslations(x.Texts)
                })
                .ToArray();

            dbContext.Questions.AddRange(questions);
        }

        private static void AddQuestionnaire(QuestionnaireContext dbContext, Questionnaire questionnaire)
        {
            dbContext.Questionnairs.Add(new Domain.Models.Questionnaire
            {
                QuestionnaireId = questionnaire.QuestionnaireId
            });
        }

        private static void AddSubjects(Questionnaire questionnaire, QuestionnaireContext dbContext)
        {
            var subjects = questionnaire.QuestionnaireItems
                .Select(x => new Subject
                {
                    SubjectId = x.SubjectId,
                    OrderNumber = x.OrderNumber,
                    Texts = ToTranslations(x.Texts)
                }).ToArray();

            dbContext.Subjects.AddRange(subjects);
        }

        private static Questionnaire ReadQuestionnaireData()
        {
            var dataText = File.ReadAllText("questionnaire.json");

            return JsonSerializer.Deserialize<Questionnaire>(dataText, new JsonSerializerOptions
            {
                IgnoreNullValues = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
        }
    }
}