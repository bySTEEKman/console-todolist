using Npgsql;

namespace todolist_cli
{
    public class TododItemRepository
    {
        public TododItemRepository(NpgsqlConnection connection)
        {
    
        }
        public void CreateTodoItem(TodoItem item)
        {

        }
        public List<TodoItem> FindAll()
        {
            return null;
        }

        public void UpdateTodoItem(TodoItem item)
        {

        }
        public void DeleteTodoItem(int id)
        {

        }
        public TodoItem FindOne(int id)
        {
            return null;
        }
    }
}