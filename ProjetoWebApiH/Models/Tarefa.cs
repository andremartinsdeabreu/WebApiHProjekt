using System.ComponentModel.DataAnnotations;

namespace ProjetoWebApiH.Models
{
    public class Tarefa
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public bool IsDone { get; set; }
        [Required]
        public bool Editing { get; set; }
    }
}