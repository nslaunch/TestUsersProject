using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserApplication.Entities.TestDb
{
    public partial class Users
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Email { get; set; }
        //public string Password { get; set; }
        [Required]
        public string FullName { get; set; }
        //public DateTime? LastLogin { get; set; }
        public DateTime CreateDate { get; set; }
        [Required]
        public bool Status { get; set; }
    }
}
