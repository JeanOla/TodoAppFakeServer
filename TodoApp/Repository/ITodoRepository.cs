using TodoApp.Models;
using TodoApp.ViewModels;

namespace TodoApp.Repository
{
    public interface ITodoRepository // contract 
    {
        
        List<todofakeviewmodel> GetAllTodos();
        //List<Todo> GetAllTodos();

        // get any specific todo
        todofakeviewmodel GetTodoById(int Id);

        // add todo into the list
        todofakeviewmodel AddTodo(todofakeviewmodel newTodo);

        // update todo in the list
        todofakeviewmodel UpdateTodo(todofakeviewmodel newTodo);
        todofakeviewmodel DeleteTodo(int Id);
        // delete 
        // Todo DeleteTodo(int todoId);
    }
}
