using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleStuff.Tests.Commands
{
    public class CommandMenu
    {
        private readonly Dictionary<string, Command> _commands;

        public CommandMenu(params Command[] commands)
        {
            if(commands.Count(c => c.IsDefault) > 1) throw new ArgumentException("Cannot have more than one default.", nameof(commands));

            _commands = commands.ToDictionary(k => k.Verb, k => k);
        }

        public bool Execute(string commandString)
        {
            var words = commandString.Split(' ');
            var verb = words.First();
            var commandParams = string.Join(" ", words.Skip(1));

            Command cmd;
            if (_commands.ContainsKey(verb))
            {
                cmd = _commands[verb];
            }
            else
            {
                var key = _commands.Keys.FirstOrDefault(k => k.StartsWith(verb));
                if (key != null)
                {
                    cmd = _commands[key];
                }
                else
                {
                    cmd = _commands.Values.FirstOrDefault(v => v.IsDefault);
                }

                if (cmd == null) return false;
            }

            cmd.Action(commandParams);

            return true;
        }

        public string HelpText()
        {
            var sb = new StringBuilder();
            foreach (var kvp in _commands)
            {
                var v = kvp.Value;
                sb.Append($"{kvp.Key.PadRight(15)}");

                if(!string.IsNullOrEmpty(v.OneLiner))
                {
                    sb.Append($" - {v.OneLiner}");
                }

                sb.AppendLine();
            }

            return sb.ToString();
        }
    }
}