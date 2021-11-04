using NpgsqlTypes;

namespace todolist_cli
{
    internal class TodoItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public NpgsqlDate? DueDate { get; set; }
        public bool? Done { get; set; }
    }
}