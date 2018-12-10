using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatProgramClient
{
    class MessagePackage
    {
        public int MessageType { get; set; }
        public int IsFile { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Message { get; set; }
        public string ComposedMessage { get; set; }

        public MessagePackage()
        { }
        public MessagePackage(string message)
        {
            ComposedMessage = message;
        }

        public MessagePackage(int type, int file, string from, string to, string message)
        {
            IsFile = file;
            MessageType = type;
            From = from;
            To = to;
            Message = message;
        }

        public Tuple<int, int, string, string, string> SliceMessage(string message)
        {
            int pos;
            pos = message.IndexOf("<=>");
            MessageType = Int32.Parse(message.Substring(0, pos));
            message = message.Remove(0, pos + 3);

            pos = message.IndexOf("<=>");
            IsFile = Int32.Parse(message.Substring(0, pos));
            message = message.Remove(0, pos + 3);

            pos = message.IndexOf("<=>");
            From = message.Substring(0, pos);
            message = message.Remove(0, pos + 3);

            pos = message.IndexOf("<=>");
            To = message.Substring(0, pos);
            message = message.Remove(0, pos + 3);

            Message = message;

            return Tuple.Create(MessageType, IsFile, From, To, Message);
        }

        public string ConcatenateMessage(int type, int file, string from, string to, string message)
        {
            return type + "<=>" + file + "<=>" + from + "<=>" + to + "<=>" + message;
        }
        public string ConcatenateMessage()
        {
            return MessageType + "<=>" + IsFile + "<=>" + From + "<=>" + To + "<=>" + Message;
        }

        public List<string> ToUsernameList(string strNames)
        {
            List<string> returnList = new List<string>();
            string[] names = strNames.Split(',');
            foreach (string s in names)
            {
                if (!s.Equals(String.Empty))
                    returnList.Add(s);
            }
            return returnList;
        }

        public string ToUsernameString(List<string> list)
        {
            string usernames = string.Empty;
            for (int i = 0; i < list.Count(); ++i)
            {
                usernames += list.ElementAt(i);
                int j = i + 1;
                if (j == list.Count())
                    continue;
                usernames += ",";
            }
            return usernames;
        }
    }
}
