﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobiOcean.MDM.Web.Log.Interface
{
    public interface ILog
    {
        void LogData(string message);
        void LogAPIData(string message);
    }
}
