﻿using API.Models.Domain.Extra;
using System.ComponentModel.DataAnnotations;

namespace API.Models.DTOs.Auth
{
    public class RegisterDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string Password { get; set; }

        


        [DataType(DataType.Text)]
        public string Username { get; set; }



        //public int? FrameworkId { get; set; }


        public required ICollection<Framework> Frameworks { get; set; }



    }
}