using System;
using System.Linq;
using System.Reflection;

namespace CommandPattern.Utilities
{
    using Contracts;

    public class CommandInterpreter : ICommandInterpreter
    {
        public string Read(string args)
        {
            Assembly assembly = Assembly.GetEntryAssembly();

            string cmdName = args
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)[0];

            string[] cmdArgs = args
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Skip(1)
                .ToArray();

            Type cmdType = assembly
                .GetTypes()
                .FirstOrDefault(t => t.Name == $"{cmdName}Command");

            if (cmdType == null)
                throw new InvalidOperationException("Invalid command type!");

            MethodInfo cmdMethodInfo = cmdType
                .GetMethods()
                .FirstOrDefault(m => m.Name == "Execute");

            if (cmdMethodInfo == null)
                throw new InvalidOperationException("Execute method not found. Try implementing the ICommand interface!");

            object cmdInstance = Activator.CreateInstance(cmdType);

            string result = (string)cmdMethodInfo
                .Invoke(cmdInstance, new object[] { cmdArgs });

            return result;
        }
    }
}
