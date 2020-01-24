using System;

namespace Player.View.Card
{
    internal class UrlOperations
    {
        public string GetUrl(string Uinput)
        {
            try
            {
                if (Uinput.Substring(0, 8) != "https://" )
                {
                    Uinput = "https://" + Uinput;
                    return Uinput;
                }
                else
                {
                    return Uinput;
                }
            }
            catch (ArgumentOutOfRangeException e) {
                return GetSearchUrl(Uinput);
            }
        }

        internal string GetSearchUrl(string Uinput)
        {
            Uinput.Replace(' ', '+');
            Uinput = "https://google.com/search?q=" + Uinput;
            return Uinput;
        }

        internal string GetUrlToShow(string Uinput)
        {
            if(Uinput.Substring(0, 8) == "https://")
            Uinput = Uinput.Substring(8);
            else if(Uinput.Substring(0, 7) == "http://")
                Uinput = Uinput.Substring(7);
            return Uinput;
        }
    }
}