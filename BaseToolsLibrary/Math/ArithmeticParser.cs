using System;
using System.Linq;

namespace BaseToolsLibrary.Math
{
    public static class ArithmeticParser
    {
        private static string handle_parenthesis(string expr)
        {
            int open_index = expr.IndexOf('(');
            int close_index;

            int amount_open = 1;
            for (close_index = open_index + 1; close_index < expr.Length; close_index += 1)
            {
                if (expr[close_index] == '(')
                    amount_open += 1;
                else if (expr[close_index] == ')')
                    amount_open -= 1;
                if (amount_open == 0)
                    break;
            }
            if (amount_open != 0)
                throw new ArithmeticException("Invalid format in " + expr);

            string left = expr.Substring(0, open_index);
            string middle = expr.Substring(open_index + 1, close_index - open_index - 1);
            string right = expr.Substring(close_index + 1);

            return left + EvaluateExpression(middle).ToString() + right;
        }

        /// <summary>
        ///     Will evaluate an arithmetic expression (operators "+-*/()") 
        ///     1(2+3) is not valid, this will expect 1*(2+3)
        ///     
        ///     warn: will throw ArithmeticException in case of division by 0 or wring syntax
        /// </summary>
        /// <param name="expr"></param>
        /// <returns></returns>
        public static int EvaluateExpression(string expr)
        {
            char op;
            int op_index;
            string left;
            string right;

            expr = expr.Replace("--", "+");
            expr = expr.Replace("-+", "-");
            expr = expr.Replace("+-", "-");
            expr = expr.Replace("++", "+");

            // parenthesis handling are made out of LL parser to make the parser grammar significantly easier
            while (expr.Contains('('))
                expr = handle_parenthesis(expr);

            // Handling of the lowest priority first as the evaluation is made while going back up the recursive tree => first handled, last evaluated 
            if (expr.Contains('+') || expr.Contains('-'))
            {
                op = expr.Last<char>((c) => (c == '+' || c == '-'));
                op_index = (expr.LastIndexOf('+') > expr.LastIndexOf('-') ? expr.LastIndexOf('+') : expr.LastIndexOf('-'));

                if (op_index == 0)
                    // handles +2 or -2 as operations 0+2 or 0-2
                    left = "0";
                else
                    left = expr.Substring(0, op_index);
                right = expr.Substring(op_index + 1);

                switch (op)
                {
                    case '+':
                        return ArithmeticParser.EvaluateExpression(left) + ArithmeticParser.EvaluateExpression(right);
                    case '-':
                        if (left.Length == 0)
                            left = "0";
                        return ArithmeticParser.EvaluateExpression(left) - ArithmeticParser.EvaluateExpression(right);
                }
            }
            // Handling of the highest priority last as the evaluation is made while going back up the recursive tree => last handled, first evaluated 
            else if (expr.Contains('*') || expr.Contains('/'))
            {
                op = expr.Last<char>((c) => (c == '*' || c == '/'));
                op_index = (expr.LastIndexOf('*') > expr.LastIndexOf('/') ? expr.LastIndexOf('*') : expr.LastIndexOf('/'));

                left = expr.Substring(0, op_index);
                right = expr.Substring(op_index + 1);

                switch (op)
                {
                    case '*':
                        return ArithmeticParser.EvaluateExpression(left) * ArithmeticParser.EvaluateExpression(right);
                    case '/':
                        int right_result = ArithmeticParser.EvaluateExpression(right);
                        if (right_result == 0)
                            throw new ArithmeticException(String.Format("Division by zero in {0}", expr));
                        return ArithmeticParser.EvaluateExpression(left) / right_result;
                }
            }
            // handling of the "canonical" case where the arithmetical expression is reduced to its simplest form, an integer
            else
                try
                {
                    return Int32.Parse(expr);
                }
                catch (FormatException) { }
            throw new ArithmeticException(String.Format("Expression {0} was invalid", expr));
        }

    }
}
