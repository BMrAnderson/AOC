﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public interface IDocumentLinesReaderAsync
    {
        Task<IEnumerable<string>> ReadAsync(string filepath);
    }
}
