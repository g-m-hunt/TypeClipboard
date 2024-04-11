﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TypeClipboard
{
    class Typer
    {
        private const int INTERKEY_DELAY = 20;

        private bool _typeEnter = false;
        public bool TypeEnter { get => _typeEnter; set => _typeEnter = value; }

        public void Type(String str, int delay = 5000)
        {
            Thread.Sleep(delay);
            foreach (Char c in str.ToCharArray())
            {
                // Some characters have special meaning
                // https://docs.microsoft.com/en-us/office/vba/language/reference/user-interface-help/sendkeys-statement
                switch (c)
                {
                    case '\n':
                        if (_typeEnter)
                        {
                            SendKeys.Send("{ENTER}");
                            break;
                        } else
                        {
                            return;
                        }
                    case '\r':
                        if (_typeEnter)
                        {
                            break;
                        }
                        else
                        {
                            return;
                        }
                    case '{':
                        SendKeys.Send("{{}");
                        break;
                    case '}':
                        SendKeys.Send("{}}");
                        break;
                    case '+':
                        SendKeys.Send("{+}");
                        break;
                    case '^':
                        SendKeys.Send("{^}");
                        break;
                    case '%':
                        SendKeys.Send("{%}");
                        break;
                    case '~':
                        SendKeys.Send("{~}");
                        break;
                    case '(':
                        SendKeys.Send("{(}");
                        break;
                    case ')':
                        SendKeys.Send("{)}");
                        break;
                    default:
                        SendKeys.Send(c.ToString());
                        break;
                }
                Thread.Sleep(INTERKEY_DELAY);
            }
        }

        public void TypeClipboard(int delay = 5000)
        {
            if (Clipboard.ContainsText(TextDataFormat.UnicodeText))
            {
                String clipboard = Clipboard.GetText(TextDataFormat.UnicodeText);
                this.Type(clipboard, delay);
            }
        }
    }
}
