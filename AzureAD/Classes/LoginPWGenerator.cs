using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GuntCore
{
    public class TLoginPwdGenerator
    {
        private List<string> loginList;
        private Random RG;
        public TLoginPwdGenerator()
        {
            RG = new Random();
            loginList = new List<string>();
        }
        public const int minminPwdLength = 4;
        public const int minPwdLength = 8;
        public const int maxPwdLength = 12;
        private bool testDispo(string l)
        {
            return false;
            if (loginList.Contains(l)) return false;
//            return AdsiHelper.testADdispo(l, FCreds);
        }
        public void clearReserved()
        {
            loginList.Clear();
        }
        public void reserveLogin(string l)
        {
            loginList.Add(l);
        }
        private string truncAndDeco(string s)
        {
            int fin = s.Length;
            fin = (fin < maxPwdLength) ? fin : maxPwdLength;
            for (int i = minPwdLength; i <= fin; i++)
            {
                string ss = s.Substring(0, i);
                if (testDispo(ss)) return ss;
            }
            return Deco(s.Substring(0, minPwdLength));
        }

        private string Deco(string s)
        {
            for (int i = 1; i < 9999; i++)
            {
                string ss = s + i.ToString();
                if (testDispo(ss)) return ss;
            }
            return s;
        }

        public string suggestLogin(string n, string p)
        {
            if (n == "") return "";
            string s = stringManip.getInitial(p);
            n = stringManip.simplifyName(n);
            string[] nameParts = n.Split('-');
            s = s + nameParts[0];
            int i = 1;
            while ((i < nameParts.Length) && ((s.Length + nameParts[i].Length <= maxPwdLength) || (s.Length <= minminPwdLength)))
            {
                s = s + nameParts[i++];
            }
            if (s.Length > maxPwdLength) return truncAndDeco(s);
            if (testDispo(s)) return s;
            if (i < nameParts.Length)
            {
                string ss = s + "-" + nameParts[i].Substring(0, 1);
                if (testDispo(ss)) return ss;
            }
            if (s.Length > minPwdLength) return truncAndDeco(s);
            return Deco(s);
        }

        private char rndalpha()
        {
            int cha = Convert.ToInt32('a');
            int chl = Convert.ToInt32('l');
            int chz = Convert.ToInt32('z');
            int code = RG.Next(cha, chz);
            if (code == chl) return 'k';
            return Convert.ToChar(code);
        }

        public string suggestPwd()
        {
            StringBuilder s = new StringBuilder();
            int ch0 = Convert.ToInt32('0');
            int ch1 = Convert.ToInt32('1');
            int ch9 = Convert.ToInt32('9');
            s.Append(rndalpha());
            s.Append(Convert.ToChar(RG.Next(ch1, ch9)));
            s.Append(rndalpha());
            s.Append(Convert.ToChar(RG.Next(ch1, ch9)));
            s.Append(Convert.ToChar(RG.Next(ch0, ch9)));
            return s.ToString();
        }
    }
}
