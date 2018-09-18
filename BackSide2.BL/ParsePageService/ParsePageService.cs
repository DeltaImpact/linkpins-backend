using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using BackSide2.BL.Entity;
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


        private static HtmlAttribute GetAttr(HtmlNode linkTag, string attr)
        {
            return linkTag.Attributes.FirstOrDefault(x =>
                x.Name.Equals(attr, StringComparison.InvariantCultureIgnoreCase));
        }

        public async Task<object> ParsePageAsync(ParsePageDto model)
        {
            HtmlWeb web = new HtmlWeb();
            //web.OverrideEncoding = Encoding.UTF8;
            //web.OverrideEncoding = Encoding;

            //web.OverrideEncoding = Encoding.GetEncoding("ISO-8859-1");
            //web.OverrideEncoding = Encoding.GetEncoding("iso-8859-1");
            web.AutoDetectEncoding = false;
            web.OverrideEncoding = Encoding.UTF8;

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


            string rootAdress = web.ResponseUri.Host;
            string fullRootAdress = web.ResponseUri.AbsoluteUri.Split(rootAdress, StringSplitOptions.None)[0] + rootAdress;


            var titleNode = htmlDoc.DocumentNode.SelectSingleNode("//head/title");

            //var titleNode1 = htmlDoc.DocumentNode.SelectNodes("//link/title");


            //foreach (Match match in Regex.Matches(htmlText, "<link .*? href=\"(.*?.ico)\""))
            //{
            //    String url = match.Groups[1].Value;

            //    Console.WriteLine(url);
            //}

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            //List<string> hrefTags = new List<string>();

            //foreach (HtmlNode link in htmlDoc.DocumentNode.SelectNodes("//a[@href]"))
            //{
            //    HtmlAttribute att = link.Attributes["href"];
            //    hrefTags.Add(att.Value);
            //}

            List<string> ihrefTags = new List<string>();

            List<string> favicon = new List<string>();
            List<string> possibleLogo = new List<string>();
            List<string> possibleAvatar = new List<string>();
            List<string> otherImages = new List<string>();

            //string siteLink = model.Url.TrimEnd('/');
            string siteLink = fullRootAdress;
            //siteLink = "";

            foreach (HtmlNode link in htmlDoc.DocumentNode.SelectNodes("//link[@href]"))
            {
                HtmlAttribute att = link.Attributes["href"];
                if (att.Value.EndsWith(".ico"))
                {
                    var faviconLink = att.Value;
                    if (faviconLink.StartsWith('/'))
                    {
                        faviconLink = siteLink + faviconLink;
                    }
                    favicon.Add(faviconLink);
                }
            }

            //foreach (Match match in Regex.Matches(htmlDoc.ToString(), "<link .*? href=\"(.*?.ico)\""))
            //{
            //    String url = match.Groups[1].Value;
            //    //favicon.Add(url);

            //    Console.WriteLine(url);
            //}

            foreach (HtmlNode link in htmlDoc.DocumentNode.SelectNodes("//img"))
            {
                string scr = link.Attributes["src"].Value;

                if (scr.StartsWith("//"))
                {
                } else if (scr.StartsWith('/'))
                {
                    scr = siteLink + scr;
                }

                string linkClass = "";
                //string linkWidth = null;
                //string linkHeight = null;
                //linkClass = link.Attributes["class"].Value;

                foreach (var attr in link.Attributes)
                {
                    if (attr.Name == "class")
                    {
                        linkClass = attr.Value;
                    }

                    //if (attr.Name == "width") linkWidth = attr.Value;
                    //if (attr.Name == "height") linkHeight = attr.Value;
                }

                if (scr != "")
                {
                    if (linkClass.Contains("logo") || scr.Contains("logo"))
                    {
                        possibleLogo.Add(scr);
                    }
                    else if (linkClass.Contains("avatar") || scr.Contains("avatar"))
                    {
                        possibleAvatar.Add(scr);
                    }
                    else if (linkClass != null)
                    {
                        otherImages.Add(scr);
                    }
                }

                //if (scr != "") ihrefTags.Add(scr);
            }
            ihrefTags.AddRange(favicon);
            ihrefTags.AddRange(possibleLogo);
            ihrefTags.AddRange(otherImages);
            ihrefTags.AddRange(possibleAvatar);
            ihrefTags = ihrefTags.Distinct().ToList();

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////


            //var texts = htmlDoc.DocumentNode.Descendants("p");

            List<string> text = new List<string>();
            foreach (HtmlNode node in htmlDoc.DocumentNode.SelectNodes("//text()"))
            {
                string textTmp = node.InnerText;
                if (!string.IsNullOrEmpty(textTmp.Trim()) && textTmp.Length > 10 && node.ParentNode.Name != "script" &&
                    node.ParentNode.Name != "style")
                    if (textTmp.Trim().Length > model.MinTextLenght)
                        text.Add(textTmp.Trim());
                //text.Add(node.InnerText);
                //Console.WriteLine("text=" + node.InnerText);
            }

            //text.Sort((x, y) => y.Length.CompareTo(x.Length));

            var response = new
            {
                url = model.Url,
                header = titleNode.InnerText,
                //images = ihrefObj,
                images = ihrefTags,
                possibleDescriptions = text
            };

            return response;
        }
    }
}