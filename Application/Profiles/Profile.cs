﻿using SQLitePCL;

namespace Application;

public class Profile
{
    public string  Username { get; set; }
    public string DisplayName { get; set; }
    public string Bio { get; set; }
    public string Image { get; set; }

    public Profile() {}
}
