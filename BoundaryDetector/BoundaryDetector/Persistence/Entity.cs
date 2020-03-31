using SQLite;

namespace BoundaryDetector.Persistence
{
    public class Entity<T> 
    {
        [PrimaryKey, AutoIncrement, Indexed]
        public T Id { get; set; }
    }
}