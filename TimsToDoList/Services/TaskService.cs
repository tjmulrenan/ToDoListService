using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using TimsToDoList.Models;

namespace TimsToDoList.Services
{
    public class TaskService
    {
        private List<Task> _tasks = new List<Task>();
        private int _count;

        public TaskService()
        {
            // Deserialize from text
            if(File.Exists("TaskData"))
            {
                _tasks = Deserialize(File.ReadAllText("TaskData"));              
            }

            _count = _tasks.Any() ? _tasks.Max(task => task.Id) : 0;
        }

        private string Serialize(List<Task> t)
        {
            var json = JsonSerializer.Serialize(t);
            return json;
        }

        private List<Task> Deserialize(string json)
        {
            var tasks = JsonSerializer.Deserialize<List<Task>>(json);
            return tasks;
        }

        public List<Task> GetAll => _tasks.OrderBy(o=>o.Created).ToList(); //expression bodied member for "() {return tasks}"

        public Task GetById (int id)
        {
            return _tasks.FirstOrDefault(task => task.Id == id); //return first item satisfying predicate
        }

        public Task Create(Task task)
        {
            task.Id = ++_count;
            task.Created = DateTime.Now;
            _tasks.Add(task);
            Save();
            return task;
        }
        
        public void Update (int id, Task task)
        {
            Task found = _tasks.FirstOrDefault(task => task.Id == id);
            found.Name = task.Name;
            found.Description = task.Description;

            Save();
        }
        
        public void Delete(int id)
        {
            _tasks.RemoveAll(task => task.Id == id);
            Save();
        }

        private void Save()
        {
            File.WriteAllText("TaskData", Serialize(_tasks));
        }
    }
}
