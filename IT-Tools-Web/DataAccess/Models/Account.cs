﻿using System.ComponentModel.DataAnnotations;

namespace IT_Tools_Web.DataAccess.Models;

public class Account
{
    public int Id { get; set; }

    [Required]
    [StringLength(50, MinimumLength = 3)]
    public string Username { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 6)]
    public string Password { get; set; }

    [Required]
    public string Type { get; set; }

    public ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();
}
