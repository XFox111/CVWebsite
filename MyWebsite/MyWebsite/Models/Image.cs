﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyWebsite.Models
{
    public class Image
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required]
        [Column(TypeName = "varchar(100)")]
        public string Title { get; set; }
        [Required]
        [Column(TypeName = "varchar(255)")]
        public string Description { get; set; }
        [Required]
        public DateTime CreationDate { get; set; }
        [Required]
        [Column(TypeName = "varchar(20)")]
        public string FileName { get; set; }
        [Required]
        [Column(TypeName = "varchar(10)")]
        public string Language { get; set; }
    }
}