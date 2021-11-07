using Npgsql;

namespace todolist_cli
{
    public class TododItemRepository
    {
        private NpgsqlConnection connection;
        private TodoItemNphsql nphsql = new TodoItemNphsql();
        public TododItemRepository(NpgsqlConnection connection)
        {
            this.connection = connection;
        }

        public void CreateTodoItem(TodoItem item)
        {
            return nphsql.AddTask(connection, item);
        }
        public List<TodoItem> FindAll()
        {
            return nphsql.GetAllTasks(connection);
        }

        public void UpdateTodoItem(TodoItem item)
        {
            nphsql.UpdateTask(item);
        }
        public void DeleteTodoItem(int id)
        {
            nphsql.DeleteTask(connection, id);
        }
        public TodoItem FindOne(int id)
        {
            return nphsql.GetTaskById(connection, id);
        }
    }
}