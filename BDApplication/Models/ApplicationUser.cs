using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BDApplication.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }

        public virtual List<Sketch> Sketches { get; set; }
        public virtual List<Comment> Comments { get; set; }
    }

    public class Sketch
    {
        [Key]
        public int SketchId { get; set; }
        public string SketchName { get; set; }
        public string SketchPhoto { get; set; }

        public int? UserId { get; set; }
        public User User { get; set; }

        public virtual List<Comment> Comments { get; set; }

    }

    public class Comment
    {
        [Key]
        public int CommentId { get; set; }
        public string CommentDescription { get; set; }
        public bool CommentTrueFalse { get; set; }

        public int? UserId { get; set; }
        public User User { get; set; }

        public int? SketchId { get; set; }
        public Sketch Sketch { get; set; }

    }

   [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    public class SketcherContext : DbContext
    {
        public SketcherContext() : base("DbContext")
        { }

        public DbSet<User> Users { get; set; }
        public DbSet<Sketch> Sketches { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}