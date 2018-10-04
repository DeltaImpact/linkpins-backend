using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackSide2.BL.Models.ParseDto;
using HtmlAgilityPack;
using Microsoft.Extensions.Configuration;

namespace BackSide2.BL.ParsePageService
{
    public class ParsePageService : IParsePageService
    {
        private readonly IConfiguration _configuration;

        public ParsePageService(
            IConfiguration configuration
        )
        {
            _configuration = configuration;
        }

        public async Task<object> ParsePageAsync(ParsePageDto model)
        {
            var web = new HtmlWeb
            {
                AutoDetectEncoding = false,
                OverrideEncoding = Encoding.UTF8
            };


            HtmlDocument htmlDoc;
            try
            {
                htmlDoc = web.Load(model.Url);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }


            var encoding = htmlDoc.Encoding;


            string rootAddress = web.ResponseUri.Host;
            string fullRootAddress = web.ResponseUri.AbsoluteUri.Split(rootAddress)[0] + rootAddress;


            var titleNode = htmlDoc.DocumentNode.SelectSingleNode("//head/title");

            List<string> ihrefTags = new List<string>();

            List<string> favicon = new List<string>();
            List<string> possibleLogo = new List<string>();
            List<string> possibleAvatar = new List<string>();
            List<string> otherImages = new List<string>();

            string siteLink = fullRootAddress;

            foreach (HtmlNode link in htmlDoc.DocumentNode.SelectNodes("//link[@href]"))
            {
                HtmlAttribute att = link.Attributes["href"];
                if (att.Value.EndsWith(".ico"))
                {
                    var faviconLink = att.Value;
                    if (faviconLink.StartsWith('/')) faviconLink = siteLink + faviconLink;
                    favicon.Add(faviconLink);
                }
            }

            foreach (HtmlNode link in htmlDoc.DocumentNode.SelectNodes("//img"))
            {
                string scr = link.Attributes["src"].Value;

                if (scr.StartsWith("//"))
                {
                }
                else if (scr.StartsWith('/'))
                {
                    scr = siteLink + scr;
                }

                string linkClass = "";

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

            ihrefTags.AddRange(favicon);
            ihrefTags.AddRange(possibleLogo);
            ihrefTags.AddRange(otherImages);
            ihrefTags.AddRange(possibleAvatar);
            ihrefTags = ihrefTags.Distinct().ToList();


            List<string> text = new List<string>();
            foreach (HtmlNode node in htmlDoc.DocumentNode.SelectNodes("//text()"))
            {
                string textTmp = node.InnerText;
                if (!string.IsNullOrEmpty(textTmp.Trim()) && textTmp.Length > 10 && node.ParentNode.Name != "script" &&
                    node.ParentNode.Name != "style")
                    if (textTmp.Trim().Length > model.MinTextLenght)
                        text.Add(textTmp.Trim());
            }


            var response = new
            {
                url = model.Url,
                header = titleNode.InnerText,
                images = ihrefTags,
                possibleDescriptions = text
            };

            return response;
        }


        private static HtmlAttribute GetAttr(HtmlNode linkTag, string attr)
        {
            return linkTag.Attributes.FirstOrDefault(x =>
                x.Name.Equals(attr, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}