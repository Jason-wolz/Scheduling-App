using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

//database schema
namespace WGUTermApp.Models
{
    [Table("people")]
    public class Person
    {
        [PrimaryKey, AutoIncrement]
        public int PersonID { get; set; }
        [MaxLength(50), NotNull]
        public string Name { get; set; }
        [MaxLength(15), NotNull]
        public string Phone { get; set; }
        [MaxLength(50), NotNull]
        public string Email { get; set; }
    }
    [Table("classes")]
    public class Course
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [MaxLength(50), NotNull]
        public string Name { get; set; }
        [NotNull]
        public DateTime Start { get; set; }
        [NotNull]
        public DateTime End { get; set; }
        [NotNull]
        public string Status { get; set; }
        [NotNull]
        public int Instructor { get; set; }
        [MaxLength(100), NotNull]
        public string Performance { get; set; }
        [MaxLength(100), NotNull]
        public string Objective { get; set; }
    }
}
