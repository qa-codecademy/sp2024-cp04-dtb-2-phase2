﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers
{
    public interface ITokenHelper
    {
        int GetUserId();
        bool GetUserRole();
    }
}
