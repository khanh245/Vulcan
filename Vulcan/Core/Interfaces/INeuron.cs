﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vulcan.Core.Interfaces
{
    internal interface INeuron
    {
        double GetWeight(int id);

        double GetInput(int id);

        double GetOutput(int id);
    }
}