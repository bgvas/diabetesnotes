﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DiabetesNotes.Models
{
    public class User
    {
        [Key]
        public int Id { get; }

        public string Name { get; set; }

        public string Email { get; set; }

        public User()
        {
        }

        public User(string _Name, string _Email)
        {
            this.Name = _Name;
            this.Email = _Email;
        }
    }

}