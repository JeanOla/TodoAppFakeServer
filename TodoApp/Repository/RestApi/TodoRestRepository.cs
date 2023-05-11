using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text;
using TodoApp.Data;
using TodoApp.Models;
using TodoApp.ViewModels;

namespace TodoApp.Repository.MsSQL
{
    public class TodoRestRepository : ITodoRepository
    {
        // interact with database and perform CRUD 

        string baseURL = "https://jsonplaceholder.typicode.com";
        string baseURLLocal = "http://localhost:30010/api/v1";

        HttpClient httpClient = new HttpClient();

        // http requestmessage 
        // 5, host, version, header, body, verb [get, post, put, delete, trace ...]
        // http responsemessage
        // 4, status code, headers, body, version

        public TodoRestRepository()
        {
        }

        public List<todofakeviewmodel> GetAllTodos()
        {
            // fetch todos from rest service -> http request message
            // http://localhost:30010/api/v1/todos
            var response = httpClient.GetAsync(baseURL + "/todos").Result;
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;// json standard
                // any json http response can be converted to a C# object by doing a
                // serialization -> convert c# object to other data type [json, xml, paintext]
                // deserialization -> convert other data type [json, xml, paintext] to C# object

                // newtonsoft library which does serial deserial 
                List<todofakeviewmodel> todos = JsonConvert.DeserializeObject<List<todofakeviewmodel>>(data);
                return todos;
            }
            return null;
        }

        public todofakeviewmodel GetTodoById(int Id)
        {
            var response = httpClient.GetAsync(baseURL + "/todos/" + Id).Result;
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;// json standard
                todofakeviewmodel todo = JsonConvert.DeserializeObject<todofakeviewmodel>(data);
                return todo;
            }
            return null;
        }


        public todofakeviewmodel AddTodo(todofakeviewmodel newTodo)
        {
            // Todo id, TodoDTO without id mapp the data and make a request to rest api

            todofakeviewmodel todoVM = new todofakeviewmodel();
            todoVM.title = newTodo.title;
            todoVM.completed = newTodo.completed;

            string data = JsonConvert.SerializeObject(todoVM);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            var response = httpClient.PostAsync(baseURL + "/todos", content).Result;
            if (response.IsSuccessStatusCode)
            {
                var responsecontent = response.Content.ReadAsStringAsync().Result;
                todofakeviewmodel todo = JsonConvert.DeserializeObject<todofakeviewmodel>(responsecontent);
                return todo;
            }
            return null;
        }

        public todofakeviewmodel UpdateTodo(todofakeviewmodel newTodo)
        {
            string data = JsonConvert.SerializeObject(newTodo);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            var response = httpClient.PutAsync(baseURL + "/todos/" + newTodo.Id, content).Result;
            if (response.IsSuccessStatusCode)
            {
                var responsecontent = response.Content.ReadAsStringAsync().Result;
                todofakeviewmodel todo = JsonConvert.DeserializeObject<todofakeviewmodel>(responsecontent);
                return todo;
            }
            return null;
        }

        public todofakeviewmodel DeleteTodo(int Id)
        {
            var response = httpClient.DeleteAsync(baseURL + "/todos/" + Id).Result;
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;// json standard
                todofakeviewmodel todo = JsonConvert.DeserializeObject<todofakeviewmodel>(data);
                return todo;
            }
            return null;
        }


        //    public Todo AddTodo(Todo newTodo)
        //    {
        //        // Todo id, TodoDTO without id mapp the data and make a request to rest api

        //        TodoViewModel todoVM = new TodoViewModel();
        //        todoVM.Title = newTodo.Title;
        //        todoVM.Description = newTodo.Description;
        //        todoVM.Status = newTodo.Status;
        //        todoVM.DueDate = newTodo.DueDate;

        //        string data = JsonConvert.SerializeObject(todoVM);
        //        StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

        //        var response = httpClient.PostAsync(baseURLLocal + "/todos", content).Result;
        //        if (response.IsSuccessStatusCode)
        //        {
        //            var responsecontent = response.Content.ReadAsStringAsync().Result;
        //            Todo todo = JsonConvert.DeserializeObject<Todo>(responsecontent);
        //            return todo;
        //        }
        //        return null;
        //    }

        //    public Todo DeleteTodo(int todoId)
        //    {
        //        var response = httpClient.DeleteAsync(baseURLLocal + "/todos/" + todoId).Result;
        //        if (response.IsSuccessStatusCode)
        //        {
        //            var data = response.Content.ReadAsStringAsync().Result;// json standard
        //            Todo todo = JsonConvert.DeserializeObject<Todo>(data);
        //            return todo;
        //        }
        //        return null;
        //    }



        //    public Todo UpdateTodo(int todoId, Todo newTodo)
        //    {
        //        string data = JsonConvert.SerializeObject(newTodo);
        //        StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

        //        var response = httpClient.PutAsync(baseURL + "/todos/"+todoId, content).Result;
        //        if (response.IsSuccessStatusCode)
        //        {
        //            var responsecontent = response.Content.ReadAsStringAsync().Result;
        //            Todo todo = JsonConvert.DeserializeObject<Todo>(responsecontent);
        //            return todo;
        //        }
        //        return null;
        //    }
        //}
    }
}
