using System;
using System.Collections.Generic;
using System.Text;

namespace CommandPattern.Models
{
    using Contracts;

    public class HelloCommand : ICommand
    {
        public string Execute(string[] args) => $"Hello, {args[0]}";
    }
}
