﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twitter.Core.Entities;

namespace Twitter.Business.Dtos.AuthDtos
{
    public class TokenParamsDto
    {
        public AppUser user { get; set; }
        public string role { get; set; }
    }
}
