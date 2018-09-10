using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BackSide2.BL.authorize;
using BackSide2.BL.Entity;
using BackSide2.DAO.Entities;
using BackSide2.DAO.Repository;
using HtmlAgilityPack;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

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
            HtmlWeb web = new HtmlWeb();
            HtmlDocument htmlDoc;
            htmlDoc = web.Load(model.Url);

            var titleNode = htmlDoc.DocumentNode.SelectSingleNode("//head/title");

            var urls = htmlDoc.DocumentNode.Descendants("img")
                .Select(e => e.GetAttributeValue("src", null))
                .Where(s => !String.IsNullOrEmpty(s));

            var texts = htmlDoc.DocumentNode.Descendants("p");

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
                images = urls,
                possibleDescriptions = text
            };

            return response;
        }
    }
}