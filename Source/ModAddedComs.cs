using System;
using System.Linq;
using System.Collections.Generic;

public class CPHInline
{
    public bool AddCommand()
    {
        var trigger = GetTrigger();
        //if command_input0 exists then do nothing
        var comcheck = CPH.GetGlobalVar<string>($"command_{trigger}", true);
        if (comcheck == null)
        {
            var comout = args["rawInput"].ToString();
            var triglen = trigger.Length;
            var outputcom = (comout.Remove(0, triglen+1)).Trim();
            CPH.SetGlobalVar($"command_{trigger}", outputcom, true);
            CPH.SendAction($"Command {trigger} has been added >> [{outputcom}]");
            var comlist =  GetComList();
            comlist.Add(trigger);
            CPH.SetGlobalVar("com_list", comlist, true);
            return true;
        }
        else
        {
            CPH.SendAction("Command already exists");
            return true;
        }
    }
    
    private ISet<string> GetComList() 
		=> CPH.GetGlobalVar<HashSet<string>>("com_list", true) ?? new HashSet<string>();
		
	private string GetTrigger()
		=> args["input0"].ToString().ToLower().TrimStart('!');

    public bool PullCommandList()
    {
		var comlist =  GetComList();
        if(!comlist.Any())
			CPH.SendAction("No commands found for list.");
        else
        {
			var comout = comlist.Aggregate((x, y) => x + ", " + y);
			CPH.SendAction(comout);
		}
        return true;
    }

    public bool DeleteCommand()
    {
        var trigger = GetTrigger();
        var comcheck = CPH.GetGlobalVar<string>($"command_{trigger}", true);
        if (comcheck != null)
        {
            CPH.UnsetGlobalVar($"command_{trigger}", true);
            var comlist = GetComList();
            comlist.Remove(trigger);
            CPH.SetGlobalVar("com_list", comlist, true);
            CPH.SendAction($"Command {trigger} has been deleted");
            return true;
        }
        else
        {
            CPH.SendAction("Command not found");
            return true;
        }
    }

    public bool EditCommand()
    {
        var trigger = GetTrigger();
        //if command_input0 exists then do nothing
        var comcheck = CPH.GetGlobalVar<string>($"command_{trigger}", true);
        if (comcheck != null)
        {
            var comout = args["rawInput"].ToString();
            var triglen = trigger.Length;
            var outputcom = (comout.Remove(0, triglen+1)).Trim();
            CPH.SetGlobalVar($"command_{trigger}", outputcom, true);
            CPH.SendAction($"Command {trigger} has been updated >> [{outputcom}]");
            return true;
        }
        else
        {
            CPH.SendAction("Command Doesn't Exists");
            return true;
        }
    }

    public bool CommandCheck()
    {
        var trigger = args["input0"].ToString().ToLower().TrimStart('!');
        var comcheck = CPH.GetGlobalVar<string>($"command_{trigger}", true);
        return comcheck != null;
    }

    public bool CommandOutput()
    {
        var command = args["input0"].ToString();
        if (command.StartsWith(args["command"].ToString()))
        {
            command = (command.Remove(0, args["command"].ToString().Length));
        }

        var trigger = command.ToLower();
        var message = CPH.GetGlobalVar<string>($"command_{trigger}", true);
        foreach (string word in message.Split('%'))
        { //SPLIT WORDS UPON %
            if (args.ContainsKey(word) && !String.IsNullOrEmpty(word))
            {
                //CHECK IF ITS A VALID ARGUMENT BY SB
                message = message.Replace("%" + word + "%", args[word].ToString());
            //REPLACE WITH THE ARGUMENT VALUE IF IT IS A VALID ARGUMENT
            }
        }

        CPH.SendMessage(message);
        return true;
    }
}