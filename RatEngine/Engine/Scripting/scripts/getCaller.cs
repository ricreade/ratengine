using System;
using System.Text.RegularExpressions;
using RatEngine.Engine.Scripting;

public class GetCaller : IScriptInstance
{
    private readonly Regex _regex = new Regex(@"^get\scaller\sg(?<elem>\d?\d)$");
    private readonly string _strRegNum = "elem";
    
    public IScriptResult ProcessRequest(IScriptRequest request)
    {
        string strRegNum = null;
        int regNum;
        string instr = request.Instruction.Instruction;
        Match match = _regex.Match(instr);
        if (match.Success)
        {
            Group grpRegNum = match.Groups[_strRegNum];
            strRegNum = grpRegNum.Value;
            if (int.TryParse(strRegNum, out regNum))
            {
                if (regNum < request.Command.ResultSet.ElementRegister.Length)
                {
                    request.Command.ResultSet.ElementRegister[regNum] = request.Command.Source;
                    return ScriptUtil.CreateResult(ScriptResult.ResultType.Success, request.Command);
                }
            }
        }
        return ScriptUtil.CreateResult(ScriptResult.ResultType.Fail, request.Command);
    }
}