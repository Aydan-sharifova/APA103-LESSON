using System;
namespace _27_FrontToBackSqlConnection.Models
{
	public abstract class BaseEntity
	{
        public int Id { get; set; }
        public bool isDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
    }
    
}

