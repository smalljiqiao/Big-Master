using BM.Core.Data;
using BM.Core.Localization;
using BM.Data;
using System.Data;
using System.Text;
using System.Xml;

namespace BM.Services.Localization
{
    public partial class LocalizationService : ILocalizationService
    {
        private const string CodeMessage_ALL_KEY = "BM.CM.All";

        private readonly IDbContext _idbContext;
        private readonly IDataProvider _idataProvider;

        public LocalizationService(IDbContext dbContext, IDataProvider dataProvider)
        {
            this._idbContext = dbContext;
            this._idataProvider = dataProvider;
        }

        public virtual void ImportCodeMessageFromXml(string xml)
        {
            if (string.IsNullOrEmpty(xml))
                return;

            //SQL 2005 insists that your XML schema incoding be in UTF-16.
            //Otherwise, you'll get "XML parsing: line 1, character XXX, unable to switch the encoding"
            //so let's remove XML declaration
            var inDoc = new XmlDocument();
            inDoc.LoadXml(xml);
            var sb = new StringBuilder();
            using (var xWriter = XmlWriter.Create(sb, new XmlWriterSettings() { OmitXmlDeclaration = true }))
            {
                inDoc.Save(xWriter);
                xWriter.Close();
            }
            var outDoc = new XmlDocument();
            outDoc.LoadXml(sb.ToString());
            xml = outDoc.OuterXml;

            var pXmlPackage = _idataProvider.GetParameter();
            pXmlPackage.ParameterName = "XmlPackage";
            pXmlPackage.Value = xml;
            //pXmlPackage.DbType = DbType.Xml;
            pXmlPackage.DbType = DbType.String;

            _idbContext.ExecuteSqlCommand("EXEC CodeMessageResourcesImport @XmlPackage", false, 300, pXmlPackage);
        }
    }
}
