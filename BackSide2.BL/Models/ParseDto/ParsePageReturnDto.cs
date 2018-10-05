using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BackSide2.BL.Models.ParseDto
{
    public class ParsePageReturnDto
    {
        public ParsePageReturnDto(string header, string url, List<string> images, List<string> possibleDescriptions)
        {
            Url = url;
            Header = header;
            Images = images;
            PossibleDescriptions = possibleDescriptions;
        }


        [Url] public string Url { get; set; }
        [Url] public string Header { get; set; }
        public List<string> Images { get; set; }
        public List<string> PossibleDescriptions { get; set; }
    }
}