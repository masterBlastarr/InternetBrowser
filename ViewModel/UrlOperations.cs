using System;

namespace Player.View.Card
{
    internal class UrlOperations
    {
        public string GetUrl(string Uinput)
        {
            try
            {
                if (Uinput.Substring(0, 8) != "https://" || Uinput.Length < 8)
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
            Uinput = Uinput.Substring(8);
            return Uinput;
        }
    }
}