using NPOI.OpenXml4Net.OPC;
using NPOI.OpenXmlFormats.Wordprocessing;
using System;
using System.IO;
using System.Xml;

namespace NPOI.XWPF.UserModel
{
	public class XWPFSettings : POIXMLDocumentPart
	{
		private CT_Settings ctSettings;

		public XWPFSettings(PackagePart part, PackageRelationship rel)
			: base(part, rel)
		{
		}

		public XWPFSettings()
		{
			ctSettings = new CT_Settings();
		}

		internal override void OnDocumentRead()
		{
			base.OnDocumentRead();
			ReadFrom(GetPackagePart().GetInputStream());
		}

		/// In the zoom tag inside Settings.xml file <br />
		/// it Sets the value of zoom
		/// @return percentage as an integer of zoom level
		public long GetZoomPercent()
		{
			CT_Zoom zoom = ctSettings.zoom;
			zoom = (ctSettings.IsSetZoom() ? ctSettings.zoom : ctSettings.AddNewZoom());
			return long.Parse(zoom.percent);
		}

		/// <summary>
		/// Set zoom. In the zoom tag inside settings.xml file it sets the value of zoom
		/// </summary>
		/// <param name="zoomPercent"></param>
		/// <example>
		/// sample snippet from Settings.xml 
		///
		/// &lt;w:zoom w:percent="50" /&gt;
		/// </example>
		public void SetZoomPercent(long zoomPercent)
		{
			if (!ctSettings.IsSetZoom())
			{
				ctSettings.AddNewZoom();
			}
			CT_Zoom zoom = ctSettings.zoom;
			zoom.percent = zoomPercent.ToString();
		}

		/// Verifies the documentProtection tag inside Settings.xml file <br />
		/// if the protection is enforced (w:enforcement="1") <br />
		/// and if the kind of protection Equals to passed (STDocProtect.Enum editValue) <br />
		///
		/// <br />
		/// sample snippet from Settings.xml
		/// <pre>
		///     &lt;w:settings  ... &gt;
		///         &lt;w:documentProtection w:edit="readOnly" w:enforcement="1"/&gt;
		/// </pre>
		///
		/// @return true if documentProtection is enforced with option ReadOnly
		public bool IsEnforcedWith(ST_DocProtect editValue)
		{
			CT_DocProtect documentProtection = ctSettings.documentProtection;
			if (documentProtection == null)
			{
				return false;
			}
			if (documentProtection.enforcement.Equals(ST_OnOff.on))
			{
				return documentProtection.edit.Equals(editValue);
			}
			return false;
		}

		/// Enforces the protection with the option specified by passed editValue.<br />
		/// <br />
		/// In the documentProtection tag inside Settings.xml file <br />
		/// it Sets the value of enforcement to "1" (w:enforcement="1") <br />
		/// and the value of edit to the passed editValue (w:edit="[passed editValue]")<br />
		/// <br />
		/// sample snippet from Settings.xml
		/// <pre>
		///     &lt;w:settings  ... &gt;
		///         &lt;w:documentProtection w:edit="[passed editValue]" w:enforcement="1"/&gt;
		/// </pre>
		public void SetEnforcementEditValue(ST_DocProtect editValue)
		{
			SafeGetDocumentProtection().enforcement = ST_OnOff.on;
			SafeGetDocumentProtection().edit = editValue;
		}

		/// Removes protection enforcement.<br />
		/// In the documentProtection tag inside Settings.xml file <br />
		/// it Sets the value of enforcement to "0" (w:enforcement="0") <br />
		public void RemoveEnforcement()
		{
			SafeGetDocumentProtection().enforcement = ST_OnOff.off;
		}

		/// Enforces fields update on document open (in Word).
		/// In the settings.xml file <br />
		/// sets the updateSettings value to true (w:updateSettings w:val="true")
		///
		///  NOTICES:
		///  <ul>
		///  	<li>Causing Word to ask on open: "This document contains fields that may refer to other files. Do you want to update the fields in this document?"
		///           (if "Update automatic links at open" is enabled)</li>
		///  	<li>Flag is removed after saving with changes in Word </li>
		///  </ul> 
		public void SetUpdateFields()
		{
			CT_OnOff cT_OnOff = new CT_OnOff();
			cT_OnOff.val = true;
			ctSettings.updateFields = cT_OnOff;
		}

		public bool IsUpdateFields()
		{
			if (ctSettings.IsSetUpdateFields())
			{
				return ctSettings.updateFields.val;
			}
			return false;
		}

		protected override void Commit()
		{
			if (ctSettings == null)
			{
				throw new InvalidOperationException("Unable to write out settings that were never read in!");
			}
			PackagePart packagePart = GetPackagePart();
			using (Stream stream = packagePart.GetOutputStream())
			{
				SettingsDocument settingsDocument = new SettingsDocument(ctSettings);
				settingsDocument.Save(stream);
			}
		}

		private CT_DocProtect SafeGetDocumentProtection()
		{
			CT_DocProtect documentProtection = ctSettings.documentProtection;
			if (documentProtection == null)
			{
				documentProtection = new CT_DocProtect();
				ctSettings.documentProtection = documentProtection;
			}
			return ctSettings.documentProtection;
		}

		private void ReadFrom(Stream inputStream)
		{
			try
			{
				XmlDocument doc = POIXMLDocumentPart.ConvertStreamToXml(inputStream);
				ctSettings = SettingsDocument.Parse(doc, POIXMLDocumentPart.NamespaceManager).Settings;
			}
			catch (Exception innerException)
			{
				throw new Exception("SettingsDocument parse failed", innerException);
			}
		}
	}
}
