using Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Book:BaseEntity
    {
        [Required, MaxLength(50)]
        public string Title { get; set; }
        public int YearOfPublishing { get; set; }
        public decimal Price { get; set; }

        public int AuthorId { get; set; }
        public Author Author { get; set; }
        public int GenreId  { get; set; }
        public Genre Genre { get; set; }
        public int TopicId  { get; set; }
        public Topic Topic { get; set; }
    }
}
