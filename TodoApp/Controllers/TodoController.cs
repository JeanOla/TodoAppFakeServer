using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoApp.Models;
using TodoApp.Repository;
using TodoApp.Repository.MsSQL;
using TodoApp.ViewModels;

namespace TodoApp.Controllers
{
   // [Authorize(Roles = "Administrator, User" )]
    public class TodoController : Controller
    {
        // inmemory
        // database
        // RDBMS
        // NoSQL
        // Files

        ITodoRepository _repo;

        // tightly coupled object 
        //ITodoRepository _repo = new InMemoryRepository();
        //ITodoRepository _repo1 = new DBRepository();

        // lossely coupled implementation
        public TodoController(ITodoRepository repo)
        {
            this._repo = repo;
        }
        // [AllowAnonymous]
        public IActionResult GetAllTodos()
        {
            var todolist = _repo.GetAllTodos();
            return View(todolist);
        }
        public IActionResult Details(int Id)
        {
            var todo = _repo.GetTodoById(Id);
            return View(todo);
        }
        //public IActionResult Delete(int todoId)
        //{
        //    //var todolist = _repo.DeleteTodo(todoId);
        //    //return RedirectToAction(controllerName: "Todo", actionName: "GetAllTodos"); // reload the getall page it self
        //}

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(todofakeviewmodel newTodo) // model binded this where the views data is accepted 
        {
            if (ModelState.IsValid){
                var todo = _repo.AddTodo(newTodo);
                return RedirectToAction("GetAllTodos");
            }
            ViewData["Message"] = "Data is not valid to create the Todo";
            return BadRequest("not added");
        }

        [HttpGet]
        public IActionResult Update(int Id)
        {
            var oldTodo = _repo.GetTodoById(Id);
            return View(oldTodo);
        }
        [HttpPost]
        public IActionResult Update(todofakeviewmodel newTodo)
        {
            if (ModelState.IsValid)
            {
                var todo = _repo.UpdateTodo(newTodo);
                return RedirectToAction("GetAllTodos");
            }
            ViewData["Message"] = "Data is not valid to create the Todo";
            return BadRequest("not added");

        }

        public IActionResult Delete(int Id)
        {
            _repo.DeleteTodo(Id);
            return RedirectToAction("Index");
        }


    }
}
