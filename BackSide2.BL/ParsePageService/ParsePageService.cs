using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackSide2.BL.Models.ParseDto;
using HtmlAgilityPack;

namespace BackSide2.BL.ParsePageService
{
    public class ParsePageService : IParsePageService
    {
        public async Task<ParsePageReturnDto> ParsePageAsync(ParsePageDto model)
        {
            var web = new HtmlWeb
            {
                AutoDetectEncoding = false,
                OverrideEncoding = Encoding.UTF8
            };
            var htmlDoc = await LoadPage(model.Url, web);

            var rootAddress = web.ResponseUri.Host;
            var fullRootAddress = web.ResponseUri.AbsoluteUri.Split(rootAddress)[0] + rootAddress;

            var pageImages = new List<string>();
            pageImages.AddRange(GetPageFavicons(htmlDoc, fullRootAddress));
            pageImages.AddRange(GetPageImages(htmlDoc, fullRootAddress));
            pageImages = pageImages.Distinct().ToList();

            return new ParsePageReturnDto(GetPageTitle(htmlDoc), model.Url, pageImages,
                GetPageTexts(htmlDoc, model.MinTextLenght, model.MaxTextLenght));
        }

        private static async Task<HtmlDocument> LoadPage(string url, HtmlWeb web)
        {
            try
            {
                return await Task.Run(() => web.Load(url));
            }
            catch (Exception)
            {
                throw new Exception("Unable to load page.");
            }
        }

        private static string GetPageTitle(HtmlDocument htmlDoc)
        {
            return htmlDoc.DocumentNode.SelectSingleNode("//head/title").InnerText;
        }

        private static string ConvertUrlToAbsolute(string url, string fullRootAddress)
        {
            if (url.StartsWith('/') && !url.StartsWith("//"))
            {
                url = fullRootAddress + url;
            }

            return url;
        }

        private static IEnumerable<string> GetPageFavicons(HtmlDocument htmlDoc, string fullRootAddress)
        {
            var favicon = new List<string>();
            foreach (var link in htmlDoc.DocumentNode.SelectNodes("//link[@href]"))
            {
                var att = link.Attributes["href"];
                if (att.Value.EndsWith(".ico"))
                {
                    var faviconLink = ConvertUrlToAbsolute(att.Value, fullRootAddress);
                    favicon.Add(faviconLink);
                }
            }

            return favicon;
        }

        private static IEnumerable<string> GetPageImages(HtmlDocument htmlDoc, string fullRootAddress)
        {
            var resultList = new List<string>();
            var possibleLogo = new List<string>();
            var possibleAvatar = new List<string>();
            var otherImages = new List<string>();

            foreach (var link in htmlDoc.DocumentNode.SelectNodes("//img"))
            {
                var scr = ConvertUrlToAbsolute(link.Attributes["src"].Value, fullRootAddress);

                var linkClass = "";

                foreach (var attr in link.Attributes)
                    if (attr.Name == "class")
                        linkClass = attr.Value;


                if (scr != "")
                {
                    if (linkClass.Contains("logo") || scr.Contains("logo"))
                        possibleLogo.Add(scr);
                    else if (linkClass.Contains("avatar") || scr.Contains("avatar"))
                        possibleAvatar.Add(scr);
                    else otherImages.Add(scr);
                }
            }

            resultList.AddRange(possibleLogo);
            resultList.AddRange(otherImages);
            resultList.AddRange(possibleAvatar);
            return resultList;
        }

        private static List<string> GetPageTexts(HtmlDocument htmlDoc, int minTextLenght,
            int maxTextLenght)
        {
            var text = new List<string>();
            foreach (var node in htmlDoc.DocumentNode.SelectNodes("//text()"))
            {
                var textTmp = node.InnerText;
                if (!string.IsNullOrEmpty(textTmp.Trim()) && textTmp.Length > 10 && node.ParentNode.Name != "script" &&
                    node.ParentNode.Name != "style")
                    if (textTmp.Trim().Length > minTextLenght)
                    {
                        var currentString = textTmp.Trim();
                        text.Add(currentString.Length <= maxTextLenght
                            ? currentString
                            : currentString.Substring(0, maxTextLenght - 3) + "...");
                    }
            }

            return text;
        }
    }
}