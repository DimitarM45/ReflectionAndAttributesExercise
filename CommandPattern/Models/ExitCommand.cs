using System;
using System.Collections.Generic;
using System.Text;

namespace CommandPattern.Models
{
    using Contracts;

    public class ExitCommand : ICommand
    {
        private const int DEFAULT_ERROR_CODE = 0;

        public string Execute(string[] args)
        {
            Environment.Exit(DEFAULT_ERROR_CODE);

            return null;
        }
    }
}
