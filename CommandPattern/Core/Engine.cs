using System;
using System.Collections.Generic;
using System.Text;

namespace CommandPattern.Core
{
    using CommandPattern.Utilities.Contracts;
    using Contracts;

    public class Engine : IEngine
    {
        private readonly ICommandInterpreter commandInterpreter;

        public Engine(ICommandInterpreter commandInterpreter)
        {
            this.commandInterpreter = commandInterpreter;
        }

        public void Run()
        {
            string cmdArgs = Console.ReadLine();

            string result = commandInterpreter.Read(cmdArgs);

            Console.WriteLine(result);
        }
    }
}
