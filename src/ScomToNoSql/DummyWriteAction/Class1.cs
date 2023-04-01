using System;
using System.IO;
using Microsoft.EnterpriseManagement.Runtime;
using Microsoft.EnterpriseManagement.ConnectorFramework;
using System.Xml;

namespace DummyWriteAction
{

    public class TextFileWriteAction : WriteActionModuleBase
    {
        protected override void OnInitialize()
        {
            base.OnInitialize();
        }

        protected override void OnShutdown()
        {
            base.OnShutdown();
        }

        protected override void OnNewDataItems(DataItemBase[] dataItems)
        {
            foreach (var dataItem in dataItems)
            {
                if (dataItem is PropertyBagDataItem propertyBagDataItem)
                {
                    WritePropertyBagToFile(propertyBagDataItem);
                }
            }
        }

        private void WritePropertyBagToFile(PropertyBagDataItem propertyBagDataItem)
        {
            string logPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TextFileWriteActionLog.txt");

            try
            {
                using (StreamWriter writer = new StreamWriter(logPath, true))
                {
                    writer.WriteLine("Timestamp: {0}", DateTime.Now);
                    writer.WriteLine("DataItem:");

                    XmlDocument propertyBagXml = new XmlDocument();
                    propertyBagXml.LoadXml(propertyBagDataItem.PropertyBag);

                    XmlNodeList properties = propertyBagXml.SelectNodes("//DataItem/Property");

                    foreach (XmlNode property in properties)
                    {
                        writer.WriteLine("{0}: {1}", property.Attributes["Name"].Value, property.InnerText);
                    }

                    writer.WriteLine();
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., log errors or retry writing)
            }
        }
    }
}




