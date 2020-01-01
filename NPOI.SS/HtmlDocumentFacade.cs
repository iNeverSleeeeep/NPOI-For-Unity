using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace NPOI.SS
{
	public class HtmlDocumentFacade
	{
		protected XmlElement body;

		protected XmlDocument document;

		protected XmlElement head;

		protected XmlElement html;

		private Dictionary<string, Dictionary<string, string>> stylesheet = new Dictionary<string, Dictionary<string, string>>();

		private XmlElement stylesheetElement;

		protected XmlElement title;

		protected XmlText titleText;

		public XmlElement Body => body;

		public XmlDocument Document => document;

		public XmlElement Head => head;

		public string Title
		{
			get
			{
				if (title == null)
				{
					return null;
				}
				return titleText.InnerText;
			}
			set
			{
				if (string.IsNullOrEmpty(value) && title != null)
				{
					head.RemoveChild(title);
					title = null;
					titleText = null;
				}
				if (title == null)
				{
					title = document.CreateElement("title");
					titleText = document.CreateTextNode(value);
					title.AppendChild(titleText);
					head.AppendChild(title);
				}
				titleText.InnerText = value;
			}
		}

		public HtmlDocumentFacade(XmlDocument document)
		{
			this.document = document;
			html = document.CreateElement("html");
			document.AppendChild(html);
			body = document.CreateElement("body");
			head = document.CreateElement("head");
			stylesheetElement = document.CreateElement("style");
			stylesheetElement.SetAttribute("type", "text/css");
			html.AppendChild(head);
			html.AppendChild(body);
			head.AppendChild(stylesheetElement);
			AddCharset();
			AddStyleClass(body, "b", "white-space-collapsing:preserve;");
		}

		public void AddCharset()
		{
			XmlElement xmlElement = document.CreateElement("meta");
			xmlElement.SetAttribute("http-equiv", "Content-Type");
			xmlElement.SetAttribute("content", "text/html; charset=UTF-8");
			head.AppendChild(xmlElement);
		}

		public void AddAuthor(string value)
		{
			AddMeta("author", value);
		}

		public void AddDescription(string value)
		{
			AddMeta("description", value);
		}

		public void AddKeywords(string value)
		{
			AddMeta("keywords", value);
		}

		public void AddMeta(string name, string value)
		{
			XmlElement xmlElement = document.CreateElement("meta");
			xmlElement.SetAttribute("name", name);
			xmlElement.SetAttribute("content", value);
			head.AppendChild(xmlElement);
		}

		public void AddStyleClass(XmlElement element, string classNamePrefix, string style)
		{
			string attribute = element.GetAttribute("class");
			string orCreateCssClass = GetOrCreateCssClass(element.Name, classNamePrefix, style);
			string namespaceURI = string.IsNullOrEmpty(attribute) ? orCreateCssClass : (attribute + " " + orCreateCssClass);
			element.GetAttribute("class", namespaceURI);
		}

		public XmlElement CreateBlock()
		{
			return document.CreateElement("div");
		}

		public XmlElement CreateBookmark(string name)
		{
			XmlElement xmlElement = document.CreateElement("a");
			xmlElement.SetAttribute("name", name);
			return xmlElement;
		}

		public XmlElement CreateHeader1()
		{
			return document.CreateElement("h1");
		}

		public XmlElement CreateHeader2()
		{
			return document.CreateElement("h2");
		}

		public XmlElement CreateHyperlink(string internalDestination)
		{
			XmlElement xmlElement = document.CreateElement("a");
			xmlElement.SetAttribute("href", internalDestination);
			return xmlElement;
		}

		public XmlElement CreateImage(string src)
		{
			XmlElement xmlElement = document.CreateElement("img");
			xmlElement.SetAttribute("src", src);
			return xmlElement;
		}

		public XmlElement CreateLineBreak()
		{
			return document.CreateElement("br");
		}

		public XmlElement CreateListItem()
		{
			return document.CreateElement("li");
		}

		public XmlElement CreateParagraph()
		{
			return document.CreateElement("p");
		}

		public XmlElement CreateTable()
		{
			return document.CreateElement("table");
		}

		public XmlElement CreateTableBody()
		{
			return document.CreateElement("tbody");
		}

		public XmlElement CreateTableCell()
		{
			return document.CreateElement("td");
		}

		public XmlElement CreateTableColumn()
		{
			return document.CreateElement("col");
		}

		public XmlElement CreateTableColumnGroup()
		{
			return document.CreateElement("colgroup");
		}

		public XmlElement CreateTableHeader()
		{
			return document.CreateElement("thead");
		}

		public XmlElement CreateTableHeaderCell()
		{
			return document.CreateElement("th");
		}

		public XmlElement CreateTableRow()
		{
			return document.CreateElement("tr");
		}

		public XmlText CreateText(string data)
		{
			return document.CreateTextNode(data);
		}

		public XmlElement CreateUnorderedList()
		{
			return document.CreateElement("ul");
		}

		public string GetOrCreateCssClass(string tagName, string classNamePrefix, string style)
		{
			if (!stylesheet.ContainsKey(tagName))
			{
				stylesheet.Add(tagName, new Dictionary<string, string>(1));
			}
			Dictionary<string, string> dictionary = stylesheet[tagName];
			if (dictionary.ContainsKey(style))
			{
				return dictionary[style];
			}
			string text = classNamePrefix + (dictionary.Count + 1);
			dictionary.Add(style, text);
			return text;
		}

		public void UpdateStylesheet()
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (KeyValuePair<string, Dictionary<string, string>> item in stylesheet)
			{
				string key = item.Key;
				foreach (KeyValuePair<string, string> item2 in item.Value)
				{
					string key2 = item2.Key;
					string value = item2.Value;
					stringBuilder.Append(key + "." + value + "{" + key2 + "}\n");
				}
			}
			stylesheetElement.InnerText = stringBuilder.ToString();
		}
	}
}
