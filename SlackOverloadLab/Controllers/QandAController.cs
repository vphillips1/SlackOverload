using Dapper.Contrib.Extensions;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using SlackOverloadLab.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SlackOverloadLab.Controllers
{
    public class QandAController : Controller
    {
      
        public IActionResult Index()
        {
            List<Question> question = DAL.ReadAllQuestions();
            return View(question);
        }

        public IActionResult Detail(int id)
        {

            Question question = DAL.ReadOneQuestion(id);
            ViewBag.quest = question;
            List<Answer> answer = DAL.ReadAnswersInQuestions(id);
            return View(answer);
        }

        public IActionResult DeleteAnswerForm(int id)
        {

            return View(id);
        }
        public IActionResult DeleteAnswer(int question, int answer)
        {
            if(DAL.DeleteAnswer(answer)==false)
            {

                return Content("Sorry, no access.");
            }

            return Redirect($"/QandA/Detail?id={question}");
        }

       

        public IActionResult AddAnswerForm(int id)
        {
            ViewBag.qid = id;

            return View();
        }

        [HttpPost]
        public IActionResult AddAnswer(Answer answer)
        {
            DAL.AddAnswer(answer);
            return Redirect($"/QandA/Detail?id={answer.questionId}");

        }


        public IActionResult EditAnswerForm(int id)
        {

            Answer answer = DAL.ReadOneAnswer(id);
            return View(answer);
        }

        [HttpPost]

        public IActionResult EditAnswer(Answer answer)
        {

            DAL.EditAnswer(answer);

            return RedirectToAction("Index");
        }

        public IActionResult EditQuestionForm(int id)
        {

            Question question = DAL.ReadOneQuestion(id);
            return View(question);
        }

        [HttpPost]

        public IActionResult EditQuestion(Question question)
        {

            DAL.EditQuestion(question);

            return RedirectToAction("Index");
        }


        public IActionResult AddQuestionForm()
        {
            return View();

        }


        public IActionResult AddQuestion(Question question)
        {

            DAL.AddQuestion(question);

            return RedirectToAction("index");     
                
        }

        public IActionResult DeleteQuestionForm(int id)
        {

            return View(id);
        }
        public IActionResult DeleteQuestion(int id)
        {
            DAL.DeleteQuestion(id);
            return RedirectToAction("index");

        }

        //public IActionResult DeleteQuestion(int answer, int question)
        //{
        //    if (DAL.DeleteAnswer(question) == false)
        //    {

        //        return Content("Sorry, no access.");
        //    }

        //    return Redirect($"/QandA/Detail?id={answer}");
        //}



        //public IActionResult Index()
        //{
        //    List<Answer> answer = DAL.GetAllAnswers();
        //    return View(answer);

        //}

        //public IActionResult AddForm()
        //{

        //    return View();
        //}

        //[HttpPost]
        //public IActionResult Add(Answer answer)
        //{
        //    DAL.CreateAnswer(answer);

        //    return RedirectToAction("Index");

        //}


        //public IActionResult EditForm(int id)
        //{

        //    Answer answer = DAL.GetIndividualAnswer(id);
        //    return View(answer);
        //}

        //[HttpPost]

        //public IActionResult Edit(Answer answer)
        //{

        //    DAL.EditAnswer(answer);

        //    return RedirectToAction("Index");
        //}

        //public IActionResult DeleteForm(int id)
        //{

        //    return View(id);
        //}

        //public IActionResult Delete(int id)
        //{
        //    DAL.DeleteAnswer(id);
        //    return RedirectToAction("Index");

        //}

    }
}
