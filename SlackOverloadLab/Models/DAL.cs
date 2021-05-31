using Dapper;
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SlackOverloadLab.Models
{
    public class DAL
    {

        public static IDbConnection db;

        static public string CurrentUser;
        static public void AddQuestion(Question question)
        {
            question.username = DAL.CurrentUser;
            question.posted = DateTime.Now;
            db.Insert(question);
        }

        static public List<Question> ReadAllQuestions()
        {

            return db.GetAll<Question>().ToList();
        }
  
        static public Question ReadOneQuestion(int id)
        {
            return db.Get<Question>(id);

        }

        public static void EditQuestion(Question question)
        {

            db.Update(question);
        }

        public static void DeleteQuestion(int id)
        {

            Question tempobj = new Question();
            tempobj.id = id;
            db.Delete(tempobj);
        }




        public static List<Answer> ReadAnswersInQuestions(int questionId)
        {
            return db.Query<Answer>($"select * from answer where questionId = '{questionId}'").ToList();

        }

        public static bool DeleteAnswer(int id)
        {

            Answer answer = db.Get<Answer>(id);
            if(answer.username == DAL.CurrentUser || DAL.CurrentUser =="admin")
            {

                db.Delete(answer);
                return true;
            }

            return false;
        }

        public static void AddAnswer(Answer answer)
        {

            answer.username = DAL.CurrentUser;
            db.Insert(answer);
        }

        static public Answer ReadOneAnswer(int id)
        {
            return db.Get<Answer>(id);

        }

        public static void EditAnswer(Answer answer)
        {

            db.Update(answer);
        }
        //static public List<Question> GetAllQuestions()
        //{
        //    List<Question> question = db.Query<Question>("select * from question").ToList();
        //    return question;


        //}

        //static public Question GetIndividualQuestion(int id)
        //{
        //    return db.Get<Question>(id);

        //}

        //static public void EditQuestion(Question question)
        //{

        //    db.Update(question);
        //}

        //static public void DeleteQuestion(int id)
        //{
        //    Question question = new Question();
        //    question.id = id;
        //    db.Delete<Question>(question);
        //}






        //static public void CreateAnswer(Answer answer)
        //{

        //    db.Insert(answer);
        //}

        //static public List<Answer> GetAnswerByCategory()
        //{

        //    List<Answer> answer = db.Query<Answer>("select * from answer order by questionId").ToList();
        //    return answer;
        //}

        ////static public List<Question> GetAllQuestions(int id)
        ////{
        ////    List<Question> ans = db.Query<Question>($"select * from question where id = '{id}' ").ToList();
        ////    return ans;


        ////}

        //static public List<Answer> GetAllAnswers()
        //{
        //    List<Answer> answer = db.Query<Answer>("select * from answer").ToList();
        //    return answer;


        //}

        //static public Answer GetIndividualAnswer(int id)
        //{
        //    return db.Get<Answer>(id);

        //}

        //static public void EditAnswer(Answer answer)
        //{

        //    db.Update(answer);
        //}

        //static public void DeleteAnswer(int id)
        //{
        //    Answer answer = new Answer();
        //    answer.id = id;
        //    db.Delete<Answer>(answer);
        //}


        //public static List<Answer> ReadAnswersInQuestion(int questionId)
        //{

        //    return db.Query<Answer>($"select * from answer where question = {questionId}").ToList();
        //}

    }
}
