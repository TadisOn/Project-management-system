﻿using PMS.Auth.Model;
using System.ComponentModel.DataAnnotations;

namespace PMS.Data.Entities
{
    public class Project
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required DateTime CreationDate { get; set; }

        [Required]
        public required string UserId { get; set; }
        public PMSRestUser User { get; set; }
    }

    public record ProjectDto(int Id, string Name, string Description, DateTime CreationDate);
}
