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
        public int PersonId { get; set; }
        [MaxLength(50), NotNull]
        public string Name { get; set; }
        [MaxLength(15), NotNull]
        public string Phone { get; set; }
        [MaxLength(50), NotNull]
        public string Email { get; set; }

        public Person(string name, string phone, string email)
        {
            Name = name;
            Phone = phone;
            Email = email;
        }

        public Person()
        {
        }

        public bool IsNullOrEmpty()
        {
            return this == null || string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Name) || PersonId < 0 || string.IsNullOrEmpty(Phone);
        }
    }
    [Table("classes")]
    public class Course
    {
        public Course()
        {
        }

        public Course(string name, DateTime start, DateTime end, string status, int instructor, int performance, int objective, int term)
        {
            Name = name;
            Start = start;
            End = end;
            Status = status;
            Instructor = instructor;
            Performance = performance;
            Objective = objective;
            Term = term;
        }

        [PrimaryKey, AutoIncrement]
        public int CourseId { get; set; }
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
        public int Performance { get; set; }
        [MaxLength(100), NotNull]
        public int Objective { get; set; }
        [NotNull]
        public int Term { get; set; }

        public bool IsNullOrEmpty()
        {
            if (this == null)
            {
                return true;
            }
            if (CourseId < 0 || string.IsNullOrEmpty(Name))
            {
                return true;
            }

            if (Start == default || End == default)
            {
                return true;
            }

            return string.IsNullOrEmpty(Status) || Instructor < 0 || Performance < 0 || Objective < 0 || Term < 0;
        }
    }
    [Table("assessments")]
    public class Assessment
    {
        [PrimaryKey, AutoIncrement]
        public int AssessmentId { get; set; }
        [MaxLength(50), NotNull]
        public string Name { get; set; }
        [MaxLength(50), NotNull]
        public string Type { get; set; }
        [MaxLength(500), NotNull]
        public string Description { get; set; }
        [NotNull]
        public DateTime EstDueDate { get; set; }

        public Assessment() { }

        public Assessment(string name, string type, string description, DateTime dueDate)
        {
            Name = name;
            Type = type;
            Description = description;
            EstDueDate = dueDate;
        }

        public bool IsNullOrEmpty()
        {
            return string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Type) || string.IsNullOrEmpty(Description) || EstDueDate == default;
        }
    }
    [Table("term")]
    public class Term
    {
        [PrimaryKey, AutoIncrement]
        public int TermId { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        [NotNull]
        public DateTime Start { get; set; }
        [NotNull]
        public DateTime End { get; set; }

        public Term() { }

        public Term(string name, DateTime start, DateTime end)
        {
            Name = name;
            Start = start;
            End = end;
        }

        public bool IsNullOrEmpty()
        {
            return Start == default || End == default;
        }
    }
}
