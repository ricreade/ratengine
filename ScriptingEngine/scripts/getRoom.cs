using System;
using System.Text.RegularExpressions;
using ScriptingEngine;

public class GetRoom : IScriptInstance
{
    private readonly string _regexsyntax = @"^get\sroom\sg(?<elem>\d?\d)$";
    //private readonly Regex _regex = new Regex(@"^get\sroom\sg(?<elem>\d?\d)$");
    private readonly string _strRegNum = "elem";

    public IScriptResult ProcessRequest(IScriptRequest request)
    {
        string strRegNum = null;
        int regNum;
        string instr = request.Instruction.Instruction;
        if (request.Instruction.Syntax.IsSyntaxMatch(instr) && request.Instruction.Syntax.RegularExpression.ToString().Equals(_regexsyntax))
        {
            Match match = request.Instruction.Syntax.RegularExpression.Match(instr);
            Group grpRegNum = match.Groups[_strRegNum];
            strRegNum = grpRegNum.Value;
            if (int.TryParse(strRegNum, out regNum))
            {
                if (regNum < request.Command.ResultSet.ElementRegister.Length)
                {
                    request.Command.ResultSet.ElementRegister[regNum] = request.Command.Source.Location;
                    return ScriptUtil.CreateResult(ScriptResult.ResultType.Success, request.Command);
                }
            }
        }
        //Match match = _regex.Match(instr);
        //if (match.Success)
        //{
        //    Group grpRegNum = match.Groups[_strRegNum];
        //    strRegNum = grpRegNum.Value;
        //    if (int.TryParse(strRegNum, out regNum))
        //    {
        //        if (regNum < request.Command.ResultSet.ElementRegister.Length)
        //        {
        //            request.Command.ResultSet.ElementRegister[regNum] = request.Command.Source.Location;
        //            return ScriptUtil.CreateResult(ScriptResult.ResultType.Success, request.Command);
        //        }
        //    }
        //}
        return ScriptUtil.CreateResult(ScriptResult.ResultType.Fail, request.Command);
    }
}