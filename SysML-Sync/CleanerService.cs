using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace SysML_Sync
{
    public class CleanerService
    {
                /// <summary>
        /// This Method creates a Cleaner and Adds all packages which were found
        /// in the "FindPackagesUmlFile" Method.
        /// </summary>
        /// <param name="packages"></param>
        /// <param name="xmi"></param>
        /// <returns>A Cleaner, that has all packages from the UML-File</returns>
        public static Cleaner CreateCleanerPackageOptions(IEnumerable<XElement> packages, XNamespace xmi)
        {
            Dictionary<string, string> content = new Dictionary<string, string>();
            Cleaner cleaner = new Cleaner(content);

            foreach (var package in packages)
            {
                string id = package.Attribute(xmi + "id").Value;
                string name = package.Attribute("name").Value;
                cleaner.Content.Add(id, name);
            }
            return cleaner;
        }

        /// <summary>
        /// This Method searches all Descendants in the xmlFile, 
        /// which are from the type "uml:Package" and saves them in a List.
        /// </summary>
        /// <param name="xmlFile"></param>
        /// <param name="xmi"></param>
        /// <returns>A List of all XElements which are from the type uml:Package</returns>
        public static IEnumerable<XElement> FindPackagesUmlFile(XDocument umlFile, XNamespace xmi)
        {
            var packages = umlFile.Descendants().Where(x => (string)x.Attribute(xmi + "type") == "uml:Package");
            return packages;
        }

        /// <summary>
        /// This Method deletes all packagedElements in the umlFile,
        /// which were selected to be cleaned through the cleaner.
        /// </summary>
        /// <param name="umlFile"></param>
        /// <param name="cleaner"></param>
        /// <param name="xmi"></param>
        /// <returns>A cleaned uml File</returns>
        public static XDocument CleanUmlFile(XDocument umlFile, Cleaner cleaner, XNamespace xmi)
        {
            for (int i = 0; i < cleaner.Content.Count; i++)
            {
                var queryPackage = umlFile.Descendants("packagedElement").Where(element => (string)element.Attribute(xmi + "id") == cleaner.Content.ElementAt(i).Key);
                queryPackage.Remove();

                var queryDependency = umlFile.Descendants("packagedElement").Where(element => (string)element.Attribute("supplier") == cleaner.Content.ElementAt(i).Key);
                queryDependency.Remove();
            }
            return umlFile;
        }
        /// <summary>
        /// This Method deletes all packagedElements in the notationFile,
        /// which were selected to be cleaned through the cleaner.
        /// </summary>
        /// <param name="notationFile"></param>
        /// <param name="cleaner"></param>
        /// <returns>A cleaned notation File</returns>
        public static XDocument CleanNotationFile(XDocument notationFile, Cleaner cleaner)
        {
            for (int i = 0; i < cleaner.Content.Count; i++)
            {
                var query = notationFile.Descendants("children").Where(child => child.Element("element")?.Attribute("href")?.Value == $"TestDataSysMLMoreComplex_2.uml#{cleaner.Content.ElementAt(i).Key}").FirstOrDefault();
                query.Remove();
            }
            return notationFile;
        }
    }
}