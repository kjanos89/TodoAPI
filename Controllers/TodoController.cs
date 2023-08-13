using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace TodoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private static List<TodoItem> _todoItems = new List<TodoItem>();
        private static int _nextId = 1;

        [HttpGet]
        public ActionResult<IEnumerable<TodoItem>> Get()
        {
            return _todoItems;
        }

        [HttpGet("{id}")]
        public ActionResult<TodoItem> GetById(int id)
        {
            var todoItem = _todoItems.FirstOrDefault(item => item.Id == id);
            if (todoItem == null)
            {
                return NotFound();
            }
            return todoItem;
        }

        [HttpPost]
        [HttpPost]
        public ActionResult<TodoItem> Create(TodoItem todoItem)
        {
            todoItem.Id = _nextId++;
            _todoItems.Add(todoItem);
            return CreatedAtAction(nameof(GetById), new { id = todoItem.Id }, todoItem);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, TodoItem updatedTodo)
        {
            var existingTodo = _todoItems.FirstOrDefault(item => item.Id == id);
            if (existingTodo == null)
            {
                return NotFound();
            }
            existingTodo.Title = updatedTodo.Title;
            existingTodo.IsCompleted = updatedTodo.IsCompleted;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var todoItem = _todoItems.FirstOrDefault(item => item.Id == id);
            if (todoItem == null)
            {
                return NotFound();
            }
            _todoItems.Remove(todoItem);
            return NoContent();
        }
    }
}

