﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectFacebook.ApplicationLayer.Models.DTOs
{
    public class SearchUserDTO
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string ImagePath { get; set; }
    }
}
