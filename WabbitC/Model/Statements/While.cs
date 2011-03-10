﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using WabbitC.Model.Types;

namespace WabbitC.Model.Statements
{
    class While : ControlStatement
    {
		Block Condition;
        Block Body;
        Label Label;
        private Declaration condDecl;

        public static While Parse(Block parent, ref List<Token>.Enumerator tokens)
        {
            Debug.Assert(tokens.Current.Text == "while");

            tokens.MoveNext();
            Debug.Assert(tokens.Current.Text == "(");
            tokens.MoveNext();
            List<Token> conditionList = Tokenizer.GetArgument(ref tokens);
            tokens.MoveNext();
            Debug.Assert(tokens.Current.Text == "{");
            tokens.MoveNext();

            Block body = Block.ParseBlock(ref tokens, parent);
            tokens.MoveNext();

            var condDecl = parent.CreateTempDeclaration(new BuiltInType("int"));
            Block condBlock = new Block();
            condBlock.Parent = parent;
            AssignmentHelper.Parse(condBlock, condDecl, conditionList);

            return new While(parent, condDecl, condBlock, body);
        }

        public While(Block parent, Declaration decl,  Block cond, Block body)
        {
            Debug.Assert(parent != null);
            Debug.Assert(cond != null);
            Debug.Assert(body != null);

            body.Parent = parent;

            Condition = cond;
            Body = body;
            Label = parent.CreateTempLabel();
            condDecl = decl;
        }

        public override string ToString()
        {
            string result = "goto " + Label + ";\n";
            result += "do\n";
            result += "{\n";

            foreach (Statement statement in Body.Statements)
            {
                result += statement.ToString() + "\n";
            }
            result += Label + ":\n";
            foreach (Statement statement in Condition.Statements)
            {
                result += statement.ToString() + "\n";
            }
            result += "}";
            result += "while (" + condDecl.Name + ");";
            
            return result;
        }
    }
}
