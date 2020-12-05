﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba1
{
    interface IHttpClient
    {
        bool Ping(string url);
        string GetInputData(string url);
        void WriteAnswer(string url, string json);
    }
}
