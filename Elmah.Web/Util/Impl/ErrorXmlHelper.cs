
using Elmah.Web.Models;
using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Xml;

namespace Elmah.Web.Util.Impl
{
    [Serializable]
    public class ErrorXmlHelper : IErrorXmlHelper
    {
        /// <summary>
        /// Decodes an <see cref="OriginalError"/> object from its default XML 
        /// representation.
        /// </summary>

        public OriginalError DecodeString(string xml)
        {
            using (StringReader sr = new StringReader(xml))
            {
                XmlTextReader reader = new XmlTextReader(sr);

                if (!reader.IsStartElement("error"))
                    throw new ApplicationException("The originalError XML is not in the expected format.");

                return Decode(reader);
            }
        }

        /// <summary>
        /// Decodes an <see cref="OriginalError"/> object from its XML representation.
        /// </summary>

        public OriginalError Decode(XmlReader reader)
        {
            if (reader == null)
                throw new ArgumentNullException("reader");

            //
            // Reader must be positioned on an element!
            //

            if (!reader.IsStartElement())
                throw new ArgumentException("Reader is not positioned at the start of an element.", "reader");

            //
            // Read out the attributes that contain the simple
            // typed state.
            //

            OriginalError originalError = new OriginalError();
            ReadXmlAttributes(reader, originalError);

            //
            // Move past the element. If it's not empty, then
            // read also the inner XML that contains complex
            // types like collections.
            //

            bool isEmpty = reader.IsEmptyElement;
            reader.Read();

            if (!isEmpty)
            {
                ReadInnerXml(reader, originalError);
                while (reader.NodeType != XmlNodeType.EndElement)
                    reader.Skip();
                reader.ReadEndElement();
            }

            return originalError;
        }

        /// <summary>
        /// Encodes the default XML representation of an <see cref="OriginalError"/> 
        /// object to a string.
        /// </summary>

        public string EncodeString(OriginalError originalError)
        {
            StringWriter sw = new StringWriter();

#if !NET_1_0 && !NET_1_1
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.NewLineOnAttributes = true;
            settings.CheckCharacters = false;
            settings.OmitXmlDeclaration = true;
            XmlWriter writer = XmlWriter.Create(sw, settings);
#else
            XmlTextWriter writer = new XmlTextWriter(sw);
            writer.Formatting = Formatting.Indented;
#endif

            try
            {
                writer.WriteStartElement("error");
                Encode(originalError, writer);
                writer.WriteEndElement();
                writer.Flush();
            }
            finally
            {
                writer.Close();
            }

            return sw.ToString();
        }

        /// <summary>
        /// Encodes the XML representation of an <see cref="OriginalError"/> object.
        /// </summary>

        public void Encode(OriginalError originalError, XmlWriter writer)
        {
            if (writer == null)
                throw new ArgumentNullException("writer");

            if (writer.WriteState != WriteState.Element)
                throw new ArgumentException("Writer is not in the expected Element state.", "writer");

            //
            // Write out the basic typed information in attributes
            // followed by collections as inner elements.
            //

            WriteXmlAttributes(originalError, writer);
            WriteInnerXml(originalError, writer);
        }

        /// <summary>
        /// Reads the originalError data in XML attributes.
        /// </summary>

        private void ReadXmlAttributes(XmlReader reader, OriginalError originalError)
        {
            if (reader == null)
                throw new ArgumentNullException("reader");

            if (!reader.IsStartElement())
                throw new ArgumentException("Reader is not positioned at the start of an element.", "reader");

            originalError.ApplicationName = reader.GetAttribute("application");
            originalError.HostName = reader.GetAttribute("host");
            originalError.Type = reader.GetAttribute("type");
            originalError.Message = reader.GetAttribute("message");
            originalError.Source = reader.GetAttribute("source");
            originalError.Detail = reader.GetAttribute("detail");
            originalError.User = reader.GetAttribute("user");
            string timeString = string.IsNullOrEmpty(reader.GetAttribute("time")) ? string.Empty : reader.GetAttribute("time");
            originalError.Time = timeString.Length == 0 ? new DateTime() : XmlConvert.ToDateTime(timeString);
            string statusCodeString = string.IsNullOrEmpty(reader.GetAttribute("statusCode")) ? string.Empty : reader.GetAttribute("statusCode");
            originalError.StatusCode = statusCodeString.Length == 0 ? 0 : XmlConvert.ToInt32(statusCodeString);
            originalError.WebHostHtmlMessage = reader.GetAttribute("webHostHtmlMessage");
        }

        /// <summary>
        /// Reads the originalError data in child nodes.
        /// </summary>

        private void ReadInnerXml(XmlReader reader, OriginalError originalError)
        {
            if (reader == null)
                throw new ArgumentNullException("reader");

            //
            // Loop through the elements, reading those that we
            // recognize. If an unknown element is found then
            // this method bails out immediately without
            // consuming it, assuming that it belongs to a subclass.
            //

            while (reader.IsStartElement())
            {
                //
                // Optimization Note: This block could be re-wired slightly 
                // to be more efficient by not causing a collection to be
                // created if the element is going to be empty.
                //

                if (reader.IsEmptyElement)
                    reader.Read();
                else
                {
                    switch (reader.Name.ToLower())
                    {
                        case "servervariables":
                            originalError.ServerVariables = UpcodeTo(reader);
                            break;
                        case "querystring":
                            originalError.QueryString = UpcodeTo(reader);
                            break;
                        case "form":
                            originalError.Form = UpcodeTo(reader);
                            break;
                        case "cookies":
                            originalError.Cookies = UpcodeTo(reader);
                            break;
                        default:
                            reader.Skip();
                            continue;
                    }
                }
            }
        }

        /// <summary>
        /// Writes the originalError data that belongs in XML attributes.
        /// </summary>

        private void WriteXmlAttributes(OriginalError originalError, XmlWriter writer)
        {
            if (writer == null)
                throw new ArgumentNullException("writer");

            WriteXmlAttribute(writer, "application", originalError.ApplicationName);
            WriteXmlAttribute(writer, "host", originalError.HostName);
            WriteXmlAttribute(writer, "type", originalError.Type);
            WriteXmlAttribute(writer, "message", originalError.Message);
            WriteXmlAttribute(writer, "source", originalError.Source);
            WriteXmlAttribute(writer, "detail", originalError.Detail);
            WriteXmlAttribute(writer, "user", originalError.User);
            if (originalError.Time != DateTime.MinValue)
                WriteXmlAttribute(writer, "time",
                    XmlConvert.ToString(originalError.Time.ToUniversalTime(), @"yyyy-MM-dd\THH:mm:ss.fffffff\Z"));
            if (originalError.StatusCode != 0)
                WriteXmlAttribute(writer, "statusCode", XmlConvert.ToString(originalError.StatusCode));
            WriteXmlAttribute(writer, "webHostHtmlMessage", originalError.WebHostHtmlMessage);
        }

        /// <summary>
        /// Writes the originalError data that belongs in child nodes.
        /// </summary>

        private void WriteInnerXml(OriginalError originalError, XmlWriter writer)
        {
            if (writer == null)
                throw new ArgumentNullException("writer");

            WriteCollection(writer, "serverVariables", originalError.ServerVariables);
            WriteCollection(writer, "queryString", originalError.QueryString);
            WriteCollection(writer, "form", originalError.Form);
            WriteCollection(writer, "cookies", originalError.Cookies);
        }

        private void WriteCollection(XmlWriter writer, string name, NameValueCollection collection)
        {
            Debug.Assert(writer != null);
            Debug.Assert(!string.IsNullOrEmpty(name));

            if (collection != null && collection.Count != 0)
            {
                writer.WriteStartElement(name);
                Encode(collection, writer);
                writer.WriteEndElement();
            }
        }

        private void WriteXmlAttribute(XmlWriter writer, string name, string value)
        {
            Debug.Assert(writer != null);
            Debug.Assert(!string.IsNullOrEmpty(name));

            if (value != null && value.Length != 0)
                writer.WriteAttributeString(name, value);
        }

        /// <summary>
        /// Encodes an XML representation for a 
        /// <see cref="NameValueCollection" /> object.
        /// </summary>

        private void Encode(NameValueCollection collection, XmlWriter writer)
        {
            if (collection == null)
                throw new ArgumentNullException("collection");

            if (writer == null)
                throw new ArgumentNullException("writer");

            if (collection.Count == 0)
                return;

            foreach (string key in collection.Keys)
            {
                writer.WriteStartElement("item");
                writer.WriteAttributeString("name", key);

                string[] values = collection.GetValues(key);

                if (values != null)
                {
                    foreach (string value in values)
                    {
                        writer.WriteStartElement("value");
                        writer.WriteAttributeString("string", value);
                        writer.WriteEndElement();
                    }
                }

                writer.WriteEndElement();
            }
        }

        /// <summary>
        /// Updates an existing <see cref="NameValueCollection" /> object from
        /// its XML representation.
        /// </summary>

        private NameValueCollection UpcodeTo(XmlReader reader)
        {
            NameValueCollection collection = new NameValueCollection();

            if (reader == null)
                throw new ArgumentNullException("reader");

            Debug.Assert(!reader.IsEmptyElement);
            reader.Read();

            //
            // Add entries into the collection as <item> elements
            // with child <value> elements are found.
            //

            while (reader.NodeType != XmlNodeType.EndElement)
            {
                if (reader.IsStartElement("item"))
                {
                    string name = reader.GetAttribute("name");
                    bool isNull = reader.IsEmptyElement;

                    reader.Read(); // <item>

                    if (!isNull)
                    {
                        while (reader.NodeType != XmlNodeType.EndElement)
                        {
                            if (reader.IsStartElement("value")) // <value ...>
                            {
                                string value = reader.GetAttribute("string");
                                collection.Add(name, value);
                                if (reader.IsEmptyElement)
                                {
                                    reader.Read();
                                }
                                else
                                {
                                    reader.Read();
                                    while (reader.NodeType != XmlNodeType.EndElement)
                                        reader.Skip();
                                    reader.ReadEndElement();
                                }
                            }
                            else
                            {
                                reader.Skip();
                            }

                            reader.MoveToContent();
                        }

                        reader.ReadEndElement(); // </item>
                    }
                    else
                    {
                        collection.Add(name, null);
                    }
                }
                else
                {
                    reader.Skip();
                }

                reader.MoveToContent();
            }

            reader.ReadEndElement();
            return collection;
        }

    }
}