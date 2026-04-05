using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DeviceMonitor.Domain
{
    public class CommandBlocker
    {
        private readonly List<string[]> _blocklist = new List<string[]>()
        {
            new[] {"rm", "-rf" },
            new[] { "shutdown"},
            new[] {"mkfs"},
            new[] {"dd"}
        };
        public bool CheckIsBlocked (string command)
        {
            List<string> tokens = Tokenize(Normalize(command));

            foreach (string[] blocked in _blocklist) 
            {
                if(IsMatch(tokens, blocked))
                {
                    return false;
                }
            }
            return true;
        }
        private string Normalize (string input)
        {
            input = input.ToLower();
            input = Regex.Replace(input, @"\s+", " ");
            return input.Trim();
        }
        private List<string> Tokenize (string input)
        {
            return input.Split(" ", StringSplitOptions.RemoveEmptyEntries).ToList();
        }
        private bool IsMatch (List<string> inputTokens, string[] blockedTokens)
        {
            if (inputTokens.Count < blockedTokens.Length)
            {
                return false;
            }
            for (int i = 0; i <= inputTokens.Count - blockedTokens.Length; i++)
            {
                bool match = true;
                for (int j = 0; j < blockedTokens.Length; j++)
                {
                    if (inputTokens[i + j] != blockedTokens[j])
                    {
                        match = false;
                        break;
                    }
                }
                if(match)
                {
                    return true;
                }                
            }
            return false;
        }     
    }
}
