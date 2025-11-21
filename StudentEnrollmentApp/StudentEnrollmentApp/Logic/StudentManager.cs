using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using StudentEnrollmentApp.Models;

namespace StudentEnrollmentApp.Logic
{
    /// <summary>
    /// StudentManager поддържа списък от студенти и предоставя методи
    /// за добавяне, премахване, търсене и сериализация (XML).
    /// Автор: Владимир Василев Владимиров - F113578
    /// </summary>
    public class StudentManager
    {
        private readonly List<Student> _students;
        private readonly string _storageFile;

        public StudentManager(string storageFile = "students.xml")
        {
            _students = new List<Student>();
            _storageFile = storageFile;
        }

        public List<Student> GetAllStudents()
        {
            return _students.ToList();
        }

        public void AddStudent(Student student)
        {
            if (_students.Any(s => s.FacultyNumber == student.FacultyNumber))
                throw new ArgumentException($"Student with faculty number {student.FacultyNumber} already exists.");

            _students.Add(student);
        }

        public bool RemoveStudent(Guid id)
        {
            var s = _students.FirstOrDefault(x => x.Id == id);
            if (s == null) return false;
            _students.Remove(s);
            return true;
        }

        public List<Student> Search(string query)
        {
            query = (query ?? string.Empty).Trim().ToLowerInvariant();
            return _students
                .Where(s => (s.FirstName ?? string.Empty).ToLowerInvariant().Contains(query)
                         || (s.LastName ?? string.Empty).ToLowerInvariant().Contains(query)
                         || (s.FacultyNumber ?? string.Empty).ToLowerInvariant().Contains(query))
                .ToList();
        }

        public void Save()
        {
            var serializer = new XmlSerializer(typeof(List<Student>));
            using (var stream = File.Create(_storageFile))
            {
                serializer.Serialize(stream, _students);
            }
        }

        public void Load()
        {
            if (!File.Exists(_storageFile)) return;
            var serializer = new XmlSerializer(typeof(List<Student>));
            using (var stream = File.OpenRead(_storageFile))
            {
                var items = (List<Student>)serializer.Deserialize(stream);
                _students.Clear();
                if (items != null) _students.AddRange(items);
            }
        }
    }
}
